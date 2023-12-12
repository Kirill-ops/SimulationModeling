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

            double x = 10.0;    // добыча
            double y = 5.0;     // хищники
            int t = 0;          // время

            double a = 0.09;  // скорость роста добычи
            double b = 0.01;  // смерть добычи
            double c = 0.01;  // скорость роста хищников
            double d = 0.04;  // смерть хищников 

            var seriesX = new Series();
            seriesX.Name = "Хищники";
            seriesX.Points.Add(x);

            var seriesY = new Series();
            seriesY.Name = "Добыча";
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
            Добавление фактора насыщения (конкуренции):
            В этом случае, популяции ограничены доступными ресурсами и конкурируют друг с другом.
        */
        private void button2_Click(object sender, EventArgs e)
        {
            double initialX = 10;
            double initialY = 5;

            /*double alpha = 0.09; // скорость роста добычи
            double beta = 0.01;   // смерть добычи
            double delta = 0.01;  // скорость роста хищников
            double gamma = 0.04;  // смерть хищников 
            double k = 0.001;
            int countIteration = 100000;*/

            double alpha = 1;   // скорость роста добычи
            double beta = 0.5;  // смерть добычи
            double delta = 0.5; // скорость роста хищников
            double gamma = 2;   // смерть хищников 
            double k = 0.1;
            int countIteration = 1000;

            VolterraLotkaModel model = new VolterraLotkaModel(initialX, initialY, alpha, beta, delta, gamma, k);

            var seriesX = new Series();
            seriesX.Name = "Популяция №1";
            //seriesX.Points.Add(initialX);

            var seriesY = new Series();
            seriesY.Name = "Популяция №2";
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