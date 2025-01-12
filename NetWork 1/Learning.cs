using System.Diagnostics;

namespace NetWork_1
{
    public class Learning
    {
        public struct dataInfo
        {
            public double[] pixels;
            public int digit;
        }
        public static dataNetWork ReadDataNetWork(string path)
        {
            dataNetWork data;
            StreamReader reader = new StreamReader(path);
            Console.WriteLine($"{path} loading...");
            string tmp;
            int layersAmount;
            tmp = reader.ReadLine();
            if (tmp == "NetWork")
            {
                layersAmount = int.Parse(reader.ReadLine());
                data.layersAmount = layersAmount;
                data.layerSize = new int[layersAmount];
                for (int i = 0; i < layersAmount; i++)
                {
                    string element = reader.ReadLine();
                    if (element != null)
                        data.layerSize[i] = int.Parse(element);
                    else 
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid data.");
                data = new dataNetWork();
            }
            reader.Close();
            return data;
        }
        public static dataInfo[] ReadData(string path, dataNetWork dataNW, ref int examples) 
        {
            dataInfo[] data;
            StreamReader reader = new StreamReader(path);
            Console.WriteLine($"{path} loading...");
            string dataStream = reader.ReadToEnd();
            dataStream = dataStream.Replace("\r", " ");
            dataStream = dataStream.Replace("\n", " ");
            string[] dataArray = dataStream.Split(" ");
            string tmp = dataArray[0];
            if (tmp == "Examples")
            {
                examples = int.Parse(dataArray[1]);
                data = new dataInfo[examples];
                int iterator = 2;
                for (int i = 0; i < examples; ++i)
                    data[i].pixels = new double[dataNW.layerSize[0]];

                for (int i = 0; i < examples; ++i)
                {
                    if (dataArray[iterator] == "")
                    {
                        --i;
                        iterator++;
                        continue;
                    }
                    data[i].digit = int.Parse(dataArray[iterator++]);                   
                    for (int j = 0; j < dataNW.layerSize[0]; ++j)
                    {
                        if (dataArray[iterator] == "")
                        {
                            --j;
                            iterator++;
                            continue;
                        }
                        data[i].pixels[j] = double.Parse(dataArray[iterator++], System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
            }
            else throw new ArgumentException("Files data is not valid"); 
            reader.Close();
            Console.WriteLine("lib_MNIST");
            return data;          
        }


        static void Main(string[] args)
        {
            dataNetWork nwConfig;
            dataInfo[] data;
            double right, predict, maxRightAnswers = 0;
            int epoch = 0;
            bool isStudying, isRepeating = true;
            var stopwatch = new Stopwatch();

            nwConfig = ReadDataNetWork("Config.txt");
            var NW = new NetWork(nwConfig);
            NW.PrintConfig();

            while (isRepeating) 
            {
                Console.WriteLine("STUDY? (1/0)");
                isStudying = (Console.ReadLine() == "1");            
                double rightAnswers = 0;
                if (isStudying)
                {
                    int examples = 0;
                    data = ReadData("lib_MNIST_edit.txt", nwConfig, ref examples);
                    stopwatch.Start();
                    while ((double)(rightAnswers / examples) * 100 < 100) 
                    {
                        rightAnswers = 0;
                        for (int i = 0; i < examples; ++i)
                        {
                            NW.SetInput(data[i].pixels);
                            right = data[i].digit;
                            predict = NW.ForwardFeed();
                            if (predict != right)
                            {
                                NW.BackPropogation(right);
                                NW.WeightsUpdater(0.15 * Math.Exp(-epoch / 20.0));
                            }
                            else rightAnswers++;
                        }
                        stopwatch.Stop();
                        if (rightAnswers > maxRightAnswers) maxRightAnswers = rightAnswers;
                        Console.WriteLine($"rightAnswers: {rightAnswers / examples * 100}\tmaxra = {maxRightAnswers / examples * 100}\t epoch={epoch + 1}");
                        epoch++;
                        if (epoch >= 20) break;
                    }
                    Console.WriteLine($"TIME: {stopwatch} seconds");
                    NW.SaveWeights();
                }
                else
                {
                    NW.ReadWeights();
                }
                Console.WriteLine("TEST? (1/0)");
                bool test_flag = (Console.ReadLine() == "1");
                if (test_flag )
                {
                    int ex_test = 0;
                    dataInfo[] data_test;
                    data_test = ReadData("lib_10k.txt", nwConfig, ref ex_test);
                    rightAnswers = 0;
                    for (int i = 0; i < ex_test; ++i)
                    {
                        NW.SetInput(data_test[i].pixels);
                        predict = NW.ForwardFeed();
                        right = data_test[i].digit;
                        if (right == predict) rightAnswers++;
                    }
                    Console.WriteLine($"RA = {rightAnswers / ex_test * 100}");
                }
                Console.WriteLine("REPEAT? (1/0)");
                isRepeating = (Console.ReadLine() == "1");
            }
            Console.ReadKey();
        }
    }
}
