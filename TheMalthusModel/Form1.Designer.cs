namespace TheMalthusModel
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            labelK = new Label();
            textBoxK = new TextBox();
            buttonRun = new Button();
            listBoxOut = new ListBox();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(22, 12);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(850, 450);
            chart1.TabIndex = 0;
            chart1.Text = "chart1";
            // 
            // labelK
            // 
            labelK.AutoSize = true;
            labelK.Location = new Point(22, 480);
            labelK.Name = "labelK";
            labelK.Size = new Size(94, 25);
            labelK.TabIndex = 1;
            labelK.Text = "k [-1, ∞] =";
            // 
            // textBoxK
            // 
            textBoxK.Location = new Point(112, 477);
            textBoxK.Name = "textBoxK";
            textBoxK.Size = new Size(150, 31);
            textBoxK.TabIndex = 2;
            textBoxK.Text = "0.1";
            // 
            // buttonRun
            // 
            buttonRun.Location = new Point(22, 514);
            buttonRun.Name = "buttonRun";
            buttonRun.Size = new Size(112, 34);
            buttonRun.TabIndex = 3;
            buttonRun.Text = "Выполнить";
            buttonRun.UseVisualStyleBackColor = true;
            buttonRun.Click += buttonRun_Click;
            // 
            // listBoxOut
            // 
            listBoxOut.FormattingEnabled = true;
            listBoxOut.ItemHeight = 25;
            listBoxOut.Location = new Point(889, 12);
            listBoxOut.Name = "listBoxOut";
            listBoxOut.Size = new Size(510, 529);
            listBoxOut.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1418, 557);
            Controls.Add(listBoxOut);
            Controls.Add(buttonRun);
            Controls.Add(textBoxK);
            Controls.Add(labelK);
            Controls.Add(chart1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Модель Мальтуса — модель";
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Label labelK;
        private TextBox textBoxK;
        private Button buttonRun;
        private ListBox listBoxOut;
    }
}