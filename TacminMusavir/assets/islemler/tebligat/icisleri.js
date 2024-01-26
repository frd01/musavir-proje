async function icisleriTebligatIslem(token,firmaId) {

    const tebligat_listesi = await icisleriTebligatListesi(token,firmaId)
    

    return tebligat_listesi
}

async function icisleriTebligatListesi(token,firmaId) {
    

    // "cmd=mukellefService_tebligatlariListele&callid=8deb48bc4a5ad-31&pageName=RG_DIGER_TEBLIGAT&token=&jp=%7B%22
    // kurumKodu % 22 % 3A % 2224312041 % 22 % 2C % 22ilkListelemeMi % 22 % 3Atrue % 7D"

    const data_str = "cmd=mukellefService_tebligatlariListele&callid=6fcb0e888718f-31&pageName=RG_DIGER_TEBLIGAT&token=" + token
        + "&jp=%7B%22kurumKodu%22%3A%2224312041%22%2C%22ilkListelemeMi%22%3Atrue%7D"

    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data : data_str
    })

    const tebligat_listesi = []

    let maliye_icisleri_data_list = []

    try {
        maliye_icisleri_data_list = response.data.data
    } catch  {

        return tebligat_listesi
    }

   

    for (let i = 0; i < maliye_icisleri_data_list.length; i++) {

        const item = maliye_icisleri_data_list[i]

        const model = {

            ...item,
            firmaId: firmaId
        }

        tebligat_listesi.push(model)
    }

    if (tebligat_listesi == null) return []

    return tebligat_listesi

}

async function ic_isleri_ekleri_al(token, oid, kurumKodu) {

    const data_str = "cmd=mukellefService_ketsisTebligEkListele&callid=6fcb0e888718f-33&pageName=RG_DIGER_TEBLIGAT&token=" + token
        + "&jp=%7B%22kurumKodu%22%3A%22" + kurumKodu + "%22%2C%22tebligOid%22%3A%22" + oid + "%22%7D"

    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data : data_str
    })

    var ust_yazi_link = "https://ivd.gib.gov.tr/tvd_server/tebligat?token=" + token
        + "&cmd=downloadKetsisPdf&kurumKodu=" + kurumKodu
        + "&oid=" + oid
        + "&islemTuru=teblig&tur=download&bucketTuru=TEBLIG&dosyaAdi=" + oid + ".pdf&";

    const result = response.data

    const item_model = {

        ustYaziPdfBaseStr: await getFileBase64(ust_yazi_link),
        dosyalar : []
    }

    if (result == null) return []

   

    for (let i = 0; i < result.length; i++) {

        const item = result[i]

        //const ekFile = await dosyaIndir(link)
        const dosya_uzanti = item.gonderilenDosyaAdi.split(".")[1]

        var ek_link = "https://ivd.gib.gov.tr/tvd_server/tebligat?token=" + token
            + "&cmd=downloadKetsisPdf&kurumKodu=" + kurumKodu
            + "&oid=" + item.belgeOid
            + "&islemTuru=ek&tur=download&bucketTuru=TEBLIG&dosyaAdi=" + item.DosyaAdi + "&";

        const model = {

            Id: null,
            TebligatId: null,
            DosyaAdi: item.gonderilenDosyaAdi,
            Tur: dosya_uzanti,
            MimeType: "image/" + dosya_uzanti,
            Icerik: null,
            DosyaBaseStr: await getFileBase64(ek_link),
            belgeOid: item.oid
        }

        item_model.dosyalar.push(model)
    }

    return item_model
}

async function dosyaIndir(url) {

    const response = await fetch(url, {
        method: "GET",
        mode: "cors",
        headers: {
            'Access-Control-Allow-Origin': '*'
        }
    })

    return response.blob()
}

