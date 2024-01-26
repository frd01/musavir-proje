
async function getFileBase64(link) {

    const result = await $.ajax({

        url: "http://localhost:5510/node-api/get-file-base64str",
        method: "POST",
        data: { link: link }
    })

    return result.fileStr
}