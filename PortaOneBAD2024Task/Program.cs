//Код на мові С#
//Використовувався фреймворк .NET 8 з включеним native AOT publish
//У мене цей код виконувався в середньому 5 секунд
//Посилання на репозиторій 
namespace PortaOneBAD2024Task
{
    internal class Program
    {
        static void Calculate(string inputFilePath, string outputFilePath)
        {
            List<int> numberList = new List<int>();
            using (StreamReader sr = new StreamReader(inputFilePath))
            {
                while (!sr.EndOfStream)
                {
                    numberList.Add(int.Parse(sr.ReadLine()));
                }
            }
            int[] numbers = numberList.ToArray() ;
            int[] numbersSorted = numbers.Order().ToArray();
            int max = numbersSorted[numbersSorted.Length-1];
            int min = numbersSorted[0];
            double median;
            if (numbersSorted.Length % 2 == 0)
            {
                median = (numbersSorted[numbersSorted.Length / 2 - 1] + numbersSorted[numbersSorted.Length / 2]) / (double)2;
            }
            else
            {
                median = numbersSorted[numbersSorted.Length / 2];
            }
            double avg;
            long sum=0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }
            avg = sum / (double)numbers.Length;
            int longestIncreaseStart = 0;
            int longestIncreaseStartRecord = 0;
            List<int> longestIncrease = new List<int>();
            List<int> longestIncreaseRecord = new List<int>();
            int longestDecreaseStart = 0;
            int longestDecreaseStartRecord = 0;
            List<int> longestDecrease = new List<int>();
            List<int> longestDecreaseRecord = new List<int>();
            longestIncrease.Add(numbers[0]);
            longestDecrease.Add(numbers[0]);

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > numbers[i - 1])
                {
                    longestIncrease.Add(numbers[i]);
                }
                else
                {
                    if (longestIncrease.Count > longestIncreaseRecord.Count)
                    {
                        longestIncreaseRecord = longestIncrease;
                        longestIncreaseStartRecord = longestIncreaseStart;
                    }
                    longestIncrease = [numbers[i]];
                    longestIncreaseStart = i;
                }
                if (numbers[i] < numbers[i - 1])
                {
                    longestDecrease.Add(numbers[i]);
                }
                else
                {
                    if (longestDecrease.Count > longestDecreaseRecord.Count)
                    {
                        longestDecreaseRecord = longestDecrease;
                        longestDecreaseStartRecord = longestDecreaseStart;
                     }
                    longestDecrease = [numbers[i]];
                    longestDecreaseStart = i;
                }
            }
            if (longestIncrease.Count > longestIncreaseRecord.Count)
            {
                longestIncreaseRecord = longestIncrease;
                longestIncreaseStartRecord = longestIncreaseStart;
            }
            if (longestDecrease.Count > longestDecreaseRecord.Count)
            {
                longestDecreaseRecord = longestDecrease;
                longestDecreaseStartRecord = longestDecreaseStart;
            }

            string[] results =
            [
                max.ToString(),
                min.ToString(),
                median.ToString(),
                avg.ToString(),
                $"[{string.Join(',', longestIncreaseRecord)}] start (from 0) {longestIncreaseStartRecord}",
                $"[{string.Join(',', longestDecreaseRecord)}] start (from 0) {longestDecreaseStartRecord}",
            ];

            File.WriteAllLines(outputFilePath,results);
        }

        static void Main(string[] args)
        {
            if (args.Length!=2)
            {
                Console.WriteLine("wrong args count");
                Console.WriteLine("usage: [this program path] [input file path] [output file path]");
                return;
            }

            try
            {
                Calculate(args[0], args[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine($"Sucussefuly written to '{args[1]}'");
        }
    }
}
