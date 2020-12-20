using System;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;

namespace Lab2
{
    class Program
    {
        static void Main()
        {
            //Создаём, инициализируем и выводим V2MainCollection c помощью format
            Console.WriteLine("Вывод с помощью format:");
            V2DataCollection obj1 = new V2DataCollection("file.txt");
            Console.Write(obj1.ToLongString("F2"));
            Console.WriteLine();

            //Создаём объектр V2MainCollection, вызываем для него AddDefaults() и выводим результат
            Console.WriteLine("Вывод AddDefaults");
            V2MainCollection obj2 = new V2MainCollection();
            obj2.AddDefaults();
            Console.Write(obj2.ToLongString("F2"));
            Console.WriteLine();

            //Свойства класса V2MainCollection с запросами LINQ:
            Console.WriteLine("Вывод LINQ");
            Console.WriteLine("Среднее значение модуля: " + obj2.Mid + ".");
            Console.WriteLine("Результаты измерений, наиболее отличающиеся от среднего значения:");
            foreach (var i in obj2.Dif) Console.WriteLine(i);
            Console.WriteLine();
            Console.WriteLine("Результаты измерений, встречающиеся несколько раз:");
            foreach (var i in obj2.Twice) Console.WriteLine(i);
            Console.WriteLine();
            
            //Console.Read();
        }
    }
}
