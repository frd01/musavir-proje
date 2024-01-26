

function mailGonder() {

    Email.send({
        Host: "smtp.gmail.com",
        Username: "inan79@gmail.com",
        Password: "Fer602757$",
        To: 'inan79@gmail.com',
        From: "inan79@gmail.com",
        Subject: "This is the subject",
        Body: "And this is the body"
    }).then(
        message => alert(message)
    );
}