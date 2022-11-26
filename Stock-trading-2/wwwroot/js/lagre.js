function validerOgLagreAksje() {
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const aksjenavnOK = validerAksjeNavn($("#aksjeNavn").val());
    const prisOK = validerPris($("#pris").val());
    const antallOK = validerAntall($("#antall").val());
    if (fornavnOK && etternavnOK && aksjenavnOK && prisOK && antallOK) {
        lagreAksje();
    }
}

function lagreAksje() {
    const aksje = {
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        aksjeNavn: $("#aksjeNavn").val(),
        pris: $("#pris").val(),
        antall: $("#antall").val()
    }

    const url = "Aksje/Lagre";
    $.post(url, aksje, function () {
        window.location.href = 'index.html';
    })
        .fail(function () {
            $("#feil").html("Feil på server - prøv igjen senere");
        });
}