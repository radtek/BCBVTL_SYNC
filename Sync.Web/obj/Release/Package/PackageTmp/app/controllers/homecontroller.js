(function (app) {
    app.controller('homecontroller', HomeController);
    HomeController.$inject = ['$scope', '$filter', 'apiservices', '$rootScope', '$window', '$timeout', '$rootScope'];
    function HomeController($scope, $filter, apiservices, $rootScope, $window, $timeout, $rootScope) {
        $scope.listProcess = [];
        $scope.process = {};
        $scope.interval4Log = null;
        $scope.listDataTypes = [];
        $scope.taskName = '';
        $scope.danhSachHoSo = [];

        function getTypesData() {
            $.ajax({
                url: '/Home/GetTypesData',
                type: "GET",
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    if (response.code == "200") {
                        $scope.listDataTypes = response.data;
                        $scope.$apply();
                    } else {
                        toastr.error(response.message);
                    }
                }
            });
        }
        getTypesData();

        function getKhuVuc() {
            $.ajax({
                url: '/Home/GetKhuVuc',
                type: "GET",
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    if (response.code == "200") {
                        $scope.KhuVuc = response.data;
                        $scope.$apply();
                    } else {
                        toastr.error(response.message);
                    }
                }
            });
        }

        getKhuVuc();

        function getListProcess() {
            $.ajax({
                url: '/Home/GetListProcess',
                type: "GET",
                dataType: "json",
                success: function (response) {
                    if (response.code == "200") {
                        $scope.listProcess = response.data
                        $scope.$apply()
                    } else {
                        toastr.error(response.message)
                    }
                },
                error: function (xhr) {

                    console.log('error');
                }
            });
        }

        getListProcess()

        function getLogJob() {
            $scope.interval4Log = setTimeout(function () {
                getLog();
            }, 5000);

        }

        function getLog() {
            $.ajax({
                type: "Post",
                url: "/Home/ShowConfig",
                data: {
                    name: $scope.process.Code
                },
                success: function (rs) {
                    $scope.logFile = rs.message;
                    $scope.$apply();

                    getLogJob();
                }
            });
        }

        $scope.selectProcess = function (process) {
            $scope.process = { ...process }
            getLog();
        }

        $scope.updateProcess = function () {
            $.ajax({
                url: '/Home/UpdateProcess',
                type: "POST",
                data: $scope.process,
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    if (response.code == "200") {
                        $scope.listProcess = response.data
                        $scope.$apply()
                        toastr.success("Thành công")
                    } else {
                        toastr.error(response.message)
                    }
                },
                error: function (xhr) {

                    console.log('error');
                }
            });
        }

        $scope.onchangeDataType = function (value) {
            $scope.taskName = value;
        }

        $scope.getData = function () {
            if ($scope.dataType != "") {
                $.ajax({
                    url: '/Home/GetResendDataAsync',
                    type: "POST",
                    data: {
                        taskName: $scope.dataType
                    },
                    success: function (response) {
                        console.log(response);
                        if (response.data.length > 0) {
                            $scope.danhSachHoSo = response.data;
                            $scope.$apply();
                        } else {
                            $scope.danhSachHoSo = [];
                            $scope.$apply();
                            toastr['warning']('Không có hồ sơ cần gửi lại.');
                        }
                    }

                });
            }
        }

        $scope.resendData = function () {

            if ($scope.danhSachHoSo.length > 0) {

                $.ajax({
                    url: '/Home/ResendDataAsync',
                    type: "POST",
                    data: {
                        dsHoSo: $scope.danhSachHoSo
                    },
                    success: function (response) {
                        console.log(response);
                        toastr['success']('Cập nhật hàng đợi thành công!');
                        $scope.getData()
                    }
                });
            }
            else {
                toastr['warning']('Không có hồ sơ cần gửi lại.');
            }
        }

        $scope.changeKhuVuc = function (khuVuc) {
            $.ajax({
                url: '/Home/ChangeKhuVuc',
                type: "POST",
                data: {
                    khuVuc
                },
                success: function (response) {
                    console.log(response);
                    if (response.code == '200') {
                        toastr['success']('Cập nhật trung tâm điều hành thành công!');
                        getKhuVuc()
                    } else {
                        toastr['error'](response.message);
                    }

                }
            });
        }
    }
})(angular.module('e-app'));
