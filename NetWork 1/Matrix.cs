using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NetWork_1
{
    internal class Matrix
    {
        private double[,] matrix; 
        private int rows, cols;
        public Matrix(int row, int col)
        {
            this.rows = row;
            this.cols = col;
            matrix = new double[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    matrix[i, j] = 0;
            }
        }

        public void Rand()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++) 
                { 
                    var rand = new Random();
                    matrix[i, j] = ((rand.Next() % 100) * 0.03 / (rows + 35));
                }
            }
        }
        public double this[int x, int y]
        {
            get { return matrix[x, y]; }
            set { matrix[x, y] = value; }
        }
        public static void Multiply(Matrix weights, double[] neurons, int neuronsSize, double[] results)
        {
            if (weights.cols != neuronsSize) throw new ArgumentException("Matrix dimensions aren't valid");

            for (int i = 0; i < weights.rows; ++i)
            {
                double temp = 0;
                for (int j = 0; j < weights.cols; ++j)
                {
                    temp += weights.matrix[i, j] * neurons[j];
                }
                results[i] = temp;
            }
        }
        public static void MultiplyT(Matrix weights, double[] neurons, int neuronsSize, double[] results)
        {
            if (weights.rows != neuronsSize) throw new ArgumentException("Matrix dimensions aren't valid");

            for (int i = 0; i < weights.cols; ++i)
            {
                double temp = 0;
                for (int j = 0; j < weights.rows; ++j)
                {
                    temp += weights.matrix[j, i] * neurons[j];
                }
                results[i] = temp;
            }
        }
        public static void SumVector(double[] vector1, double[] vector2, int vectorSize) 
        {
            for (int i = 0; i < vectorSize; i++)
            {
                vector1[i] += vector2[i];
            }
        }
    }
}
