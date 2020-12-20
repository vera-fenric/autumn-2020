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
            V2MainCollection obj1 = new V2MainCollection();
            obj1.DataChanged += DataChangedHandler;

            //добавляем элементы в коллекцию
            obj1.AddDefaults();
            /* ---------------ВЫВОД---------------
            Objects was changed: Add (frequency: 1)
            Objects was changed: Add (frequency: 2)
            Objects was changed: Add (frequency: 3)
            Objects was changed: Add (frequency: 4)
            ---------------КОНЕЦ ВЫВОДА---------------*/
            //В AddDefaults() мы добавляем 4 элемента с Frquency 1, 2, 3, 4


            //изменяем элемент в коллекции
            V2DataCollection obj2 = new V2DataCollection("file.txt");
            obj1[0] = obj2;
            /* ---------------ВЫВОД---------------
            Objects was changed: Replace (frequency: 190,1)
            ---------------КОНЕЦ ВЫВОДА---------------*/
            //В файле file.txt находится объект V2DataCollection с Frequency 190,1, его мы только что загрузили  


            //изменяем свойства объекта в коллекции
            obj1[0].Info = "new info";
            /* ---------------ВЫВОД---------------
            Objects was changed: ItemChanged (frequency: 190,1)
            ---------------КОНЕЦ ВЫВОДА---------------*/
            //У только что добавленного объексты мы меняем поле Info, поле Frequency остаётся старым 190,1   


            obj1[0].Frequency = 1.25;
            /* ---------------ВЫВОД---------------
            Objects was changed: ItemChanged (frequency: 1,25)
            ---------------КОНЕЦ ВЫВОДА---------------*/
            //У того же самого объекта меняем поле Frequency на 1.25, как раз это значение и выводится


            //удаляем объект из коллекции
            obj1.Remove("new info", 1.25);
            /* ---------------ВЫВОД---------------
            Objects was changed: Remove (frequency: 1,25)
            ---------------КОНЕЦ ВЫВОДА---------------*/
            //Объект у нас задаётся по полу Info и Frequency, так что мы объект из file.txt изменили, и его же удалим
        }

        static void DataChangedHandler(object sender, DataChangedEventArgs args)
        {
            Console.WriteLine("Objects was changed: " + args.ToString());
        }
    }
}
