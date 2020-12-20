using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;
using System.Linq;

namespace Lab2
{
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
            for (int i = l.Count - 1; i > -1; i--)
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
            //элемент V2DataOnGrid
            V2DataOnGrid g1 = new V2DataOnGrid("info 1", 1, new Grid1D(1, 2), new Grid1D(1, 2)); g1.Init(); Add(g1);
            //элемент V2DataOnGrid, у которого число узлов сетки 0
            V2DataOnGrid g2 = new V2DataOnGrid("info 2", 2, new Grid1D(0, 0), new Grid1D(0, 0)); Add(g2);
            //V2DataOnGrid g3 = new V2DataOnGrid("info 3", 3, new Grid1D(1, 2), new Grid1D(1, 3)); g3.InitRandom(0, 10); Add(g3);
            //элемент V2DataCollection
            V2DataCollection c1 = new V2DataCollection("info 1", 1); c1.Init(5); Add(c1);
            //элемент V2DataCollcection, у которого в списке нет элементов
            V2DataCollection c2 = new V2DataCollection("info 2", 2); Add(c2);
            //V2DataCollection c3 = new V2DataCollection("info 3", 3); c3.InitRandom(1, 0, 0, 0, 100); Add(c3);
        }
        public override string ToString()
        {
            string s = "";
            foreach (V2Data data in this)
                s = s + data;
            return s;
        }

        public string ToLongString(string format)
        {
            string s = "";
            foreach (V2Data data in this)
                s = s + data.ToLongString(format);
            return s;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)l).GetEnumerator();
        }
        IEnumerator<V2Data> IEnumerable<V2Data>.GetEnumerator()
        {
            return l.GetEnumerator();
        }

        //LINQ!

        public double Mid
        {
            get
            {
                var q1 = from v2 in l where v2 is V2DataOnGrid select v2;
                var q2 = from v2 in l where v2 is V2DataCollection select v2;
                var q1_2 = from V2DataOnGrid item in q1 from x in item select x;
                var q2_2 = from V2DataCollection item in q2 from x in item select x;
                var q3 = q1_2.Union(q2_2);
                return (from item in q3 select item.Val.Magnitude).Average();
            } 
        }
        
        public IEnumerable<DataItem> Dif
        {
            get
            {
                var q1 = from v2 in l where v2 is V2DataOnGrid select v2;
                var q2 = from v2 in l where v2 is V2DataCollection select v2;
                var q1_2 = from V2DataOnGrid item in q1 from x in item select x;
                var q2_2 = from V2DataCollection item in q2 from x in item select x;
                var q3 = q1_2.Union(q2_2);
                var max = (from item in q3 select item).Max(x => Math.Abs(x.Val.Magnitude - Mid));
                return from item in q3 where Math.Abs(item.Val.Magnitude - Mid) == max select item;
            }
        }
        public IEnumerable<Vector2> Twice
        {
            get
            {
                var q1 = from v2 in l where v2 is V2DataOnGrid select v2;
                var q2 = from v2 in l where v2 is V2DataCollection select v2;
                var q1_2 = from V2DataOnGrid item in q1 from x in item select x;
                var q2_2 = from V2DataCollection item in q2 from x in item select x;
                var q3 = q1_2.Concat(q2_2);
                var q4 = from item in q3 group item by item.Point into groups where groups.Count() > 1 select groups;
                return from x in q4 select x.Key;
            }
        }
    }

}
