let host_name = window.location.host;

let gib_firma_listesi = []

const sleep = (ms) => {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve()
        }, ms);
    })
}

async function sorgu_durum_kontrol() {

    const servis_url = host_name + "/main/GunlukOtoSorguDurum"

    const result = await $.ajax({

        url: servis_url,
        method: "GET",
        dataType : "Json"
    })

    return result
}

async function gib_firma_liste_olustur() {


    const servis_url = host_name + "/main/GetGibFirmaListesi"

    const result = await $.ajax({

        url: servis_url,
        method: "GET"
    })

    return result
}

async function vergi_denetim_tebligat_indir() {

    const model = {

        Id: null,
        ModulAdi: "Vergi Denetim Başkanlığı",
        EvrakSayisi: 0,
        Aciklama: "",
        Tarih: null
    }

    

    const tebligat_listesi = []

    for (let i = 0; i < gib_firma_listesi.length; i++) {

        const item = gib_firma_listesi[i]

        const token_islem = await inter_aktif_giris(item.GibKodu, item.GibSifre, item.GibParola)

        if (token_islem.islemDurum == true) {


            const mly_data_list = await vergiDenetimTebligatIslem(token_islem.token, item.Id)

            if (mly_data_list.length>0) {

                const data_list = await $.ajax({

                    url: host_name + "/vergidenetim/MaliyeListeKayitKontrolIslem",
                    method: "POST",
                    dataType: "Json",
                    data: { clientList: mly_data_list }
                })

                if (data_list.length>0) {

                    for (let d = 0; d < data_list.length; d++) {

                        const data_item = data_list[d]

                        const detay = await vergi_denetim_tebligat_detay(token_islem.token, data_item.oid)

                        const model = {

                            ...data_item,
                            ustYaziBaseStr: detay.ustYaziBaseStr,
                            ekListesi: detay.eklistesi
                        }

                        tebligat_listesi.push(model)

                        await Promise.all([sleep(1000)])
                        
                    }
                }
            }

            await inter_aktif_cikis(token_islem.model)

            await Promise.all([sleep(2000)])
        }

        
    }

    if (tebligat_listesi.length>0) {

        const data_tebligat_liste = await $.ajax({

            url: host_name + "/vergidenetim/TebligatKayitIslem",
            method: "POST",
            dataType: "Json",
            data: { clientList: tebligat_listesi }
        })

        model.EvrakSayisi = data_tebligat_liste.length
    }

    return model
}

async function ticaret_bak_teblgat_indir() {

    const model = {

        Id: null,
        ModulAdi: "Ticaret Bakanlığı",
        EvrakSayisi: 0,
        Aciklama: "",
        Tarih: null
    }

    const tebligat_listesi = []

    for (let i = 0; i < gib_firma_listesi.length; i++) {

        const item = gib_firma_listesi[i]

        const token_islem = await inter_aktif_giris(item.GibKodu, item.GibSifre, item.GibParola)

        if (token_islem.islemDurum == true) {

            const mly_data_list = await ticaretBakTebligatIslem(token_islem.token, item.Id)

            if (mly_data_list.length>0) {

                const data_list = await $.ajax({

                    url: host_name + "/ticarettebligat/MaliteListeKayitKontrolIslem",
                    method: "POST",
                    dataType: "Json",
                    data: { clientList: mly_data_list }
                })

                if (data_list.length>0) {

                    for (var d = 0; d < data_list.length; d++) {

                        const data_item = data_list[d]

                        const detay = await ticaret_tebligat_detay(token_islem.token, data_item.oid)

                        const model = {

                            ...data_item,
                            ustYaziBaseStr: detay.ustYaziBaseStr,
                            ekListesi: detay.ekListesi
                        }

                        tebligat_listesi.push(model)

                        await Promise.all([sleep(1000)])
                    }
                }
            }

            await inter_aktif_cikis(token_islem.model)

            await Promise.all([sleep(2000)])
        }

        
    }

    if (tebligat_listesi.length>0) {

        const tebligat_data_list = await $.ajax({

            url: host_name + "/ticarettebligat/TebligatKayitIslem",
            method: "POST",
            dataType: "Json",
            data: { clientList: tebligat_listesi }
        })

        model.EvrakSayisi = tebligat_data_list.length
    }


    return model
}

