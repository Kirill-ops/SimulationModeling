using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarkovProcesses
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        public static List<double> DistGenerate(int numb)
        {
            var random = new Random();
            var container = new List<double>();
            for (int i = 0; i < numb; i++)
            {
                container.Add(random.Next(0, 101));
            }
            var s = container.Sum();
            container = container.Select(elem => elem / s).ToList();
            return container;
        }

        public static int RandomState(List<double> probVector)
        {
            var random = new Random();
            var r = random.NextDouble();
            var s = probVector[0];
            int numb = probVector.Count;
            for (int k = 0; k < numb; k++)
            {
                if (r < s)
                    return k;
                else
                    s += probVector[k + 1];
            }
            return -1;
        }

        public static List<double> VecMulMatr(List<double> vec, List<List<double>> mat)
        {
            var result = new List<double>();
            for (int j = 0; j < vec.Count; j++)
            {
                double sumx = 0;
                for (int k = 0; k < vec.Count; k++)
                    sumx += vec[k] * mat[k][j];
                result.Add(sumx);
            }
            return result;
        }


        private void buttonRun_Click(object sender, EventArgs e)
        {

            // общее кол-во состо€ний
            int countState = 30;

            // макс кол-во состо€ний
            int maxCountState = 100;

            // начальное распределение веро€тностей состо€ний
            // помещаем в контейнер, сохран€ющий распределени€
            var pVector = DistGenerate(countState);

            // лист состо€ний
            var sVector = new List<int>();

            // матрица переходных веро€тностей
            var pMatrix = new List<List<double>>();
            for (int i = 0; i < countState; i++)
                pMatrix.Add(DistGenerate(countState));

            var time = new List<int>();
            for (int t = 0; t < maxCountState; t++)
            {
                time.Add(t);
                sVector.Add(RandomState(pVector));
                pVector = VecMulMatr(pVector, pMatrix);
            }

            // отобажаем результаты в виде графика
            chart1.Series.Clear();
            var series = CreatSeries(sVector);
            series.Name = "√рафик состо€ний системы";
            series.ChartType = SeriesChartType.Line;
            chart1.Series.Add(series);
            // настраиваем оси X, Y
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = sVector.Count;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = sVector.Max();
            chart1.Series[0].BorderWidth = 2;
            chart1.Update();

            label1.AutoSize = true;
            label1.AutoEllipsis = true;

            label1.Text = $"–езультат: {StateVectorToString(sVector)}";
        }


        public string StateVectorToString(List<int> sVector)
        {
            string result = "";
            for (int i = 0; i < sVector.Count - 1; i++)
            {

                result += $"[{i}, {sVector[i]}], ";
                if (i % 10 == 0 && i != 0)
                    result += "\n";
            }
            result += $"[{sVector.Count}, {sVector[sVector.Count - 1]}]";
            return result;
        }


        public Series CreatSeries(IEnumerable<int> q)
        {
            var series = new Series();
            foreach (var item in q)
            {
                series.Points.Add(item);
            }

            return series;
        }

    }
}