// seccionesController.js

(function () {
    "use strict";

    // Llama al módulo principal e incluye el controlador
    angular.module("app")
        .controller("seccionesController", seccionesController);

    // Interactua con la Api seccionesController.cs
    function seccionesController($http) {
        var vm = this;
        vm.secciones = [];
        vm.grados = [];
        vm.isBusy = true;

        //Pagination
        vm.pageSize = 10;

        vm.actualCount; // Cantidad de secciones actual
        vm.currentSeccion = {}; // Sección actual para editar o eliminar
        vm.newSeccion = {}; // Nuevo sección para crear

        // Solicita las secciones Disponibles (Las que estan con estado 1)
        $http.get("/api/v2/secciones")
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.secciones);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de secciones.");
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Solicita la lista de grados Disponibles (Los que están en estado 1)
        $http.get("/api/v2/grados")
            .then(function (response) {
                // Success
                angular.copy(response.data, vm.grados);
            }, function (error) {
                // Failure
                vm.errorMessage = "No se pudo cargar la información de grados.";
            });

        //Recupera los datos de la sección
        vm.getSeccion = function (id) {
            $http.get("/api/v2/secciones/" + id)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.currentSeccion);
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información de la sección.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Agrega la sección
        vm.addSeccion = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.post("/api/v2/secciones/", vm.newSeccion)
                .then(function (response) {
                    // Success                      
                    vm.secciones.push(response.data);
                    vm.newSeccion = {}
                    
                    toastr.success("Se registró la sección correctamente.");
                },
                function (error) {
                    // Failure                   
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo registrar la sección.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Realiza la actualización de la sección
        vm.updateSeccion = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.put("/api/v2/secciones/", vm.currentSeccion)
                .then(function (response) {
                    // Success              
                    var index = vm.secciones.findIndex(obj => obj.id === vm.currentSeccion.id);
                    vm.secciones[index] = response.data;
                    $('#datos-seccion').modal('hide');

                    toastr.success("Se actualizó la sección correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo actualizar la sección.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Elimina la sección
        vm.deleteSeccion = function (id) {
            vm.isBusy = true;

            $http.delete("/api/v2/secciones/" + id)
                .then(function (response) {
                    // Success                                
                    var index = vm.secciones.findIndex(obj => obj.id === id);
                    vm.secciones.splice(index, 1);

                    toastr.success("Se eliminó la sección correctamente.");
                },
                function (error) {
                    // Failure
                    toastr.error("No se pudo eliminar la sección.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        } 
    }
})();