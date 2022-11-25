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