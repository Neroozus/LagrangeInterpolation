using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lagrange
{
    class PrepareData
    {
        //Объявляем событие с именем waitHandle1
        AutoResetEvent waitHandle1;
        /*Функция разделения двумерного массива на два одномерных,принимающая в качестве аргументов
         *сам двумерный массив и два одномерных типа double*/
        public void ConvertTo1(double[,] arrm, double[] arrx, double[] arry, AutoResetEvent waitHandle)
        {
            //waitHandle1 присваиваем значение waitHandle
            waitHandle1 = waitHandle;
            /*Цикл разделения двумерного массива по x и присваивание одномерному массиву arrx значения x*/
            for (int k = 0; k < arrm.GetLength(0); k++)
            {
                arrx[k] = arrm[k, 0];
                arry[k] = arrm[k, 1];
                /*Цикл разделения двумерного массива по y и присваивание одномерному массиву arry значения y*/
               // for (int i = 1; i < arrm.GetLength(1); i += 2)
                //{
                   // arry[i] = arrm[k, i];
                    
                //}
            }
            //Устанавливаем сигнальное событие
            waitHandle1.Set();
        }
        /*Функция отвечает за загрузку математической функции*/
        public void LoadFunction(double[] arrx,double[] arry,int size,AutoResetEvent waitHandle)
        {
            //waitHandle1 присваиваем значение waitHandle
            waitHandle1 = waitHandle;
            for (int i = 0; i < size; i++)
            {
                arrx[i] = arrx[i];
                arry[i] = Math.Pow(arrx[i],2)-1;
                    // * Math.Sqrt(arrx[i]); // cos(x)*sqrt(x)
                    //Math.Sin(arrx[i]);
                    
            }
            //Устанавливаем сигнальное событие
            waitHandle1.Set();
        }
    }
}
