async function tokenAl(gib_kod, gib_sifre) {

    const data_str = "assoscmd=anologin&rtype=json&userid=" + gib_kod + "&sifre=" + gib_sifre + "&sifre2=" + gib_sifre + "&parola=1&"

    const url = "https://earsivportal.efatura.gov.tr/earsiv-services/assos-login"

    const response = await axios.post(url, data_str)

    const result = response.data

    const model = {

        token: "",
        mesaj: "",
        isHata: false
    }

    if (result.hasOwnProperty("token")) {

        model.token = result.token
    }

    if (!result.hasOwnProperty("token")) {

        model.isHata = true
        model.mesaj = result.messages[0].text
    }

    return model
}

async function cikisYap(token) {

    const data_str = "assoscmd=logout&rtype=json&token=" + token + "&"
    const url = "https://earsivportal.efatura.gov.tr/earsiv-services/dispatch"

    const response = await axios.post(url, data_str)

    console.log("Çıkış Yapıldı : ", response.data)
}