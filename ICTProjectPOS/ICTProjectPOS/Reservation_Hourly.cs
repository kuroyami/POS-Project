using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICTProjectPOS
{
    struct Reservation_Hourly
    {
        public int resNum;
        public int tableNum;

        public Reservation_Hourly(int resNum, int tableNum)
        {
            this.resNum = resNum;
            this.tableNum = tableNum;
        }

    }
}
