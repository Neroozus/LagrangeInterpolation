using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Threading;
using System.Runtime.Serialization;

namespace Lagrange
{
    //Наш пайп сервер
    class PipeServer
    {
       
        /*Объявляем метод LunchServer, возвращающий двумерный массив типа double.
         * В данном методе происходит запуск сервера и клиента*/
        public double [,] LunchServer(ref double [,] arrm)
        {
            //string[] arr = arrm.Select(s => string.Parse(s)).ToArray();
            /*Создаем анонимный пайп сервер pipeServer, который принимает в аргументы входной канал и дескриптор,
             *который наследуется дочерними процессами*/
            using (AnonymousPipeServerStream pipeServer =
            new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable))
            {
                //Задаем мод на чтение по байтам
                pipeServer.ReadMode = PipeTransmissionMode.Byte;
                //Создаем процесс pipeClient
                Process pipeClient = new Process();
                //Указываем путь для запуска клиента
                pipeClient.StartInfo.FileName = "C:\\Users\\artur\\source\\repos\\PipeClient\\PipeClient\\bin\\Debug\\PipeClient.exe";
                /*pipeClient получает в качестве аргумента
                 *Подключенный анонимный пайп клиент поток дескриптора объекта в виде строки*/
                pipeClient.StartInfo.Arguments = pipeServer.GetClientHandleAsString();
                pipeClient.StartInfo.RedirectStandardInput = true;
                //Делаем так, чтобы не использовалалась оболочка операционной системы для запуска процесса
                pipeClient.StartInfo.UseShellExecute = false;
                //Запускаем pipeClient
                pipeClient.Start();
                /*Считываем все, что получили от клиента в виде двоичных значений*/
                  using (BinaryReader sr = new BinaryReader(pipeServer))
                {
                    byte[] buffer = new byte[11000];
                    int readBytes = sr.Read(buffer, 0, buffer.Length);
                    /*Все, что считали переводим в тип double*/
                    BinaryFormatter bf = new BinaryFormatter();
                    using (MemoryStream ms = new MemoryStream(buffer, 0, readBytes))
                    {
                        try
                        {
                            arrm = (double[,]) bf.Deserialize(ms);
                        }
                        catch (SerializationException)
                        {
                            arrm = (double[,])bf.Deserialize(ms);
                        }
                        
}
                }
                /*Ждем завершения работы pipeClient, а после закрываем pipeClient*/
                pipeClient.WaitForExit();
                pipeClient.Close();
                //Возвращаем массив arrm
                return arrm;
            }
        }

        }
        
    }
