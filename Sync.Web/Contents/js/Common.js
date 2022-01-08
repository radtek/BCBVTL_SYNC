var ws_urlbase = "http://localhost:8088/SPIDService/";
var api_url = "http://192.168.1.15:8066/api";
$(document).ajaxComplete(function (event, request, settings) {
    if (request.status === 400)
        window.location.href = "/Sys/Login";
});

$(document).ajaxError(function (event, jqxhr, settings, exception) {
    if (jqxhr.status === 400) {
        window.location.href = '/Sys/Login';
    }
});

//Kiểm tra chuỗi có chứa ký tự đặc biệt, trả về kiểu bool: true/false
function containsSpecialCharacters(str) {
    var regex = /[ !@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/g;
    return regex.test(str);
}

function checkLogin(str) {
    var regex = /^[A-Za-z0-9_]{4,20}$/;
    return regex.test(str);
}

//Check password, trả về kiểu bool: true/false
function checkPassword(str) {
    var regex = /^[A-Za-z0-9!@#$%^&*()_]{6,20}$/;
    return regex.test(str);
}

//Kiểm tra định dạng Email, trả về kiểu bool: true/false
function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}

function loadIframe(url) {
    var ifr = $('<iframe/>', {
        id: 'AdminFrame',
        src: url,
        frameborder: 0,
        style: 'width:99.6%;height:700px',
        load: function () {
            $("#loadingaj").hide();
            $(this).show();
        }
    });
    console.log(ifr);
    $('#WrapIframe').html(ifr);
}

function GetPageSize() {
    var result = 0;

    $.ajax({
        url: '/Login/GetPageSize',
        method: 'GET',
        async: false,
        success: function (size) {
            result = parseInt(size);
        }
    });

    return result;
}

function GetDateYYYYMMDD(ddMMyyyy) {
    try {
        if (ddMMyyyy) {
            var date = ddMMyyyy.split('/');
            var yyyy = date[2];
            var MM = date[1];
            var dd = date[0];
            return yyyy + MM + dd;
        }
        else
            return "";
    } catch (e) {
        return ddMMyyyy;
    }
}

function GetDateDDMMYYYY(yyyyMMdd) {
    try {
        if (yyyyMMdd) {
            var yyyy = yyyyMMdd.substring(0, 4);
            var MM = yyyyMMdd.substring(4, 6);
            var dd = yyyyMMdd.substring(6, 8);
            return dd + '/' + MM + '/' + yyyy;
        }
        else
            return "";
    } catch (e) {
        return yyyyMMdd;
    }
}

function GetDateTime14(dateTime14) {
    try {
        if (dateTime14) {
            var yyyy = dateTime14.substring(0, 4);
            var MM = dateTime14.substring(4, 6);
            var dd = dateTime14.substring(6, 8);
            var hh = dateTime14.substring(8, 10);
            var mm = dateTime14.substring(10, 12);
            var ss = dateTime14.substring(12, 14);
            return dd + '/' + MM + '/' + yyyy + ' ' + hh + ':' + mm + ':' + ss;
        }
        else
            return "";
    } catch (e) {
        return dateTime14;
    }
}

function Date19ToDate10(dateTime19) {
    try {
        if (dateTime19) {
            return dateTime19.split(' ')[0];
        }
        else
            return "";
    } catch (e) {
        return dateTime19;
    }
}

//Lấy định dạng ngày tháng năm (yyyy-mm-dd) truyền lên API phân hệ trong
function GetDateTimeFormat(dateInput) {
    if (dateInput instanceof Date) {
        var date = new Date(dateInput);
        if (!isNaN(date)) {
            var dd = date.getDate();
            var mm = date.getMonth() + 1;
            var yyyy = date.getFullYear();

            if (dd < 10) {
                dd = '0' + dd;
            }

            if (mm < 10) {
                mm = '0' + mm;
            }

            return yyyy + '-' + mm + '-' + dd;
        } else if (dateInput.split('/').length > 0) {
            var arrDate = dateInput.split('/');
            return arrDate[2] + '-' + arrDate[1] + '-' + arrDate[0];
        } else {
            return "2017-01-01";
        }
    } else {
        var arrDate = dateInput.split('/');
        if (arrDate.length < 3) {
            return dateInput.slice(4, 8) + '-' + dateInput.slice(2, 4) + '-' + dateInput.slice(0, 2);
        } else {
            return arrDate[2] + '-' + arrDate[1] + '-' + arrDate[0];
        }
    }
}

function GetDateTimeSimple(dateInput) {
    if (dateInput instanceof Date) {
        var date = new Date(dateInput);
        if (!isNaN(date)) {
            var dd = date.getDate();
            var mm = date.getMonth() + 1;
            var yyyy = date.getFullYear();

            if (dd < 10) {
                dd = '0' + dd;
            }

            if (mm < 10) {
                mm = '0' + mm;
            }

            return dd + '/' + mm + '/' + yyyy;
        } else if (dateInput.split('/').length > 0) {
            var arrDate = dateInput.split('/');
            return arrDate[0] + '/' + arrDate[1] + '/' + arrDate[2];
        } else {
            return "01/01/2017";
        }
    } else {
        var arrDate = dateInput.split('/');
        if (arrDate.length >= 3) {
            return arrDate[0] + '/' + arrDate[1] + '/' + arrDate[2];
        } else
        {
            return dateInput;
        }
    }
}

function GetDateView(dateInput) {
    var date = new Date(dateInput);
    if (!isNaN(date)) {
        var dd = date.getDate();
        var mm = date.getMonth() + 1;
        var yyyy = date.getFullYear();

        if (dd < 10) {
            dd = '0' + dd;
        }

        if (mm < 10) {
            mm = '0' + mm;
        }

        return dd + '/' + mm + '/' + yyyy;
    } else if (dateInput.split('/').length > 0) {
        var arrDate = dateInput.split('/');
        return arrDate[0] + '/' + arrDate[1] + '/' + arrDate[2];
    } else {
        return "01/01/2017";
    }
}

//Lấy định dạng ngày tháng năm (dd/mm/yyyy) truyền lên datetime picker
function GetDateTimeToday() {
    var date = new Date();
    var dd = date.getDate();
    var mm = date.getMonth() + 1;
    var yyyy = date.getFullYear();

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    return dd + '/' + mm + '/' + yyyy;
}

//require numeral.min.js
function formartCurrency(value) {
    var currVal = numeral(value).format('0,0');
    try {
        var roundValue = roundNumber(currVal);
        currVal = numeral(roundValue).format('0,0');
        return currVal;
    } catch (e) {
        return currVal;
    }
}

//require numeral.min.js
function roundNumber(value) {
    try {
        var currVal = value;
        var arr = value.split(',');
        if (arr.length > 1) {
            var minCurrcy = arr[arr.length - 1];
            var beforeDot = '';
            for (var i = 0; i < arr.length; i++) {
                if (i != arr.length - 1)
                    beforeDot += arr[i];
            }
            var currValue = parseFloat(beforeDot + '.' + minCurrcy);
            var roundValue = Math.round(currValue) * 1000;
            currVal = roundValue;
        }
        return currVal;
    } catch (e) {
        return currVal;
    }
}

$(document).ready(function () {
    $('.datepicker').datetimepicker({
        useCurrent: false,
        locale: 'vi',
        format: 'DD/MM/YYYY',
    });

    $('.validate-number').keypress(function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    });

    $('.e-table-fix-head').tableHeadFixer({ 'foot': true, 'head': true });

    var $frmChangePassword = $('#frmChangePassword');
    $frmChangePassword.submit(function (e) {
        e.preventDefault();
        var form_data = $(this).serialize();
        $.ajax({
            type: this.method,
            url: this.action,
            data: form_data,
            beforeSend: function (xhr) {
                isBusyAddTaskForm = true;
            },
            success: function (data) {
                if (data.status === true) {
                    toastr["success"](data.message);
                    window.location.href = "/Sys/Logout";
                } else {
                    toastr['error'](data.message);
                }
            },
            error: function (data) {
            }
        });
    });
});

