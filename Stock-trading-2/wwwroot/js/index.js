$(function () {
    hentAlleAksjene();
});

function hentAlleAksjene() {
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
            "<td>" + aksje.Navn + "</td>" +
            "<td>" + aksje.Pris + "</td>" +
            "<td>" + aksje.Antall + "</td>" +
            "</tr>";
    }

    ut += "</table>";
    $("#akjene").html(ut);
}