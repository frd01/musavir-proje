async function gelirIdaresiTebligatIslem(token,firmaId) {

   
    const tebligatListe = await gelirIdaresiTebligatListesi(token,firmaId)

   
    return tebligatListe

}

async function gelirIdaresiTebligatListesi(token,firmaId) {

    const data_str = "cmd=mukellefService_zarflariGetir&callid=4df914e8fb608-33&pageName=undefined&token=" + token
        + "&jp=%7B%22parametreler%22%3A%7B%22duzBirim%22%3A%22%22%2C%22duzBasZaman%22%3A%22%22%2C%22duzBitZaman%22%3A%22%22%2C%22tebligBasZaman%22%3A%22%22%2C%22tebligBitZaman%22%3A%22%22%2C%22durum%22%3A%5B%22400%22%2C%22500%22%2C%22600%22%5D%2C%22belgeTuru%22%3A%22%22%2C%22belgeNo%22%3A%22%22%2C%22paging%22%3Atrue%7D%2C%22respKeyParam%22%3A%22list%22%2C%22pv%22%3A%7B%22start%22%3A0%2C%22limit%22%3A%2225%22%2C%22sorters%22%3A%5B%5D%7D%7D"

    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data: data_str
        
    })

    console.log("Gelir Tebligat Liste : ", response.data)

    let tebligat_listesi = []

    try {
        tebligat_listesi = response.data.list
    } catch  {

        return tebligat_listesi
    }
    

    

    if (tebligat_listesi == null) return []

    const liste = []

    for (let i = 0; i < tebligat_listesi.length; i++) {

        const item = tebligat_listesi[i]

        const listModel = {

            ...item,
            firmaId: firmaId,
            ekListesi: [],
            dosyaAdi: "",
            Dosyalar: []
        }

        liste.push(listModel)
    }

    return liste

}

async function gelirIdaresiTebligat_detay(token, zarfOid) {

    const data_str = "cmd=mukellefService_zarfDetayGetir&callid=4df914e8fb608-45&pageName=RG_GIB_TEBLIGAT&token=" + token + "&jp=%7B%22zarfOid%22%3A%22" + zarfOid + "%22%7D"

    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data: data_str
    })

    console.log("response : ", response)

    const result = response.data

    const model = {

        dosyaAdi: "",
        ekListesi : []
    }

    if (result == null) {

        model;
    }

    model.dosyaAdi = result.dosyaAdi
    model.ekListesi = []

    

    if (result.ekList != null) model.ekListesi = result.ekList

    return model
}



