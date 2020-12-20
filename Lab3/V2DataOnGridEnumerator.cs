using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections;

namespace Lab3
{
    class V2DataOnGridEnumerator: IEnumerator<DataItem>
    {
        public V2DataOnGrid v;
        private int currentX;
        private int currentY;
        public V2DataOnGridEnumerator(V2DataOnGrid v_new)
        {
            v = v_new;
            currentX = 0;
            currentY = -1;
        }
        void IDisposable.Dispose() { }
        public DataItem Current
        {
            get
            {
                if ((currentX >= v.Grid[0].Nodes) || (currentY >= v.Grid[1].Nodes)) throw new Exception("Current error");
                if ((currentX < 0) || (currentY < 0)) throw new Exception("Current error");
                return new DataItem(new Vector2((float)(v.Grid[0].Step) * currentX, (float)(v.Grid[1].Step) * currentY), v.array[currentX, currentY]);
            }
        }
        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            currentY++;
            if (currentY >= v.Grid[1].Nodes)
            {
                currentY = 0;
                currentX++;
            }
            if (currentX >= v.Grid[0].Nodes) return false;
            return true;
        }
        public void Reset()
        {
            currentX = -1;
            currentY = -1;
        }
    }
}
