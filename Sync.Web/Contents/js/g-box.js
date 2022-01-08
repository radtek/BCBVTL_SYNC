function gAlert(message) {
    bootbox.alert({
        size: 'small',
        title: "<label>Thông báo</label>",
        message: '<i style="display: inline-block;" class="glyphicon glyphicon-exclamation-sign gi-2x"></i> <p style="display: inline-block; position: absolute; margin-left: 10px; margin-top: 5px;">' + message + '</p>',
        buttons: {
            ok: {
                label: '<i class="glyphicon glyphicon-remove"></i> Đóng',
                className: 'btn-warning'
            }
        }
    })
}

function TAlert(data) {
    var $ul, $li;
    $ul = $('<ul class="epp-ul-imf"/>');
    for (var i = 0; i < data.length; i++) {
        $li = $('<li/>')
        var $imf = $('<i style="display: inline-block;" class="glyphicon glyphicon-exclamation-sign gi-2x"></i> <p style="display: inline-block; position: absolute; margin-left: 10px; margin-top: 5px;">' + data[i] + '</p>');
        $li.append($imf);
        $ul.append($li);
    }
    bootbox.alert({
        size: 'default',
        title: "<label>Thông báo</label>",
        message: $ul,
        buttons: {
            ok: {
                label: '<i class="glyphicon glyphicon-remove"></i> Đóng',
                className: 'btn-warning'
            }
        }
    })
}

function gConfirm(message, callback) {
    bootbox.confirm({
        size: 'medium',
        title: "<label>Thông báo</label>",
        message: '<i style="display: inline-block;" class="glyphicon glyphicon-question-sign gi-2x"></i> <p style="display: inline-block; position: absolute; margin-left: 10px; margin-top: 5px; font-size:16px;">' + message + '</p>',
        buttons: {
            confirm: {
                label: '<i class="glyphicon glyphicon-ok"></i> Có',
                className: 'btn color-blue'
            },
            cancel: {
                label: '<i class="glyphicon glyphicon-remove"></i> Không',
                className: 'btn-warning'
            }
        },
        callback: function (result) {
            if (result)
                callback();
        }
    })
}

function tConfirm(message, callback) {
    bootbox.confirm({
        size: 'small',
        title: "<label>Thông báo</label>",
        message: '<i style="display: inline-block;" class="glyphicon glyphicon-question-sign gi-2x"></i> <p style="display: inline-block; position: absolute; margin-left: 10px; margin-top: 5px;">' + message + '</p>',
        buttons: {
            confirm: {
                label: '<i class="glyphicon glyphicon-ok"></i> Có',
                className: 'color-blue'
            },
            cancel: {
                label: '<i class="glyphicon glyphicon-remove"></i> Không',
                className: 'btn-warning'
            }
        },
        callback: function (result) {
            callback(result);
        }
    });
}

function tkConfirm(message, message2, message3, callback) {
    bootbox.confirm({       
        size: 'medium',
        title: "<label>Thông báo</label>",
        message: '<i style="display: inline-block;" class="glyphicon glyphicon-question-sign gi-2x"></i> <p style="display: inline-block; position: absolute; margin-left: 10px; margin-top: 5px; font-size:16px;">' + message + '</p>' + '<p style="margin-left: 40px; font-size:16px;" > ' + message2 + '</p>' + '<p style="margin-left: 40px; font-size:16px;" > ' + message3 + '</p>',
        buttons: {
            confirm: {
                label: '<i class="glyphicon glyphicon-ok"></i> Có',
                className: 'color-blue'
            },
            cancel: {
                label: '<i class="glyphicon glyphicon-remove"></i> Không',
                className: 'btn-warning'
            }
        },
        callback: function (result) {
            if (result)
            callback(result);
        }
    });
}

function tConfirmNT(message, callback) {
    bootbox.confirm({
        size: 'small',
        title: "<label>Thông báo</label>",
        message: '<i style="display: inline-block;" class="glyphicon glyphicon-question-sign gi-2x"></i> <p style="display: inline-block; position: absolute; margin-left: 10px; margin-top: 5px;">' + message + '</p>',
        buttons: {
            confirm: {
                label: '<i class="glyphicon glyphicon-circle-arrow-right"></i> Hồ sơ tiếp theo',
                className: 'color-blue'
            },
            cancel: {
                label: '<i class="glyphicon glyphicon-remove"></i> Không',
                className: 'btn-warning'
            }
        },
        callback: function (result) {
            callback(result);
        }
    });
}

function gAlertx(message, callback) {
    bootbox.confirm({
        size: 'small',
        title: "<label>Thông báo</label>",
        message: '<i style="display: inline-block;" class="glyphicon glyphicon-question-sign gi-2x"></i> <p style="display: inline-block; position: absolute; margin-left: 10px; margin-top: 5px;">' + message + '</p>',
        buttons: {
            confirm: {
                label: '<i class="glyphicon glyphicon-ok"></i> OK',
                className: 'btn-primary'
            }
        },
        callback: function (result) {
            if (result)
                callback();
        }
    })
}


function gSuccess(message, callback) {
    bootbox.confirm({
        size: 'small',
        title: "<label>Thông báo</label>",
        message: '<i style="display: inline-block;color:#22b423;font-size: 20px;" class="glyphicon glyphicon-ok"></i> <p style="display: inline-block; position: absolute; margin-left: 10px; margin-top: 5px;">' + message + '</p>',
        buttons: {
            confirm: {
                label: '<i class="glyphicon glyphicon-ok"></i> OK',
                className: 'displayNone'
            },
            cancel: {
                label: '<i class="glyphicon glyphicon-remove"></i> Đóng',
                className: 'btn-warning'
            }
        },
        callback: function (result) {
            callback(result);
        }
    });
}

function gError(message, callback) {
    bootbox.confirm({
        size: 'small',
        title: "<label>Thông báo</label>",
        message: '<i style="display: inline-block;color:red;font-size: 20px;" class="glyphicon glyphicon-remove"></i> <p style="display: inline-block; position: absolute; margin-left: 10px; margin-top: 5px;">' + message + '</p>',
        buttons: {
            confirm: {
                label: '<i class="glyphicon glyphicon-ok"></i> OK',
                className: 'displayNone'
            },
            cancel: {
                label: '<i class="glyphicon glyphicon-remove"></i> Đóng',
                className: 'btn-warning'
            }
        },
        callback: function (result) {
            callback(result);
        }
    });
}