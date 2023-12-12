using System.Diagnostics;
using System.Drawing;

namespace ciaod_laba12_13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(7);
            dataGridView1.Rows[0].Cells[1].Value = "Обмен";
            dataGridView1.Rows[1].Cells[1].Value = "Выбор";
            dataGridView1.Rows[2].Cells[1].Value = "Включение";
            dataGridView1.Rows[3].Cells[1].Value = "Шелла";
            dataGridView1.Rows[4].Cells[1].Value = "Быстрая";
            dataGridView1.Rows[5].Cells[1].Value = "Линейная";
            dataGridView1.Rows[6].Cells[1].Value = "Встроенная";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = true;
            }

        }
        public struct rezult
        {
            public long time;
            public int comparisons;
            public int reinstallation;
            public rezult()
            {
                time =comparisons=reinstallation= 0;
            }
            public rezult(long _time, int _comparisons,int _reinstallation)
            {
                time= _time;
                comparisons= _comparisons;
                reinstallation= _reinstallation;
            }
        }
        public static rezult BubbleSort(int[] Array)
        {
            Stopwatch time = new Stopwatch();
            int comparisons  = 0;
            int reinstallation = 0;
            time.Start();
            //for (int i = 0; i < Array.Length - 1; i++)
            //{
            //    for (int j = (Array.Length - 1); j > i; j--) // для всех элементов после i-ого
            //    {
            //        comparisons++;
            //        if (Array[j - 1] > Array[j]) // если текущий элемент меньше предыдущего
            //        {
            //            reinstallation++;
            //            int temp = Array[j - 1]; // меняем их местами
            //            Array[j - 1] = Array[j];
            //            Array[j] = temp;
            //        }
            //    }
            //}
            for (int i = 0; i < Array.Length; i++)
            {
                for (int j = 0; j < Array.Length - 1; j++)
                {
                    comparisons++;
                    if (Array[j] > Array[j + 1])
                    {
                        reinstallation++;
                        int temp = Array[j];
                        Array[j] = Array[j + 1];
                        Array[j + 1] = temp;
                    }
                }
            }
            time.Stop();
            return new rezult(time.ElapsedMilliseconds,comparisons, reinstallation);
        }

        static public rezult ViborSort(int[] mas)
        {
            Stopwatch time = new Stopwatch();
            int comparisons = 0;
            int reinstallation = 0;
            time.Start();
            for (int i = 0; i < mas.Length - 1; i++)
            {
                //поиск минимального числа
                int min = i;
                for (int j = i + 1; j < mas.Length; j++)
                {
                    comparisons++;
                    if (mas[j] < mas[min])
                    {
                        
                        min = j;
                    }
                }
                //обмен элементов
                reinstallation++;
                int temp = mas[min];
                mas[min] = mas[i];
                mas[i] = temp;
            }
            time.Stop();
            return new rezult(time.ElapsedMilliseconds, comparisons, reinstallation);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for(int i=0;i< dataGridView1.Rows.Count; i++)
            {
                for (int j = 2; j < 5; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "";
                }
            }
            Random rnd = new Random();
            int[] array = new int[Convert.ToInt32( numericUpDown1.Value)];
            for(int i=0; i<array.Length; i++)
            {
                array[i] = rnd.Next();
            }
            rezult rez;
            if (dataGridView1.Rows[0].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = array;
                rez = BubbleSort(array_tmp);
                dataGridView1.Rows[0].Cells[2].Value = Convert.ToString(rez.comparisons);
                dataGridView1.Rows[0].Cells[3].Value = Convert.ToString(rez.reinstallation);
                dataGridView1.Rows[0].Cells[4].Value = Convert.ToString(rez.time);
            }
            if (dataGridView1.Rows[1].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = array;
                rez = ViborSort(array_tmp);
                dataGridView1.Rows[1].Cells[2].Value = Convert.ToString(rez.comparisons);
                dataGridView1.Rows[1].Cells[3].Value = Convert.ToString(rez.reinstallation);
                dataGridView1.Rows[1].Cells[4].Value = Convert.ToString(rez.time);
            }
        }
    }
}
