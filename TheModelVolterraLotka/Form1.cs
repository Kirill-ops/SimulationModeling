using System.Windows.Forms.DataVisualization.Charting;

namespace TheModelVolterraLotka
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            double x = 10.0;    // ������
            double y = 5.0;     // �������
            int t = 0;          // �����

            double a = 0.09;  // �������� ����� ������
            double b = 0.01;  // ������ ������
            double c = 0.01;  // �������� ����� ��������
            double d = 0.04;  // ������ �������� 

            var seriesX = new Series();
            seriesX.Name = "�������";
            seriesX.Points.Add(x);

            var seriesY = new Series();
            seriesY.Name = "������";
            seriesY.Points.Add(y);

            int countIteration = 900;


            for (int i = 0; i < countIteration; ++i)
            {
                double dx = (a * x) - (b * x * y);
                double dy = (c * x * y) - (d * y);
                int dt = 1;

                x = x + dx / dt;
                y = y + dy / dt;
                t = t + dt;

                seriesX.Points.Add(y);
                seriesY.Points.Add(x);
            }

            chart1.Series.Clear();
            chart1.Series.Add(seriesX);
            chart1.Series.Add(seriesY);
        }

        /*
            ���������� ������� ��������� (�����������):
            � ���� ������, ��������� ���������� ���������� ��������� � ����������� ���� � ������.
        */
        private void button2_Click(object sender, EventArgs e)
        {
            double initialX = 10;
            double initialY = 5;

            /*double alpha = 0.09; // �������� ����� ������
            double beta = 0.01;   // ������ ������
            double delta = 0.01;  // �������� ����� ��������
            double gamma = 0.04;  // ������ �������� 
            double k = 0.001;
            int countIteration = 100000;*/

            double alpha = 1;   // �������� ����� ������
            double beta = 0.5;  // ������ ������
            double delta = 0.5; // �������� ����� ��������
            double gamma = 2;   // ������ �������� 
            double k = 0.1;
            int countIteration = 1000;

            VolterraLotkaModel model = new VolterraLotkaModel(initialX, initialY, alpha, beta, delta, gamma, k);

            var seriesX = new Series();
            seriesX.Name = "��������� �1";
            //seriesX.Points.Add(initialX);

            var seriesY = new Series();
            seriesY.Name = "��������� �2";
            //seriesY.Points.Add(initialY);

            
            var dt = 0.01;

            for (int i = 0; i < countIteration; ++i)
            {
                model.Update(dt);
                seriesX.Points.Add(model.X);
                seriesY.Points.Add(model.Y);
            }

            chart1.Series.Clear();
            chart1.Series.Add(seriesX);
            chart1.Series.Add(seriesY);
        }
    }

    public class VolterraLotkaModel
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Alpha { get; private set; }
        public double Beta { get; private set; }
        public double Delta { get; private set; }
        public double Gamma { get; private set; }
        public double K { get; private set; }

        private double _previousX;

        public VolterraLotkaModel(double x, double y, double alpha, double beta, double delta, double gamma, double k)
        {
            X = x;
            _previousX = X;
            Y = y;
            Alpha = alpha;
            Beta = beta;
            Delta = delta;
            Gamma = gamma;
            K = k;
        }

        public void Update(double dt)
        {
            double newX = X + dt * (Alpha * X - Beta * X * Y / (1 + K * Y));
            double newY = Y + dt * (-Gamma * Y + Delta * X * Y / (1 + K * X));

            X = newX;
            Y = newY;

            /*double newX = X + dt * (Alpha * X - Beta * _previousX * Y);
            double newY = Y + dt * (-Gamma * Y + Delta * _previousX * Y);

            X = newX;
            Y = newY;

            _previousX = X;*/
        }
    }
}