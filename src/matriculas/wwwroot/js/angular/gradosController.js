// gradosController.js

(function () {
    "use strict";

    // Llama al módulo principal e incluye el controlador
    angular.module("app")
        .controller("gradosController", gradosController);

    // Interactua con la Api gradosController.cs
    function gradosController($http) {
        var vm = this;
        vm.grados = [];
        vm.niveles = [];
        vm.isBusy = true;

        // Pagination
        vm.pageSize = 10;

        vm.actualCount; // Cantidad actual de grados
        vm.currentGrado = {}; // Grado actual para editar o eliminar
        vm.newGrado = {}; // Nuevo grado para crear

        // Solicita los grados disponibles (Los que están con estado 1) 
        $http.get("/api/grados")
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.grados);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de grados.");
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Solicita la lista de niveles
        $http.get("/api/niveles")
            .then(function (response) {
                // Success
                angular.copy(response.data, vm.niveles);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de niveles.");
            });

        // Agrega el grado
        vm.addGrado = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.post("/api/grados/crear", vm.newGrado)
                .then(function (response) {
                    // Success                                
                    vm.grados.push(response.data);
                    vm.newGrado = {}
                    
                    toastr.success("Se registró el grado correctamente.");
                },
                function (error) {
                    // Failure           
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo registrar el grado.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Elimina el grado
        vm.deleteGrado = function (idGrado) {
            vm.isBusy = true;
            vm.currentGrado = { id: idGrado }
            vm.errors = [];

            $http.post("/api/grados/eliminar", vm.currentGrado)
                .then(function (response) {
                    // Success    
                    var index = vm.grados.findIndex(obj => obj.id === vm.currentGrado.id);
                    vm.grados.splice(index, 1);

                    toastr.success("Se eliminó el grado correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.eliminacionMessageValidation !== "undefined")
                        toastr.warning(vm.errors.eliminacionMessageValidation);

                    toastr.error("No se pudo eliminar el grado.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        //Recupera los datos del colaborador
        vm.getGrado = function (id) {
            $http.get("/api/grados/" + id)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.currentGrado);
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del grado.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Actualiza del grado
        vm.updateGrado = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.post("/api/grados/editar", vm.currentGrado)
                .then(function (response) {
                    // Success         
                    var index = vm.grados.findIndex(obj => obj.id === vm.currentGrado.id);
                    vm.grados[index] = response.data;
                    $('#datos-grado').modal('hide');
                    toastr.success("Se actualizó el grado correctamente.");
                },
                function (error) {
                    // Failure                 
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation[0]);

                    toastr.error("No se pudo actualizar el grado.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }
    }
})();