
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTLWeb
{
    public partial class LamBaiThi : System.Web.UI.Page
    {
        // Thông tin câu hỏi
        [Serializable]
        public class CauHoiThi
        {
            public string MaCauHoi { get; set; }
            public string NoiDung { get; set; }
            public string DapAnA { get; set; }
            public string DapAnB { get; set; }
            public string DapAnC { get; set; }
            public string DapAnD { get; set; }
            public string DapAnDung { get; set; }
        }

        private const string KEY_CAU_HOI = "DanhSachCauHoi";
        private const string KEY_MA_DE = "MaDeThiDangLam";
        private const string KEY_BAT_DAU = "ThoiDiemBatDau";
        private const string KEY_THOI_GIAN = "ThoiGianThi";
        private const string KEY_DA_NOP = "MaDeDaNop";

        private string MaDe
        {
            get
            {
                return Session[KEY_MA_DE] == null
                    ? ""
                    : Session[KEY_MA_DE].ToString();
            }
        }

        // Đọc dữ liệu và tạo các câu hỏi từ sớm để
        // ASP.NET có thể nhận lại đáp án khi người dùng nộp bài.
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("DangNhap.aspx");
                return;
            }

            string maDeTrenUrl = Request.QueryString["MaDe"];

            if (string.IsNullOrWhiteSpace(maDeTrenUrl))
            {
                Response.Redirect("DanhSachDeThi.aspx");
                return;
            }

            // Nếu chưa có lượt thi hoặc chọn đề khác thì tạo lượt mới.
            if (Session[KEY_MA_DE] == null ||
                Session[KEY_MA_DE].ToString() != maDeTrenUrl)
            {
                KhoiTaoBaiThi(maDeTrenUrl);
            }

            List<CauHoiThi> ds =
                Session[KEY_CAU_HOI] as List<CauHoiThi>;

            if (ds == null || ds.Count == 0)
            {
                Response.Redirect("DanhSachDeThi.aspx");
                return;
            }

            // Bind ở Page_Init để đáp án được khôi phục khi postback.
            rptCauHoi.DataSource = ds;
            rptCauHoi.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[KEY_DA_NOP] != null &&
                Session[KEY_DA_NOP].ToString() == MaDe)
            {
                Response.Redirect("DanhSachDeThi.aspx");
                return;
            }

            if (!IsPostBack)
            {
                HienThiThongTinDeThi();
            }

            CapNhatDongHo();
        }

        // Khởi tạo bài thi
        private void KhoiTaoBaiThi(string maDe)
        {
            string pathDeThi =
                Server.MapPath("~/App_Data/DeThi.txt");

            string pathCauHoi =
                Server.MapPath("~/App_Data/CauHoi.txt");

            if (!File.Exists(pathDeThi) ||
                !File.Exists(pathCauHoi))
            {
                return;
            }

            string[] thongTinDe = null;

            // Tìm đề thi đang được mở
            foreach (string dong in File.ReadAllLines(pathDeThi))
            {
                string[] p = dong.Split('|');

                if (p.Length == 7 &&
                    p[0].Trim() == maDe &&
                    p[6].Trim() == "1")
                {
                    thongTinDe = p;
                    break;
                }
            }

            if (thongTinDe == null)
                return;

            int thoiGian;
            int soCau;

            if (!int.TryParse(thongTinDe[3], out thoiGian) ||
                !int.TryParse(thongTinDe[4], out soCau) ||
                thoiGian <= 0 || soCau <= 0)
            {
                return;
            }

            List<CauHoiThi> ds = new List<CauHoiThi>();

            // Đọc câu hỏi từ file CauHoi.txt
            foreach (string dong in File.ReadAllLines(pathCauHoi))
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] p = dong.Split('|');

                if (p.Length != 8)
                    continue;

                if (p[1].Trim() != maDe)
                    continue;

                string dapAnDung = p[7].Trim().ToUpper();

                if (dapAnDung != "A" &&
                    dapAnDung != "B" &&
                    dapAnDung != "C" &&
                    dapAnDung != "D")
                {
                    continue;
                }

                CauHoiThi cauHoi = new CauHoiThi();

                cauHoi.MaCauHoi = p[0].Trim();
                cauHoi.NoiDung = p[2].Trim();
                cauHoi.DapAnA = p[3].Trim();
                cauHoi.DapAnB = p[4].Trim();
                cauHoi.DapAnC = p[5].Trim();
                cauHoi.DapAnD = p[6].Trim();
                cauHoi.DapAnDung = dapAnDung;

                ds.Add(cauHoi);

                if (ds.Count >= soCau)
                    break;
            }

            if (ds.Count == 0)
                return;

            Session[KEY_CAU_HOI] = ds;
            Session[KEY_MA_DE] = maDe;
            Session[KEY_BAT_DAU] = DateTime.Now;
            Session[KEY_THOI_GIAN] = thoiGian;

            Session.Remove(KEY_DA_NOP);
        }

        // Hiển thị thông tin đề thi
        private void HienThiThongTinDeThi()
        {
            string path =
                Server.MapPath("~/App_Data/DeThi.txt");

            if (!File.Exists(path))
                return;

            foreach (string dong in File.ReadAllLines(path))
            {
                string[] p = dong.Split('|');

                if (p.Length == 7 && p[0].Trim() == MaDe)
                {
                    lblTenDe.Text = Server.HtmlEncode(p[1]);
                    lblMaDe.Text = Server.HtmlEncode(p[0]);
                    lblThoiGian.Text = p[3] + " phút";

                    List<CauHoiThi> ds =
                        Session[KEY_CAU_HOI] as List<CauHoiThi>;

                    lblTongCau.Text =
                        ds == null ? "0" : ds.Count.ToString();

                    break;
                }
            }
        }

        // Tạo đáp án A, B, C, D cho từng câu
        protected void rptCauHoi_ItemDataBound(
            object sender,
            RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item &&
                e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            CauHoiThi cauHoi = e.Item.DataItem as CauHoiThi;

            if (cauHoi == null)
                return;

            RadioButtonList rbl =
                e.Item.FindControl("rblDapAn") as RadioButtonList;

            if (rbl == null)
                return;

            rbl.Items.Clear();

            rbl.Items.Add(
                new ListItem("A. " + cauHoi.DapAnA, "A"));

            rbl.Items.Add(
                new ListItem("B. " + cauHoi.DapAnB, "B"));

            rbl.Items.Add(
                new ListItem("C. " + cauHoi.DapAnC, "C"));

            rbl.Items.Add(
                new ListItem("D. " + cauHoi.DapAnD, "D"));
        }

        // Hiển thị đồng hồ đếm ngược
        private void CapNhatDongHo()
        {
            if (Session[KEY_BAT_DAU] == null ||
                Session[KEY_THOI_GIAN] == null)
            {
                return;
            }

            DateTime batDau =
                (DateTime)Session[KEY_BAT_DAU];

            int soPhut =
                Convert.ToInt32(Session[KEY_THOI_GIAN]);

            double soGiay =
                soPhut * 60 -
                (DateTime.Now - batDau).TotalSeconds;

            int giayConLai =
                Math.Max(0, (int)Math.Ceiling(soGiay));

            demNguoc.Text = string.Format(
                "{0:00}:{1:00}",
                giayConLai / 60,
                giayConLai % 60
            );

            demNguoc.Attributes["data-seconds"] =
                giayConLai.ToString();
        }

        // Xử lý khi người dùng nộp bài
        protected void btnNopBai_Click(object sender, EventArgs e)
        {
            if (Session[KEY_DA_NOP] != null &&
                Session[KEY_DA_NOP].ToString() == MaDe)
            {
                Response.Redirect("DanhSachDeThi.aspx");
                return;
            }

            List<CauHoiThi> ds =
                Session[KEY_CAU_HOI] as List<CauHoiThi>;

            if (ds == null || ds.Count == 0)
            {
                Response.Redirect("DanhSachDeThi.aspx");
                return;
            }

            if (Session[KEY_BAT_DAU] == null ||
                Session[KEY_THOI_GIAN] == null)
            {
                Response.Redirect("DanhSachDeThi.aspx");
                return;
            }

            DateTime batDau =
                (DateTime)Session[KEY_BAT_DAU];

            int thoiGian =
                Convert.ToInt32(Session[KEY_THOI_GIAN]);

            bool hetGio =
                (DateTime.Now - batDau).TotalSeconds
                >= thoiGian * 60;

            int tongCau = ds.Count;
            int daTraLoi = 0;
            int cauDung = 0;

            // Kiểm tra đáp án từng câu
            for (int i = 0; i < rptCauHoi.Items.Count; i++)
            {
                RadioButtonList rbl =
                    rptCauHoi.Items[i].FindControl("rblDapAn")
                    as RadioButtonList;

                if (rbl == null)
                    continue;

                string dapAnChon = rbl.SelectedValue;

                if (dapAnChon == "A" ||
                    dapAnChon == "B" ||
                    dapAnChon == "C" ||
                    dapAnChon == "D")
                {
                    daTraLoi++;

                    if (dapAnChon == ds[i].DapAnDung)
                    {
                        cauDung++;
                    }
                }
            }

            // Tính điểm theo thang 10
            double diem = 0;

            if (tongCau > 0)
            {
                diem = Math.Round(
                    (double)cauDung / tongCau * 10,
                    2
                );
            }

            string maKetQua =
                "KQ" + DateTime.Now.ToString(
                    "yyyyMMddHHmmssfff"
                );

            string username =
                Session["TenDangNhap"].ToString();

            string dongKetQua = string.Join("|",
                new string[]
                {
                    maKetQua,
                    username,
                    MaDe,
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    tongCau.ToString(),
                    daTraLoi.ToString(),
                    cauDung.ToString(),
                    diem.ToString(CultureInfo.InvariantCulture)
                }
            );

            string path =
                Server.MapPath("~/App_Data/KetQuaThi.txt");

            try
            {
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "");
                }

                File.AppendAllText(
                    path,
                    dongKetQua + Environment.NewLine
                );

                // Lưu thông tin để trang kết quả sử dụng
                Session["MaKetQua"] = maKetQua;
                Session["MaDeKetQua"] = MaDe;
                Session["TongCau"] = tongCau;
                Session["DaTraLoi"] = daTraLoi;
                Session["CauDung"] = cauDung;
                Session["Diem"] = diem;
                Session["ThoiGianHetGio"] = hetGio;

                // Đánh dấu đã nộp để tránh nộp lại
                Session[KEY_DA_NOP] = MaDe;

                Session.Remove(KEY_CAU_HOI);
                Session.Remove(KEY_MA_DE);
                Session.Remove(KEY_BAT_DAU);
                Session.Remove(KEY_THOI_GIAN);

                Response.Redirect(
                    "KetQuaThi.aspx",
                    false
                );

                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                lblThongBao.Text =
                    "Không thể lưu kết quả thi. " +
                    "Vui lòng kiểm tra quyền ghi file App_Data.";
            }
        }
    }
}
