
<%@ Page Title="Trang chủ" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="TrangChu.aspx.cs"
    Inherits="BTLWeb.TrangChu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CSS/TrangChu.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="hero">
        <div class="hero-content">
            <span class="hero-label">QUIZ ONLINE</span>

            <h1>Chào mừng đến với Website Thi Trắc Nghiệm</h1>

            <p>
                Học tập, luyện tập và kiểm tra kiến thức mọi lúc,
                mọi nơi với hệ thống thi trắc nghiệm trực tuyến.
            </p>

            <div class="hero-buttons">
                <a href="DanhSachDeThi.aspx" class="btn-primary">
                    Khám phá đề thi
                </a>

                <asp:PlaceHolder ID="phChuaDangNhap"
                    runat="server">

                    <a href="DangKy.aspx" class="btn-secondary">
                        Đăng ký ngay
                    </a>

                </asp:PlaceHolder>
            </div>
        </div>
    </section>

    
<section class="features">
    <h2>Những chức năng nổi bật</h2>

    <div class="feature-list">

        <!-- KHO ĐỀ THI -->
        <a href="DanhSachDeThi.aspx" class="feature-card">
            <div class="feature-icon">📚</div>
            <h3>Kho đề thi</h3>
            <p>
                Cung cấp nhiều đề thi thuộc các môn học
                và chủ đề khác nhau.
            </p>
        </a>

        <!-- THI TRỰC TUYẾN -->
        <a href="LamBaiThi.aspx" class="feature-card">
            <div class="feature-icon">⏱️</div>
            <h3>Thi trực tuyến</h3>
            <p>
                Làm bài thi với đồng hồ đếm ngược
                và tự động nộp bài khi hết giờ.
            </p>
        </a>

        <!-- KẾT QUẢ THI -->
        <a href="KetQuaThi.aspx" class="feature-card">
            <div class="feature-icon">📊</div>
            <h3>Kết quả tức thì</h3>
            <p>
                Xem điểm số, số câu đúng, số câu sai
                ngay sau khi hoàn thành bài thi.
            </p>
        </a>

        <!-- LỊCH SỬ THI -->
        <a href="LichSuThi.aspx" class="feature-card">
            <div class="feature-icon">📈</div>
            <h3>Lịch sử thi</h3>
            <p>
                Theo dõi kết quả các lần thi
                để đánh giá quá trình học tập.
            </p>
        </a>

    </div>
</section>


    <section class="home-bottom">
        <h2>Bắt đầu kiểm tra kiến thức của bạn!</h2>
        <p>Chọn một đề thi phù hợp và thử sức ngay hôm nay.</p>

        <a href="DanhSachDeThi.aspx" class="btn-primary">
            Xem danh sách đề thi
        </a>
    </section>

</asp:Content>
