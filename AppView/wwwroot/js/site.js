// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var AllTheThings = document.getElementsByClassName("TotalTally");
var Total = 0

if (AllTheThings.length > 0) {
    for (var i = 0; i < AllTheThings.length; i++) {
        Total += Number(AllTheThings[i].textContent)
    }
    document.getElementById("TotalPrice").innerText = Total;
}