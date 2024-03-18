$(document).ready(function () {
    // Yarını set etmek için buton click
    $('#setTomorrowButton').click(function () {

        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);  // Yarın tarihini hesapla

        var year = tomorrow.getFullYear();
        var month = (tomorrow.getMonth() + 1).toString().padStart(2, "0"); // Ayı iki haneli olarak al
        var day = tomorrow.getDate().toString().padStart(2, "0"); // Günü iki haneli olarak al
        var tomorrowDate = `${year}-${month}-${day}`;

        document.getElementById("departureDate").value = tomorrowDate
    });

    $('#setTodayButton').click(function () {
       // Bugünü set etmek için buton click
        var currentDate = new Date();
        currentDate.setDate(currentDate.getDate()); // Bugün tarihini hesapla

        var year = currentDate.getFullYear();
        var month = (currentDate.getMonth() + 1).toString().padStart(2, "0"); // Ayı iki haneli olarak al
        var day = currentDate.getDate().toString().padStart(2, "0"); // Günü iki haneli olarak al
        var todayDate = `${year}-${month}-${day}`;

        document.getElementById("departureDate").value = todayDate;
    });
});
function swapLocations() {
    var origion = document.getElementById("originSelect");
    var destination = document.getElementById("destinationSelect");

    var oldinex = destination.selectedIndex;

    destination.selectedIndex = origion.selectedIndex;
    origion.selectedIndex = oldinex;
}