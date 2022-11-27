$(function () {
    const id = window.location.search.substring(1);
    const url = "Aksje/HentEn?" + id;
    $.get(url, function (aksje) {
        $("#id").val(aksje.id);
        $("#fornavn").val(aksje.fornavn);
        $("#etternavn").val(aksje.etternavn);
        $("#aksjeNavn").val(aksje.aksjenavn);
        $("#pris").val(aksje.pris);
        $("#antall").val(aksje.antall);
    });
});

function validerOgEndreAksje() {
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const aksjenavnOK = validerAksjeNavn($("#aksjeNavn").val());
    const prisOK = validerPris($("#pris").val());
    const antallOK = validerAntall($("#antall").val());
    if (fornavnOK && etternavnOK && aksjenavnOK && prisOK && antallOK) {
        endreAksje();
    }
}

function endreAksje() {
    const aksje = {
        id: $("#id").val(),
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        aksjeNavn: $("#aksjeNavn").val(),
        pris: $("#pris").val(),
        antall: $("#antall").val()
    };

    $.post("Aksje/Endre", aksje, function () {
        window.location.href = 'index.html';
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = "loggInn.html";
        }
        else {
            $("#feil").html("Feil på server - Prøv igjen senere!");
        }
    })
}