$(document).ready(function () {
    $(document).on('blur', '.g-date', function () {
        var date = this.value;
        var dateout = "";
        if (date.length == 10 && date.indexOf('/') > -1) {
            dateout = date;
        }
        else if (date.length >= 4 && date.length <= 10) {
            if (date.length == 4) {
                dateout = "01/01/" + date;
            }
            else if (date.length >= 6 && date.length <= 8) {
                if (date.indexOf('/') == -1) {
                    dateout = date.substring(0, 2) + "/" + date.substring(2, 4) + "/" + format_year(date.substring(4));
                }
                else if (date.indexOf('/') > -1) {
                    var datea = date.split('/');
                    dateout = format_date(datea[0]) + "/" + format_date(datea[1]) + "/" + format_year(datea[2]);
                }
            }
        }
        this.value = dateout;
    })
})

function format_date(date) {
    if (date.length == 1)
        date = '0' + date;
    return date;
}

function format_year(year) {
    if (year.length == 2) {
        if (year > 30)
            year = '19' + year;
        else
            year = '20' + year;
    }
    return year;
}