$(function () {
    const id = window.location.search.substring(1);
    const url = "Aksje/HentEn?" + id;
    $.get(url, function (aksje) {
        $("#id").val(aksje.id);
        $("#navn").val(aksje.navn);
        $("#pris").val(aksje.pris);
        $("#antall").val(aksje.antall);
    });
});

function endreAksje() {
    const aksje = {
        id: $("#id").val(),
        navn: $("#navn").val(),
        pris: $("#pris").val(),
        antall: $("#antall").val()
    }

    $.post("Aksje/Endre", aksje, function (OK) {
        if (OK) {
            window.location.href = "index.html";
        }
        else {
            $("#feil").html("Feil i DB - Prøv igjen senere!")
        }
    })
}