"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
document.getElementById("sendButton").disabled = true;
connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);

    li.textContent = `${user} says ${message}`;

    // Add a line break before appending another message
    var br = document.createElement("br");
    li.appendChild(br);

    // Save the message to localStorage
    var messages = JSON.parse(localStorage.getItem('messages')) || [];
    messages.push(li.textContent);
    localStorage.setItem('messages', JSON.stringify(messages));
});

// Retrieve and display the messages from localStorage on page load
var messages = JSON.parse(localStorage.getItem('messages')) || [];
messages.forEach(function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = message;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("messageInput").value = "";
    event.preventDefault();
});








//"use strict";
//var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
//document.getElementById("sendButton").disabled = true;
//connection.on("ReceiveMessage", function (user, message) {
//    var li = document.createElement("li");
//    document.getElementById("messagesList").appendChild(li);

//    li.textContent = `${user} says ${message}`;
//});
//connection.start().then(function () {
//    document.getElementById("sendButton").disabled = false;
//}).catch(function (err) {
//    return console.error(err.toString());
//});
//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    document.getElementById("messageInput").value = "";
//    event.preventDefault();
//});
