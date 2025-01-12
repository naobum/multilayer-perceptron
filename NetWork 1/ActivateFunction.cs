using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetWork_1
{
    enum activateFunc { sigmoid = 1, ReLU, thx }
    internal class ActivateFunction
    {
        private activateFunc actFunc;
        public void Set()
        {
            Console.WriteLine("Set actFunc:\n1. sigmoid\n2. ReLU\n3. thx");
            int.TryParse(Console.ReadLine(), out int tmp);
            switch (tmp)
            {
                case 1:
                    actFunc = activateFunc.sigmoid;
                    break;
                case 2:
                    actFunc = activateFunc.ReLU;
                    break;
                case 3:
                    actFunc = activateFunc.thx;
                    break;
                default:
                    throw new Exception("Error read actFunc");
            }
        }
        public void Set(string name)
        {
            name = name.ToLower();
            switch (name)
            {
                case "sigmoid":
                    actFunc = activateFunc.sigmoid;
                    break;
                case "relu":
                    actFunc = activateFunc.ReLU;
                    break;
                case "thx":
                    actFunc = activateFunc.thx;
                    break;
                default:
                    throw new Exception("Error read actFunc");
            }
        }
        public void Use(double[] value, int n)
        {
            switch (actFunc)
            {
                case activateFunc.sigmoid:
                    for (int i = 0; i < n; i++)
                        value[i] = 1 / (1 - Math.Exp(-value[i]));
                    break;
                case activateFunc.ReLU:
                    for (int i = 0; i < n; i++)
                    {
                        if (value[i] < 0)
                            value[i] *= 0.01;
                        else if (value[i] > 1)
                            value[i] = 1 + 0.01 * (value[i] - 1);
                    }
                    break;
                case activateFunc.thx:
                    for (int i = 0; i < n; i++)
                    {
                        if (value[i] < 0)
                            value[i] = 0.01 * (Math.Exp(value[i]) - Math.Exp(-value[i])) / (Math.Exp(value[i]) + Math.Exp(-value[i]));
                        else if (value[i] > 1)
                            value[i] = (Math.Exp(value[i]) - Math.Exp(-value[i])) / (Math.Exp(value[i]) + Math.Exp(-value[i]));
                    }
                    break;
                default: throw new Exception("Error actFunc");
            }
        }
        public void UsedDer(double[] value, int n)
        {
            switch(actFunc)
            {
                case activateFunc.sigmoid:
                    for (int i = 0; i < n; i++)
                        value[i] = value[i] * (1 - value[i]);
                    break;
                case activateFunc.ReLU:
                    for (int i = 0; i < n; i++)
                    {
                        if (value[i] > 0 || value[i] > 1)
                            value[i] = 0.01;
                        else 
                            value[i] = 1;
                    }
                    break;
                case activateFunc.thx: 
                    for (int i = 0; i < n; i++)
                    {
                        if (value[i] < 0)
                            value[i] = 0.01 * (1 - value[i] * value[i]);
                        else
                            value[i] = 1 - value[i] * value[i];
                    }
                     break;
                    default: throw new Exception("Error actFunc");
            }
        }
        public double UseDer(double value)
        {
            switch (actFunc)
            {
                case activateFunc.sigmoid:
                    value = value * (1 - value);
                    break;
                case activateFunc.ReLU:
                     if (value > 0 || value > 1)
                         value = 0.01;
                     else
                         value = 1;
                    
                    break;
                case activateFunc.thx:
                    if (value < 0)
                        value = 0.01 * (1 - value * value);
                    else
                        value = 1 - value * value;
                    
                    break;
                default: throw new Exception("Error actFunc");
            }
            return value;
        }
    }
}
