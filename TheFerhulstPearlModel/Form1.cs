using System.Windows.Forms.DataVisualization.Charting;

namespace TheFerhulstPearlModel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        public static Series CreatSeries(List<double> list)
        {
            var series = new Series();
            for (int i = 0; i < list.Count; i++)
                series.Points.AddXY(i, list[i]);
            return series;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int countIteration = 40;
            var b = 0.001;
            var k = new List<double>() { 0, 0.1, 0.15, 0.2, 0.25, 0.3, 0.35, 0.4, 0.5 };
            var list = new List<double>(countIteration);
            chart1.Series.Clear();


            for (int j = 0; j < k.Count; j++)
            {
                list.Clear();
                list.Add(250);
                for (int i = 1; i < countIteration; i++)
                    list.Add((1 + k[j]) * list[i - 1] - b * (list[i - 1] * list[i - 1]));


                var series = CreatSeries(list);
                series.Name = $"k = {k[j]}\n{Math.Round(list.Min(), 3)}\n{Math.Round(list.Max(), 3)}";
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                chart1.Series.Add(series);

            }

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = list.Count;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = list.Max();
        }
    }
}