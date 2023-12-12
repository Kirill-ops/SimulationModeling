using System.Windows.Forms.DataVisualization.Charting;

namespace TheMalthusModel
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


        private void buttonRun_Click(object sender, EventArgs e)
        {
            listBoxOut.Items.Clear();
            var rand = new Random();

            var k = 0.0;
            try
            {
                k = Convert.ToDouble(textBoxK.Text);
            }
            catch
            {
                k = Convert.ToDouble(textBoxK.Text.Replace('.', ','));
            }


            var c = rand.Next(500);
            int countIteration = 20;
            var r = 8000;
            var yPoints = new List<double> { c };
            

            for (int i = 0; i < countIteration; i++)
            {
                var dop = yPoints[i] * Math.Pow(1 + k, i);
                if (dop < r)
                {
                    yPoints.Add(dop);
                }
                else
                {
                    yPoints.Add(dop - (dop - r));
                }
                listBoxOut.Items.Add($"{i}) нач.={Math.Round(yPoints[i], 2)} посл.={Math.Round(yPoints.Last(), 2)} r={r}");
            }

            // график функции
            chart1.Series.Clear();
            var series = CreatSeries(yPoints);
            series.Name = "Points";
            series.Color = Color.LightSeaGreen;
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 3;
            chart1.Series.Add(series);

            chart1.ChartAreas[0].AxisX.Minimum = 1;
            chart1.ChartAreas[0].AxisX.Maximum = countIteration + 1;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = yPoints.Max();
        }
    }
}