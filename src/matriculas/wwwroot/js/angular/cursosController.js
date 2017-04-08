// cursosController.js

(function () {
    "use strict";

    // Llama al módulo principal e incluye el controlador
    angular.module("app")
        .controller("cursosController", cursosController);

    // Interactua con la Api cursosController.cs
    function cursosController($http) {
        var vm = this;
        vm.cursos = [];
        vm.grados = [];
        vm.profesores = [];
        vm.isBusy = true;

        // Pagination
        vm.pageSize = 10;

        vm.actualCount; // Cantidad actual de cursos
        vm.currentCurso = {}; // Curso actual para editar o eliminar
        vm.newCurso = {}; // Nuevo curso para crear
        vm.currentProfesor = {};

        // Solicita los cursos disponibles (Los que están con estado 1) 
        $http.get("/api/v2/cursos")
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

        // Solicita la lista de grados
        $http.get("/api/v2/grados")
            .then(function (response) {
                // Success
                angular.copy(response.data, vm.grados);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de grados.");
            });

        //Recupera los datos del colaborador
        vm.getCurso = function (id) {
            $http.get("/api/v2/cursos/" + id)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.currentCurso);
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del curso.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Agrega el curso
        vm.addCurso = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.post("/api/v2/cursos/", vm.newCurso)
                .then(function (response) {
                    // Success                                
                    vm.cursos.push(response.data);
                    vm.newCurso = {}
                    
                    toastr.success("Se registró el curso correctamente.");
                },
                function (error) {
                    // Failure                    
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.excesoHorasMessageValidation !== "undefined")
                        toastr.warning(vm.errors.excesoHorasMessageValidation);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo registrar el curso.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Actualiza del curso
        vm.updateCurso = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.put("/api/v2/cursos/", vm.currentCurso)
                .then(function (response) {
                    // Success         
                    var index = vm.cursos.findIndex(obj => obj.id === vm.currentCurso.id);
                    vm.cursos[index] = response.data;
                    $('#datos-curso').modal('hide');

                    toastr.success("Se actualizó el curso correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.excesoHorasMessageValidation !== "undefined")
                        toastr.warning(vm.errors.excesoHorasMessageValidation);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo actualizar el curso.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Elimina el curso
        vm.deleteCurso = function (id) {
            vm.isBusy = true;
            vm.errors = [];

            $http.delete("/api/v2/cursos/" + id)
                .then(function (response) {
                    // Success    
                    var index = vm.cursos.findIndex(obj => obj.id === id);
                    vm.cursos.splice(index, 1);

                    toastr.success("Se eliminó el curso correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);
                 
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        //Recupera los datos del profesor
        vm.getProfesor = function (id) {
            vm.currentCurso = {};
            $http.get("/api/v2/cursos/" + id + "/profesor")
                .then(function (response) {
                    // Success  
                    angular.copy(response.data, vm.currentProfesor);            
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del curso.";
                })
                .finally(function () {
                    vm.getCurso(id);
                    vm.searchProfesores(id);
                    vm.isBusy = false;
                });
        }

        // Solicita la lista de profesores disponibles para un curso específico
        vm.searchProfesores = function (id) {
            $http.get("/api/v2/cursos/" + id + "/searchProfesores")
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
        }

        // Asigna un profesor al curso
        vm.assignProfesor = function () {
            vm.isBusy = true;
            vm.errors = [];
            var result = vm.currentProfesor != null ? vm.currentProfesor.id : 0;
            $http.post("/api/v2/cursos/" + vm.currentCurso.id + "/assignProfesor/" + result)
                .then(function (response) {
                    // Success         
                    $('#datos-profesor').modal('hide');
                    toastr.success("Se actualizó el profesor correctamente.");
                },
                function (error) {
                    toastr.error("No se pudo actualizar el profesor.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }
    }
})();