async function icisleri_tebligat_indir() {

    const model = {

        Id: null,
        ModulAdi: "İçişleri Bakanlığı",
        EvrakSayisi: 0,
        Aciklama: "",
        Tarih: null
    }

    const tebligat_listesi = []

    for (let i = 0; i < gib_firma_listesi.length; i++) {

        const item = gib_firma_listesi[i]

        const token_islem = await inter_aktif_giris(item.GibKodu, item.GibSifre, item.GibParola)

        if (token_islem.islemDurum == true) {

            const mly_data_list = await icisleriTebligatIslem(token_islem.token, item.Id)

            if (mly_data_list.length>0) {

                const data_list = await $.ajax({

                    url: host_name + "/icisleritebligat/MaliyeListeKayitKontrolIslem",
                    method: "POST",
                    dataType: "Json",
                    data: { clientListe: mly_data_list }
                })

                if (data_list.length>0) {

                    for (let d = 0; d < data_list.length; d++) {

                        const data_item = data_list[d]

                        const detay = await ic_isleri_ekleri_al(token_islem.token, data_item.oid, data_item.kurumKodu)

                        const model = {

                            ...data_item,
                            ekListesi: detay.dosyalar,
                            ustYaziPdfBaseStr: detay.ustYaziPdfBaseStr

                        }
                        tebligat_listesi.push(model)

                        await Promise.all([sleep(1000)])

                    }
                }
            }

            await inter_aktif_cikis(token_islem.model)

            await Promise.all([sleep(2000)])
        }

        
    }

    if (tebligat_listesi.length>0) {


        const data_tebligat_listesi = await $.ajax({

            url: host_name + "/icisleritebligat/TebligatKayitIslem",
            method: "POST",
            dataType: "Json",
            data: { clientListe: tebligat_listesi }
        })

        model.EvrakSayisi = data_tebligat_listesi.length
    }


    return model
}

async function gelir_idaresi_tebligat_indir() {

    const model = {

        Id: null,
        ModulAdi: "Gelir İdaresi Başkanlığı",
        EvrakSayisi: 0,
        Aciklama: "",
        Tarih : null
    }

    const tebligat_listesi = []

    for (let i = 0; i < gib_firma_listesi.length; i++) {


        const item = gib_firma_listesi[i]

        const token_islem = await inter_aktif_giris(item.GibKodu, item.GibSifre, item.GibParola)

        if (token_islem.islemDurum == false) {

            console.log("Token İşlem : ", token_islem)
        }

        if (token_islem.islemDurum == true) {

            const mly_data_list = await gelirIdaresiTebligatIslem(token_islem.token, item.Id)


            if (mly_data_list.length >0) {

                const data_list = await $.ajax({

                    url: host_name + "/gelirtebligat/ListeKayitKontrolIslem",
                    method: "POST",
                    dataType: "Json",
                    data: { clientList: mly_data_list }
                })

                if (data_list.length > 0) {

                    for (let d = 0; d < data_list.length; d++) {

                        const data_item = data_list[d]

                        const dosya_link = "https://ivd.gib.gov.tr/tvd_server/islem/pdf/?oid=" + data_item.belgeOid
                            + "&dosyaismi=" + data_item.belgeOid
                            + "&uzanti=imz&tur=teblig&belgeTuru=tebligat&cmd=getTebligatPdf&islem=view&token=" + token_islem.token
                            + "&userId=26105500";

                        const model = {

                            ...data_item,
                            ustYaziBase64Str: await getFileBase64(dosya_link)
                        }

                        console.log("Gelir Tebligat Detay : ", model)

                        await Promise.all([sleep(1000)])

                        tebligat_listesi.push(model)
                    }

                }
            }

            await inter_aktif_cikis(token_islem.token)

            await Promise.all([sleep(1000)])
           
        }

    }

    if (tebligat_listesi.length>0) {


        const data_tebligat_listesi = await $.ajax({

            url: host_name + "/gelirtebligat/TebligatKayit",
            method: "POST",
            dataType: "Json",
            data: { tebligatListesi: tebligat_listesi }
        })

        model.EvrakSayisi = data_tebligat_listesi.length

    }



    return model
}

async function get_gelen_fatura_listesi(token, ilkTarih, sonTarih) {

    const data_str = "cmd=EFaturaIslemleri_eFaturaGoruntuleSorgula&callid=074d4fa89e435-25&pageName=P_EFATURA&token=" + token
        + "&jp=%7B%22faturaTarihBas%22%3A%22" + ilkTarih
        + "%22%2C%22textBox%22%3A%22%22%2C%22faturaTarihSon%22%3A%22" + sonTarih
        + "%22%7D"

    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data: data_str
    })

    let result = response.data

    let liste = []

    try {

        liste = result.FATURASONUC

    } catch {
        console.log("hata : ", response)
        return []
    }

    if (typeof (liste) == "string") {

        console.log(result.FATURASONUC)
        liste = []
    }
   

    return liste
}

