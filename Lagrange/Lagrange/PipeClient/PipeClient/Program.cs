using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PipeClient
{
    class Program
    {
        /*Функция считывания чисел из файла и записи этих чисел в массив*/
       static public double[,] FillFileForClient()
        {
            
                    string[] lines = File.ReadAllLines("Массив1.txt");
                   

                    /*Создаем двумерный массив arr типа double и инициализируем его каждым значением до пробела и после,
           то есть элементу arr[0,0] будет присвоено значение до пробела,
           а элементу arr[0,1] будет присвоено значение после пробела*/
                    double[,] arr = new double[lines.Length, lines[0].Split(' ').Length];
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] temp = lines[i].Split(' ');
                        for (int j = 0; j < temp.Length; j++)
                        {
                    /*Контроль считанных данных из файла,если проверка не проходит,
                     то пользователю выводится соответствующее значение как можно исправить ошибку.
                     И на экран выводится сообщение,чтобы пользователь нажал пробел,когда исправит ошибку
                     Когда пользователь нажимает на проблем,то программа возвращается назад и снова проверяет считанные данные,
                     если данные снова не пройдут проверку,то программа снова попросит исправить.*/
                    double.TryParse(temp[j], out arr[i, j]);
                            
                        }

                    }
                         return arr;
                    }
       static public void SortArray(double[,] arr)
        {
            int min;
            double tempX, tempY;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                min = i;
                for (int j = i + 1; j < arr.GetLength(0); j++)
                {
                    if (arr[j, 0] < arr[min, 0])
                    {
                        min = j;
                    }
                }
                tempX = arr[i, 0];
                arr[i, 0] = arr[min, 0];
                arr[min, 0] = tempX;
                tempY = arr[i, 1];
                arr[i, 1] = arr[min, 1];
                arr[min, 1] = tempY;
            }
        }

        static void Main(string[] args)
        {
            /*Двумерный массив arr типа double инициализируем возвращаемым значением метода FillFileForClient,
             *то есть инициализируем массивом, полученный с текстового файла Массив1.txt*/
            double[,] arr = FillFileForClient();
            //Вызываем сортировку
            SortArray(arr);
            //Объявляем массив toSend типа byte
            byte[] toSend;
            //Создаем экземпляр класса BinaryFormatter с именем bf, с помощью которого мы сериализуем массив arr
            BinaryFormatter bf = new BinaryFormatter();
            //Создаем экземпляр класса MemoryStream с именем ms, который нам даст поток памяти с расширяемой емкостью
            using (MemoryStream ms = new MemoryStream())
            {
                //Сериализуем
                bf.Serialize(ms, arr);
                //В массив байтов toSend записываем полученный массив
                toSend = ms.ToArray();
            }
            /*Если размер аргмуентов больше 0, то создаем анонимный pipeClient и передаем в него нулевой индекс аргументов
             Далее pipeClient задается настройка чтения: байты
             Создаем экземпляр класса BinaryWritter с именем sw и передаем в качестве аргументов pipeClient
             Ожидаем завершения считывания всех отправленных байтов на противоположном конце канала
             Записываем области массива байтов в текущий поток*/
            if (args.Length > 0)
            {
                using (PipeStream pipeClient =
                    new AnonymousPipeClientStream(PipeDirection.Out, args[0]))
                {
                    pipeClient.ReadMode = PipeTransmissionMode.Byte;

                    using (BinaryWriter sw = new BinaryWriter(pipeClient))
                    {
                        pipeClient.WaitForPipeDrain();
                        sw.Write(toSend, 0, toSend.Length);
                    }
                }
            }
        }
    }
}
