using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{

    public class DateRange
    {

        #region constructor

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        #endregion

        #region properties

        public DateTime Start { get; }
        public DateTime End { get; }

        #endregion 

    }

}
