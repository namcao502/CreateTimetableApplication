using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using GUI.HeredityObjects;

namespace GUI
{
    public partial class MainForm : Form
    {
        private BusinessLogic bll = new BusinessLogic();
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = Color.Transparent;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Combox môn học
            cbMonHoc.Items.AddRange(bll.SubjectList());

            // Table chọn lịch rảnh
            dgvLichRanh.DataSource = createDataTable();
            dgvLichRanh.RowHeadersVisible = false;
            dgvLichRanh.ColumnHeadersDefaultCellStyle.ForeColor = Color.Maroon;
            dgvLichRanh.DefaultCellStyle.BackColor = SystemColors.Window;
            dgvLichRanh.Columns[0].Width = 300;
            dgvLichRanh.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLichRanh.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLichRanh.ClearSelection();
            dgvLichRanh.ReadOnly = true;
            dgvLichRanh.Rows[5].DefaultCellStyle.BackColor = Color.LightGray;
            dgvLichRanh.Rows[5].DefaultCellStyle.SelectionBackColor = Color.Transparent;

            // Cài đặt kích thước các groupbox
            gbDangky.Width = Width * 40 / 100;
            gbDangky.Height = Height * 35 / 100;
            gbLichRanh.Location = new Point(gbDangky.Location.X + gbDangky.Width + 5, gbLichRanh.Location.Y);
            gbLichRanh.Width = Width * 585 / 1000;
            gbLichRanh.Height = Height * 35 / 100;
            gbThoiKhoaBieu.Location = new Point(gbDangky.Location.X, gbDangky.Location.Y + gbDangky.Height + 6);
            gbThoiKhoaBieu.Width = Width * 785 / 1000;
            gbThoiKhoaBieu.Height = Height * 56 / 100;
            gbChiTiet.Location = new Point(gbThoiKhoaBieu.Location.X + gbThoiKhoaBieu.Width + 6, gbThoiKhoaBieu.Location.Y);
            gbChiTiet.Width = Width * 20 / 100;
            gbChiTiet.Height = gbThoiKhoaBieu.Height;
            btnLamMoi3.ForeColor = Color.White;

            // Textbox chi tiết
            tbMonHoc.ReadOnly = true;
            tbPhong.ReadOnly = true;
            tbTinChi.ReadOnly = true;
            tbThu.ReadOnly = true;
            tbTiet.ReadOnly = true;
            tbThoiGian.ReadOnly = true;
            tbGiangVien.ReadOnly = true;
            tbCoSo.ReadOnly = true;

