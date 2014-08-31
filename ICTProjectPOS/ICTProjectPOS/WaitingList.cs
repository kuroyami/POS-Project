using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICTProjectPOS
{
    struct WaitingList
    {
        internal string listNum;
        internal string firstName;
        internal string lastName;
        internal string resDate;

        public WaitingList(string listNum, string firstName, string lastName, string resDate)
        {
            this.listNum = listNum;
            this.firstName = firstName;
            this.lastName = lastName;
            this.resDate = resDate.Split(' ')[0];
        }

    }
}
