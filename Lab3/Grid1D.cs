using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;

namespace Lab3
{
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

        public string ToString(string format) //LAB2
        {
            return "Step: " + Step.ToString(format) + " Nodes: " + Nodes;
        }
        /*возвращает строку с данными структуры и использует параметр format для чисел с плавающей запятой*/
    }
}
