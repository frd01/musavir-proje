let banka_islem_tur = ""
let is_data_update = false

function banka_cari_fis_ac(item) {

    banka_islem_tur = item.IslemTuru

    //const tarih = new Date(parseInt(item.Tarih.substr(6)))

    let tutar = 0

    if (item.Yatan != null) tutar = item.Yatan

    if (item.Cekilen != null) tutar = item.Cekilen

    const tx_cari_banka_kodu = $("#tx_cari_banka_kodu").data("kendoTextBox")
    tx_cari_banka_kodu.value(item.BankaKodu)
    tx_cari_banka_kodu.enable(false)

    $("#tx_cari_cari_kodu").data("kendoTextBox").value(item.CariKodu)
    const tx_cari_fis_no = $("#tx_cari_fis_no").data("kendoTextBox")
    tx_cari_fis_no.value(item.FisNo)
    tx_cari_fis_no.enable(false)

    const tx_cari_islem_tutari = $("#tx_cari_islem_tutari").data("kendoNumericTextBox")
    tx_cari_islem_tutari.value(tutar)
    tx_cari_islem_tutari.enable(false)

    const tx_cari_aciklama = $("#tx_cari_aciklama").data("kendoTextArea")
    tx_cari_aciklama.value(item.TabloAciklama)
    tx_cari_aciklama.enable(false)

    const cm_cari_bankalar = $("#cm_cari_bankalar").data("kendoComboBox")
    cm_cari_bankalar.enable(false)
    cm_cari_bankalar.value(item.BankaAdi)

    const cm_cari_cariler = $("#cm_cari_cariler").data("kendoComboBox")
    cm_cari_cariler.value(item.CariUnvan)
    cm_cari_cariler.enable(false)

    const dt_cari_tarih = $("#dt_cari_tarih").data("kendoDatePicker")
    dt_cari_tarih.value(item.Tarih)
    dt_cari_tarih.enable(false)

    $("#crf_table_id").val(item.TabloId)
    $("#crf_cari_id").val(item.CariId)
    $("#crf_banka_id").val(item.BankaId)

    $("#btn_cari_banka_fis_kayit").attr("disabled", true)



    const wn_havale = $("#wn_havale").data("kendoWindow")

    wn_havale.title("Banka İşlem Fişi")
    wn_havale.center()
    wn_havale.open()
}

function banka_cari_fis_duzelt(item) {

    banka_islem_tur = item.IslemTuru

    //const tarih = new Date(parseInt(item.Tarih.substr(6)))

    let tutar = 0

    if (item.Yatan != null) tutar = item.Yatan

    if (item.Cekilen != null) tutar = item.Cekilen

    const tx_cari_banka_kodu = $("#tx_cari_banka_kodu").data("kendoTextBox")
    tx_cari_banka_kodu.value(item.BankaKodu)
    tx_cari_banka_kodu.enable(false)

    $("#tx_cari_cari_kodu").data("kendoTextBox").value(item.CariKodu)
    const tx_cari_fis_no = $("#tx_cari_fis_no").data("kendoTextBox")
    tx_cari_fis_no.value(item.FisNo)
    tx_cari_fis_no.enable(true)

    const tx_cari_islem_tutari = $("#tx_cari_islem_tutari").data("kendoNumericTextBox")
    tx_cari_islem_tutari.value(tutar)
    tx_cari_islem_tutari.enable(true)

    const tx_cari_aciklama = $("#tx_cari_aciklama").data("kendoTextArea")
    tx_cari_aciklama.value(item.TabloAciklama)
    tx_cari_aciklama.enable(true)

    const cm_cari_bankalar = $("#cm_cari_bankalar").data("kendoComboBox")
    cm_cari_bankalar.enable(true)
    cm_cari_bankalar.value(item.BankaAdi)

    const cm_cari_cariler = $("#cm_cari_cariler").data("kendoComboBox")
    cm_cari_cariler.value(item.CariUnvan)
    cm_cari_cariler.enable(true)

    const dt_cari_tarih = $("#dt_cari_tarih").data("kendoDatePicker")
    dt_cari_tarih.value(item.Tarih)
    dt_cari_tarih.enable(true)

    $("#crf_table_id").val(item.TabloId)
    $("#crf_cari_id").val(item.CariId)
    $("#crf_banka_id").val(item.BankaId)

    $("#btn_cari_banka_fis_kayit").attr("disabled", false)



    const wn_havale = $("#wn_havale").data("kendoWindow")

    wn_havale.title("Banka İşlem Fişi")
    wn_havale.center()
    wn_havale.open()
}

