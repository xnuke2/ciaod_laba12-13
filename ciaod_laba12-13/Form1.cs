using System;
using System.Collections.Generic;
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
            dataGridView1.Rows[3].Cells[1].Value = "Быстрая";
            dataGridView1.Rows[4].Cells[1].Value = "Шелла";
            dataGridView1.Rows[5].Cells[1].Value = "Линейная";
            dataGridView1.Rows[6].Cells[1].Value = "Встроенная";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = true;
            }

        }
        public struct rezult
        {
            public ulong time;
            public ulong comparisons;
            public ulong reinstallation;
            public int[] newArray;
            public rezult()
            {
                time =comparisons=reinstallation= 0;
            }
            public rezult(long _time, int _comparisons,int _reinstallation, int[] _newArray)
            {
                time= (ulong)_time;
                comparisons= (ulong)_comparisons;
                reinstallation= (ulong)_reinstallation;
                newArray = _newArray;
            }
            public rezult(long _time, ulong _comparisons, ulong _reinstallation, int[] _newArray)
            {
                time = (ulong)_time;
                comparisons = _comparisons;
                reinstallation = _reinstallation;
                newArray = _newArray;
            }
        }
        public bool Check(int[] Array)
        {
            for (int i = 1;i< Array.Length;i++)
            {
                if (Array[i] < Array[i-1]) {
                    return false;
                }
            }
            return true;
        }
        
        public static rezult BubbleSort(int[] Array)
        {
            Stopwatch time = new Stopwatch();
            int comparisons  = 0;
            int reinstallation = 0;
            time.Start();
            bool have_reinstallation=true;
            for (int i = 0; i < Array.Length - 1; i++)
            {
                have_reinstallation = false;
                for (int j = 0; j < Array.Length-i - 1; j++)
                {
                    comparisons++;
                    if (Array[j] > Array[j + 1])
                    {
                        have_reinstallation = true;
                        reinstallation++;
                        int temp = Array[j];
                        Array[j] = Array[j + 1];
                        Array[j + 1] = temp;
                    }
                }
                if (have_reinstallation == false)
                {
                    break;
                }
                if (have_reinstallation == false)
                {
                    break;
                }
            }
            time.Stop();
            return new rezult(time.ElapsedMilliseconds,comparisons, reinstallation,Array);
        }

        static public rezult ViborSort(int[] Array)
        {
            Stopwatch time = new Stopwatch();
            int comparisons = 0;
            int reinstallation = 0;
            time.Start();
            for (int i = 0; i < Array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < Array.Length; j++)
                {
                    comparisons++;
                    if (Array[j] < Array[min])
                    {
                        min = j;
                    }
                }
                reinstallation++;
                int temp = Array[min];
                Array[min] = Array[i];
                Array[i] = temp;
            }
            time.Stop();
            return new rezult(time.ElapsedMilliseconds, comparisons, reinstallation, Array);
        }

        static public rezult SortingByDirectInclusions(int[] Array)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            int comparisons = 0;
            int reinstallation = 0;
            int min = Array[0];
            int j = 0;
            for (int i = 1; i < Array.Length; i++)
            {
                comparisons++;
                if (Array[i] < min)
                {
                    min = Array[i];
                    j = i;
                }
            }
            Array[j] = Array[0];
            Array[0] = min;
            reinstallation += 2;
            for (int i = 1; i < Array.Length; i++)
            {
                int value = Array[i]; 
                int index = i;     
                while ((index > 0) && (Array[index - 1] > value))
                {
                    comparisons++;
                    reinstallation++;
                    Array[index] = Array[index - 1];
                    index--;    
                }
                comparisons++;
                reinstallation++;
                Array[index] = value;
            }
            time.Stop();
            return new rezult(time.ElapsedMilliseconds,comparisons, reinstallation, Array);
        }

        static public rezult Quicksort(int[] array,int startindex,int endindex)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            int index = startindex;
            ulong comparisons = 0;
            ulong reinstallation = 0;
            for (int i = startindex+1; i < endindex; i++)
            {
                comparisons+=2;
                if (array[i] < array[index])
                {
                    reinstallation += 2;
                    int tmp = array[index];
                    array[index] = array[i];
                    array[i]= array[index+1];
                    array[index+1] = tmp;
                    index++;
                }
            }

            rezult rez1 = QuicksortWT(array,startindex,index);
            rezult rez2 = QuicksortWT(array,index+1,endindex);
            time.Stop();
            return new rezult(time.ElapsedMilliseconds, comparisons + rez1.comparisons+rez2.comparisons, reinstallation+rez1.reinstallation+rez2.reinstallation, array);
        }
        static public rezult QuicksortWT(int[] array, int startindex, int endindex)
        {
            if (startindex >= endindex)
            {
                rezult rezult = new rezult();
                rezult.comparisons = 1;
                return rezult;
            }
            int index = startindex;
            ulong comparisons = 0;
            ulong reinstallation = 0;
            for (int i = startindex + 1; i < endindex; i++)
            {
                comparisons+=2;
                if (array[i] < array[index])
                {
                    reinstallation += 2;
                    int tmp = array[index];
                    array[index] = array[i];
                    array[i] = array[index + 1];
                    array[index + 1] = tmp;
                    index++;
                }
            }

            rezult rez1 = Quicksort(array, startindex, index);
            rezult rez2 = Quicksort(array, index + 1, endindex);
            rezult rez = new rezult();
            rez.comparisons = comparisons+rez1.comparisons+rez2.comparisons;
            rez.reinstallation=reinstallation+rez1.reinstallation+rez2.reinstallation;
            return rez;
        }

        rezult ShellSort(int[] array)
        {
            ulong comparisons = 0;
            ulong reinstallation = 0;
            var sw = new Stopwatch();
            sw.Start();
            int j;
            int step =0;
            while ((step*3+1)*3+1 < array.Length )
            {
                step = 3 * step + 1;
            }
            while (step > 0)
            {
                for (int i = 0; i < (array.Length - step); i++)
                {
                    j = i;
                    int tmp = array[j+step];
                    while ((j >= 0) && (array[j] > tmp))
                    {
                        comparisons ++;
                        array[j + step] = array[j];
                        j -= step;
                        reinstallation++;
                    }
                    array[j + step] = tmp;
                    
                    comparisons ++;
                }
                step = (step-1) / 3;
            }

            sw.Stop();
            return new rezult(sw.ElapsedMilliseconds, comparisons, reinstallation,array);
        }

        rezult LinearSort(int[] array)
        {
            ulong comparisons = 0;
            ulong reinstallation = 0;
            int[] tmpArray= new int[array.Length];
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0;i < array.Length;i++)
            {
                tmpArray[i] = 0;
            }
            for(int i = 0; i < array.Length; i++) 
            {
                comparisons++;
                reinstallation++;
                tmpArray[array[i]]++;
            }
            int index = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for(int j = 0; j < tmpArray[i]; j++)
                {
                    reinstallation++;
                    array[index] =i;
                    index++;
                }
            }
            sw.Stop();
            return new rezult(sw.ElapsedMilliseconds, comparisons, reinstallation, array);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label3.Text= string.Empty;
            for(int i=0;i< dataGridView1.Rows.Count; i++)
            {
                for (int j = 2; j < 6; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "";
                }
            }
            Random rnd = new Random();
            int[] array = new int[Convert.ToInt32( numericUpDown1.Value)];
            for(int i=0; i<array.Length; i++)
            {
                array[i] = rnd.Next(0,array.Length);
            }
            rezult rez;
            if (dataGridView1.Rows[0].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                rez = BubbleSort(array_tmp);
                dataGridView1.Rows[0].Cells[2].Value = Convert.ToString(rez.comparisons);
                dataGridView1.Rows[0].Cells[3].Value = Convert.ToString(rez.reinstallation);
                dataGridView1.Rows[0].Cells[4].Value = Convert.ToString(rez.time);
                if (Check(rez.newArray))
                {
                    dataGridView1.Rows[0].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[0].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[1].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                rez = ViborSort(array_tmp);
                dataGridView1.Rows[1].Cells[2].Value = Convert.ToString(rez.comparisons);
                dataGridView1.Rows[1].Cells[3].Value = Convert.ToString(rez.reinstallation);
                dataGridView1.Rows[1].Cells[4].Value = Convert.ToString(rez.time);
                if (Check(rez.newArray))
                {
                    dataGridView1.Rows[1].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[1].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[2].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                rez =SortingByDirectInclusions(array_tmp);
                dataGridView1.Rows[2].Cells[2].Value = Convert.ToString(rez.comparisons);
                dataGridView1.Rows[2].Cells[3].Value = Convert.ToString(rez.reinstallation);
                dataGridView1.Rows[2].Cells[4].Value = Convert.ToString(rez.time);
                if (Check(rez.newArray))
                {
                    dataGridView1.Rows[2].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[2].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[3].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                 rez = Quicksort(array_tmp,0,array_tmp.Length);
                dataGridView1.Rows[3].Cells[2].Value = Convert.ToString(rez.comparisons);
                dataGridView1.Rows[3].Cells[3].Value = Convert.ToString(rez.reinstallation);
                dataGridView1.Rows[3].Cells[4].Value = Convert.ToString(rez.time);
                if (Check(rez.newArray))
                {
                    dataGridView1.Rows[3].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[3].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[4].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                rez = ShellSort(array_tmp);
                dataGridView1.Rows[4].Cells[2].Value = Convert.ToString(rez.comparisons);
                dataGridView1.Rows[4].Cells[3].Value = Convert.ToString(rez.reinstallation);
                dataGridView1.Rows[4].Cells[4].Value = Convert.ToString(rez.time);
                if (Check(rez.newArray))
                {
                    dataGridView1.Rows[4].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[4].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[5].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                rez = LinearSort(array_tmp);
                dataGridView1.Rows[5].Cells[2].Value = Convert.ToString(rez.comparisons);
                dataGridView1.Rows[5].Cells[3].Value = Convert.ToString(rez.reinstallation);
                dataGridView1.Rows[5].Cells[4].Value = Convert.ToString(rez.time);
                if (Check(rez.newArray))
                {
                    dataGridView1.Rows[5].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[5].Cells[5].Value = "нет";
                }
            }
            if (dataGridView1.Rows[6].Cells[0].Value.Equals(true))
            {
                int[] array_tmp = (int[])array.Clone();
                var sw = new Stopwatch();
                sw.Start();
                Array.Sort(array_tmp);
                sw.Stop();
                dataGridView1.Rows[6].Cells[2].Value = "-";
                dataGridView1.Rows[6].Cells[3].Value = "-";
                dataGridView1.Rows[6].Cells[4].Value = Convert.ToString(sw.ElapsedMilliseconds);
                if (Check(array_tmp))
                {
                    dataGridView1.Rows[6].Cells[5].Value = "да";
                }
                else
                {
                    dataGridView1.Rows[6].Cells[5].Value = "нет";
                }
            }
            bool cheked = false;
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.Equals(true))
                {
                    cheked = true;
                    break;
                }

            }
            if (!cheked) {
                label3.Visible = true;
                label3.Text = "Ничего не выбрано";
            }

        }
    }
}
