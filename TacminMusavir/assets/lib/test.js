import { io } from "https://cdn.socket.io/4.4.1/socket.io.esm.min.js";

const socket = io("http://localhost:5510");

function deneme() {

    const model = {

        adi: "deneme",
        soyadi : "deneme"
    }

    socket.emit("test_socket_method", model);
}

