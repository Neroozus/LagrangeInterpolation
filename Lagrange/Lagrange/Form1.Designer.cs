namespace Lagrange
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ReadData = new System.Windows.Forms.Button();
            this.SortData = new System.Windows.Forms.Button();
            this.PrepareData = new System.Windows.Forms.Button();
            this.CalculateLagrange = new System.Windows.Forms.Button();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CalculateY = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ShowGraphMemory = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(465, 11);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(619, 339);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(447, 195);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // ReadData
            // 
            this.ReadData.Location = new System.Drawing.Point(23, 213);
            this.ReadData.Name = "ReadData";
            this.ReadData.Size = new System.Drawing.Size(122, 75);
            this.ReadData.TabIndex = 3;
            this.ReadData.Text = "Считать координаты из файла Массив1.txt\r\nи вывести эти координаты";
            this.ReadData.UseVisualStyleBackColor = true;
            this.ReadData.Click += new System.EventHandler(this.ReadData_Click);
            // 
            // SortData
            // 
            this.SortData.Location = new System.Drawing.Point(265, 213);
            this.SortData.Name = "SortData";
            this.SortData.Size = new System.Drawing.Size(132, 76);
            this.SortData.TabIndex = 5;
            this.SortData.Text = "Отсортировать координаты по X и вывести отсортированные координаты";
            this.SortData.UseVisualStyleBackColor = true;
            this.SortData.Click += new System.EventHandler(this.SortData_Click);
            // 
            // PrepareData
            // 
            this.PrepareData.Location = new System.Drawing.Point(23, 294);
            this.PrepareData.Name = "PrepareData";
            this.PrepareData.Size = new System.Drawing.Size(122, 70);
            this.PrepareData.TabIndex = 6;
            this.PrepareData.Text = "Подготовить координаты к интерполяции методом Лагранжа";
            this.PrepareData.UseVisualStyleBackColor = true;
            this.PrepareData.Click += new System.EventHandler(this.ConvertData_Click);
            // 
            // CalculateLagrange
            // 
            this.CalculateLagrange.Location = new System.Drawing.Point(265, 294);
            this.CalculateLagrange.Name = "CalculateLagrange";
            this.CalculateLagrange.Size = new System.Drawing.Size(132, 70);
            this.CalculateLagrange.TabIndex = 7;
            this.CalculateLagrange.Text = "Расчитать полиномы 2,3,4 степеней и вывести их графики";
            this.CalculateLagrange.UseVisualStyleBackColor = true;
            this.CalculateLagrange.Click += new System.EventHandler(this.CalculateLagrange_Click);
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(139, 399);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(148, 20);
            this.textBoxY.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 26);
            this.label1.TabIndex = 9;
            this.label1.Text = "Подсчет Y.\r\nВведите X, чтобы найти соответствующий Y";
            // 
            // CalculateY
            // 
            this.CalculateY.Location = new System.Drawing.Point(158, 425);
            this.CalculateY.Name = "CalculateY";
            this.CalculateY.Size = new System.Drawing.Size(100, 23);
            this.CalculateY.TabIndex = 10;
            this.CalculateY.Text = "Подсчитать Y";
            this.CalculateY.UseVisualStyleBackColor = true;
            this.CalculateY.Click += new System.EventHandler(this.CalculateY_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(715, 406);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(84, 32);
            this.Clear.TabIndex = 11;
            this.Clear.Text = "Очистить";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(582, 364);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(342, 39);
            this.label2.TabIndex = 12;
            this.label2.Text = "Нажмите на кнопку снизу,\r\nесли хотите пересчитать интерполяционные полиномы Лагра" +
    "нжа\r\nс новыми данными";
            // 
            // ShowGraphMemory
            // 
            this.ShowGraphMemory.Location = new System.Drawing.Point(971, 398);
            this.ShowGraphMemory.Name = "ShowGraphMemory";
            this.ShowGraphMemory.Size = new System.Drawing.Size(113, 49);
            this.ShowGraphMemory.TabIndex = 13;
            this.ShowGraphMemory.Text = "Показать график потребления памятью";
            this.ShowGraphMemory.UseVisualStyleBackColor = true;
            this.ShowGraphMemory.Click += new System.EventHandler(this.ShowGraphMemory_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1096, 460);
            this.Controls.Add(this.ShowGraphMemory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.CalculateY);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxY);
            this.Controls.Add(this.CalculateLagrange);
            this.Controls.Add(this.PrepareData);
            this.Controls.Add(this.SortData);
            this.Controls.Add(this.ReadData);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.zedGraphControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Программа для вычисления интерполяционных полиномов 2,3,4 степеней Лагранжа ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ReadData;
        private System.Windows.Forms.Button SortData;
        private System.Windows.Forms.Button PrepareData;
        private System.Windows.Forms.Button CalculateLagrange;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CalculateY;
        public System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button ShowGraphMemory;
        private System.Windows.Forms.Timer timer1;
    }
}

