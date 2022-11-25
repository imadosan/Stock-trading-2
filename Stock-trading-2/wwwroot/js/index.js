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
        "<th>Fornavn</th><th>Etternavn</th><th>Aksje</th><th>Markedspris</th><th>Antall</th><th></th><th></th>" +
        "</tr>";
    for (let aksje of aksjer) {
        ut += "<tr>" +
            "<td>" + aksje.fornavn + "</td>" +
            "<td>" + aksje.etternavn + "</td>" +
            "<td>" + aksje.aksjenavn + "</td>" +
            "<td>" + aksje.pris + "</td>" +
            "<td>" + aksje.antall + "</td>" +
            "<td> <a class='btn btn-primary' href='endre.html?id="+aksje.id+"'>Endre</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettAksje("+aksje.id+")'>Selg</button></td>" +
            "</tr>";
    }

    ut += "</table>";
    $("#aksjene").html(ut);
}

function slettAksje(id) {
    const url = "Aksje/Slett?id=" + id;
    $.get(url, function (OK) {
        if (OK) {
            window.location.href = "index.html"
        }
        else {
            $("#feil").html("Feil i DB - Prøv igjen senere!")
        }
    })
}