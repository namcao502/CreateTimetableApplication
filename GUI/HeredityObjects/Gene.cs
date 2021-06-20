using DTO;

namespace GUI.HeredityObjects
{
    public class Gene
    {
        // Local variables
        private string subject;
        private Time classTime;

        // Constructors
        public Gene() { classTime = new Time(); }
        public Gene(string subject, int weekday, int startClass, int endClass)
        {
            this.subject = subject;
            classTime = new Time(weekday, startClass, endClass);
        }
        public Gene(string subject, string weekday, string classTime)
        {
            this.subject = subject;
            this.classTime = new Time(weekday, classTime);
        }
        public Gene(Gene g)
        {
            subject = g.subject;
            classTime = new Time(g.ClassTime);
        }
        public Gene(string subject, Time classTime)
        {
            this.subject = subject;
            this.classTime = new Time(classTime);
        }

        // Property
        public string Subject
        {
            get => subject;
            set => subject = value;
        }
        public Time ClassTime
        {
            get => new Time(classTime);
            set => classTime = value;
        }
        public int Weekday
        {
            get => classTime.Weekday;
            set => classTime.Weekday = value;
        }
        public int StartTime
        {
            get => classTime.StartTime;
            set => classTime.StartTime = value;
        }
        public int EndTime
        {
            get => classTime.EndTime;
            set => classTime.EndTime = value;
        }
        public int RangeTime { get => ClassTime.RangeTime; }

        // Main methods
        public string[] GetLectureAndClassroom() => Heredity.bll.LectureAndClassroom(Subject);

        // Define operator ==, !=
        public static bool operator ==(Gene a, Gene b)
        {
            if (a.Subject != b.Subject)
                return false;
            return a.Weekday == b.Weekday && a.StartTime == b.StartTime;
        }
        public static bool operator !=(Gene a, Gene b)
        {
            return !(a == b);
        }
    }
}
