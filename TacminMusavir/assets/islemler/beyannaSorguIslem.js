function get_bilgi_model(modul_adi, evrak_sayisi, aciklama) {

    const model = {

        Id: null,
        ModulAdi: modul_adi,
        EvrakSayisi: evrak_sayisi,
        Aciklama: aciklama,
        Tarih: null
    }

    return model
}

async function haftalik_beyanname_indirme(baseUrl) {

    let model_liste = []

    let model = null

    const token_model = await $.ajax({

        url: baseUrl + "/beyanname/GetToken",
        method : "GET"
    })

    if (token_model.messages != null) {

        model = get_bilgi_model("E-Beyanname", 0, token_model.messages[0].text)
        model_liste.push(model)
        return model_liste
    }

    const result = await $.ajax({

        url: baseUrl + "/beyanname/GibBeyannameSorgula",
        method: "POST",
        dataType: "Json",
        data: { client_veri: null, token: token_model.Token, is_aktif_cari : false }
    })

    if (result.MukellefSayisi > 0) {

        model = get_bilgi_model("E-Beyanname", result.MukellefSayisi, "Yeni Müşteri Kaydı")
        model_liste.push({ ...model})
    }
    if (result.BildirgeSayisi > 0) {

        model = get_bilgi_model("E-Beyanname", result.BildirgeSayisi, "İndirilen Bildirgeler")
        model_liste.push({ ...model })
    }
    if (result.BeyannameSayisi > 0) {

        model = get_bilgi_model("E-Beyanname", result.BeyannameSayisi, "İndirilen Beyannameler")
        model_liste.push({ ...model })
    }

    return model_liste

}