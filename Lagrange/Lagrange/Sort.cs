using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Lagrange
{
    class Sort
    {
        //Объявляем экземпляр класса Mutex с именем mutexObj1
        static Mutex mutexObj1 = new Mutex();
        /*Функция сортировки массива по возрастанию x,принимающая в аргумент массив arr типа double*/
        public void SortArray(double[,] arr)
        {
            
            //Ждем сигнала
           mutexObj1.WaitOne();
            /*Объявляем переменную min, который будет отвечать за минимальное значение*/
            int min;
            /*Объявляем переменные, с помощью которых будем менять местами*/
            double tempX, tempY;
            /*Пока i < длинны массива */
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                /*Берем i за индекс минимального значения*/
                min = i;
                /*Пока j = i + 1 < длины массива*/
                for (int j =i+1; j < arr.GetLength(0); j++)
                {
                    /*Если значение меньше минимального X*/
                    if (arr[j,0] < arr[min, 0])
                    {
                        /*Берем j за индекс минимального значения*/
                        min = j;
                    }
                }
                /*Меняем местами*/
                tempX = arr[i,0];
                arr[i,0] = arr[min,0];
                arr[min,0] = tempX;
                tempY = arr[i,1];
                arr[i,1] = arr[min,1];
                arr[min,1] = tempY;
            }
                //Освобождаем мьютекс
                mutexObj1.ReleaseMutex();
        }
        /*Вывод отсортированного массива по x*/
        public void DisplayArray(double[,] arr, TextBox textBox1)
        {
            //Ждем сигнала
            mutexObj1.WaitOne();
            textBox1.Invoke((MethodInvoker)delegate () 
            {
                textBox1.AppendText("Отсортированные координаты по x:" + "\r" + "\n");
                 textBox1.AppendText("       x       y" + "\r" + "\n");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    /*Выводим массив,предсматривая размеры чисел,чтобы все было красиво и не лезло друг на друга*/
                    textBox1.AppendText( String.Format("{0,7}", arr[i, j]));
                }
                textBox1.AppendText("\r" + "\n");
            }
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                textBox1.Refresh();
            });
            /*Выводим массив в два столбца x и y*/
            
            //Освобождаем мьютекс
            mutexObj1.ReleaseMutex();
        }
    }
}
