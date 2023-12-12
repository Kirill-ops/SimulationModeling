using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationModelingLaba1
{
    class ExponGenerator
    {
        public double Lmbd;
        public int TiksPerSecond;
        public int TimeToNextRequest;

        public ExponGenerator(double lmbd, int tiksPerSecond)
        {
            Lmbd = lmbd;
            TiksPerSecond = tiksPerSecond;
            TimeToNextRequest = 0;
        }

        public void Generate()
        {
            Random rand = new();
            double uniformRandomValue = rand.NextDouble();
            //TimeToNextRequest = Convert.ToInt32(Math.Log(1 - uniformRandomValue) * (-1/Lmbd));
            double d = Math.Log(1 - uniformRandomValue) * (-1 / Lmbd);
            TimeToNextRequest = (int)Math.Round(d * TiksPerSecond);
            var a = 0;
        }
    }

    public class Request
    {
        public int Index;
        public int TreatmentTime = 0;
        public int WaitingTime = 0;

        public Request(int index) => Index = index;

        public string toString() => $"Index: {Index}; TreatmentTime: {TreatmentTime}; WaitingTime: {WaitingTime}";

    }


    public class RequestContainer
    {
        public Queue<Request> q;

        public RequestContainer()
        {
            q = new();
        }

        public void AddRequest(Request request)
        {
            q.Enqueue(request);
        }

        public string toString()
        {
            string str = "cont: ";

            foreach (Request request in q)
                str += request.toString() + " ";

            return str;
        }
    }


    public class MyQueue
    {
        public Queue<Request> q = new Queue<Request>();
        public int Length;

        public MyQueue(int length) => Length = length;

        public MyQueue() => Length = 0;

        public bool AddRequest(Request request)
        {
            if (q.Count < Length)
            {
                q.Enqueue(request);
                return true;
            }
            else
                return false;
        }

        public Request PopRequest()
        {
            if (q.Count > 0)
                return q.Dequeue();
            else
                return null;
        }

        public string toString()
        {
            string str = "cont: ";

            foreach (Request request in q)
                str += request.toString() + " ";

            return str;
        }

    }

    class Processor
    {
        public int TrearmentTime;
        public Queue<Request> q;

        public Processor()
        {
            TrearmentTime = 0;
            q = new();
        }

        public bool AddRequest(Request request)
        {
            if (q.Count == 0)
            {
                q.Enqueue(request);
                TrearmentTime = request.TreatmentTime;
                return true;
            }
            else
                return false;
        }

        public Request PopRequest()
        {
            if (q.Count > 0)
                return q.Dequeue();
            else
                return null;
        }

        public string toString()
        {
            string str = "cont: ";

            foreach (Request request in q)
                str += request.toString() + " ";
            str += $"\n tt: {TrearmentTime}";

            return str;
        }


    }
}
