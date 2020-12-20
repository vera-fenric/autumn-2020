using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;
using System.IO;
using System.Globalization;

namespace Lab2
{
    //------------------>V2DataCollection<---------------------
    class V2DataCollection : V2Data, IEnumerable<DataItem>
    {
        public List<DataItem> MyList
        { get; set; }

        public V2DataCollection(string s, double d) : base(s, d)
        {
            MyList = new List<DataItem>();
        }

        public V2DataCollection(string filename): base("", 0)
        {
            double x, y, r, i;
            FileStream fs = null;
            StreamReader sr;
            MyList = new List<DataItem>();
            string s;
            string[] s1;
            string[] s2;
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                base.Info = sr.ReadLine();
                base.Frequency = Convert.ToDouble(sr.ReadLine());
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    s1 = s.Split(new Char[] { ':' }, StringSplitOptions.None);
                    if (s1.Length != 2) throw new Exception("Wrong input");
                    s2 = s1[0].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (s2.Length != 2) throw new Exception("Wrong input");
                    x = Convert.ToDouble(s2[0]);
                    y = Convert.ToDouble(s2[1]);
                    s2 = s1[1].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (s2.Length != 2) throw new Exception("Wrong input");
                    r = Convert.ToDouble(s2[0]);
                    i = Convert.ToDouble(s2[1]);
                    MyList.Add(new DataItem(new Vector2((float)x, (float)y), new Complex(i, r)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
        //инициализирует объект данными из filename try-catch-finally

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
                MyList.Add(data);
            }
        }

        public void Init(int n)
        {
            DataItem data;
            Vector2 point;
            Complex val;
            for (int i = 0; i < n; i++)
            {
                point = new Vector2(i,i);
                val = new Complex(i, i);
                data = new DataItem(point, val);
                MyList.Add(data);
            }
        }
        public override Complex[] NearAverage(float eps)
        {
            List<Complex> spare = new List<Complex>();
            float mid = 0;
            for (int i = 0; i < MyList.Count; i++)
            {
                mid = mid + (float)MyList[i].Val.Real;
            }
            mid = mid / MyList.Count;
            for (int i = 0; i < MyList.Count; i++)
            {
                if ((float)(MyList[i].Val.Real - mid) > eps)
                    spare.Add(MyList[i].Val);
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
            return "V2DataCollection:\nInfo: '" + base.Info + "' Frequency: " + base.Frequency + " Number of elims: " + MyList.Count + "\n"; //с именем типа,  данными базового класса и данными сеток по осям Ox и Oy;
        }
        public override string ToLongString()
        {//возвращает строку с именем типа,  данными базового класса и данными сеток по осям Ox и Oy;
            string s = this + "\n";
            for (int i = 0; i < MyList.Count; i++)
            {
                s = s + MyList[i] + " ";
            }
            s = s + "\n";
            return s;
        }


        //реализация интерфейса IEnumerable<DataItem>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)MyList).GetEnumerator();
        }
        IEnumerator<DataItem> IEnumerable<DataItem>.GetEnumerator()
        {
            return MyList.GetEnumerator();
        }


        public override string ToLongString(string format)
        {
            string s = "V2DataCollection:\nInfo: '" + base.Info + "' Frequency: " + base.Frequency.ToString(format) + " Number of elims: " + MyList.Count + "\n";
            /*for (int i = 0; i < MyList.Count; i++)
            {
                s = s + MyList[i].ToString(format) + " ";
            }
            s = s + "\n";
            return s;*/
            //раз уж мы сделали IEnumerable
            foreach (DataItem data in this)
            {
                s = s + data.ToString(format);
            }
            return s;
        }
        /*возвращает строку с именем типа,  данными базового класса, информацию для каждого элемента List<DataItem> 
         * (координаты точек измерения, комплексное значение поля и модуль значения) и использует параметр format для чисел с плавающей запятой*/
    }
}
