using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Dispatcher
{
    public partial class MainForm : Form
    {
        private const int Port = 5000;
        private const string Host = "127.0.0.1";
        private const int MaxPoints = 60;

        private readonly List<double> _temperatures = new List<double>();
        private readonly List<double> _pressures = new List<double>();

        private TcpClient _tcpClient;
        private Thread _receiveThread;
        private bool _isConnected = false;

        public MainForm()
        {
            InitializeComponent();
        }

        // ----------------------------------------------------------------
        // Обработчики формы
        // ----------------------------------------------------------------

        private void MainForm_Load(object sender, EventArgs e)
        {
            LayoutCharts();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        // ----------------------------------------------------------------
        // Разметка графиков — вызывается при загрузке и при изменении размера
        // ----------------------------------------------------------------

        private void panelCharts_Resize(object sender, EventArgs e)
        {
            LayoutCharts();
        }

        private void LayoutCharts()
        {
            const int margin = 5;
            const int labelH = 18;

            int totalH = panelCharts.Height - margin * 3 - labelH * 2;
            int halfH = totalH / 2;
            int w = panelCharts.Width - margin * 2;

            if (halfH < 10 || w < 10)
            {
                return;
            }

            // График температуры
            labelTempChart.Location = new Point(margin, margin);
            panelChartTemperature.SetBounds(margin, margin + labelH, w, halfH);

            // График давления
            int secondTop = margin + labelH + halfH + margin;
            labelPressChart.Location = new Point(margin, secondTop);
            panelChartPressure.SetBounds(margin, secondTop + labelH, w, halfH);
        }

        // ----------------------------------------------------------------
        // Обработчики кнопок
        // ----------------------------------------------------------------

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(Host, Port);

                _isConnected = true;
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                labelStatus.Text = "Статус: Подключено";

                _receiveThread = new Thread(ReceiveLoop);
                _receiveThread.IsBackground = true;
                _receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        // ----------------------------------------------------------------
        // Отрисовка графиков
        // ----------------------------------------------------------------

        private void panelChartTemperature_Paint(object sender, PaintEventArgs e)
        {
            DrawChart(e.Graphics, panelChartTemperature, _temperatures, 0.0, 100.0, Color.Red);
        }

        private void panelChartPressure_Paint(object sender, PaintEventArgs e)
        {
            DrawChart(e.Graphics, panelChartPressure, _pressures, 0.0, 6.0, Color.Blue);
        }

        private void DrawChart(
            Graphics g,
            Panel panel,
            List<double> values,
            double minValue,
            double maxValue,
            Color lineColor)
        {
            const int padLeft = 45;
            const int padRight = 5;
            const int padTop = 5;
            const int padBottom = 20;

            int chartW = panel.Width - padLeft - padRight;
            int chartH = panel.Height - padTop - padBottom;

            if (chartW <= 0 || chartH <= 0)
            {
                return;
            }

            g.Clear(Color.White);

            // Сетка и подписи оси Y
            using (Pen gridPen = new Pen(Color.LightGray, 1))
            {
                const int gridLines = 5;
                for (int i = 0; i <= gridLines; i++)
                {
                    int y = padTop + i * chartH / gridLines;
                    g.DrawLine(gridPen, padLeft, y, padLeft + chartW, y);

                    double value = maxValue - i * (maxValue - minValue) / gridLines;
                    string label = value.ToString("F1", CultureInfo.InvariantCulture);
                    SizeF sz = g.MeasureString(label, this.Font);
                    g.DrawString(label, this.Font, Brushes.Black,
                        padLeft - sz.Width - 2, y - sz.Height / 2);
                }
            }

            // Оси
            using (Pen axisPen = new Pen(Color.Black, 1))
            {
                g.DrawLine(axisPen, padLeft, padTop, padLeft, padTop + chartH);
                g.DrawLine(axisPen, padLeft, padTop + chartH, padLeft + chartW, padTop + chartH);
            }

            if (values.Count < 2)
            {
                return;
            }

            // Линия данных
            PointF[] points = new PointF[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                float x = padLeft + (float)i / (MaxPoints - 1) * chartW;
                double clamped = Math.Max(minValue, Math.Min(maxValue, values[i]));
                float y = padTop + (float)((maxValue - clamped) / (maxValue - minValue)) * chartH;
                points[i] = new PointF(x, y);
            }

            using (Pen linePen = new Pen(lineColor, 2))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLines(linePen, points);
            }
        }

        // ----------------------------------------------------------------
        // Сетевая логика
        // ----------------------------------------------------------------

        private void Disconnect()
        {
            _isConnected = false;

            if (_tcpClient != null)
            {
                _tcpClient.Close();
                _tcpClient = null;
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateUiOnDisconnect));
            }
            else
            {
                UpdateUiOnDisconnect();
            }
        }

        private void UpdateUiOnDisconnect()
        {
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            labelStatus.Text = "Статус: Отключено";
        }

        private void ReceiveLoop()
        {
            try
            {
                NetworkStream stream = _tcpClient.GetStream();
                byte[] buffer = new byte[1024];
                StringBuilder received = new StringBuilder();

                while (_isConnected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    received.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                    string data = received.ToString();
                    int newlineIndex;

                    while ((newlineIndex = data.IndexOf('\n')) >= 0)
                    {
                        string line = data.Substring(0, newlineIndex).Trim();
                        data = data.Substring(newlineIndex + 1);
                        ProcessMessage(line);
                    }

                    received.Clear();
                    received.Append(data);
                }
            }
            catch (Exception)
            {
                // Соединение разорвано
            }
            finally
            {
                Disconnect();
            }
        }

        private void ProcessMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            string[] parts = message.Split(';');
            if (parts.Length != 2)
            {
                return;
            }

            if (!double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double temp))
            {
                return;
            }

            if (!double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double pressure))
            {
                return;
            }

            this.Invoke(new Action(() =>
            {
                AddValue(_temperatures, temp);
                AddValue(_pressures, pressure);

                labelTemperature.Text = $"{temp:F2} °C";
                labelPressure.Text = $"{pressure:F2} атм";

                panelChartTemperature.Invalidate();
                panelChartPressure.Invalidate();
            }));
        }

        private void AddValue(List<double> list, double value)
        {
            list.Add(value);
            if (list.Count > MaxPoints)
            {
                list.RemoveAt(0);
            }
        }
    }
}