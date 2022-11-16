$(function () {
    hentAlleAksjer();
});

function hentAlleAksjer() {
    $.get("aksje/hentAlle", function (aksjer) {
        formaterAksjer(aksjer)
    });
}

function formaterAksjer(aksjer) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Navn</th><th>Pris</th><th>Antall</th>" +
        "</tr>";
    for (let aksje of aksjer) {
        ut += "<tr>" +
            "<td>" + aksje.navn + "</td>" +
            "<td>" + aksje.pris + "</td>" +
            "<td>" + aksje.antall + "</td>" +
            "</tr>";
    }

    ut += "</table>";
    $("#aksjene").html(ut);
}