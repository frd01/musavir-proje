

async function faturaIslem(sorguModel) {

    const model = await tokenAl(sorguModel.gibKodu, sorguModel.gibSifre)

   

    const fatura_model = {
        ...model,
        faturaListesi : []
    }

    if ((await model).isHata == true) {

        return fatura_model
    }

    fatura_model.faturaListesi = await faturaListe(model.token, sorguModel)

    

    return fatura_model
}

async function faturaListe(token,item) {

    const data_str = "cmd=EARSIV_PORTAL_TASLAKLARI_GETIR&callid=3d3d323de5341-7&pageName=RG_BASITTASLAKLAR&token=" + token
        + "&jp=%7B%22baslangic%22%3A%22" + item.bastGun
        + "%2F" + item.bastAy
        + "%2F" + item.bastYil
        + "%22%2C%22bitis%22%3A%22" + item.btGun
        + "%2F" + item.btAy
        + "%2F" + item.btYil
        + "%22%2C%22hangiTip%22%3A%225000%2F30000%22%7D"

    const url = "https://earsivportal.efatura.gov.tr/earsiv-services/dispatch"

    const response = await axios.post(url, data_str)

    const result = response.data

    console.log("java script data : ", result)

    let liste = []

    if (result.data != null) {

        for (let i = 0; i < result.data.length; i++) { 

            const item = result.data[i]

            const detayModel = await html_fatura(token, item.ettn)

            console.log("detay model : ", detayModel)

            const veriModel = {

                ...detayModel.veriData,
                aliciUnvanAdSoyad: item.aliciUnvanAdSoyad,
                belgeTuru: item.belgeTuru,
                onayDurumu: item.onayDurumu,
                htmltext: detayModel.html_text,
                belgeNumarasi: item.belgeNumarasi,
                ettn: item.ettn,
                Unvan : item.unvan
            }

            const newModel = getModelWrite(veriModel)

            liste.push(newModel)
        }
    }

    return liste
}

async function html_fatura(token, ettnNo) {

    const data_str = "cmd=EARSIV_PORTAL_FATURA_GOSTER&callid=3d3d323de5341-8&pageName=RG_BASITTASLAKLAR&token=" + token
        + "&jp=%7B%22ettn%22%3A%22" + ettnNo + "%22%2C%22onayDurumu%22%3A%22Onayland%C4%B1%22%7D"

    const url = "https://earsivportal.efatura.gov.tr/earsiv-services/dispatch"

    const response = await axios.post(url, data_str)

    const html_data = response.data.data

    const baslangic = html_data.indexOf('id="qrvalue">')
    const bitis = html_data.indexOf('}</div>')

    let text_veri = html_data.slice(baslangic + 14, bitis + 1)

    const jsonVeri = JSON.parse(text_veri)

    

    const model = {

        html_text: html_data,
        veriData: jsonVeri
    }

    return model

}

function getModelWrite(dataModel) {

    const model = {
        firmaId : 1217,
        aliciUnvan: dataModel.aliciUnvanAdSoyad,
        aliciVergiNo: dataModel.avkntckn,
        belgeNo: dataModel.belgeNumarasi,
        belgeTuru: dataModel.belgeTuru,
        ettn: dataModel.ettn,
        htmlText: dataModel.htmltext,
        malHizmetToplam: numberFormat(dataModel.malhizmettoplam),
        odenecek: numberFormat(dataModel.odenecek),
        onayDurumu: dataModel.onayDurumu,
        paraBirimi: dataModel.parabirimi,
        senaryo: dataModel.senaryo,
        tarih: dataModel.tarih,
        tip: dataModel.tip,
        vergidahilToplam: numberFormat(dataModel.vergidahil),
        hesaplananKdv_0: null,
        hesaplananKdv_1: null,
        hesaplananKdv_8: null,
        hesaplananKdv_10: null,
        hesaplananKdv_18: null,
        hesaplananKdv_20: null,
        kdvMatrah_0: null,
        kdvMatrah_1: null,
        kdvMatrah_8: null,
        kdvMatrah_10: null,
        kdvMatrah_18: null,
        kdvMatrah_20: null,
        iskontoToplam: null,
        tevfikatKdvTutar : null
 
    }

    if (dataModel.hasOwnProperty("kdvmatrah(0)")) {

        model.kdvMatrah_0 = numberFormat(dataModel['kdvmatrah(0)'])
        model.hesaplananKdv_0 = numberFormat(dataModel["hesaplanankdv(0)"])
    }

    if (dataModel.hasOwnProperty("kdvmatrah(1)")) {

        model.kdvMatrah_1 = numberFormat(dataModel['kdvmatrah(1)'])
        model.hesaplananKdv_1 = numberFormat(dataModel["hesaplanankdv(1)"])
    }

    if (dataModel.hasOwnProperty("kdvmatrah(8)")) {

        model.kdvMatrah_8 = numberFormat(dataModel['kdvmatrah(8)'])
        model.hesaplananKdv_8 = numberFormat(dataModel["hesaplanankdv(8)"])
    }

    if (dataModel.hasOwnProperty("kdvmatrah(10)")) {

        model.kdvMatrah_10 = numberFormat(dataModel['kdvmatrah(10)'])
        model.hesaplananKdv_10 = numberFormat(dataModel["hesaplanankdv(10)"])
    }

    if (dataModel.hasOwnProperty("kdvmatrah(18)")) {

        model.kdvMatrah_18 = numberFormat(dataModel['kdvmatrah(18)'])
        model.hesaplananKdv_18 = numberFormat(dataModel["hesaplanankdv(18)"])
    }

    if (dataModel.hasOwnProperty("kdvmatrah(20)")) {

        model.kdvMatrah_20 = numberFormat(dataModel['kdvmatrah(20)'])
        model.hesaplananKdv_20 = numberFormat(dataModel["hesaplanankdv(20)"])
    }

    return model
}

function numberFormat(value) {

    if (value == null) return value

    return value.replace(".",",")
}