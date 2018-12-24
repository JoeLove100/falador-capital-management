using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{

    public class DateRange : IIsSameAs
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

        #region public methods

        public bool IsSameAs(IIsSameAs comparison)
        {
            DateRange comparator = comparison as DateRange;
            if (comparator is null) return false;

            if (comparator.Start != Start) return false;
            if (comparator.End != End) return false;

            return true;
        }

        public DateRange Clone()
        {
            return new DateRange(Start, End);
        }

        #endregion


        #region  static methods

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

        public static IEnumerable<DateTime> GetDaysBetweenWeekly(DateTime start,
            DateTime end)
        {
            if (end < start)
            {
                throw new ArgumentException("End date should be after start date");
            }

            List<DateTime> output = new List<DateTime>();

            while (start <= end)
            {
                output.Add(start);
                start = start.AddDays(7);
            }

            return output;
        }

        public static IEnumerable<DateTime> GetMonthEndsBetween(DateTime start, 
            DateTime end)
        {
            ///<summary>
            ///returns an enumerable of month end
            ///dates lying between the two input
            ///dates (inclusive)
            ///</summary>

            if (end < start)
            {
                throw new ArgumentException("End date should be after start date");
            }

            List<DateTime> output = new List<DateTime>();
            DateTime date = GetMonthEnd(start);

            while(date <= end)
            {
                output.Add(date);
                date = GetMonthEnd(date.AddDays(1));
            }

            return output;
        }

        public static IEnumerable<DateTime> GetYearEndsBetween(DateTime start,
    DateTime end)
        {
            ///<summary>
            ///returns an enumerable of year end
            ///dates lying between the two input
            ///dates (inclusive)
            ///</summary>

            if (end < start)
            {
                throw new ArgumentException("End date should be after start date");
            }

            List<DateTime> output = new List<DateTime>();
            DateTime date = GetYearEnd(start);

            while (date <= end)
            {
                output.Add(date);
                date = GetYearEnd(date.AddDays(1));
            }

            return output;
        }

        public static string[] GetDatesAsStrings(List<DateTime> dates)
        {
            List<string> output = new List<string>();

            for (int i = 0; i < dates.Count; i++)
            {
                output.Add(dates[i].ToShortDateString());
            }

            return output.ToArray<string>();
        }

        #endregion

        #region private methods

        private static DateTime GetMonthEnd(DateTime date)
        {
            ///<summary>
            ///Gets the end of month date
            ///for a corresponding date input
            ///</summary>
            
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            DateTime monthEnd = new DateTime(date.Year, date.Month,
                daysInMonth);

            return monthEnd;
        }

        private static DateTime GetYearEnd(DateTime date)
        {
            ///<summary>
            ///Gets the end of month date
            ///for a corresponding date input
            ///</summary>

            DateTime yearEnd = new DateTime(date.Year, 12, 31);
            return yearEnd;
        }

        #endregion

    }

}
