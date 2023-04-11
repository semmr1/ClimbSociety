"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub", {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
}).build();

function getValue(id) {
    return document.getElementById(id).value;
}

function getUser() {
    return getValue("userInput");
}

function getGroup() {
    return getValue("groupInput");
}

function getMessage() {
    return getValue("messageInput");
}

function getConnId() {
    return getValue("connIdInput");
}

function addMessage(message) {
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("messagesList").appendChild(li);
}

//var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build
//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    addMessage(`${user} says ${message}`);
});

connection.on("JoinedOrLeft", function (message) {
    addMessage(`${message}`);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("SendMessageToGroup", getGroup(), getMessage(), getUser()).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("joinButton").addEventListener("click", function (event) {
    connection.invoke("AddToGroup", getGroup(), getUser()).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});