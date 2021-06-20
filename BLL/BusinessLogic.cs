using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BusinessLogic
    {
        // Local variables
        private DataAccess dal = new DataAccess();

        // Main method
        public List<Time> AvailableTimeOfSubject(string subject) => dal.GetAvailableTimesOfSubject(subject);
        public string[] SubjectList() => dal.GetSubjectList();
        public string[] LectureAndClassroom(string subject) => dal.GetLectureAndClassroom(subject);
        public string Lecture(string subject) => dal.GetLecture(subject);
    }
}
