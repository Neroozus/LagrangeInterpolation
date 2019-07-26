using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ZedGraph;

namespace Lagrange
{
    public partial class Form1 : Form
    {

        //Объявляем счетчик, который отвечает за увеличение координаты x на графике потребления памятью
        int countForMemoryX = 0;
        //Создаем экземпляр класса FormMemory с именем formMemory
        FormMemory formMemory = new FormMemory();
        //Объявляем список точек
        PointPairList list_5 = new PointPairList();
        //Создаем статический экземпляр класса Mutex с именем mutexObj
        static Mutex mutexObj = new Mutex();
        //Создаем экземпляр класса события с именем waitHandle и инициализируем его начальным значением false
         public AutoResetEvent waitHandle = new AutoResetEvent(false);
        //Создаем коллекцию потоков threads
        static List<Thread> threads = new List<Thread>();
        //Объявляем статический двумерный массив arrm типа double
        static double[,] arrm;
        //Объявляем переменную size типа int, в которой будем хранить размерность массива
        int size;
        //Создаем статический экземпляр класса Semaphore с именем  ResPool
        public static Semaphore ResPool;   
        /*Создаем два одномерных массива arrx и arry типа double*/
        public  double[] arrx;
        public double[] arry;
        //Конструктор класса Form1, в котором при запуске приложения происходит загрузка всех графических элементов на Форму
        public Form1()
        {
            InitializeComponent();
        }
        /*Метод, который рисует график получает информацию о потреблении памяти*/
        public void ValueOfMemory()
        {
            //Получаем имя процесса
            string prcName = Process.GetCurrentProcess().ProcessName;
            //Берем информацию о потреблении памяти процессом
            var counter = new PerformanceCounter("Process", "Working Set - Private", prcName);
            //Добавляем в список точек точки потребления памяти процессом
           list_5.Add (countForMemoryX,counter.RawValue / 1024);
            //Увеличиваем счетчик на два
            countForMemoryX+=2;
        }
        //Метод, который передает интервал тика Таймера
        public void PropTimer()
        {
            //Задаем интервал в 300 миллисекунд
            timer1.Interval = 300;
            //По истечению времени вызывается обработчик события тика Таймера
            timer1.Tick += timer1_Tick;
            //Включаем Таймер
            timer1.Enabled = true;
            
        }
        /*функция вычисления интерполяционного полинома лагранжа,которая принимает в аргументы значение x
         *и два одномерных массива x,y типа double*/
        public double Lagrange(ref double x0,double[] x,double[] y)
        {
            //Ждем сигнала
            mutexObj.WaitOne();
            /*Объявление переменной polinom типа double и инициализация нулем*/
            double polinom = 0;
            /*Цикл вычисления интерполяционного полинома Лагранжа*/
            for (int j = 0; j < x.Length; j++)
            {
                /*Объявление переменной p типа double и инициализация единицей*/
                double p = 1;
                /*Цикл вычисление базисных полиномов*/
                for (int i = 0; i < x.Length; i++)
                {
                    if (j != i)
                    {
                        /*Вычисление базисных полиномов по формуле Лагранжа*/
                        p *= (x0 - x[i]) / (x[j] - x[i]);
                    }
                }
                    
                /*Вычисление интерполяционного полинома Лагранжа*/
                polinom += y[j] * p;
            }
            //освобождаем мьютекс
            mutexObj.ReleaseMutex();
            /*Возвращение значения polinom*/
            return polinom;
        }
        /*Функция, которая добавляет получившуюся точку на существующий график*/
        public void DrawPoint(double x, double y)
        {
           //Ждем сигнала
           mutexObj.WaitOne();
            //Получаем панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;
            //Объявляем экземпляр класса PointPairList с именем list, в котором будет храниться точка
            PointPairList list = new PointPairList();
            //Добавляем точку
            list.Add(x, y);
            // которая будет рисоваться темно-бордовым цветом (Color.Blue),
            // Опорные точки будут выделяться звездочкой (SymbolType.None)
            LineItem myCurve = pane.AddCurve("Найденная точка", list, Color.Maroon, SymbolType.Star);

            //Вызываем метод AxisChange (), чтобы обновить данные об осях. 
            //В противном случае на рисунке будет показана только часть графика, 
            //которая умещается в интервалы по осям, установленные по умолчанию
            zedGraphControl1.AxisChange();

            //Обновляем график
            zedGraphControl1.Invalidate();
            //Освобождаем мьютекс
            mutexObj.ReleaseMutex();
        }

