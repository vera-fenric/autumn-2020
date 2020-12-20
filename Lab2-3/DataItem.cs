using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;

namespace Lab2
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

        public string ToString(string format) //LAB2
        {
            return Point.ToString(format) + ": " + Val.ToString(format) + " - " + Val.Magnitude.ToString(format) + "\n";
        }
            /*возвращает строку, содержащую координаты точки, в которой измеряется поле,
            комплексное значение поля в этой точке и модуль значения поля, 
            и использует параметр format для чисел с плавающей запятой*/
    }
}
