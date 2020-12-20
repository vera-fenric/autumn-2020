using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;

namespace Lab2
{
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
        public abstract string ToLongString(string format); //LAB2
    }
}