            dgvLichRanh.DefaultCellStyle.Font = new Font("Consolas", 8, FontStyle.Bold);
            dgvThoiKhoaBieu.DefaultCellStyle.Font = new Font("Barlow", 14, FontStyle.Bold);
        }
        private DataTable createDataTable()
        {

            DataTable dt = new DataTable();
            string[] thu = new string[] { "Thời gian (tiết)", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            string[] tiet = new string[]
            {
                "07h00-07h50 (1)",
                "07h55-08h45 (2)",
                "08h50-09h40 (3)",
                "09h45-10h35 (4)",
                "10h40-11h30 (5)",
                "11h35-12h25 (6)",
                "12h30-13h20 (7)",
                "13h25-14h15 (8)",
                "14h20-15h10 (9)",
                "15h15-16h05 (10)",
                "16h10-17h00 (11)",
                "17h05-17h55 (12)",
                "18h00-18h50 (13)",
                "18h55-19h45 (14)",
                "19h50-20h40 (15)"
            };
            foreach (var t in thu)
                dt.Columns.Add(t, typeof(string));
            for (int i = 0; i < 15; ++i)
                dt.Rows.Add(tiet[i], "", "", "", "", "", "");
            return dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbMonHoc.Text))
                return;
            string subject = cbMonHoc.SelectedItem.ToString();
            lbMonHocDangKy.Items.Add(subject);
            cbMonHoc.Items.Remove(subject);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lbMonHocDangKy.SelectedIndex == -1)
                return;
            string subject = lbMonHocDangKy.SelectedItem.ToString();
            cbMonHoc.Items.Add(subject);
            lbMonHocDangKy.Items.Remove(subject);
        }
        private bool xacnhan1 = false;
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            xacnhan1 = !xacnhan1;
            var color = xacnhan1 ? Color.Silver : Color.FromArgb(192, 64, 0);
            cbMonHoc.Enabled = !xacnhan1;
            cbMonHoc.BackColor = xacnhan1 ? Color.Silver : SystemColors.Window;
            lbMonHocDangKy.Enabled = !xacnhan1;
            lbMonHocDangKy.BackColor = xacnhan1 ? Color.Silver : SystemColors.Window;
            btnThem.Enabled = !xacnhan1;
            btnThem.BackColor = color;
            btnXoa.Enabled = !xacnhan1;
            btnXoa.BackColor = color;
            btnLamMoi.Enabled = !xacnhan1;
            btnLamMoi.BackColor = color;
            btnXacNhan.Text = xacnhan1 ? "Chỉnh sửa" : "Xác nhận";
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            foreach (var item in lbMonHocDangKy.Items)
                cbMonHoc.Items.Add(item);
            lbMonHocDangKy.Items.Clear();
            xacnhan1 = true;
            btnXacNhan.PerformClick();
        }

        private bool add = false;
        private void btnThem2_Click(object sender, EventArgs e)
        {
            dgvLichRanh.Enabled = true;
            btnThem2.BackColor = Color.Lime;
            btnXoa2.BackColor = Color.FromArgb(192, 64, 0);
            add = true;
            dgvLichRanh.Focus();
        }

        private void btnXoa2_Click(object sender, EventArgs e)
        {
            dgvLichRanh.Enabled = true;
            btnXoa2.BackColor = Color.Red;
            btnThem2.BackColor = Color.FromArgb(192, 64, 0);
            add = false;
            dgvLichRanh.Focus();
        }

        private void dgvLichRanh_SelectionChanged(object sender, EventArgs e)
        {
            var backcolor = add ? Color.Lime : SystemColors.Window;
            if (!add)
                dgvLichRanh.Cursor = Cursors.Default;
            dgvLichRanh.DefaultCellStyle.SelectionBackColor = backcolor;
            foreach (DataGridViewTextBoxCell cell in dgvLichRanh.SelectedCells)
            {
                if (cell.ColumnIndex == 0 || cell.RowIndex == 5)
                    continue;
                cell.Style.BackColor = backcolor;
            }
        }

        private bool xacnhan2 = false;
        private void btnXacNhan2_Click(object sender, EventArgs e)
        {
            xacnhan2 = !xacnhan2;
            dgvLichRanh.Enabled = !xacnhan2;
            var color = xacnhan2 ? Color.Silver : Color.FromArgb(192, 64, 0);
            btnThem2.Enabled = !xacnhan2;
            btnThem2.BackColor = color;
            btnXoa2.Enabled = !xacnhan2;
            btnXoa2.BackColor = color;
            btnLamMoi2.Enabled = !xacnhan2;
            btnLamMoi2.BackColor = color;
            btnXacNhan2.Text = xacnhan2 ? "Chỉnh sửa" : "Xác nhận";
        }

        private void btnLamMoi2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvLichRanh.Rows.Count; ++i)
                for (int j = 0; j < dgvLichRanh.Columns.Count; ++j)
                    if (dgvLichRanh.Rows[i].Cells[j].Style.BackColor == Color.Lime)
                        dgvLichRanh.Rows[i].Cells[j].Style.BackColor = SystemColors.Window;
            Update();
        }

        private List<string> subjects;
        private List<Time> possibleTime;
        private int[] fBefore;
        private Chromosome timetable;
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (!xacnhan1)
                btnXacNhan.PerformClick();
            if (!xacnhan2)
                btnXacNhan2.PerformClick();
            subjects = new List<string>();
            possibleTime = new List<Time>();
            fBefore = new int[Heredity.numberChrome];
            if (!GetData())
                return;
            timetable = YourTimetable();
            if (timetable == null)
            {
                MessageBox.Show("Không có thời khóa biểu phù hợp với thời gian của bạn");
                return;
            }
            string[] lecture = new string[subjects.Count], room = new string[subjects.Count];
            for (int i = 0; i < timetable.Length; ++i)
            {
                string[] temp = timetable.GeneAt(i).GetLectureAndClassroom();
                lecture[i] = temp[0];
                room[i] = temp[1];
            }
            dgvThoiKhoaBieu.DataSource = DesignTimeTable(timetable, lecture, room);
            dgvThoiKhoaBieu.RowHeadersVisible = false;
            dgvThoiKhoaBieu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvThoiKhoaBieu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvThoiKhoaBieu.ReadOnly = true;
            int height = (dgvThoiKhoaBieu.Height - dgvThoiKhoaBieu.Rows[0].Height - 20) / dgvThoiKhoaBieu.RowCount;
            for (int i = 0; i < dgvThoiKhoaBieu.RowCount; ++i)
                dgvThoiKhoaBieu.Rows[i].Height = height;
            dgvThoiKhoaBieu.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            btnLamMoi3.Enabled = true;
        }

        private bool GetData()
        {
            if (lbMonHocDangKy.Items.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn môn học đăng ký!");
                btnXacNhan.PerformClick();
                return false;
            }
            foreach (var item in lbMonHocDangKy.Items)
                subjects.Add(item.ToString());
            List<Point> cells = new List<Point>();
            for (int i = 0; i < dgvLichRanh.Rows.Count; ++i)
                for (int j = 0; j < dgvLichRanh.Columns.Count; ++j)
                    if (dgvLichRanh.Rows[i].Cells[j].Style.BackColor == Color.Lime)
                        cells.Add(new Point(i, j));
            if (cells.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn thời gian mong muốn!");
                btnXacNhan2.PerformClick();
                return false;
            }
            cells.Sort((p1, p2) =>
            {
                if (p1 == p2)
                    return 0;
                if (p1.Y != p2.Y)
                    return p1.Y > p2.Y ? 1 : -1;
                return p1.X > p2.X ? 1 : -1;
            });
            for (int i = 0, weekday, start, end; i < cells.Count; ++i)
            {
                weekday = cells[i].Y;
                start = cells[i].X;
                end = start;
                for (int j = i + 1; j < cells.Count; ++j)
                {
                    if (cells[j].Y == weekday && cells[j].X == end + 1)
                        end++;
                    else
                    {
                        i = j - 1;
                        possibleTime.Add(new Time(weekday + 1, start + 1, end + 1));
                        break;
                    }
                    if (j == cells.Count - 1)
                    {
                        i = j - 1;
                        possibleTime.Add(new Time(weekday + 1, start + 1, end + 1));
                    }
                }
            }
            return true;
        }

        private Chromosome YourTimetable()
        {
            Population population = Heredity.Initialize(subjects, possibleTime);
            int res = 0;
            for (int i = 0; Finished(ref res, i); ++i)
            {
                if (i == 5000)
                    return null;
                population = Heredity.Selection(population);
                fBefore = population.Fitnesses();
                population = Heredity.Crossover(population);
                fBefore = population.Fitnesses();
                Heredity.Mutation(population);
                fBefore = population.Fitnesses();
            }
            return population.ChromosomeAt(res);
        }

        private bool Finished(ref int res, int number)
        {
            for (int i = 0; i < fBefore.Length; ++i)
                if (fBefore[i] == subjects.Count)
                {
                    res = i;
                    return false;
                }
            return true;
        }

        private DataTable DesignTimeTable(Chromosome timetable, string[] lecture, string[] room)
        {
            DataTable dt = new DataTable();
            string[] thu = new string[] { "Phòng", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            foreach (var t in thu)
                dt.Columns.Add(t, typeof(string));
            int countRoom= 0;
            ArrayList temp = new ArrayList();
            for (int i = 0; i < room.Length; ++i)
                if (Array.IndexOf(room, room[i]) >= i)
                    temp.Add(room[i]);
            string[,] matrix = new string[temp.Count, 7];
            for (int i = 0; i < matrix.GetLength(0); ++i)
                matrix[i, 0] = temp[i].ToString();
            for (int i = 0; i < timetable.Length; ++i)
            {
                int row = temp.IndexOf(room[i]);
                int col = timetable.GeneAt(i).Weekday - 1;
                matrix[row, col] = timetable.GeneAt(i).Subject;
            }
            for (int i = 0; i < matrix.GetLength(0); ++i)
                dt.Rows.Add(matrix[i, 0], matrix[i, 1], matrix[i, 2], matrix[i, 3], matrix[i, 4], matrix[i, 5], matrix[i, 6]);
            return dt;
        }

        private void btnLamMoi3_Click(object sender, EventArgs e)
        {
            if (xacnhan1)
                btnXacNhan.PerformClick();
            if (xacnhan2)
                btnXacNhan2.PerformClick();
            btnLamMoi.PerformClick();
            btnLamMoi2.PerformClick();
            while (dgvThoiKhoaBieu.RowCount > 0)
                dgvThoiKhoaBieu.Rows.Remove(dgvThoiKhoaBieu.Rows[0]);
        }

        private void dgvThoiKhoaBieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = dgvThoiKhoaBieu.CurrentCell.ColumnIndex;
            int row = dgvThoiKhoaBieu.CurrentCell.RowIndex;
            if (col == 0)
                return;
            string subject = dgvThoiKhoaBieu.CurrentCell.Value.ToString();
            if (string.IsNullOrEmpty(subject))
            {
                tbMonHoc.Clear();
                tbPhong.Clear();
                tbTinChi.Clear();
                tbThu.Clear();
                tbTiet.Clear();
                tbThoiGian.Clear();
                tbGiangVien.Clear();
                tbCoSo.Clear();
                return;
            }
            tbMonHoc.Text = subject;
            tbPhong.Text = dgvThoiKhoaBieu.Rows[row].Cells[0].Value.ToString();
            tbTinChi.Text = (new Random()).Next(2, 5).ToString();
            tbThu.Text = (col + 1).ToString();
            Time classTime = timetable.GetClassTime(subject);
            int start = classTime.StartTime;
            int finish = classTime.EndTime;
            tbTiet.Text = start.ToString() + '-' + finish.ToString();
            tbThoiGian.Text = dgvLichRanh.Rows[start - 1].Cells[0].Value.ToString().Substring(0, 5) + "-"
                + dgvLichRanh.Rows[finish - 1].Cells[0].Value.ToString().Substring(6, 5);
            tbGiangVien.Text = bll.Lecture(subject);
            tbCoSo.Text = "Trường ĐH SPKT TPHCM";
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
