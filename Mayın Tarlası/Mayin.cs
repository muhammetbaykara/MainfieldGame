using System;
using System.Drawing;

namespace Mayın_Tarlası
{
    public class Mayin
    {
        private Point loc;
        private bool dolu;
        private bool bakildiFlag;

        public Mayin(Point loca)
        {
            dolu = false;
            this.loc = loca;
            bakildiFlag = false;
        }

        public Point konum_al
        {
            get { return loc; }
        }

        public bool mayin_var_mi
        {
            get { return dolu; }
            set { dolu = value; }
        }

        public bool bakildi
        {
            get { return bakildiFlag; }
            set { bakildiFlag = value; }
        }
    }
}
