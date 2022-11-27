function loggUt() {
    $.get("Aksje/LoggUt", function () {
        window.location.href = 'loggInn.html';
    });
}