        /*Функция,которая добавляет точки на график и рисует график функции,принимающая два массива x,y типа double*/
        public void DrawGraph(double[] x, double[] y, int n)
        {
                //Ждем сигнала
                 //ResPool.WaitOne();
                /* Получим панель для рисования*/
                GraphPane pane = zedGraphControl1.GraphPane;
                pane.Title.Text = "Интерполяционный полином Лагранжа 2,3,4 степеней";
            /*Создадим список точек*/       
                PointPairList list_1 = new PointPairList();
                PointPairList list_2 = new PointPairList();
                PointPairList list_3 = new PointPairList();
                PointPairList list_4 = new PointPairList();
                /*Заполняем список точек*/
                for (int i = 0; i < x.Length; i++)
                {
                    list_1.Add(x[i], y[i]);
                }

                /*Интерполируем несколько точек между узлами*/
                for (int i = 0; i < x.Length - 1; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        /*Объявляем переменную local типа double и присваиваем выражение,по которому вычисляется будущий x*/
                        double local = x[i] + j * (x[i + 1] - x[i]) / n;
                        /*Заполнение списка точек,кроме последней точки,
                         *вызовом функции вычисления интерполяционного полинома Лагранжа*/
                         /*Если степень равна 2 или 3, или 4, то происходит добавление точек в list_2, list_3, list_4 соответственно*/
                        if (n == 2)
                        {
                            list_2.Add(local, Lagrange(ref local,  x,  y));
                        }
                        if (n == 3)
                        {
                            list_3.Add(local, Lagrange(ref local,x, y));
                        }
                        if (n == 4)
                        {
                            list_4.Add(local, Lagrange(ref local, x,y));
                        }
                    }
                }
                /*Если степень равна 2 или 3, или 4, то происходит добавление последней точки в list_2, list_3, list_4 соответственно*/
                if (n == 2)
                {
                    list_2.Add(x[x.Length - 1], y[y.Length - 1]);
                }
                if (n == 3)
                {
                    list_3.Add(x[x.Length - 1], y[y.Length - 1]);
                }
                if (n == 4)
                {
                    list_4.Add(x[x.Length - 1], y[y.Length - 1]);
                }
                /*Если степень равна 2 или 3, или 4, то происходит заполнение точками графика и построение кривой соотвественно
                  исходная функция-фиолетовый цвет, 2 степень-красный цвет, 3 степень-голубой цвет, 4 степень-зеленый цвет*/
                if (n == 2)
                {
                    LineItem myCurve2 = pane.AddCurve("Интерполяционный полином 2-й степени", list_2, Color.Red, SymbolType.None);
                    LineItem myCurve1 = pane.AddCurve("Исходная функция", list_1, Color.Purple, SymbolType.Diamond);
                }
                if (n == 3)
                {
                    LineItem myCurve2 = pane.AddCurve("Интерполяционный полином 3-й степени", list_3, Color.Blue, SymbolType.None);
                }
                if (n == 4)
                {
                    LineItem myCurve2 = pane.AddCurve("Интерполяционный полином 4-й степени", list_4, Color.Green, SymbolType.None);
                }
                
