// profesoresController.js

(function () {
    "use strict";
    // Llama al módulo prinicipal e incluye el controlador
    angular.module("app")
        .controller("profesoresController", profesoresController);

    // Interactua con la Api profesoresController.cs
    function profesoresController($http) {
        var vm = this;
        vm.profesores = [];
        vm.cursos = [];
        vm.grados = [];
        vm.isBusy = true;
        
        // Pagination
        vm.pageSize = 10;

        vm.actualCount; // Cantidad actual de profesores
        vm.currentProfesor = {}; // Profesor actual para editar o eliminar
        vm.newProfesor = {}; // Nuevo colaborador para crear

        // Solicita los grados disponibles (Los que están con estado 1) 
        $http.get("/api/grados")
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.grados);
                
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de profesores.");
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Solicita los cursos disponibles (Los que están con estado 1) 
        $http.get("/api/cursos")
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.cursos);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de cursos.");
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Solicita la lista de profesores disponibles (Los que están con estado 1)
        $http.get("/api/profesores")
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.profesores);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de profesores.");
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Agrega el colaborador
        vm.addProfesor = function () {
            vm.isBusy = true;
            vm.errors = [];
            
            $http.post("/api/profesores/crear", vm.newProfesor)
                .then(function (response) {                   
                    // Success                                
                    vm.profesores.push(response.data);                  
                    vm.newProfesor = {}

                    toastr.success("Se registró el profesor correctamente.");
                },
                function (error) {
                    // Failure         
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.dniMessageValidation !== "undefined")
                        toastr.warning(vm.errors.dniMessageValidation);

                    toastr.error("No se pudo registrar el profesor.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Elimina el colaborador
        vm.deleteProfesor = function (idProfesor) {
            vm.isBusy = true;
            vm.currentProfesor = { id: idProfesor }

            $http.post("/api/profesores/eliminar", vm.currentProfesor)
                .then(function (response) {
                    // Success            
                    var index = vm.profesores.findIndex(obj => obj.id === vm.currentProfesor.id);
                    vm.profesores.splice(index, 1);

                    toastr.success("Se eliminó el profesor correctamente.");
                },
                function (error) {
                    // Failure
                    toastr.error("No se pudo eliminar el profesor.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        //Recupera los datos del profesor
        vm.getProfesor = function (id) {         
            $http.get("/api/profesores/" + id)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.currentProfesor);
                    $http.get("/api/cursosProfesor/" + id)
                        .then(function (response) {
                            // Success   
                            angular.copy(response.data, vm.currentProfesor.cursos);
                        }, function (error) {
                            // Failure
                            vm.errorMessage = "No se pudo cargar la información del profesor.";
                        })
                        .finally(function () {
                            vm.isBusy = false;
                        });
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del profesor.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Actualiza del colaborador
        vm.updateProfesor = function () {
            vm.isBusy = true;
            vm.errors = [];
            $http.post("/api/profesores/editar", vm.currentProfesor)
                .then(function (response) {
                    // Success  
                    var index = vm.profesores.findIndex(obj => obj.id === vm.currentProfesor.id);
                    vm.profesores[index] = response.data;
                    $('#datos-profesor').modal('hide');
                    collapseAccordion();

                    toastr.success("Se actualizó el profesor correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.dniMessageValidation !== "undefined")
                        toastr.warning(vm.errors.dniMessageValidation[0]);

                    toastr.error("No se pudo actualizar el profesor.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        } 
    } 
})();