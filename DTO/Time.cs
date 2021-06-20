using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// Chứa 1 đoạn cụ thể trong thời khóa biểu gồm thứ, tiết đầu và cuối
    /// </summary>
    public class Time
    {
        // Local variables
        private int weekday;
        private int startTime;
        private int endTime;

        // Constructors
        public Time() { }
        public Time(int weekday, int startClass, int endClass)
        {
            this.weekday = weekday;
            startTime = startClass;
            endTime = endClass;
        }
        public Time(string weekday, string classTime)
        {   // Khởi tạo dành cho dữ liệu vừa được lấy trong database ra
            this.weekday = Convert.ToInt32(weekday);
            string[] temp = classTime.Split('-');
            startTime = int.Parse(temp[0]);
            endTime = int.Parse(temp[1]);
        }
        public Time(Time t)
        {
            weekday = t.weekday;
            startTime = t.startTime;
            endTime = t.endTime;
        }

        // Property
        public int Weekday
        {
            get => weekday;
            set => weekday = value;
        }
        public int StartTime
        {
            get => startTime;
            set => startTime = value;
        }
        public int EndTime
        {
            get => endTime;
            set => endTime = value;
        }
        public int RangeTime { get => endTime - startTime + 1; }    // Số tiết của khoảng thời gian này

        // Override method
        public override string ToString()
        {   // Chuyển sang dạng string theo format như dưới
            return string.Format("Thu {0}/{1}-{2}", weekday, startTime, endTime);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Time))
                throw new Exception("The object type is not Time");
            Time t = obj as Time;
            return weekday == t.weekday && startTime == t.startTime && endTime == t.endTime;
        }
        public override int GetHashCode() => base.GetHashCode();

        // Define operators: >, <, ==, !=
        public static bool operator >(Time a, Time b)
        {
            if (a.weekday != b.weekday)
                return a.weekday > b.weekday;
            if (a.startTime != b.startTime)
                return a.startTime > b.startTime;
            return a.endTime > b.endTime;
        }
        public static bool operator <(Time a, Time b)
        {
            if (a.weekday != b.weekday)
                return a.weekday < b.weekday;
            if (a.startTime != b.startTime)
                return a.startTime < b.startTime;
            return a.endTime < b.endTime;
        }
        public static bool operator ==(Time a, Time b)
        {
            return a.weekday == b.weekday && a.startTime == b.startTime && a.endTime == b.endTime;
        }
        public static bool operator !=(Time a, Time b)
        {
            return a.weekday != b.weekday || a.startTime != b.startTime || a.endTime != b.endTime;
        }
        public static bool ClassInFreetime(Time classTime)
        {
            foreach (var time in Schedule.possibleTimes)
                if (time.Weekday == classTime.Weekday && time.StartTime <= classTime.startTime && classTime.EndTime <= time.EndTime)
                    return true;
            return false;
        }
    }
}
