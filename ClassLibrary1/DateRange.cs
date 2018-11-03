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

        #region methods

        public static IEnumerable<DateTime> GetDaysBetween(DateTime start, DateTime end)
        {
            if (end < start)
            {
                throw new ArgumentException("End date should be after start date");
            }

            List<DateTime> output = new List<DateTime>();

            while(start <= end)
            {
                output.Add(start);
                start = start.AddDays(1);
            }

            return output;
        }

        #endregion 

    }

}