//Show imge from base64
function base64Encode(str) {
    var CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    var out = "", i = 0, len = str.length, c1, c2, c3;
    while (i < len) {
        c1 = str.charCodeAt(i++) & 0xff;
        if (i == len) {
            out += CHARS.charAt(c1 >> 2);
            out += CHARS.charAt((c1 & 0x3) << 4);
            out += "==";
            break;
        }
        c2 = str.charCodeAt(i++);
        if (i == len) {
            out += CHARS.charAt(c1 >> 2);
            out += CHARS.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
            out += CHARS.charAt((c2 & 0xF) << 2);
            out += "=";
            break;
        }
        c3 = str.charCodeAt(i++);
        out += CHARS.charAt(c1 >> 2);
        out += CHARS.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
        out += CHARS.charAt(((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6));
        out += CHARS.charAt(c3 & 0x3F);
    }
    return out;
}

/*Json response must contain: status= '00' || '01'*/
function SetMessage(data) {
    var code = data["status"];
    if (code == "00")
        toastr["error"](data["message"]);
    else {
        toastr["success"](data["message"]);
    }
}

function validate(form) {
    var err = true
    $("form#" + form + " input[type='text'], form#" + form + " select, form#" + form + " textarea,form#" + form + " input[type='file']").each(function () {
        if ($(this).attr("validate")) {
            if ($(this).hasClass("combobox")) {
                if ($(this).next("span.combo").find("input[type='hidden']").val() === "") {
                    alert($(this).closest("div").prev("label").text().replace('*', '') + ' Không được để trống');
                    $(this).focus();
                    err = false
                    return false;
                }
            } else if ($(this).val() === "") {
                alert($(this).closest("div").prev("label").text().replace('*', '') + ' Không được để trống');
                $(this).focus();
                err = false
                return false;
            }

        }
    });
    return err;
}

function validatev2(form, edit) {
    var err = true;
    var editCond = !edit ? ", form#" + form + " input[type='password']" : '';
    $("form#" + form + " input[type='text'], form#" + form + " select, form#" + form + " textarea,form#" + form + " input[type='file']" + editCond).each(function () {
        if ($(this).attr("validate")) {
            if ($(this).hasClass("combobox")) {
                if ($(this).next("span.combo").find("input[type='hidden']").val() === "") {
                    toastr['error']($(this).closest("div").prev("label").text().replace('*', '') + ' không được để trống');
                    $(this).addClass('e-validate');
                    $(this).focus();
                    err = false
                    return false;
                }
                else
                    $(this).removeClass('e-validate');
            } else if ($(this).val() === "") {
                toastr['error']($(this).closest("div").prev("label").text().replace('*', '') + ' không được để trống');
                $(this).addClass('e-validate');
                $(this).focus();
                err = false
                return false;
            }
            else
                $(this).removeClass('e-validate');
        }
    });
    return err;
}

//bangnq
function validate_tk(form) {
    var err = true;
    var rad = false
    $("form#" + form + " input[type='text'], form#" + form + " select, form#" + form + " textarea, form#" + form + " input[type='radio'], form#" + form + " input[type='file']").each(function () {
        if ($(this).hasClass("epp-radio")) {
            var radio = $(this).attr("name");

            if ($('input[name=' + radio + ']:checked').length <= 0) {
                if (!rad) {
                    gAlert($(this).closest("div").prev("label").text().replace('*', '') + 'không được để trống');
                }
                rad = true;
                err = false;
                return false;
            }
        }
        if ($(this).attr("validate")) {
            if ($(this).hasClass("combobox")) {
                if ($(this).next("span.combo").find("input[type='hidden']").val() === "") {
                    gAlert($(this).closest("div").prev("label").text().replace('*', '') + 'không được để trống');
                    $(this).focus();
                    err = false
                    return false;
                }
            } else if ($(this).val() === "") {
                gAlert($(this).closest("div").prev("label").text().replace('*', '') + 'không được để trống');
                $(this).focus();
                err = false
                return false;
            }

        }
    });
    return err;
}

function CheckDateSumbitForm(index, date, phone) {
    var check = true;
    switch (index) {
        case "0":
            if (!isDate(date)) {
                gAlert('Định dạng ngày sinh không chính xác!');
                check = false;
                return false;
            }
            if (!isPhoneNumber(phone)) {
                gAlert('Định dạng số điện thoại không chính xác!');
                check = false;
                return false;
            }
            return check;
            break;
        case "1":
            if (!isDateMoth(date)) {
                gAlert('Định dạng ngày sinh không chính xác!');
                check = false;
                return false;
            }
            if (!isPhoneNumber(phone)) {
                gAlert('Định dạng số điện thoại không chính xác!');
                check = false;
                return false;
            }
            return check;
            break;
        case "2":
            if (!isPhoneNumber(phone)) {
                gAlert('Định dạng số điện thoại không chính xác!');
                check = false;
                return false;
            }
            return check;
            break;
    }
}

function checkform(form, ns, phone, idnumber) {
    var data = [];
    var err = true;
    var rad = false;
    $("form#" + form + " input[type='text'], form#" + form + " select, form#" + form + " textarea, form#" + form + " input[type='radio'], form#" + form + " input[type='file']").each(function () {
        if ($(this).attr("validate")) {
            if ($(this).hasClass("epp-radio")) {
                var radio = $(this).attr("name");
                if ($('input[name=' + radio + ']:checked').length <= 0) {
                    if (!rad) {
                        var result = $(this).closest("div").prev("label").text().replace('*', '') + 'không được để trống';
                        data.push(result);
                    }
                    $(this).removeClass('e-validate-radio');
                    err = false;
                    rad = true;
                } else {
                    $(this).addClass('e-validate-radio');
                }
            }
            if ($(this).hasClass("combobox")) {
                if ($(this).next("span.combo").find("input[type='hidden']").val() === "") {

                    $(this).next("span.combo").find("input[type='hidden']").addClass('e-validate');
                } else {
                    $(this).next("span.combo").find("input[type='hidden']").removeClass('e-validate');
                }
            } else if ($(this).val() === "") {
                var result = $(this).closest("div").prev("label").text().replace('*', '') + 'không được để trống';
                data.push(result);
                $(this).addClass('e-validate');
                err = false;
            }
            else {
                $(this).removeClass('e-validate');
            }

        }
    });
    if (!ns) {
        data.push("Định dạng ngày sinh không chính xác");
        err = false;
    }
    if (!isPhoneNumber(phone)) {
        data.push("Định dạng số điện thoại không chính xác!");
        err = false;
    }
    if (idnumber != null && idnumber != undefined) {
        if (idnumber.length != 9 && idnumber.length != 12) {
            data.push("Số CMND/thẻ CCCD gồm 9 hoặc 12 chữ số");
            err = false;
        }
    }
    if (data.length > 0) {
        TAlert(data);
    }
    return err;
}

function isPhoneNumber(phone) {
    var check = true;
    if (phone == undefined)
        return false;
    if (phone.length < 9) {
        return false;
    } else if (phone[0] != "0") {
        return false;
    }
    return check;
}

function isDateMoth(txtDate) {
    var currVal = txtDate;
    if (currVal == '')
        return false;

    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?
    console.log(dtArray);
    if (dtArray == null)
        return false;
    var dtMonth = dtArray[1];
    var dtYear = dtArray[3];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    return true;
}

function isDate(txtDate) {
    var currVal = txtDate;
    if (currVal == '')
        return false;

    //Declare Regex  
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?
    //var rxDatePatternMoth = /^(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    //var dtArrayMoth = currVal.match(rxDatePatternMoth);
    console.log(dtArray);
    if (dtArray == null)
        return false;

    //Checks for dd/mm/yyyy format.
    var dtDay = dtArray[1];
    var dtMonth = dtArray[3];
    var dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }

    return true;
}
//end bangnq

