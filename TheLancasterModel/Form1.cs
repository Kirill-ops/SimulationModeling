using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace TheLancasterModel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var armyX = new List<double>() { 100000 };

            var armyY = new List<double>() { 150000 };
            double a = 1;
            double b = 2;

            int t = 3;

            var seriesX = new Series();
            var seriesY = new Series();
            seriesX.Points.AddXY(armyX[0], armyY[0]);

            for (int i  = 0; i < t; i++)
            {
                var x = ((armyX[i] * Math.Sqrt(a) - armyY[0] * Math.Sqrt(b))/ 2 * Math.Sqrt(a)) * Math.Exp(i * Math.Sqrt(a * b)) +
                    ((armyX[i] * Math.Sqrt(a) + armyY[0] * Math.Sqrt(b)) / 2 * Math.Sqrt(a)) * Math.Exp(-i * Math.Sqrt(a * b));
                armyX.Add(x);
                seriesX.Points.Add(x);
                var y = ((armyY[i] * Math.Sqrt(b) - armyX[i] * Math.Sqrt(a)) / 2 * Math.Sqrt(b)) * Math.Exp(i * Math.Sqrt(a * b)) +
                    ((armyY[i] * Math.Sqrt(b) + armyX[i] * Math.Sqrt(a)) / 2 * Math.Sqrt(b)) * Math.Exp(-i * Math.Sqrt(a * b));
                armyY.Add(y);
                if (x <= 0 || y <= 0)
                    break;
                seriesY.Points.Add(x);
            }
            seriesX.Name = $"armyX:{armyX[0]}";
            seriesX.ChartType = SeriesChartType.Line;
            seriesX.BorderWidth = 3;
            chart1.Series.Add(seriesX);

            seriesY.Name = $"armyY:{armyX[0]}";
            seriesY.ChartType = SeriesChartType.Line;
            seriesY.BorderWidth = 3;
            chart1.Series.Add(seriesY);

            /*chart1.ChartAreas[0].AxisX.Minimum = armyX.Min();
            chart1.ChartAreas[0].AxisX.Maximum = armyX.Max();
            chart1.ChartAreas[0].AxisY.Minimum = armyY.Min();
            chart1.ChartAreas[0].AxisY.Maximum = armyY.Max();*/



        }
    }
}