function banka_kasa_fis_ac(item) {

    banka_islem_tur = item.IslemTuru

    //const tarih = new Date(parseInt(item.Tarih.substr(6)))

    let tutar = 0

    if (item.Yatan != null) tutar = item.Yatan

    if (item.Cekilen != null) tutar = item.Cekilen

    const tx_banka_banka_kodu = $("#tx_banka_banka_kodu").data("kendoTextBox")
    tx_banka_banka_kodu.value(item.BankaKodu)
    tx_banka_banka_kodu.enable(false)

    const tx_banka_cari_kodu = $("#tx_banka_cari_kodu").data("kendoTextBox")
    tx_banka_cari_kodu.value(item.KasaKodu)
    tx_banka_cari_kodu.enable(false)

    const tx_banka_fis_no = $("#tx_banka_fis_no").data("kendoTextBox")
    tx_banka_fis_no.value(item.FisNo)
    tx_banka_fis_no.enable(false)

    const tx_banka_islem_tutari = $("#tx_banka_islem_tutari").data("kendoNumericTextBox")
    tx_banka_islem_tutari.value(tutar)
    tx_banka_islem_tutari.enable(false)

    const tx_banka_aciklama = $("#tx_banka_aciklama").data("kendoTextArea")
    tx_banka_aciklama.value(item.TabloAciklama)
    tx_banka_aciklama.enable(false)

    const cm_banka_bankalar = $("#cm_banka_bankalar").data("kendoComboBox")
    cm_banka_bankalar.value(item.BankaAdi)
    cm_banka_bankalar.enable(false)

    const cm_banka_cariler = $("#cm_banka_cariler").data("kendoComboBox")
    cm_banka_cariler.value(item.KasaAdi)
    cm_banka_cariler.enable(false)

    const dt_banka_tarih = $("#dt_banka_tarih").data("kendoDatePicker")
    dt_banka_tarih.value(item.Tarih)
    dt_banka_tarih.enable(false)

    $("#btn_banka_banka_fis_kayit").attr("disabled", true)

    $("#ksf_table_id").val(item.TabloId)
    $("#ksf_kasa_id").val(item.KasaId)
    $("#ksf_banka_id").val(item.BankaId)


    const wn_kasa = $("#wn_kasa").data("kendoWindow")

    wn_kasa.title("Banka İşlem Fişi")
    wn_kasa.center()
    wn_kasa.open()
}

function banka_kasa_fis_duzelt(item) {

    banka_islem_tur = item.IslemTuru

    //const tarih = new Date(parseInt(item.Tarih.substr(6)))

    let tutar = 0

    if (item.Yatan != null) tutar = item.Yatan

    if (item.Cekilen != null) tutar = item.Cekilen

    const tx_banka_banka_kodu = $("#tx_banka_banka_kodu").data("kendoTextBox")
    tx_banka_banka_kodu.value(item.BankaKodu)
    tx_banka_banka_kodu.enable(false)

    const tx_banka_cari_kodu = $("#tx_banka_cari_kodu").data("kendoTextBox")
    tx_banka_cari_kodu.value(item.KasaKodu)
    tx_banka_cari_kodu.enable(true)

    const tx_banka_fis_no = $("#tx_banka_fis_no").data("kendoTextBox")
    tx_banka_fis_no.value(item.FisNo)
    tx_banka_fis_no.enable(true)

    const tx_banka_islem_tutari = $("#tx_banka_islem_tutari").data("kendoNumericTextBox")
    tx_banka_islem_tutari.value(tutar)
    tx_banka_islem_tutari.enable(true)

    const tx_banka_aciklama = $("#tx_banka_aciklama").data("kendoTextArea")
    tx_banka_aciklama.value(item.TabloAciklama)
    tx_banka_aciklama.enable(true)

    const cm_banka_bankalar = $("#cm_banka_bankalar").data("kendoComboBox")
    cm_banka_bankalar.value(item.BankaAdi)
    cm_banka_bankalar.enable(true)

    const cm_banka_cariler = $("#cm_banka_cariler").data("kendoComboBox")
    cm_banka_cariler.value(item.KasaAdi)
    cm_banka_cariler.enable(true)

    const dt_banka_tarih = $("#dt_banka_tarih").data("kendoDatePicker")
    dt_banka_tarih.value(item.Tarih)
    dt_banka_tarih.enable(false)

    $("#btn_banka_banka_fis_kayit").attr("disabled", false)

    $("#ksf_table_id").val(item.TabloId)
    $("#ksf_kasa_id").val(item.KasaId)
    $("#ksf_banka_id").val(item.BankaId)


    const wn_kasa = $("#wn_kasa").data("kendoWindow")

    wn_kasa.title("Banka İşlem Fişi")
    wn_kasa.center()
    wn_kasa.open()
}

