using SimulationModelingLaba1;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;

namespace Imitation1Graph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            int TheFullTime = 1_000_000;
            int TheMaxTreatmentTime = 1_650;
            int TheMaxQueueLength = 50;
            int MyTiksPerSecond = 100;
            double MyLambda = 0.25;
            int MyRequestId = 0;

            Queue<int> RequestInQueue = new();
            Queue<int> RequestCompleted = new();
            Queue<int> RequestRejected = new();
            List<int> Time = new();

            ExponGenerator MyGenerator = new(MyLambda, MyTiksPerSecond);
            MyQueue myQueue = new(TheMaxQueueLength);

            RequestContainer myRejectedRequest = new();
            RequestContainer myCompletedRequest = new();
            Processor myProcessor = new();

            for (int tik = 0; tik <= TheFullTime; tik++)
            {
                // работаем с процессором
                if (myProcessor.TrearmentTime == 0)
                {
                    var tempRequest = myProcessor.PopRequest();
                    if (tempRequest != null)
                        myCompletedRequest.AddRequest(tempRequest);
                }
                else if (myProcessor.TrearmentTime > 0)
                    myProcessor.TrearmentTime -= 1;

                // работаем с очередью
                if (myProcessor.TrearmentTime == 0)
                {
                    var tempRequest = myQueue.PopRequest();
                    if (tempRequest != null)
                        myProcessor.AddRequest(tempRequest);
                }
                if (myQueue.q.Count > 0)
                    foreach (var request in myQueue.q)
                        request.WaitingTime += 1;

                // работаем с генератором
                if (MyGenerator.TimeToNextRequest == 0)
                {
                    MyRequestId += 1;
                    var tempRequest = new Request(MyRequestId);
                    tempRequest.TreatmentTime = rand.Next(TheMaxTreatmentTime);
                    MyGenerator.Generate();
                    if (!myQueue.AddRequest(tempRequest))
                        myRejectedRequest.AddRequest(tempRequest);
                }
                else
                    MyGenerator.TimeToNextRequest -= 1;

                // собираем статистику
                Time.Add(tik);
                RequestInQueue.Enqueue(myQueue.q.Count);
                RequestCompleted.Enqueue(myCompletedRequest.q.Count);
                RequestRejected.Enqueue(myRejectedRequest.q.Count);
            }

            label1.Text = $"Количество отброшенных заявок: {myRejectedRequest.q.Count}";
            label2.Text = $"Количество обработанных заявок: {myCompletedRequest.q.Count}";
            label3.Text = $"Осталось заявок в процессоре: {myProcessor.q.Count}";
            label4.Text = $"Осталось заявок в очереди: {myQueue.q.Count}";


            var watingTime = new List<int>();
            foreach (var request in myCompletedRequest.q)
                watingTime.Add(request.WaitingTime);


            chart1.Series.Clear();
            var series = CreatSeries(RequestInQueue);
            series.Name = "График заполненности очереди";
            series.ChartType = SeriesChartType.Line;
            chart1.Series.Add(series);
            // настраиваем оси X, Y
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = RequestInQueue.Count;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = RequestInQueue.Max();
            chart1.Update();


            chart2.Series.Clear();
            series = CreatSeries(RequestCompleted);
            series.Name = "График заполнения хранилища завершённых заявок";
            series.ChartType = SeriesChartType.Line;
            chart2.Series.Add(series);
            // настраиваем оси X, Y
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = RequestCompleted.Count;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Maximum = RequestCompleted.Max();
            chart2.Update();


            chart3.Series.Clear();
            series = CreatSeries(RequestRejected);
            series.Name = "График заполнения хранилища отброшенных заявок";
            series.ChartType = SeriesChartType.StepLine;
            chart3.Series.Add(series);
            // настраиваем оси X, Y
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.Maximum = RequestRejected.Count;
            chart3.ChartAreas[0].AxisY.Minimum = 0;
            chart3.ChartAreas[0].AxisY.Maximum = RequestRejected.Max();
            chart3.Update();

            chart4.Series.Clear();
            series = CreatSeries(watingTime);
            series.Name = "Гистограмма времени ожидания (в тиках)";
            series.ChartType = SeriesChartType.Column;
            chart4.Series.Add(series);
            // настраиваем оси X, Y
            chart4.ChartAreas[0].AxisX.Minimum = 0;
            chart4.ChartAreas[0].AxisX.Maximum = watingTime.Count;
            chart4.ChartAreas[0].AxisY.Minimum = 0;
            chart4.ChartAreas[0].AxisY.Maximum = watingTime.Max();
            chart4.Update();

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