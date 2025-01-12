using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetWork_1
{
    public struct dataNetWork
    {
        public int layersAmount;
        public int[] layerSize;
    }
    public class NetWork
    {
        private int layersAmount; // количество слоёв
        private int[] layersSize;
        private ActivateFunction actFunc = new ActivateFunction();
        private Matrix[] weights;
        private double[][] bias;
        private double[][] neuronsVal, neurons_err;
        private double[] neurons_bias_val;
        public NetWork(dataNetWork data, string nameFunc = null)
        {
            if (nameFunc != null) actFunc.Set(nameFunc);
            else actFunc.Set();
            Random random = new Random();
            layersAmount = data.layersAmount;
            layersSize = new int[layersAmount];
            for (int i = 0; i < layersAmount; i++)
                layersSize[i] = data.layerSize[i];

            weights = new Matrix[layersAmount - 1];
            bias = new double[layersAmount - 1][];
            for (int i = 0; i < layersAmount - 1; i++)
            {
                weights[i] = new Matrix(layersSize[i + 1], layersSize[i]);
                bias[i] = new double[layersSize[i + 1]];
                weights[i].Rand();
                for (int j = 0; j < layersSize[i + 1]; j++)
                    bias[i][j] = (((random.NextDouble() * 66) - layersSize[i] * 0.07) % 3) / 0.45;
            }
            neuronsVal = new double[layersAmount][]; neurons_err = new double[layersAmount][];
            for (int i = 0; i < layersAmount; i++)
            {
                neuronsVal[i] = new double[layersSize[i]]; neurons_err[i] = new double[layersSize[i]];
            }
            neurons_bias_val = new double[layersAmount - 1];
            for (int i = 0; i < layersAmount - 1; i++)
                neurons_bias_val[i] = layersSize[i+1];
        
        }
        public void PrintConfig()
        {
            Console.WriteLine("****************************************************************");
            Console.WriteLine($"NetWork has {layersAmount} layers]\nSIZE[]: ");
            for (int i = 0; i < layersAmount; i++)
                Console.Write(layersSize[i] + "\t");
            Console.WriteLine("\n****************************************************************\n\n");
        }
        public void SetInput(double[] values)
        {
            for (int i = 0; i < layersSize[0]; i++)
                neuronsVal[0][i] = values[i];
        }
        public int ForwardFeed()
        {
            for (int k = 1; k < layersAmount; ++k)
            {
                Matrix.Multiply(weights[k - 1], neuronsVal[k - 1], layersSize[k - 1], neuronsVal[k]);
                Matrix.SumVector(neuronsVal[k], bias[k - 1], layersSize[k]);
                actFunc.Use(neuronsVal[k], layersSize[k]);
            }
            int predict = SearchMaxIndex(neuronsVal[layersAmount - 1]);
            return predict;
        }
        private int SearchMaxIndex(double[] values)
        {
            double max = values[0];
            int prediction = 0;
            double tmp;
            for (int i = 1; i < values.Length; i++)
            {
                tmp = values[i];
                if ( tmp > max)
                {
                    prediction = i;
                    max = tmp;
                }
            }
            return prediction;
        }
        public void PrintValues()
        {
            for (int i = 0;  i < layersSize[layersAmount]; i++)
            {
                Console.WriteLine($"{i} {neuronsVal[layersAmount][i]}");
            }
        }

        public void BackPropogation(double except)
        {
            for (int i = 0; i < layersSize[layersAmount-1]; i++)
            {
                if (i != (int)(except))
                    neurons_err[layersAmount - 1][i] = -neuronsVal[layersAmount - 1][i] * actFunc.UseDer(neuronsVal[layersAmount - 1][i]);
                else
                    neurons_err[layersAmount - 1][i] = (1.0 - neuronsVal[layersAmount - 1][i]) * actFunc.UseDer(neuronsVal[layersAmount - 1][i]);

            }
            for (int k = layersAmount - 2; k > 0; k--)
            {
                Matrix.MultiplyT(weights[k], neurons_err[k + 1], layersSize[k + 1], neurons_err[k]);
                for (int j = 0; j < layersSize[k]; j++)
                {
                    neurons_err[k][j] *= actFunc.UseDer(neuronsVal[k][j]); 
                } 
            }
        }
        public void WeightsUpdater(double dulling)
        {
            for (int i = 0; i < layersAmount - 1; ++i)
                for (int j = 0; j < layersSize[i + 1]; ++j)
                    for (int k = 0; k < layersSize[i]; ++k)
                        weights[i][j, k] += neuronsVal[i][k] * neurons_err[i + 1][j] * dulling;
            for (int i = 0; i < layersAmount - 1; i++)
                for (int k = 0; k < layersSize[i + 1]; k++)
                    bias[i][k] += neurons_err[i + 1][k] * dulling;

        }

        public void SaveWeights()
        {
            StreamWriter streamWriter = new StreamWriter("Weights.txt");
            for (int i = 0; i < layersAmount - 1; ++i)
                for (int j = 0; j < layersSize[i + 1]; ++j)
                    for (int k = 0; k < layersSize[i]; ++k)
                        streamWriter.Write($"{weights[i][j, k]} ");
            for (int i = 0; i < layersAmount - 1; i++)
                for (int k = 0; k < layersSize[i + 1]; k++)
                    streamWriter.Write($"{bias[i][k]} ");
            streamWriter.Close();
        }
        public void ReadWeights()
        {
            StreamReader streamReader = new StreamReader("Weights.txt");
            string weightsStream = streamReader.ReadToEnd();
            string[] weightsStrings = weightsStream.Split(" ");
            double[] weightsDoubles = new double[weightsStrings.Length];
            for (int i =0; i < weightsStrings.Length - 1; i++) // -1 чтоб не считать пустой элемент, который оставил сплит
                weightsDoubles[i] = double.Parse(weightsStrings[i], System.Globalization.CultureInfo.InvariantCulture);
            streamReader.Close();
            // Parsing
            int iterator = 0;
            for (int i = 0; i < layersAmount - 1; ++i)
                for (int j = 0; j < layersSize[i + 1]; ++j)
                    for (int k = 0; k < layersSize[i]; ++k)
                        weights[i][j, k] = weightsDoubles[iterator++];
            for (int i = 0; i < layersAmount - 1; i++)
                for (int k = 0; k < layersSize[i + 1]; k++)
                    bias[i][k] = weightsDoubles[iterator++];

        }
    }
}
