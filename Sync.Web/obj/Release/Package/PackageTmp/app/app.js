var app;

(function () {
    app = angular.module("e-app", ['angularUtils.directives.dirPagination', 'LocalStorageModule', 'ui.select2']);
    app.run(['$rootScope', function ($rootScope) {
        var baseUrl = 'http://localhost:8088/' + "/SPIDService/";
        var apiUrl = 'http://10.0.0.49:8080/app/rest/v2/';
        var openAppCropPhotoApi = 'http://localhost:8484/Scan/CallAppCropPhoto?param=';
        $rootScope.baseUrl = baseUrl;
        $rootScope.apiUrl = apiUrl;
        $rootScope.openAppCropPhotoApi = openAppCropPhotoApi;
    }]);
    app.directive('customzdatetime', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {

                element.datetimepicker({
                    useCurrent: false,
                    locale: 'vi',
                    minDate: '01/01/1900',
                    format: attrs.formatdate === undefined ? 'DD/MM/YYYY' : attrs.formatdate,
                }).on('dp.change', function (e) {
                    // var date = (e.date._d.getDate() - 2) + '-' + (e.date._d.getMonth() + 1) + '-' + e.date._d.getFullYear();
                    var date = e.date._d;
                    if (date != undefined) {
                        var date = (e.date._d.getDate() > 9 ? e.date._d.getDate() : '0' + e.date._d.getDate()) + '/' + ((e.date._d.getMonth() + 1) > 9 ? (e.date._d.getMonth() + 1) : '0' + (e.date._d.getMonth() + 1)) + '/' + e.date._d.getFullYear();
                        ngModelCtrl.$setViewValue(date);
                        scope.$apply();
                    }
                }).on('focusout', function (e) {
                    //console.log(e.currentTarget.value);
                    ngModelCtrl.$setViewValue(e.currentTarget.value);
                    scope.$apply();
                });
            }
        };
    });

    app.directive('customzdatetimeleft', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                element.datetimepicker({
                    useCurrent: false,
                    locale: 'vi',
                    minDate: '01/01/1900',
                    widgetPositioning: {
                        horizontal: "left"
                    },
                    format: attrs.formatdate === undefined ? 'DD/MM/YYYY' : attrs.formatdate
                }).on('dp.change', function (e) {
                    // var date = (e.date._d.getDate() - 2) + '-' + (e.date._d.getMonth() + 1) + '-' + e.date._d.getFullYear();
                    var date = e.date._d;
                    if (date != undefined) {
                        var date = (e.date._d.getDate() > 9 ? e.date._d.getDate() : '0' + e.date._d.getDate()) + '/' + ((e.date._d.getMonth() + 1) > 9 ? (e.date._d.getMonth() + 1) : '0' + (e.date._d.getMonth() + 1)) + '/' + e.date._d.getFullYear();
                        //  console.log(e.date._d);
                        var date = e.date._d;
                        ngModelCtrl.$setViewValue(date);
                        scope.$apply();
                    }
                }).on('focusout', function (e) {
                    ngModelCtrl.$setViewValue(e.target.value);
                });
            }
        };
    });

    app.directive('customzdatetimeright', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                element.datetimepicker({
                    useCurrent: false,
                    locale: 'vi',
                    minDate: '01/01/1900',
                    widgetPositioning: {
                        horizontal: "right"
                    },
                    format: attrs.formatdate === undefined ? 'DD/MM/YYYY' : attrs.formatdate
                }).on('dp.change', function (e) {
                    // var date = (e.date._d.getDate() - 2) + '-' + (e.date._d.getMonth() + 1) + '-' + e.date._d.getFullYear();
                    var date = e.date._d;
                    if (date != undefined) {
                        var date = (e.date._d.getDate() > 9 ? e.date._d.getDate() : '0' + e.date._d.getDate()) + '/' + ((e.date._d.getMonth() + 1) > 9 ? (e.date._d.getMonth() + 1) : '0' + (e.date._d.getMonth() + 1)) + '/' + e.date._d.getFullYear();
                        //  console.log(e.date._d);
                        var date = e.date._d;
                        ngModelCtrl.$setViewValue(date);
                        scope.$apply();
                    }
                }).on('focusout', function (e) {
                    ngModelCtrl.$setViewValue(e.target.value);
                });
            }
        };
    });

    app.directive('customzdatetimeinhochieu', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {

                element.datetimepicker({
                    useCurrent: false,
                    locale: 'vi',
                    minDate: '01/01/1900',
                    format: attrs.formatdate === undefined ? 'DD/MM/YYYY' : attrs.formatdate,
                }).on('dp.change', function (e) {
                    // var date = (e.date._d.getDate() - 2) + '-' + (e.date._d.getMonth() + 1) + '-' + e.date._d.getFullYear();
                    var date = e.date._d;
                    if (date != undefined) {
                        var date = (e.date._d.getDate() > 9 ? e.date._d.getDate() : '0' + e.date._d.getDate()) + '/' + ((e.date._d.getMonth() + 1) > 9 ? (e.date._d.getMonth() + 1) : '0' + (e.date._d.getMonth() + 1)) + '/' + e.date._d.getFullYear();
                        ngModelCtrl.$setViewValue(date);
                    }
                }).on('focusout', function (e) {
                    //console.log(e.currentTarget.value);
                    ngModelCtrl.$setViewValue(e.currentTarget.value);
                    scope.$apply();
                });
            }
        };
    });

    app.directive('tcustomzdatetime', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                element.datetimepicker({
                    useCurrent: false,
                    locale: 'vi',
                    format: attrs.formatdate === undefined ? 'DD/MM/YYYY' : attrs.formatdate,
                }).on('dp.change', function (e) {
                    var date = (e.date._d.getDate() > 9 ? e.date._d.getDate() : '0' + e.date._d.getDate()) + '/' + ((e.date._d.getMonth() + 1) > 9 ? (e.date._d.getMonth() + 1) : '0' + (e.date._d.getMonth() + 1)) + '/' + e.date._d.getFullYear();
                    ngModelCtrl.$setViewValue(date);
                    scope.$apply();
                }).on('focusout', function (e) {
                    //console.log(e.currentTarget.value);
                    ngModelCtrl.$setViewValue(e.currentTarget.value);
                    scope.$apply();
                });
            }
        };
    });
    app.directive('ccustomzdatetime', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                element.datetimepicker({
                    useCurrent: false,
                    locale: 'vi',
                    format: attrs.formatdate === undefined ? 'DD/MM/YYYY' : attrs.formatdate,
                }).on('dp.change', function (e) {
                    //  var date = (e.date._d.getDate() > 9 ? e.date._d.getDate() : '0' + e.date._d.getDate()) + '/' + ((e.date._d.getMonth() + 1) > 9 ? (e.date._d.getMonth() + 1) : '0' + (e.date._d.getMonth() + 1)) + '/' + e.date._d.getFullYear();
                    ngModelCtrl.$setViewValue(e.date);
                    scope.$apply();
                }).on('focusout', function (e) {
                    //console.log(e.currentTarget.value);
                    ngModelCtrl.$setViewValue(e.currentTarget.value);
                    scope.$apply();
                });
            }
        };
    });
    app.directive('ttable', function ($compile) {
        return {
            restrict: 'A',
            replace: true,
            scope: {
                ttableLength: '@',
            },
            link: function (scope, element, attrs, ngModelCtrl) {
                var num = scope.$eval(attrs.ttableLength);
                var parent = element.closest('div');
                var eheight = screen.height - parent.attr('e-heigth') - 40;
                parent.css({ 'height': screen.height - parent.attr('e-heigth') });
                parent.css({ 'border': '1px solid #ddd' });
                var colspan = element.find('thead').find('tr').find('th').length;
                var $tthead, $tbody, $tr, $td, $div1, $div2, $div3;
                var hth = element.find('thead').find('tr').find('th');
                var offsetHeight = hth[0].offsetHeight;
                if (attrs.dheigth === undefined) {
                    if (attrs.tthd === undefined && attrs.tthd !== "true") {
                        for (var i = 0; i <= hth.length; i++) {
                            $(hth[i]).find('div').css('height', offsetHeight === 0 ? 36 : offsetHeight + 'px');
                        }
                    }
                } else {
                    for (var i = 0; i <= hth.length; i++) {
                        $(hth[i]).find('div').css('height', attrs.dheigth + 'px');
                    }
                }
                $tbody = $('<tbody ng-if="ttableLength <1" class="e-not-found"/>');
                $tr = $('<tr/>')
                $td = $('<td colspan = ' + colspan + ' style="height: ' + (attrs.dheigth === undefined ? eheight : (eheight - 12)) + 'px; border: none" />');
                $div1 = $('<div class="e-not-found-center"/>');
                $div2 = $('<div class="e-not-found-title" />').html('Chưa tìm thấy kết quả');
                $div3 = $('<div class="e-not-found-title-child"/>').html('Hãy thử tìm kiếm hoặc sử dụng bộ lọc khác');
                $div1.append($div2);
                $div1.append($div3);
                $td.append($div1);
                $tr.append($td);
                $tbody.append($tr);
                $compile($tbody)(scope);
                element.append($tbody);
            }
        };
    });
    app.directive('toUppercase', function () {

        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ctrl) {

                function parser(value) {
                    if (ctrl.$isEmpty(value)) {
                        return value;
                    }
                    var formatedValue = value.toUpperCase();
                    if (ctrl.$viewValue !== formatedValue) {
                        ctrl.$setViewValue(formatedValue);
                        ctrl.$render();
                    }
                    return formatedValue;
                }

                function formatter(value) {
                    if (ctrl.$isEmpty(value)) {
                        return value;
                    }
                    return value.toUpperCase();
                }

                ctrl.$formatters.push(formatter);
                ctrl.$parsers.push(parser);
            }
        };
    });
    app.directive("owlCarousel", function () {
        return {
            restrict: 'E',
            transclude: false,
            link: function (scope) {
                scope.initCarousel = function (element) {
                    // provide any default options you want
                    var defaultOptions = {
                        items: 1,
                        slideSpeed: 2000,
                        nav: true,
                        autoplay: true,
                        dots: true,
                        loop: true,
                        nav: false,
                        dots: false,
                        responsiveRefreshRate: 200,
                    };
                    var customOptions = scope.$eval($(element).attr('data-options'));
                    // combine the two options objects
                    for (var key in customOptions) {
                        defaultOptions[key] = customOptions[key];
                    }
                    // init carousel
                    $(element).owlCarousel(defaultOptions);
                };
            }
        };
    });
    app.directive('owlCarouselItem', [function () {
        return {
            restrict: 'A',
            transclude: false,
            link: function (scope, element) {
                // wait for the last item in the ng-repeat then call init
                if (scope.$last) {
                    scope.initCarousel(element.parent());
                }
            }
        };
    }]);
    app.directive('appFilereader', function ($q) {
        var slice = Array.prototype.slice;

        return {
            restrict: 'A',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModel) {
                if (!ngModel) return;

                ngModel.$render = function () { };

                element.bind('change', function (e) {
                    var element = e.target;

                    $q.all(slice.call(element.files, 0).map(readFile))
                        .then(function (values) {
                            if (element.multiple) ngModel.$setViewValue(values);
                            else ngModel.$setViewValue(values.length ? values[0] : null);
                        });

                    function readFile(file) {
                        var deferred = $q.defer();

                        var reader = new FileReader();
                        reader.onload = function (e) {
                            deferred.resolve(e.target.result);
                        };
                        reader.onerror = function (e) {
                            deferred.reject(e);
                        };
                        reader.readAsDataURL(file);

                        return deferred.promise;
                    }

                }); //change

            } //link
        }; //return
    });
})();