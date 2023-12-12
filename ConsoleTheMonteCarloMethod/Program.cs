namespace ConsoleTheMonteCarloMethod
{
    public class Program
    {

        // проверяем все ли элементы пооследовательности удовлетворяют условию (оно в лекции, почитай сам по братски)
        // там короче такое условие
        // если один элеиент, то оно должен лежать между 0 и 1
        // если два, то первый между 0 и 0.5, а второй между 0.5 и 1
        // если три, то 0 и 0.33, 0.33 и 0.66, 0.66 и 1
        // и тд
        // и тут короче мы это проверяем, в зависимости от кол-ва элементов, все они должны лежать в разных диапазоНАХ
        public static bool CheckSequence(List<double> sequence)
        {
            for (int i = 0; i < sequence.Count; i++)
            {
                double rangeStart = i * (1.0 / sequence.Count);
                double rangeEnd = (i + 1) * (1.0 / sequence.Count);
                if (sequence[i] < rangeStart || sequence[i] > rangeEnd)
                    return false;
            }

            return true;
        }

        // вывод начальных и конечных интервалов последовательности
        public static void PrintStartEnd(List<double> sequence)
        {
            for (int i = 0; i < sequence.Count; i++)
            {
                double rangeStart = i * (1.0 / sequence.Count);
                double rangeEnd = (i + 1) * (1.0 / sequence.Count);
                Console.WriteLine($"{i}: {rangeStart} - {rangeEnd}");
            }
        }

        // вывод списка в консоль
        public static void PrintLIst<T>(List<T> list)
        {
            Console.WriteLine($"Длина: {list.Count}");
            foreach (T item in list)
                Console.WriteLine(item);

        }

        static void Main()
        {
            var avrgLength = 0.0;
            var countExperiment = 1_000_000.0;
            var rand = new Random();

            var list = new List<double>();
            var listMax = new List<double>();


            // этот цикл отвечает за кол-во экспериментов, в которых мы ищем максимально длинную последовательность
            for (int k = 0; k < countExperiment; k++)
            {
                double buf = 0;
                //  тут мы генерируем число и если оно удовлетворяет условиям задачи (смотри в комментарии к CheckSequence),
                //  то мы его добавляем в список, если нет, то прерываем этот цикл
                for (int i = 0; i < 100; i++)
                {
                    buf = rand.NextDouble();
                    list.Add(buf);
                    list.Sort();
                    if (!CheckSequence(list))
                    {
                        break;
                    }
                }

                if (list.Count - 1 > listMax.Count)
                {
                    Console.WriteLine($"Число из-за которого прервался эксперимент: {buf}");
                    list.Remove(buf);
                    listMax = new(list);
                }
                avrgLength += list.Count;
                list.Clear();
            }

            PrintStartEnd(listMax);
            Console.WriteLine($"Средняя длина: {avrgLength / countExperiment}");
            Console.WriteLine("Наиболее длинная последовательность: ");
            PrintLIst(listMax);
        }
    }
}