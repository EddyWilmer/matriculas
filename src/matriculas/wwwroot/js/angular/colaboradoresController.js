// colaboradoresController.js

(function () {
    "use strict";
    // Llama al módulo prinicipal e incluye el controlador
    angular.module("app")
        .controller("colaboradoresController", colaboradoresController);

    // Interactua con la Api colaboradoresController.cs
    function colaboradoresController($http) {
        var vm = this;
        vm.colaboradores = [];
        vm.roles = [];
        vm.isBusy = true;

        // Pagination
        vm.pageSize = 10;

        vm.actualCount; // Cantidad actual de colaboradores
        vm.currentColaborador = {}; // Colaborador actual para editar o eliminar
        vm.newColaborador = {}; // Nuevo colaborador para crear

        // Solicita la lista de colaboradores disponibles (Los que están con estado 1)
        $http.get("/api/v2/colaboradores")
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.colaboradores);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de colaboradores.");
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Solicita la lista de roles
        $http.get("/api/v2/cargos")
            .then(function (response) {
                // Success
                angular.copy(response.data, vm.roles);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de roles.");
            });

        //Recupera los datos del colaborador
        vm.getColaborador = function (id) {
            $http.get("/api/v2/colaboradores/" + id)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.currentColaborador);
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del colaborador.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Agrega el colaborador
        vm.addColaborador = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.post("/api/v2/colaboradores/", vm.newColaborador)
                .then(function (response) {
                    // Success                                
                    vm.colaboradores.push(response.data);                  
                    vm.newColaborador = {}

                    toastr.success("Se registró el colaborador correctamente.");
                },
                function (error) {
                    // Failure                                 
                    angular.copy(error.data, vm.errors);
                
                    if (typeof vm.errors.emailMessageValidation !== "undefined")
                        toastr.warning(vm.errors.emailMessageValidation);

                    if (typeof vm.errors.dniMessageValidation !== "undefined")
                        toastr.warning(vm.errors.dniMessageValidation);
                
                    toastr.error("No se pudo registrar el colaborador.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Actualiza del colaborador
        vm.updateColaborador = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.put("/api/v2/colaboradores/", vm.currentColaborador)
                .then(function (response) {
                    // Success  
                    var index = vm.colaboradores.findIndex(obj => obj.id === vm.currentColaborador.id);
                    vm.colaboradores[index] = response.data;
                    $('#datos-colaborador').modal('hide');
                    toastr.success("Se actualizó el colaborador correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.emailMessageValidation !== "undefined")
                        toastr.warning(vm.errors.emailMessageValidation);

                    if (typeof vm.errors.dniMessageValidation !== "undefined")
                        toastr.warning(vm.errors.dniMessageValidation);

                    toastr.error("No se pudo registrar el colaborador.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Elimina el colaborador
        vm.deleteColaborador = function (id) {
            vm.isBusy = true;

            $http.delete("/api/v2/colaboradores/" + id)
                .then(function (response) {
                    // Success            
                    var index = vm.colaboradores.findIndex(obj => obj.id === id);
                    vm.colaboradores.splice(index, 1);

                    toastr.success("Se eliminó el colaborador correctamente.");
                },
                function (error) {
                    // Failure
                    toastr.error("No se pudo eliminar el colaborador.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Cambia el estado del usuario del colaborador
        vm.changeEstadoColaborador = function (id) {
            $http.post("/api/v2/colaboradores/" + id + "/toggleStatus")
                .then(function (response) {
                    // Success   
                    var index = vm.colaboradores.findIndex(obj => obj.id === id);
                    vm.colaboradores[index] = response.data;
                    if (vm.colaboradores[index].estado === "1")
                        toastr.success("Se habilitó el usuario correctamente.");
                    if (vm.colaboradores[index].estado === "3")
                        toastr.success("Se suspendió el usuario correctamente.");
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cambiar el estado del usuario del colaborador.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Cambia el estado del usuario del colaborador
        vm.restaurarPasswordColaborador = function (id) {
            $http.post("/api/v2/colaboradores/" + id + "/resetPassword")
                .then(function (response) {
                    // Success   
                    toastr.success("Se restauró la contraseña correctamente.");
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cambiar el estado del usuario del colaborador.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }
})();