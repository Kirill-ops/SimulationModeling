using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;

namespace TheLeslieModel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        public Series CreatSeries(List<double> listY)
        {
            var series = new Series();

            for (int i = 0; i < listY.Count; i++)
                series.Points.Add(listY[i]);

            return series;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var L = new Matrix(
                new List<List<double>>() {
                    new() { 0, 2, 1, 0},
                    new() { 0.2, 0, 0, 0},
                    new() { 0, 0.7, 0, 0},
                    new() { 0, 0, 0.5, 0},
                });

            var X = new Matrix();
            X.AddColumn(new List<double>() { 100, 50, 25, 10 });

            var Result = new Matrix();
            Result.AddColumn(X, 0);
            //Result.AddColumn(L * X, 0);

            int countIteration = 10;

            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = 1;
            chart1.ChartAreas[0].AxisX.Maximum = countIteration;
            chart1.ChartAreas[0].AxisY.Minimum = 1;
            chart1.ChartAreas[0].AxisY.Maximum = 125;


            for (int i = 1; i <= countIteration; i++)
            {
                var x = new Matrix();
                x.AddColumn(Result, i - 1);
                Result.AddColumn(L * x, 0);
            }

            for (int i = 0; i < Result.CountRows; i++)
            {
                var series = CreatSeries(Result[i]);
                series.Name = $"Возраст {i}";
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 2;
                chart1.Series.Add(series);
            }



        }
    }
}