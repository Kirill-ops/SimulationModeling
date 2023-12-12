using System.Windows.Forms.DataVisualization.Charting;

namespace TheMonteCarloMethod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public double F(double x)
        {
            //return Math.Round(4 / Math.Sqrt(x * x + 1), 4);
            return 4 / Math.Sqrt(x * x + 1);

        }

        public Series CreatSeries(List<double> listX, List<double> listY)
        {
            var series = new Series();

            for (int i = 0; i < listX.Count; i++)
                series.Points.AddXY(listX[i], listY[i]);


            return series;
        }


        private void buttonRun_Click(object sender, EventArgs e)
        {
            int sizeList = 100;
            var xList = new List<double>();
            for (int i = 0; i < sizeList; i++)
                xList.Add(0.1 * i);
            var yList = new List<double>();
            for (int i = 0; i < sizeList; i++)
                yList.Add(F(xList[i]));

            var rand = new Random();

            int maxPoint = 5000;
            var xPoints = new List<double>();
            var yPoints = new List<double>();
            for (int i = 0; i < maxPoint; i++)
            {
                //xPoints.Add(Math.Round(rand.NextDouble() * 4, 4));
                //yPoints.Add(Math.Round(rand.NextDouble() * 4, 4));
                xPoints.Add(rand.NextDouble() * 4);
                yPoints.Add(rand.NextDouble() * 4);
            }


            chart1.Series.Clear();

            // график функции
            var series = CreatSeries(xPoints, yPoints);
            series.Name = "Points";
            series.Color = Color.LightSeaGreen;
            series.ChartType = SeriesChartType.Point;
            chart1.Series.Add(series);

            // точки
            var seriesGraph = CreatSeries(xList, yList);
            seriesGraph.Color = Color.Red;
            seriesGraph.ChartType = SeriesChartType.Point;
            seriesGraph.Name = "FunctionGraph";
            chart1.Series.Add(seriesGraph);

            // настраиваем оси X, Y
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = xPoints.Max();
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = yPoints.Max();

            chart1.Update();


            var filteredYPoints = new List<double>();
            for (int i = 0; i < yPoints.Count; i++)
                if (xPoints[i] >= yPoints[i])
                    filteredYPoints.Add(yPoints[i]);

            double result = (double)filteredYPoints.Count / maxPoint * 16;
            labelResult.Text = $"Приближённое значение интеграла: {result}";

        }
    }
}