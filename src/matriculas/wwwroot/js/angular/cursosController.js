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

        // Solicita la lista de grados
        $http.get("/api/grados")
            .then(function (response) {
                // Success
                angular.copy(response.data, vm.grados);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de grados.");
            });

        // Agrega el curso
        vm.addCurso = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.post("/api/cursos/crear", vm.newCurso)
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

        // Elimina el curso
        vm.deleteCurso = function (idCurso) {
            vm.isBusy = true;
            vm.currentCurso = { id: idCurso }
            vm.errors = [];

            $http.post("/api/cursos/eliminar", vm.currentCurso)
                .then(function (response) {
                    // Success    
                    var index = vm.cursos.findIndex(obj => obj.id === vm.currentCurso.id);
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

        //Recupera los datos del colaborador
        vm.getCurso = function (id) {          
            $http.get("/api/cursos/" + id)
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

        //Recupera los datos del colaborador
        vm.getCursoProfesor = function (id) {
            $http.get("/api/cursos/profesor/" + id)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data.curso, vm.currentCurso);
                    angular.copy(response.data.profesor, vm.currentProfesor);

                    // Solicita la lista de profesores disponibles para un curso específico
                    $http.get("/api/cursos/profesores/" + id)
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
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del curso.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Actualiza del curso
        vm.updateCurso = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.post("/api/cursos/editar", vm.currentCurso)
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

        // Actualiza el profesor del curso
        vm.updateProfesor = function () {
            vm.isBusy = true;
            vm.errors = [];
           
            vm.aux = { profesor: vm.currentProfesor, curso: vm.currentCurso };

            $http.post("/api/cursos/profesor/editar", vm.aux)
                .then(function (response) {
                    // Success         
                    $('#datos-curso-profesor').modal('hide');

                    toastr.success("Se actualizó el profesor correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.anioAcademicoMessageValidation !== "undefined")
                        toastr.warning(vm.errors.anioAcademicoMessageValidation);

                    toastr.error("No se pudo actualizar el profesor.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }
    }
})();