
var tuDongNopBai = false;

document.addEventListener("DOMContentLoaded", function () {

    var dongHo = document.getElementById("demNguoc");

    if (!dongHo) {
        return;
    }

    var giayConLai = parseInt(
        dongHo.getAttribute("data-seconds") || "0",
        10
    );

    if (isNaN(giayConLai) || giayConLai < 0) {
        giayConLai = 0;
    }

    function capNhatDongHo() {

        var phut = Math.floor(giayConLai / 60);
        var giay = giayConLai % 60;

        dongHo.textContent =
            String(phut).padStart(2, "0") + ":" +
            String(giay).padStart(2, "0");

        if (giayConLai <= 0) {

            tuDongNopBai = true;

            alert("Đã hết thời gian làm bài! Hệ thống sẽ tự động nộp bài.");

            var nutNop = document.querySelector(
                'input[id$="btnNopBai"]'
            );

            if (nutNop) {
                nutNop.click();
            }

            return false;
        }

        giayConLai--;

        return true;
    }

    // Hiển thị thời gian ngay khi mở trang
    if (!capNhatDongHo()) {
        return;
    }

    var boDem = setInterval(function () {

        if (!capNhatDongHo()) {
            clearInterval(boDem);
        }

    }, 1000);

});

// Xác nhận khi người dùng chủ động nộp bài
function xacNhanNopBai() {

    if (tuDongNopBai) {
        return true;
    }

    return confirm("Bạn có chắc chắn muốn nộp bài thi không?");
}
