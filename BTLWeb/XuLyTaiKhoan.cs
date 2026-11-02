
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace BTLWeb
{
    public class XuLyTaiKhoan
    {
        private const int SoLanLap = 100000;
        private const int DoDaiSalt = 16;
        private const int DoDaiHash = 32;

        // Tạo mã băm mật khẩu
        private static byte[] TaoHash(string matKhau, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                matKhau, salt, SoLanLap))
            {
                return pbkdf2.GetBytes(DoDaiHash);
            }
        }

        // Kiểm tra dữ liệu có chứa ký tự phân cách hay không
        private static bool DuLieuHopLe(string value)
        {
            return !string.IsNullOrWhiteSpace(value)
                && !value.Contains("|")
                && !value.Contains("\r")
                && !value.Contains("\n");
        }

        // Đăng ký tài khoản
        public static bool DangKy(
            string path,
            string username,
            string password,
            string role,
            string fullName,
            string email)
        {
            try
            {
                username = username == null ? "" : username.Trim();
                fullName = fullName == null ? "" : fullName.Trim();
                email = email == null ? "" : email.Trim();

                if (!DuLieuHopLe(username) ||
                    !DuLieuHopLe(password) ||
                    !DuLieuHopLe(fullName) ||
                    !DuLieuHopLe(email))
                    return false;

                if (role != "Student" &&
                    role != "Teacher" &&
                    role != "Admin")
                    return false;

                string folder = Path.GetDirectoryName(path);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                if (!File.Exists(path))
                    File.WriteAllText(path, "");

                string[] danhSach = File.ReadAllLines(path);

                foreach (string dong in danhSach)
                {
                    string[] p = dong.Split('|');

                    if (p.Length == 6 &&
                        p[0].Equals(username,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }

                byte[] salt = new byte[DoDaiSalt];

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                byte[] hash = TaoHash(password, salt);

                string dongMoi =
                    username + "|" +
                    Convert.ToBase64String(salt) + "|" +
                    Convert.ToBase64String(hash) + "|" +
                    role + "|" +
                    fullName + "|" +
                    email;

                File.AppendAllText(path, dongMoi + Environment.NewLine);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Đăng nhập và lấy thông tin tài khoản
        public static bool DangNhap(
            string path,
            string username,
            string password,
            out string hoTen,
            out string email,
            out string vaiTro)
        {
            hoTen = "";
            email = "";
            vaiTro = "";

            try
            {
                if (!File.Exists(path))
                    return false;

                username = username == null ? "" : username.Trim();

                foreach (string dong in File.ReadAllLines(path))
                {
                    string[] p = dong.Split('|');

                    if (p.Length != 6)
                        continue;

                    if (!p[0].Equals(username,
                        StringComparison.OrdinalIgnoreCase))
                        continue;

                    byte[] salt = Convert.FromBase64String(p[1]);
                    byte[] hashLuu = Convert.FromBase64String(p[2]);
                    byte[] hashNhap = TaoHash(password, salt);

                    if (hashLuu.Length != hashNhap.Length)
                        return false;

                    bool dung = true;

                    for (int i = 0; i < hashLuu.Length; i++)
                    {
                        if (hashLuu[i] != hashNhap[i])
                            dung = false;
                    }

                    if (!dung)
                        return false;

                    hoTen = p[4];
                    email = p[5];
                    vaiTro = p[3];

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        // Cập nhật họ tên và email
        public static bool CapNhatThongTin(
            string path,
            string username,
            string hoTenMoi,
            string emailMoi)
        {
            try
            {
                if (!File.Exists(path) ||
                    !DuLieuHopLe(hoTenMoi) ||
                    !DuLieuHopLe(emailMoi))
                    return false;

                string[] danhSach = File.ReadAllLines(path);
                bool timThay = false;

                for (int i = 0; i < danhSach.Length; i++)
                {
                    string[] p = danhSach[i].Split('|');

                    if (p.Length == 6 &&
                        p[0].Equals(username,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        p[4] = hoTenMoi.Trim();
                        p[5] = emailMoi.Trim();

                        danhSach[i] = string.Join("|", p);
                        timThay = true;
                        break;
                    }
                }

                if (!timThay)
                    return false;

                File.WriteAllLines(path, danhSach);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Đổi mật khẩu
        public static bool DoiMatKhau(
            string path,
            string username,
            string matKhauCu,
            string matKhauMoi)
        {
            try
            {
                if (!File.Exists(path) ||
                    !DuLieuHopLe(matKhauMoi))
                    return false;

                string[] danhSach = File.ReadAllLines(path);
                bool timThay = false;

                for (int i = 0; i < danhSach.Length; i++)
                {
                    string[] p = danhSach[i].Split('|');

                    if (p.Length != 6 ||
                        !p[0].Equals(username,
                            StringComparison.OrdinalIgnoreCase))
                        continue;

                    byte[] saltCu = Convert.FromBase64String(p[1]);
                    byte[] hashCu = Convert.FromBase64String(p[2]);
                    byte[] hashNhap = TaoHash(matKhauCu, saltCu);

                    if (hashCu.Length != hashNhap.Length)
                        return false;

                    bool dung = true;

                    for (int j = 0; j < hashCu.Length; j++)
                    {
                        if (hashCu[j] != hashNhap[j])
                            dung = false;
                    }

                    if (!dung)
                        return false;

                    byte[] saltMoi = new byte[DoDaiSalt];

                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(saltMoi);
                    }

                    byte[] hashMoi = TaoHash(matKhauMoi, saltMoi);

                    p[1] = Convert.ToBase64String(saltMoi);
                    p[2] = Convert.ToBase64String(hashMoi);

                    danhSach[i] = string.Join("|", p);
                    timThay = true;
                    break;
                }

                if (!timThay)
                    return false;

                File.WriteAllLines(path, danhSach);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
