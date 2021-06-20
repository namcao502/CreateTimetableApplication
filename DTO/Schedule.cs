using System.Collections;
using System.Collections.Generic;

namespace DTO
{
    public class Schedule
    {
        // Local variables
        Dictionary<int, List<ArrayList>> schedule;  // Giải thích ở link dưới
        // https://docs.google.com/spreadsheets/d/1rQvnJoBZCnMYzfRliShDvU3FsgSdP-FKFGh-65C9FK8/edit?usp=sharing

        // Global variable
        public static Time[] possibleTimes;

        // Constructors
        public Schedule()
        {   // khởi tạo thời khóa biểu full -1
            schedule = new Dictionary<int, List<ArrayList>>();
            int[] weekday = new int[] { 2, 3, 4, 5, 6, 7 };
            List<ArrayList> temp = new List<ArrayList>();
            for (int i = 0; i < 16; ++i)
            {
                temp.Add(new ArrayList());
                temp[i].Add(-1);
            }
            foreach (int x in weekday)
                schedule.Add(x, new List<ArrayList>(temp));
            SetFreeTime();
        }
        public Schedule(Schedule s)
        {
            schedule = new Dictionary<int, List<ArrayList>>(s.schedule);
        }

        // Main methods
        public void SetFreeTime()
        {   // đặt thời gian rảnh từ tham số vào thời khóa biểu
            foreach (Time t in possibleTimes)
                for (int i = t.StartTime; i <= t.EndTime; ++i)
                    schedule[t.Weekday][i] = new ArrayList();
        }
        public void AddClass(int subjectIndex, Time t)
        {   // thêm thời gian của môn vào thời khóa biểu
            for (int i = t.StartTime; i <= t.EndTime; ++i)
                schedule[t.Weekday][i].Add(subjectIndex);
        }
        public List<int> CoincidentClasses(int thisClass, Time classTime)
        {   // trả về list index của tất cả có môn bị trùng thời gian với môn có index là thisClass
            List<int> otherClasses = new List<int>();
            for (int i = classTime.StartTime; i <= classTime.EndTime; ++i)  // duyệt từng tiết
                foreach (int x in TimeAt(classTime.Weekday, i))   // duyệt arraylist của từng tiết
                    if (x != thisClass && x != -1 && !otherClasses.Contains(x))
                        otherClasses.Add(x);
            return otherClasses;
        }
        public ArrayList TimeAt(int weekday, int time)
        {   // Trả về ArrayList tại 1 tiết (time) của 1 thứ (weekday)
            ArrayList temp = new ArrayList(schedule[weekday][time]);
            return temp;
        }
        public string[] ShowSchedule()
        {
            List<string> s = new List<string>();
            foreach (var x in schedule)
            {
                s.Add($"THỨ {x.Key}:");
                for (int i = 0; i < x.Value.Count; ++i)
                {
                    if (x.Value[i].Contains(-1))
                        continue;
                    string s2 = $"     Tiết {i}: ";
                    ArrayList temp = x.Value[i];
                    foreach (var y in x.Value[i])
                        s2 += $"{y} ";
                    s.Add(s2);
                }
            }
            return s.ToArray();
        }
    }
}
