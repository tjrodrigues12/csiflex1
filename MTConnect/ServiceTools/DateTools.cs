using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTools
{
    public class DateTools
    {
        DateTime _myDate;

        public DateTime MyDate
        {
            get
            {
                return _myDate;
            }
        }

        public DateTools(String date)
        {
            var _dateParts = date.Split(' ');

            var _date = _dateParts[0].Split('/');
            int _month = int.Parse(_date[0]);
            int _day = int.Parse(_date[1]);
            int _year = int.Parse(_date[2]);

            if (_year < 100) _year += 2000;

            int _hour = 0;
            int _min = 0;
            int _sec = 0;

            if (_dateParts.Length > 1)
            {
                var _time = _dateParts[1].Split(':');
                _hour = int.Parse(_time[0]);
                _min = int.Parse(_time[1]);
                _sec = int.Parse(_time[2]);
            }

            _myDate = new DateTime(_year, _month, _day, _hour, _min, _sec);
        }
    }
}