                /* По оси Y установим автоматический подбор масштаба*/
                pane.YAxis.Scale.MinAuto = true;
                pane.YAxis.Scale.MaxAuto = true;
            /*Установим значение параметра IsBoundedRanges как true.
              Это означает, что при автоматическом подборе масштаба 
              нужно учитывать только видимый интервал графика*/
            pane.IsBoundedRanges = true;
            //Усыпляем поток на 0,1 секунду
            Thread.Sleep(100);
            /*Обновляем данные об осях*/
                pane.AxisChange();
            /*Обновляем график*/
                zedGraphControl1.Invalidate();
        }
        //Обработчик клика по кнопке ReadData
        private void ReadData_Click(object sender, EventArgs e)
        {
            //Создаем экземпляр класса WorkWthFile с именем workWthFile
            WorkWthFile workWthFile = new WorkWthFile();
            /*Добавляем в коллекцию два потока, инициализируя их методами FillFile и OutputCoordinates соответственно,
             Присваиваем имя secondThread и thirdThread соответственно.Запускаем потоки, ждем сигнала. Очищаем коллекцию потоков*/
            threads.Add(new Thread(() => arrm = workWthFile.FillFile(ref waitHandle)));
            threads.Add(new Thread(() => size = workWthFile.OutputCoordinates(arrm, textBox1, ref waitHandle)));
            threads[0].Name = "secondThread";
            threads[1].Name = "thirdThread";
            threads[0].Start();
            waitHandle.WaitOne();
            /*Если массив arrm не пустой, то кнопку SortData делаем активной, а кнопку ReadData неактивной,
             иначе ReadData оставляем неактивной*/
            if (arrm != null)
            {              
                threads[1].Start();
                waitHandle.WaitOne();
                //MessageBox.Show("Данные считались!");
                SortData.Enabled = true;
                ReadData.Enabled = false;
                ShowGraphMemory.Enabled = true;
                
            }
            else
            {               
                ReadData.Enabled = true;
            }
            threads.Clear();
        }
        //Обработчик загрузки Формы
        private void Form1_Load(object sender, EventArgs e)
        {
            //Вызываем метод PropTimer, чтобы знать сколько потребляет памяти процесс при запуске приложения
            PropTimer();
            //Создаем экземпляр класса Searcher с именем searcher, передавая в аргмуенты путь, расширение и кнопку Clear
            Searcher searcher = new Searcher("C:\\Users\\artur\\Desktop", "*.txt",Clear);
            searcher.Run();
            /*Скрываем график и все кнопки, кроме одной*/
            zedGraphControl1.Hide();
            SortData.Enabled = false;
            PrepareData.Enabled = false;
            CalculateLagrange.Enabled = false;
            CalculateY.Enabled = false;
            ShowGraphMemory.Enabled = false;
        }

