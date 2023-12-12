using System.Collections;

namespace SimulationModelingLaba1
{
    

    public class Program
    {

        public static Random rand = new Random(1_650);

        public static  int RandomTime(int item)
        {
            return rand.Next(item);
        }

        static void Main(string[] args)
        {
            int TheFullTime = 1_000_000;
            int TheMaxTreatmentTime = 1_650;
            int TheMaxQueueLength = 50;
            int MyTiksPerSecond = 100;
            double MyLambda = 0.25;
            int MyRequestId = 0;

            Queue<int> RequestInQueue = new();
            Queue<int> RequestCompleted = new();
            Queue<int> RequestRejected = new();
            Queue<int> Time = new();

            ExponGenerator MyGenerator = new(MyLambda, MyTiksPerSecond);
            MyQueue myQueue = new(TheMaxQueueLength);

            RequestContainer myRejectedRequest = new(); // отклоненный запрос
            RequestContainer myCompletedRequest = new(); // завершенный запрос
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
                    tempRequest.TreatmentTime = RandomTime(TheMaxTreatmentTime);
                    MyGenerator.Generate();
                    if (!myQueue.AddRequest(tempRequest))
                        myRejectedRequest.AddRequest(tempRequest);
                }
                else
                    MyGenerator.TimeToNextRequest -= 1;
                
                // собираем статистику
                Time.Append(tik);
                RequestInQueue.Enqueue(myQueue.q.Count);
                RequestCompleted.Enqueue(myCompletedRequest.q.Count);
                RequestRejected.Enqueue(myRejectedRequest.q.Count);


            }


            Console.WriteLine($"Количество отброшенных заявок: {myRejectedRequest.q.Count}");
            Console.WriteLine($"Количество обработанных заявок: {myCompletedRequest.q.Count}");
            Console.WriteLine($"Осталось заявок в процессоре: {myProcessor.q.Count}");
            Console.WriteLine($"Осталось заявок в очереди: {myQueue.q.Count}");

        }



    }
}