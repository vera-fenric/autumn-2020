using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    //перечисление(enum) ChangeInfo со значениями ItemChanged, Add, Remove, Replace.
    enum ChangeInfo
    {
        ItemChanged,
        Add,
        Remove,
        Replace
    }

    //нужный делегат
    delegate void DataChangedEventHandler(object sender, DataChangedEventArgs args);

    //класс
    class DataChangedEventArgs
    {
        public ChangeInfo Info { get; set; }
        public double Param { get; set; }
        public DataChangedEventArgs(ChangeInfo Info, double Param)
        {
            this.Info = Info;   //мне понравился такой простой способ именования)
            this.Param = Param;
        }

        public override string ToString()
        {
            return Info.ToString() + " (frequency: " + Param + ")";
        }
    }
}