        //Обработчик клика по кнопке SortData
        private void SortData_Click(object sender, EventArgs e)
        {
            
            /*Создаем экземпляр класса Sort с именем sort и экземпляр класса PipeServer с именем pipeServer.
             *Добавляем в коллекцию два потока,
             инициализируя их методами DisplayArray и LunchServer соответственно,
             Присваем имя secondThread и thirdThread соответственно.Запускаем потоки.
             Очищаем коллекцию потоков*/
            Sort sort = new Sort();
            PipeServer pipeServer = new PipeServer();
            threads.Add(new Thread(() => pipeServer.LunchServer(ref arrm)));
            threads.Add(new Thread(() => sort.DisplayArray(arrm, textBox1)));
            threads[0].Name = "secondThread";
            threads[1].Name = "thiredThread";
            threads[0].Start();
            threads[0].Join();
            threads[1].Start();           
            threads.Clear();
            /*Кнопку SortData делаем неактивной, а кнопку PrepareData активной*/
            SortData.Enabled = false;
            PrepareData.Enabled = true;
            
        }
        //Обработчик клика по кнопке ConvertData
        private void ConvertData_Click(object sender, EventArgs e)
        {
            //Инициализируем два одномерных массива размерностью size
            arrx = new double[size];
            arry = new double[size];
            //Создаем экземпляр класса PrepareData с именем prepareD
            PrepareData prepareD = new PrepareData();
            /*Добавляем в коллекцию два потока,
             инициализируя их методами ConvertTo1 и LoadFunction соответственно,
             Присваем имя secondThread и thirdThread соответственно.Запускаем потоки, ждем сигнала.
             Очищаем коллекцию потоков*/
            threads.Add(new Thread(() => prepareD.ConvertTo1(arrm, arrx, arry,waitHandle)));
            threads.Add(new Thread(() => prepareD.LoadFunction(arrx, arry, size,waitHandle)));
            threads[0].Name = "secondThread";
            threads[1].Name = "thirdThread";
            for (int k = 0; k < 2; k++)
            {
                
                threads[k].Start();
                waitHandle.WaitOne();
            }
            threads.Clear();
            //Вывод на экран пользователя в textBox1
            textBox1.AppendText ("Данные подготовлены к интерполяции методом Лагранжа." + "\r" + "\n");
            textBox1.AppendText("Загружена функция x^2-1." + "\r" + "\n" +
               "Стала активной кнопка расчета полиномов 2,3,4 степеней." + "\r" + "\n");
            /*Кнопку CalculateLagrange делаем активной, а кнопку PrepareData делаем неактивной*/
            CalculateLagrange.Enabled = true;
            PrepareData.Enabled = false;
        }
        //Обработчик клика по кнопке CalculateLagrange
        private void CalculateLagrange_Click(object sender, EventArgs e)
        {
            //Инициализируем семафор ResPool начальным значением 0, а максимальным 2
                ResPool = new Semaphore(0, 3);
            /*Создаем три потока myThread1, myThread2 и myThread3, инициализируя их методами DrawGraph*/
                Thread myThread1 = (new Thread(() =>DrawGraph(arrx, arry, 2)));
            Thread myThread2 = (new Thread(() => DrawGraph(arrx, arry, 3)));
                Thread myThread3 = (new Thread(() => DrawGraph(arrx, arry, 4)));
            //Запускаем 3 потока
                myThread1.Start();
                myThread2.Start();
                myThread3.Start();
            //Выходим из семафора 3 раза
            ResPool.Release(3);
            //Кнопку CalculateLagrange делаем неактивной
                CalculateLagrange.Enabled = false;  
            //Показываем график
            zedGraphControl1.Show();
            //Вывод на экран пользователя сообщение в textBox1
            textBox1.AppendText("Графики готовы.");
            //Кнопку CalculateY делаем активной
            CalculateY.Enabled = true;

        }
        //Обработчик клика по кнопке CalculateY
        private void CalculateY_Click(object sender, EventArgs e)
        {
            //Объявляем переменную text типа string и присваиваем ей значение textBoxY
            string text = textBoxY.Text;
            /*Проверка на пустоты в textBoxY*/
            try
            {
                if (string.IsNullOrEmpty(textBoxY.Text))
            {
                MessageBox.Show("Сначала заполните поле подсчета Y. ");
                return;
            }
            //Объявляем переменую x1 типа double и инициализируем double значение из переменной text
            double x1 = double.Parse(text);
                /*Добавляем в коллекцию два потока, инициализируя их методами Lagrange и DrawPoint соответственно,
                Присваем имя secondThread и thirdThread соответственно.Запускаем потоки. Очищаем коллекцию потоков*/
                threads.Add(new Thread(() => textBoxY.Invoke((MethodInvoker)delegate ()
            {
                textBoxY.Text = Lagrange(ref x1, arrx,arry).ToString();
            })));
            threads.Add(new Thread(() => DrawPoint(x1, Lagrange(ref x1, arrx, arry))));
                threads[0].Name = "secondThread";
                threads[1].Name = "thirdThread";
            for(int k = 0; k < 2; k++)
            {
                    threads[k].Start();
            }
            threads.Clear();
            }
            //Проверка на нечисловое значение
            catch (FormatException)
            {
                MessageBox.Show("Введите число");
            }
        }
        //Обработчик клика по кнопке Clear
        public void Clear_Click(object sender, EventArgs e)
        {
            /*Перезагружаем приложение*/
           Application.Restart();
            
        }
        //Обработчик события клика по кнопке ShowGraphMemory
        private void ShowGraphMemory_Click(object sender, EventArgs e)
        {
            /*Обращаемся к классу FormMemory с помощью экземпляра formMemory и вызываем метод DrawGraphMemory,
             *который рисует график потребления памяти процессом Lagrange с помощью полученных точек из списка точек list_5*/
            formMemory.DrawGraphMemory(list_5);
            //Выводим на экран пользователя получившийся график
            formMemory.Show();
            //Делаем кнопку ShowGraphMemory неактивной
            ShowGraphMemory.Enabled = false;
            formMemory = null;
        }
        //Обрабочик тика таймера.Каждые 300 миллисекунд обновляется график потребления памяти процессом
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Запускаем асинхронно.Это позволяет динамически обновлять график потребления памяти процессом
            Task myTask = Task.Run(() => ValueOfMemory());
            
        }
    }
}

