function isNumberorAlphabet(evt) {
    const charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode < 47 || (charCode > 57 && charCode < 65) || (charCode > 90 && charCode < 97) || charCode > 122)
        return false;
    return true;
}

function isNumber(evt) {
    const charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode < 48 || charCode > 57 )
        return false;
    return true;
}

function isAlphabet(evt) {
    const charCode = (evt.which) ? evt.which : event.keyCode;
    if ( charCode < 65 || (charCode > 90 && charCode < 97) || charCode > 122)
        return false;
    return true;
}