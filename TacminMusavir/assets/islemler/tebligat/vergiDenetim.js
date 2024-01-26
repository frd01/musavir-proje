
async function vergiDenetimTebligatIslem(token,firmaId) {

   
    const tebligatListe = await vergiDenetimTebligatListesi(token,firmaId)

    return tebligatListe

}

async function vergiDenetimTebligatListesi(token,firmaId) {

    const data_str = "cmd=mukellefService_tebligatlariListele&callid=9747f1ac9a99c-31&pageName=RG_DIGER_TEBLIGAT&token=" + token
        + "&jp=%7B%22kurumKodu%22%3A%2246622688%22%2C%22ilkListelemeMi%22%3Atrue%7D"
    
    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data: data_str
    })

    const tebligat_listesi = []

    let vergi_data_list = []

    try {
        vergi_data_list = response.data.data
    } catch  {

        return tebligat_listesi
    }


    for (var i = 0; i < vergi_data_list.length; i++) {

        const item = vergi_data_list[i]

        const model = {

            ...item,
            firmaId: firmaId
        }

        tebligat_listesi.push(model)
    }

    if (tebligat_listesi == null) return []
    

    return tebligat_listesi

}

async function vergi_denetim_tebligat_detay(token, oid) {

    //const data_str = "cmd=mukellefService_zarfDetayGetir&callid=4df914e8fb608-45&pageName=RG_GIB_TEBLIGAT&token=" + token + "&jp=%7B%22zarfOid%22%3A%22" + zarfOid + "%22%7D"

    const data_str = "cmd=mukellefService_ketsisTebligEkListele&callid=b354bb33bd396-33&pageName=RG_DIGER_TEBLIGAT&token=" + token
        + "&jp=%7B%22kurumKodu%22%3A%2246622688%22%2C%22tebligOid%22%3A%22"
        + oid + "%22%7D"

       // 0tl9xvhztd1a9u

    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data: data_str
    })

    

    const result = response.data

    const yazi_link = "https://ivd.gib.gov.tr/tvd_server/tebligat/pdf/?cmd=getKetsisPdf&tur=view&=&oid=" + oid
        + "&kurumKodu=46622688&bucketTuru=TEBLIG&islemTuru=teblig&token="
        + token

    const item_model = {

        ustYaziBaseStr: await getFileBase64(yazi_link),
        eklistesi : []
    }

    for (let i = 0; i < result.length; i++) {

        const item = result[i]

        const ek_link = "https://ivd.gib.gov.tr/tvd_server/tebligat/pdf/?cmd=getKetsisPdf&tur=view&=&oid=" + item.oid
            + "&kurumKodu=46622688&bucketTuru=TEBLIG&islemTuru=ek&token="
            + token

        const model = {

            ...item,
            ekBaseStr: await getFileBase64(ek_link)
        }

        item_model.eklistesi.push(model)
    }
    

    return item_model
}



