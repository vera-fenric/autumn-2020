using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;

namespace Lab2
{
    //------------------>V2DataOnGrid<---------------------
    class V2DataOnGrid : V2Data, IEnumerable<DataItem>
    {
        public Grid1D[] Grid    //с двумя элементами для сеток по осям Ox и Oy;
        { get; set; }
        public Complex[,] array //двумерный прямоугольный массив для значений поля в узлах сетки
        { get; set; }

        public V2DataOnGrid(string s, double d, Grid1D g1, Grid1D g2) : base(s, d)
        {
            Grid = new Grid1D[2];
            Grid[0] = g1;
            Grid[1] = g2;
            array = new Complex[g1.Nodes, g2.Nodes];
        }
        public void Init()
        {
            Complex val;
            for (int i = 0; i < Grid[0].Nodes; i++)
            {
                for (int j = 0; j < Grid[1].Nodes; j++)
                {
                    val = new Complex(i,j);
                    array[i, j] = val;
                }
            }
        }
        public void InitRandom(double minValue, double maxValue)
        {
            Complex val;
            Random r = new Random();
            for (int i = 0; i < Grid[0].Nodes; i++)
            {
                for (int j = 0; j < Grid[1].Nodes; j++)
                {
                    val = new Complex(r.NextDouble() * (maxValue - minValue) + minValue, r.NextDouble() * (maxValue - minValue) + minValue);
                    array[i, j] = val;
                }
            }
        }
        public static explicit operator V2DataCollection(V2DataOnGrid g)
        {
            V2DataCollection c = new V2DataCollection(g.Info, g.Frequency);
            Vector2 v;
            for (int i = 0; i < g.Grid[0].Nodes; i++)
            {
                for (int j = 0; j < g.Grid[1].Nodes; j++)
                {
                    v = new Vector2(i * g.Grid[0].Step, j * g.Grid[1].Step);
                    c.MyList.Add(new DataItem(v, g.array[i, j]));
                }
            }
            return c;
        }
        public override Complex[] NearAverage(float eps)
        {
            List<Complex> spare = new List<Complex>();
            float mid = 0;
            for (int i = 0; i < Grid[0].Nodes; i++)
            {
                for (int j = 0; j < Grid[1].Nodes; j++)
                {
                    mid = mid + (float)(array[i, j]).Real;
                }
            }
            mid = mid / (Grid[0].Nodes * Grid[1].Nodes);
            for (int i = 0; i < Grid[0].Nodes; i++)
            {
                for (int j = 0; j < Grid[1].Nodes; j++)
                {
                    if (((float)(array[i, j]).Real - mid) <= eps)
                        spare.Add(array[i, j]);
                }
            }
            Complex[] c = new Complex[spare.Count];
            for (int i = 0; i < spare.Count; i++)
            {
                c[i] = spare[i];
            }
            return c;
        }
        public override string ToString()
        {
            return "V2DataOnGrid:\nInfo: '" + base.Info + "' Frequency: " + base.Frequency + " Ox: " + Grid[0] + " Oy: " + Grid[1] + "\n"; //с именем типа,  данными базового класса и данными сеток по осям Ox и Oy;
        }
        public override string ToLongString()
        {
            string s = this + "\n";
            for (int i = 0; i < Grid[0].Nodes; i++)
            {
                for (int j = 0; j < Grid[1].Nodes; j++)
                {
                    s = s + "array[" + i + "," + j + "]=" + array[i, j] + " ";
                }
            }
            s = s + "\n";
            return s;
        }

        //реализация интерфейса IEnumerable<DataItem>   (пришлось создать для DataOnGrid свой Enumerator)

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)array).GetEnumerator();
        }
        IEnumerator<DataItem> IEnumerable<DataItem>.GetEnumerator()
        {
            return new V2DataOnGridEnumerator(this);
        }

        public override string ToLongString(string format)
        {
            string s = "V2DataOnGrid:\nInfo: '" + base.Info + "' Frequency: " + base.Frequency.ToString(format) + " " + Grid[0].ToString(format) + " " + Grid[1].ToString(format) + "\n";
            /*for (int i = 0; i < Grid[0].Nodes; i++)
            {
                for (int j = 0; j < Grid[1].Nodes; j++)
                {
                    s = s + "array[" + i + "," + j + "]=" + array[i, j].ToString(format) + " ";
                }
            }
            s = s + "\n";
            return s;*/

            //раз уж мы делали IEnum...
            foreach (DataItem data in this)
            {
                s = s + data.ToString(format);
            }
            return s;
        }
        /*возвращает строку с именем типа, данными базового класса,
         * данными Grid1D и информацию о  каждом узле сетки
         * (координаты, комплексное значение в узле сетки, модуль значения)
         * и использует параметр format для чисел с плавающей запятой*/

    }
}
