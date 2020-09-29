using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;

namespace Lab1
{
    class Program
    {
        //------------------>DataItem<---------------------
		struct DataItem
        {    
            public Vector2 Point   //координаты двумерной точки
            { get; set; }
            public Complex Val      //комплексное значение электромагнитного поля
            { get; set; }

            public DataItem(Vector2 p, Complex v)
            {
                Point = p;
                Val = v;
            }
            public override string ToString()
            {
                return "(Point: " + Point + " Value: " + Val + ")";
            }
        }
        //------------------>Grid1D<---------------------
        struct Grid1D
        {   //для параметров равномерной сетки по одной оси;
            public float Step       //шаг
            { get; set; }
            public int Nodes        //количество узлов
            { get; set; }
            public Grid1D(float s, int n)
            {
                Step = s;
                Nodes = n;
            }
            public override string ToString()
            {
                return "Step: " + Step + " Nodes: " + Nodes;
            }
        }
        //------------------>V2Data<---------------------
        abstract class V2Data
        {
            public string Info  //для информации об измерениях и идентификации множества данных
            { get; set; }
            public double Frequency //для частоты электромагнитного поля
            { get; set; }
            public V2Data(string i, double f)
            {
                Info = i;
                Frequency = f;
            }
            public abstract Complex[] NearAverage(float eps);
            public abstract string ToLongString();
            public override string ToString()
            {
                return "Info: '" + Info + "' Frequency: " + Frequency;
            }

        }
        //------------------>V2DataOnGrid<---------------------
        class V2DataOnGrid : V2Data
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
                        v = new Vector2(i*g.Grid[0].Step, j*g.Grid[1].Step);
                        c.mylist.Add(new DataItem(v, g.array[i, j]));
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
                        if (((float)(array[i, j]).Real - mid) > eps)
                            spare.Add(array[i, j]);
                    }
                }
                Complex[] c = new Complex[spare.Count];
                for (int i=0; i< spare.Count; i++)
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
                        s=s+ "array[" + i + "," + j + "]=" + array[i, j] + " ";
                    }
                }
                s = s + "\n";
                return s;
            }
        }
        //------------------>V2DataCollection<---------------------
        class V2DataCollection : V2Data
        {
            public List<DataItem> mylist
            { get; set; }

            public V2DataCollection(string s, double d) : base(s, d)
            {
                mylist = new List<DataItem>();
            }
            public void InitRandom(int nItems, float xmax, float ymax, double minValue, double maxValue)
            {
                DataItem data;
                Vector2 point;
                Complex val;
                Random r = new Random();
                for (int i = 0; i < nItems; i++)
                {
                    point = new Vector2((float)r.NextDouble() * xmax, (float)r.NextDouble() * ymax);
                    val = new Complex(r.NextDouble() * (maxValue - minValue) + minValue, r.NextDouble() * (maxValue - minValue) + minValue);
                    data = new DataItem(point, val);
                    mylist.Add(data);
                }
            }
            public override Complex[] NearAverage(float eps)
            {
                List<Complex> spare = new List<Complex>();
                float mid = 0;
                for (int i = 0; i < mylist.Count; i++)
                {
                    mid = mid + (float)mylist[i].Val.Real;
                }
                mid = mid / mylist.Count;
                for (int i = 0; i < mylist.Count; i++)
                {
                    if ((float)(mylist[i].Val.Real - mid) > eps)
                        spare.Add(mylist[i].Val);
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
                return "V2DataCollection: Info: '" + base.Info + "' Frequency: " + base.Frequency + " Number of elims: " + mylist.Count + "\n"; //с именем типа,  данными базового класса и данными сеток по осям Ox и Oy;
            }
            public override string ToLongString()
            {//возвращает строку с именем типа,  данными базового класса и данными сеток по осям Ox и Oy;
                string s = this + "->";
                for (int i=0; i<mylist.Count; i++ )
                {
                    s = s + mylist[i] + " ";
                }
                s = s + "\n";
                return s;
            }

        }
        //------------------>V2MainCollection<---------------------
        class V2MainCollection : IEnumerable<V2Data>
        {
            private List<V2Data> l = new List<V2Data>();
	
			public int Count
            {
                get { return l.Count; }
            }
            public void Add(V2Data item)
            {
                l.Add(item);
            }
            public bool Remove(string id, double w)
            {
                bool b = false;
                for (int i=l.Count-1; i > -1; i--)
                {
                   if ((l[i].Info == id) && (l[i].Frequency == w))
                    {
                        l.Remove(l[i]);
                        b = true;
                    }
                }
                return b;
            }
            public void AddDefaults()
            {
                Grid1D x = new Grid1D(1,5);    //шаг, количество узлов
                Grid1D y = new Grid1D(1,4);
                V2DataOnGrid g1 = new V2DataOnGrid("info 1", 1, x, y); g1.InitRandom(0, 100); Add(g1);
                V2DataOnGrid g2 = new V2DataOnGrid("info 2", 2, x, y); g2.InitRandom(0, 100); Add(g2);
                V2DataOnGrid g3 = new V2DataOnGrid("info 2", 2, x, y); g3.InitRandom(0, 100); Add(g3);
                V2DataCollection c1 = new V2DataCollection("info 1", 1); c1.InitRandom(11, 10, 10, 0, 100); Add(c1);
                V2DataCollection c2 = new V2DataCollection("info 2", 2); c2.InitRandom(11, 10, 10, 0, 100); Add(c2);
                V2DataCollection c3 = new V2DataCollection("info 3", 3); c3.InitRandom(11, 10, 10, 0, 100); Add(c3);
            }
            public override string ToString()
            {
                string s = "";
                for (int i = 0; i < l.Count; i++)
                {
                    s = s + l[i];
                }
                s = s + "\n";
                return s;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)l).GetEnumerator();
            }
            IEnumerator<V2Data> IEnumerable<Program.V2Data>.GetEnumerator()
            {
                return l.GetEnumerator();
            }
        }

    static void Main()
        {
            //создаём и инициализируем объект V2DataOnGrid
            V2DataOnGrid obj1 = new V2DataOnGrid("ex1", 1,new Grid1D(1,2), new Grid1D(1,3));
            obj1.InitRandom(0, 50);
            //выводим этот объект, преобразованный к V2DataCollection
            Console.WriteLine(((V2DataCollection)obj1).ToLongString());

            //создаём, инициализируем и выводим V2MainCollection
            V2MainCollection obj2 = new V2MainCollection();
            obj2.AddDefaults();
            Console.Write(obj2);

            //используем NearAverage(eps) по всем объектам из V2MainCollection и выводим их
            float eps = 30;
            Complex[] a;
            foreach (V2Data ident in obj2)
            {
                Console.Write("->");            //этот кусок нужен исключительно для вывода Complex[] в консоль
                a = ident.NearAverage(eps);     //его можно заменить на Console.WriteLine(ident.NearAverage(eps)),
                for (int i=0; i< a.Length; i++) //если описан соответствующий ToString
                    Console.Write(a[i]+" ");
                Console.Write("\n");
             }
        }
    }
}