$(document).ready(function () {
    $('.datepicker').datetimepicker({
        useCurrent: false,
        locale: 'vi',
        format: 'DD/MM/YYYY',
    });

    $('.validate-number').keypress(function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    });
   

    $('.e-table-fix-head').tableHeadFixer({ 'foot': true, 'head': true });
});

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function getToken() {
    return getCookie("etoken");
}


$(document).on('change', '.check-all', function () {
    if (this.checked)
        $(this).parents('table').find('.check-one').each(function () {
            if (!$(this).is(':disabled'))
                $(this).prop('checked', true);
        });
    else
        $(this).parents('table').find('.check-one').prop('checked', false);
})

$(document).on('change', 'input[type=checkbox]', function () {
    var checked = $('.check-one:checked');
    if (checked.length > 0)
        $('.btn-disabled').removeClass("disabled");
    else
        $('.btn-disabled').addClass("disabled");
})

$(document).on('change', '.check-all-1', function () {
    if (this.checked)
        $(this).parents('table').find('.check-one-1').each(function () {
            if (!$(this).is(':disabled'))
                $(this).prop('checked', true);
        });
    else
        $(this).parents('table').find('.check-one-1').prop('checked', false);
})

$(document).on('change', 'input[type=checkbox]', function () {
    var checked = $('.check-one-1:checked');
    if (checked.length > 0)
        $('.btn-disabled-1').removeClass("disabled");
    else
        $('.btn-disabled-1').addClass("disabled");
})
//Trạng thái thông tin hộ chiếu
function TranslateStatusPPInfor(status) {
    switch (status) {
        case "ACTIVATED":
            return "Đang hiệu lực";
        case "EXPIRED":
            return "Hết hiệu lực";
        case "CANCELLED":
            return "Đã hủy";
        default:
            return "";
    }
}
//Tình trạng hồ sơ
function TranslateStatusProfile(status) {
    switch (status) {
        case "CREATED ":
            return "Tạo mới";
        case "PROCESSING":
            return "Đang xử lý";
        case "PROCESSED":
            return "Đã xử lý";
        case "REPROCESSING":
            return "Đang xử lý lại";
        case "REPROCESSED":
            return "Đã xử lý";
        case "INVESTIGATED":
            return "Đã tra cứu";
        case "CHECKING":
            return "Đang kiểm tra";
        case "CHECKED":
            return "Đã kiểm tra";
        case "ADDING":
            return "Đang thêm";
        case "ADDED":
            return "Đã thêm";
        case "PERSONALIZING":
            return "Đang in";
        case "PERSONALIZED":
            return "Đã in";
        case "ISSUING":
            return "Đã in danh sách C";
        case "ISSUED":
            return "Đã đã phát hành";
        case "CANCELING":
            return "Đang đợi hủy";
        case "CANCELED":
            return "Đã hủy";
        case "REJECTED":
            return "Từ chối";
        case "COMPLETED":
            return "Hoàn thành";
        default:
            return "";
    }
}

