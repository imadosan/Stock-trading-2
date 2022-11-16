function lagreAksje() {
    const aksje = {
        navn: $("#navn").val(),
        pris: $("#pris").val(),
        antall: $("#antall").val()
    }

    const url = "Aksje/Lagre";
    $.post(url, aksje, function (OK) {
        if (OK) {
            window.location.href = 'index.html'
        }
        else {
            $("#feil").html("Feil i DB - Prøv igjen senere!")
        }
    })
}