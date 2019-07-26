using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
namespace Lagrange
{
    public partial class FormMemory : Form
    {
        public FormMemory()
        {
            InitializeComponent();
        }
        //Метод, который позволяет рисовать график потребления памяти
       public void DrawGraphMemory(PointPairList list_1)
        {
            //Получаем панель для рисования
            GraphPane pane = zedGraphControl2.GraphPane;
            pane.Title.Text = "Память, потребляемая  процессом";
            pane.YAxis.Title.Text = "Значение потребления памяти" +"\n"+" процессом Lagrange в мб";
            /*Рисуем график потребления памяти*/
            LineItem myCurve2 = pane.AddCurve("График потребления памяти процессом Lagrange", list_1, Color.Red, SymbolType.None);
            /* По оси Y установим автоматический подбор масштаба*/
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.MaxAuto = true;
            /*Установим значение параметра IsBoundedRanges как true.
              Это означает, что при автоматическом подборе масштаба 
              нужно учитывать только видимый интервал графика*/
            pane.IsBoundedRanges = true;
            /*Обновляем данные об осях*/
            pane.AxisChange();
            /*Обновляем график*/
            zedGraphControl2.Invalidate();
            
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            GraphPane pane = zedGraphControl2.GraphPane;
            /*Установим значение параметра IsBoundedRanges как true.
              Это означает, что при автоматическом подборе масштаба 
              нужно учитывать только видимый интервал графика*/
            pane.IsBoundedRanges = true;
            /*Обновляем данные об осях*/
            pane.AxisChange();
            //Обновляем график
            zedGraphControl2.Invalidate();
        }

        private void FormMemory_Load(object sender, System.EventArgs e)
        {
            //Устанавливаем интервал 300 миллисекунд
            timer1.Interval = 300;
            //Вызывает таймер после того, как пройдет 300 миллисекунд
            timer1.Tick += timer1_Tick;
            //Включаем таймер
            timer1.Enabled = true;
        }
    }
}