async function oto_gelen_fataura_indir(){

    const model = {

        Id: null,
        ModulAdi: "Gelen Faturalar",
        EvrakSayisi: 0,
        Aciklama: "",
        Tarih: null
    }

    const fatura_listesi = []

    let toplam_kayit = 0

    for (let i = 0; i < gib_firma_listesi.length; i++) {

        const item = gib_firma_listesi[i]

        const token_islem = await inter_aktif_giris(item.GibKodu, item.GibSifre, item.GibParola)

        if (token_islem.islemDurum == true) {


            const tarih_listesi = await $.ajax({

                url: host_name + "/gelenfatura/TarihAralikListesiSonBirAylik",
                method: "POST",
                dataType: "Json",
                data: { ilkTarih: null, sonTarih: null }
            })

            for (let t = 0; t < tarih_listesi.length; t++) {

                const item = tarih_listesi[t]

                const fatura_list = await get_gelen_fatura_listesi(token_islem.token, item.BaslangicTarih, item.BitisTarih)

                console.log("fatura_list : ", fatura_list)

                

                if (fatura_list.length > 0) {

                    for (let f = 0; f < fatura_list.length; f++) {

                        toplam_kayit += fatura_list.length
                        const fatura = fatura_list[f]

                        fatura.toplam = fatura.toplam.replace(".", ",")
                        fatura.odenecek = fatura.odenecek.replace(".", ",")
                        fatura.vergi = fatura.vergi.replace(".", ",")

                        fatura_listesi.push(fatura)
                    }
                }
            }

            await inter_aktif_cikis(token_islem.token)

            await Promise.all([sleep(5000)])
        }

    }

    console.log("Toplam " + toplam_kayit + " Adet Fatura Listesi Alındı.")

    if (fatura_listesi.length > 0) {

        const data_fatura_list = await $.ajax({

            url: host_name + "/gelenfatura/FaturOtoKayit",
            method: "POST",
            dataType: "Json",
            data: { clientList: fatura_listesi}
        })

        model.EvrakSayisi = data_fatura_list.length
    }

    return model
}

function islem_sonuc_ilet(mesaj) {

    socket.emit("oto_indirme_islem", mesaj)
}


async function maliye_evrak_indirme_islem() {


}

async function gunluk_evrak_indirme_islem() {

    if (host_name.indexOf("localhost:62457") != -1) {

        host_name = "http://" + host_name
    }
    else {

        host_name = "https://" + host_name
    }

    //const is_sorgu = await sorgu_durum_kontrol()

    if (true) {
        islem_sonuc_ilet("İşlemler başladı")
        //let islem_durum = window.localStorage.getItem("oto_islem")

        

        gib_firma_listesi = await gib_firma_liste_olustur()

        let sorgu_ozet_list = []

        const gelir_model = await gelir_idaresi_tebligat_indir()

        console.log("Gelir İdaresi İnen Evrak : ", gelir_model.EvrakSayisi)

        /*const icisleri_model = await icisleri_tebligat_indir()

        console.log("İçişleri Bakanlığı İnen Evrak : ", icisleri_model.EvrakSayisi)

        const ticaret_model = await ticaret_bak_teblgat_indir()

        console.log("Ticaret Bakanlığı : ", ticaret_model.EvrakSayisi)

        const vergi_denetim_model = await vergi_denetim_tebligat_indir()

        console.log("Vergi Denetim Başkanlığı : ", vergi_denetim_model.EvrakSayisi)

        const gelen_fatura_model = await oto_gelen_fataura_indir()*/

        //const beyanname_islem_models = await haftalik_beyanname_indirme(host_name)

        sorgu_ozet_list.push(gelir_model)
        /*sorgu_ozet_list.push(icisleri_model)
        sorgu_ozet_list.push(ticaret_model)
        sorgu_ozet_list.push(vergi_denetim_model)
        sorgu_ozet_list.push(gelen_fatura_model)*/

       /* for (var i = 0; i < beyanname_islem_models.length; i++) {

            const item = beyanname_islem_models[i]

            sorgu_ozet_list.push(item)
        }*/

        const result = await $.ajax({

            url: host_name + "/main/GunlukOtoSorguOzetKayit",
            method: "POST",
            dataType: "Json",
            data: { clientList: sorgu_ozet_list}
        })

        islem_sonuc_ilet("İşlemler sona erdi.")
       
    }
    /*if (is_sorgu == true) {

        islem_sonuc_ilet("Bugün İndirme İşlemi Tamamlandı")
    }*/

   
}