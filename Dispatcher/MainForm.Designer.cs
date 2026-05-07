namespace Dispatcher
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.panelValues = new System.Windows.Forms.Panel();
            this.labelTempTitle = new System.Windows.Forms.Label();
            this.labelTemperature = new System.Windows.Forms.Label();
            this.labelPressTitle = new System.Windows.Forms.Label();
            this.labelPressure = new System.Windows.Forms.Label();
            this.panelCharts = new System.Windows.Forms.Panel();
            this.labelTempChart = new System.Windows.Forms.Label();
            this.panelChartTemperature = new System.Windows.Forms.Panel();
            this.labelPressChart = new System.Windows.Forms.Label();
            this.panelChartPressure = new System.Windows.Forms.Panel();

            this.panelTop.SuspendLayout();
            this.panelValues.SuspendLayout();
            this.panelCharts.SuspendLayout();
            this.SuspendLayout();

            // panelTop.
            this.panelTop.Controls.Add(this.buttonConnect);
            this.panelTop.Controls.Add(this.buttonDisconnect);
            this.panelTop.Controls.Add(this.labelStatus);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 40;
            this.panelTop.Name = "panelTop";
            this.panelTop.TabIndex = 0;

            // buttonConnect.
            this.buttonConnect.Location = new System.Drawing.Point(5, 6);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(120, 28);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Подключиться";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);

            // buttonDisconnect.
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(135, 6);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(120, 28);
            this.buttonDisconnect.TabIndex = 1;
            this.buttonDisconnect.Text = "Отключиться";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);

            // labelStatus.
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(270, 12);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.TabIndex = 2;
            this.labelStatus.Text = "Статус: Отключено";

            // panelValues.
            this.panelValues.Controls.Add(this.labelTempTitle);
            this.panelValues.Controls.Add(this.labelTemperature);
            this.panelValues.Controls.Add(this.labelPressTitle);
            this.panelValues.Controls.Add(this.labelPressure);
            this.panelValues.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelValues.Height = 30;
            this.panelValues.Name = "panelValues";
            this.panelValues.TabIndex = 1;

            // labelTempTitle.
            this.labelTempTitle.AutoSize = true;
            this.labelTempTitle.Location = new System.Drawing.Point(5, 8);
            this.labelTempTitle.Name = "labelTempTitle";
            this.labelTempTitle.TabIndex = 0;
            this.labelTempTitle.Text = "Температура:";

            // labelTemperature.
            this.labelTemperature.AutoSize = true;
            this.labelTemperature.Location = new System.Drawing.Point(95, 8);
            this.labelTemperature.Name = "labelTemperature";
            this.labelTemperature.TabIndex = 1;
            this.labelTemperature.Text = "—";

            // labelPressTitle.
            this.labelPressTitle.AutoSize = true;
            this.labelPressTitle.Location = new System.Drawing.Point(230, 8);
            this.labelPressTitle.Name = "labelPressTitle";
            this.labelPressTitle.TabIndex = 2;
            this.labelPressTitle.Text = "Давление:";

            // labelPressure.
            this.labelPressure.AutoSize = true;
            this.labelPressure.Location = new System.Drawing.Point(305, 8);
            this.labelPressure.Name = "labelPressure";
            this.labelPressure.TabIndex = 3;
            this.labelPressure.Text = "—";

            // panelCharts — Fill, дочерние элементы раскладываются в panelCharts_Resize.
            this.panelCharts.Controls.Add(this.labelTempChart);
            this.panelCharts.Controls.Add(this.panelChartTemperature);
            this.panelCharts.Controls.Add(this.labelPressChart);
            this.panelCharts.Controls.Add(this.panelChartPressure);
            this.panelCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCharts.Name = "panelCharts";
            this.panelCharts.TabIndex = 2;
            this.panelCharts.Resize += new System.EventHandler(this.panelCharts_Resize);
            
            // labelTempChart.
            this.labelTempChart.AutoSize = true;
            this.labelTempChart.Name = "labelTempChart";
            this.labelTempChart.TabIndex = 0;
            this.labelTempChart.Text = "Температура (°C)";

            // panelChartTemperature.
            this.panelChartTemperature.BackColor = System.Drawing.Color.White;
            this.panelChartTemperature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChartTemperature.Name = "panelChartTemperature";
            this.panelChartTemperature.TabIndex = 1;
            this.panelChartTemperature.Paint += new System.Windows.Forms.PaintEventHandler(this.panelChartTemperature_Paint);

            // labelPressChart.
            this.labelPressChart.AutoSize = true;
            this.labelPressChart.Name = "labelPressChart";
            this.labelPressChart.TabIndex = 2;
            this.labelPressChart.Text = "Давление (атм)";

            // panelChartPressure.
            this.panelChartPressure.BackColor = System.Drawing.Color.White;
            this.panelChartPressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChartPressure.Name = "panelChartPressure";
            this.panelChartPressure.TabIndex = 3;
            this.panelChartPressure.Paint += new System.Windows.Forms.PaintEventHandler(this.panelChartPressure_Paint);

            // MainForm.
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.panelCharts);
            this.Controls.Add(this.panelValues);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(600, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пульт диспетчера";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelValues.ResumeLayout(false);
            this.panelValues.PerformLayout();
            this.panelCharts.ResumeLayout(false);
            this.panelCharts.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Panel panelValues;
        private System.Windows.Forms.Label labelTempTitle;
        private System.Windows.Forms.Label labelTemperature;
        private System.Windows.Forms.Label labelPressTitle;
        private System.Windows.Forms.Label labelPressure;
        private System.Windows.Forms.Panel panelCharts;
        private System.Windows.Forms.Label labelTempChart;
        private System.Windows.Forms.Panel panelChartTemperature;
        private System.Windows.Forms.Label labelPressChart;
        private System.Windows.Forms.Panel panelChartPressure;
    }
}