using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Collections;
using System.ComponentModel;

namespace Lab3
{
    //------------------>V2Data<---------------------
    abstract class V2Data: INotifyPropertyChanged
    {
        //реализация интерфейса
        public event PropertyChangedEventHandler PropertyChanged;

        private string info;
        public string Info  //для информации об измерениях и идентификации множества данных
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                //бросаем событие СвойствоИзменено(Infо)
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Info"));
            }
        }
        private double frequency;
        public double Frequency //для частоты электромагнитного поля
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
                //бросаем событие СвойствоИзменено(Frequency)
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Frequency"));
            }
        }
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
