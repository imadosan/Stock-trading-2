function validerFornavn(fornavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(fornavn);
    if (!ok) {
        $("#feilFornavn").html("Fornavnet må bestå av 2 til 20 bokstaver!");
        return false;
    } else {
        $("#feilFornavn").html("");
        return true
    }
}

function validerEtternavn(etternavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(etternavn);
    if (!ok) {
        $("#feilEtternavn").html("Etternavnet må bestå av 2 til 20 bokstaver!");
        return false;
    } else {
        $("#feilEtternavn").html("");
        return true
    }
}

function validerAksjeNavn(aksjenavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(aksjenavn);
    if (!ok) {
        $("#feilAksjeNavn").html("Aksje navnet må bestå av 2 til 20 bokstaver!");
        return false;
    } else {
        $("#feilAksjeNavn").html("");
        return true
    }
}

function validerPris(pris) {
    const regexp = /^[0-9.,]{1,20}$/;
    const ok = regexp.test(pris);
    if (!ok) {
        $("#feilPris").html("Pris må være et tall!");
        return false;
    } else {
        $("#feilPris").html("");
        return true
    }
}

function validerAntall(antall) {
    const regexp = /^[0-9]{1,20}$/;
    const ok = regexp.test(antall);
    if (!ok) {
        $("#feilAntall").html("Antall må være et tall!");
        return false;
    } else {
        $("#feilAntall").html("");
        return true
    }
}

function validerBrukernavn(brukernavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(brukernavn);
    if (!ok) {
        $("#feilBrukernavn").html("Brukernavnet må bestå av 2 til 20 bokstaver!");
        return false;
    } else {
        $("#feilBrukernavn").html("");
        return true
    }
}

function validerPassord(passord) {
    const regexp = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$/;
    const ok = regexp.test(passord);
    if (!ok) {
        $("#feilPassord").html("Passordet må bestå minumum 6 tegn, minst en bokstav og et tall!");
        return false;
    } else {
        $("#feilPassord").html("");
        return true
    }
}