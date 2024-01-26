async function ticaretBakTebligatIslem(token,firmaId) {

   
    const tebligatListe = await ticaretBakTebligatListesi(token,firmaId)


    return tebligatListe

}

async function ticaretBakTebligatListesi(token,firmaId) {

    const data_str = "cmd=mukellefService_tebligatlariListele&callid=112556a2970d8-31&pageName=RG_DIGER_TEBLIGAT&token=" + token
        + "&jp=%7B%22kurumKodu%22%3A%2224308261%22%2C%22ilkListelemeMi%22%3Atrue%7D"

    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data : data_str
    })

    console.log("response : ", response)


    const tebligat_listesi = []

    const tic_data_list = []

    try {

        tic_data_list = response.data.data
    } catch  {

        return tebligat_listesi
    }


    for (var i = 0; i < tic_data_list.length; i++) {

        const item = tic_data_list[i]

        const model = {

            ...item,
            firmaId: firmaId
        }

        tebligat_listesi.push(model)
    }

    if (tebligat_listesi == null) return []

    return tebligat_listesi

}

async function ticaret_tebligat_detay(token, tebligOid) {

    const data_str = "cmd=mukellefService_ketsisTebligEkListele&callid=112556a2970d8-33&pageName=RG_DIGER_TEBLIGAT&token=" + token
        + "&jp=%7B%22kurumKodu%22%3A%2214467936%22%2C%22tebligOid%22%3A%22" + tebligOid
        +"%22%7D"

    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data: data_str
    })

    var ust_yazi_link = "https://ivd.gib.gov.tr/tvd_server/tebligat/pdf/?cmd=getKetsisPdf&tur=view&=&oid=" + tebligOid
        + "&kurumKodu=14467936&bucketTuru=TEBLIG&islemTuru=teblig&token=" + token;


    const model = {

        ustYaziBaseStr: await getFileBase64(ust_yazi_link),
        ekListesi : []
    }
   

    const result = response.data

    for (var i = 0; i < result.length; i++) {

        const item = result[i]

        var ek_link = "https://ivd.gib.gov.tr/tvd_server/tebligat?token=" + token
            + "&cmd=downloadKetsisPdf&kurumKodu=14467936&oid=" + item.oid
            + "&islemTuru=ek&tur=download&bucketTuru=TEBLIG&dosyaAdi=" + item.oid + ".pdf&";

        const ek_model = {

            ...item,
            ekBaseStr: await getFileBase64(ek_link)
        }

        model.ekListesi.push(ek_model)
    }

    return model
}



