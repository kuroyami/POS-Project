using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICTProjectPOS
{
    struct Reservation_Weekly
    {
        public string date;
        public string time;

        public Reservation_Weekly(string date, string time)
        {
            this.date = date.Substring(0, 10);

            string t = time.Substring(11, 5);

            System.Diagnostics.Debug.WriteLine(Int32.Parse(t.Substring(0, 2).ToString()));

            if (Int32.Parse(t.Substring(0, 2)) < 10)
            {
                this.time = (Int32.Parse(t.Substring(0, 2)) + 12).ToString() + ":00";
            } 
            else
            {
                this.time = t;
            }

            System.Diagnostics.Debug.WriteLine("time = " + this.time);

        }


    }
}
