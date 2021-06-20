using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class DataAccess
    {
        // Local variables
        private SqlConnection conn = new SqlConnection("Data Source=CHINSU-TUF-ASUS;" +
            "Initial Catalog=AI;Integrated Security=True;");
        private SqlCommand command;
        private SqlDataReader reader;
        private SqlDataAdapter da;

        // Main method
        public List<Time> GetAvailableTimesOfSubject(string subject)
        {   // Trả về list thời gian các lớp học của môn truyền vào theo kiểu TIME
            KeepConnection();
            command = conn.CreateCommand();
            command.CommandText = @"SELECT LOPHOC.Thu, LOPHOC.Tiet
                                    FROM MONHOC
                                    INNER JOIN LOPHOC ON MONHOC.MaMon = LOPHOC.MaMon
                                    WHERE MONHOC.Ten = N'" + subject + "'";
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            List<Time> times = new List<Time>();
            while (reader.Read())
                times.Add(new Time(reader.GetString(0), reader.GetString(1)));
            reader.Close();
            conn.Close();
            return times;
        }
        public string[] GetSubjectList()
        {
            KeepConnection();
            command = conn.CreateCommand();
            command.CommandText = "SELECT Ten FROM MONHOC";
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            List<string> subjects = new List<string>();
            while (reader.Read())
                subjects.Add(reader.GetString(0));
            reader.Close();
            conn.Close();
            return subjects.ToArray();
        }
        public string[] GetLectureAndClassroom(string subject)
        {
            KeepConnection();
            command = conn.CreateCommand();
            command.CommandText = string.Format(@"SELECT LOPHOC.GiangVien, LOPHOC.Phong
                                    FROM MONHOC
                                    INNER JOIN LOPHOC ON MONHOC.MaMon = LOPHOC.MaMon
                                    WHERE MONHOC.Ten=N'{0}'", subject);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            string[] res = new string[2];
            if (reader.Read())
            {
                res[0] = reader.GetString(0);
                res[1] = reader.GetString(1);
            }
            reader.Close();
            conn.Close();
            return res;
        }
        public string GetLecture(string subject)
        {
            return GetLectureAndClassroom(subject)[0];
        }

        // Support method
        public void KeepConnection()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }
    }
}
