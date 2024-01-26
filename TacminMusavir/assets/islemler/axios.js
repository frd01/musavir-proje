const axios_custom = axios.create({
    
    headers: {
       
        'Access-Control-Allow-Origin': '*',
    },
    timeout: 1500000
})


async function GetDosyaIndir(url) {

    const response = await axios_custom.get(url, { responseType: "blob" })
}


