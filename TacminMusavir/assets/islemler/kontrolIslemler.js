function masaustu_servis_kontrol() {

    $.ajax({
        url: "http://localhost:5510/",
        method: "GET",
        error: function () {

            mesaj("Masaüstü Servisi Yükleyiniz.", "error")
        }
    })
}