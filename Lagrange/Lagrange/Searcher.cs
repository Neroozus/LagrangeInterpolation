using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Lagrange
{
    /*Класс, который позволяет на фоне проверять - изменился ли файл или нет*/
    class Searcher
    {
        //Объявляем кнопку Clear
        Button Clear;
        //Объявляем объект, который ожидает уведомления файловой системы об изменениях файла
        FileSystemWatcher fsw;
        /*Объявляем конструктор класса Searcher, который срабатывает при создании экземпляра класса Searcher.
         Данный конструктор принимает место, где находится папка и сам файл, а также кнопку Clear*/
        public Searcher(string path, string filter, Button Clear)
        {
            fsw = new FileSystemWatcher(path, filter);
            fsw.Changed += new FileSystemEventHandler(Fsw_Changed);
            this.Clear = Clear;
        }
        /*Метод, который включает слежение за файлом Массив1.txt*/
        public void Run()
        {
            fsw.EnableRaisingEvents = true;
        }
        /*Обработчик события, который срабатывает при изменении файла.Программа дает два выбора:
         *1)Начать работу с новыми координатами(кнопка "Да")
         *2)Продолжить работу со старыми координатами(кнопка "Нет")*/
        void Fsw_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Выберите один из вариантов:\n" +
                    "Нажмите Да, если хотите заново начать работу с новыми координатами.\n" +
                    "Нажмите Нет, если хотите продолжить работу со старыми координатами.", "Файл Массив1.txt был изменен", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                /*Если пользователя нажал на кнопку "Да", то перезагружаем приложение*/
                if (result == DialogResult.Yes)
                {
                    /*Позволяет обращаться из другого потока*/
                    (Application.OpenForms[0] as Form1).Invoke(new Action(() =>
                    {
                        //Происходит нажатие на кнопку "Очистить"
                        Clear.PerformClick();
                    }));

                }
                fsw.EnableRaisingEvents = false; //отключаем слежение
            }

            finally
            {
                fsw.EnableRaisingEvents = true; //переподключаем слежение
            }
        }
    }
}
