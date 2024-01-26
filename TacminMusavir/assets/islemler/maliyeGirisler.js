async function inter_aktif_giris(GibKodu, GibSifre, GibParola) {


    const model = {

        token: "",
        mesaj: "",
        islemDurum: true
    }

    const data_str = "assoscmd=multilogin&rtype=json&userid=" + GibKodu + "&sifre=" + GibSifre + "&parola=" + GibParola + "&dk=A66NH1&controlCaptcha=false&imageID=35q6aylb6ef2th00&";
    const servisUrl = "https://ivd.gib.gov.tr/tvd_server/assos-login"

    const result = await $.ajax({

        url: servisUrl,
        method: "POST",
        data: data_str
    })

    if (!result.hasOwnProperty("token")) {


        model.mesaj = result.messages[0].text
        model.islemDurum = false
        return model
    }

    model.token = result.token

    return model
}

async function inter_aktif_cikis(token) {

    const data_str = "callid=39a2685feaf1c-23&cmd=kullaniciBilgileriService_logout&jp={}&pageName=PG_MAIN_DYNAMIC&token=" + token;

    const url = "https://ivd.gib.gov.tr/tvd_server/dispatch"

    const response = await $.ajax({

        url: url,
        method: "POST",
        data: data_str
    })
}

 function inter_aktif_portal_giris_url(token) {

     const url = "https://ivd.gib.gov.tr/tvd_side/index.jsp?token=" + token

     return url
}

async function e_arsiv_giris(GibKodu, GibSifre, GibParola) {

    

    const model = {

        token: "",
        mesaj: "",
        islemDurum: true
    }    

    const data_str = "assoscmd=anologin&rtype=json&userid=" + GibKodu + "&sifre=" + GibSifre + "&sifre2=" + GibSifre + "&parola=1&";

    const servisUrl = "https://earsivportal.efatura.gov.tr/earsiv-services/assos-login"

    const result = await $.ajax({

        url: servisUrl,
        method: "POST",
        data: data_str
    })

    if (!result.hasOwnProperty("token")) {


        model.mesaj = result.messages[0].text
        model.islemDurum = false
        return model
    }

    model.token = result.token

    return model


}

async function e_arsiv_cikis(token) {

    const data_str = "assoscmd=logout&rtype=json&token=" + token + "&";

    const servisUrl = "https://earsivportal.efatura.gov.tr/earsiv-services/assos-login"

    const response = await $.ajax({

        url: servisUrl,
        method: "POST",
        data: data_str
    })


}

function e_arsiv_portal_giris_url(token) {

    const url = "https://earsivportal.efatura.gov.tr/index.jsp?token=" + token

    return url
}

async function internet_vergi_dairesi_giris(musavirBilgi) {

    const servis_url = "http://localhost:5501/node-api/internet-vergi-dairesi-token-al"

    const res_token = await axios.post(servis_url, { user_data: musavirBilgi })

    const token_islem = res_token.data

    return token_islem
}

async function digital_vergi_dairesi_giris(musavirBilgi) {

    const servis_url = "http://localhost:5501/node-api/digital-vd-token-islem"

    const res_token = await axios.post(servis_url, { user_data: musavirBilgi })

    const token_islem = res_token.data

    console.log("token işlem :", token_islem)

    return token_islem
}

async function digital_mukellef_vergi_dairesi_giris(gibKodu, gibSifre, gibParola) {

    const servis_url = "http://localhost:5501/node-api/digital-vd-token-islem"

    const req_model = {

        id: 1,
        is_kullanici: true,
        gib_kod: gibKodu,
        gib_sifre: gibSifre,
        gib_parola: gibParola,
        token: "",
        hata_mesaj : ""
    }

    const res_token = await axios.post(servis_url, { user_data: req_model })

    const token_islem = res_token.data

    console.log("token işlem :", token_islem)

    return token_islem
}

async function digital_vergi_dairesi_cikis_yap(token, tcNo, vergiNo) {

    const url = "https://dijital.gib.gov.tr/apigateway/auth/tdvd/logout"

    const req_model = {

        token: token,
        vkn: vergiNo,
        tckn: tcNo
    }

    const response = await axios.post(url, req_model)

    console.log(response.data)
}

async function internet_vergi_dairesi_cikis(token) {

    const data_str = "callid=2bfdfe802b70e-7&cmd=kullaniciBilgileriService_logout&jp={}&token=" + token;

    const url = "https://intvrg.gib.gov.tr/intvrg_server/dispatch"

    const response = await axios.post(url, data_str)

    console.log(response.data)
}