//Tình trạng hồ sơ   (ở proposal, trường type: BusinessAction, K - REJECT, C -  REINVESTIGATE, D - APPROVE)
function TranslateStatusProposal(status) {
    switch (status) {
        case "REJECT ":
            return "Không được duyệt";
        case "REINVESTIGATE":
            return "Chưa được duyệt";
        case "APPROVE":
            return "Đã duyệt";
        default:
            return "";
    }
}
//Kết quả hồ sơ   (status của document: PERSONALIZING và PERSONALIZEDED)
function TranslateStatusDocument(status) {
    switch (status) {
        case "PERSONALIZING ":
            return "Đã in hộ chiếu";
        case "PERSONALIZEDED":
            return "Chưa in hộ chiếu";
        default:
            return "";
    }
}
//lấy về tên đầy đủ theo tên đăng nhập 
function GetFullName(inputId, user) {
    var result = "";
    $.ajax({
        url: '/TraCuuHoSoHoChieu/GetFullName',
        type: "post",
        dataType: "text",
        data: { user: user },
        cache: false,
        success: function (result) {
            if (result.length > 0) {
                $("#" + inputId).val(result);
                $("." + inputId).val(result);
                return false;
            }
            return result;
        }
    });
};

//by Suro
//Convert enum to array {key,value} Example vn.teca.epp.entity.Religion
function EnumToArray(enums) {
    var arr = [];
    try {
        var valueArray = Object.values(enums);
        var keyArray = Object.keys(enums);
        for (var i = 1; i < keyArray.length; i++) {
            var obj = { key: keyArray[i].split('.')[5], value: valueArray[i] };
            arr.push(obj);
        }
        return arr;
    } catch (e) {
        return [];
    }
}

function GetViewerImage(images) {
    var wrapper = document.createElement('div');
    var $div, $ul, $li, $img;
    $div = $('<div class= "galley"/>');
    $ul = $('<ul class = "pictures"/>');
    images.forEach(function (image) {
        $li = $('<li/>');
        $img = $(image);
        $li.append($img);
        $ul.append($li);
    });
    $div.append($ul);
    wrapper.innerHTML = $div.html();
    console.log(wrapper);
    return wrapper;
}