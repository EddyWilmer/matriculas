
// cronogramasMatriculasController.js

(function () {
    "use strict";
    
    // Llama al módulo principal e incluye el controlador
    angular.module("app")
        .controller("cronogramasMatriculasController", cronogramasMatriculasController);

    // Interactua con la Api cronogramasMatriculasController.cs
    function cronogramasMatriculasController($http) {
        var vm = this;
        vm.currentAnioAcademico = $("#anioAcademicoId").val();
        vm.cronogramasMatriculas = [];
        vm.isBusy = true;

        // Pagination
        vm.pageSize = 10;

        vm.currentCronogramaMatricula = {}; // Cronograma Matrícula actual para editar o eliminar
        vm.newCronogramaMatricula = {}; // Nuevo Cronograma Mátrícula para crear

        // Solicita los Cronogramas de Matrícula disponibles (Los que están con estado 1) de un Año Académico específico
        $http.get("/api/cronogramasMatriculas/anioAcademico/" + vm.currentAnioAcademico)
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.cronogramasMatriculas);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de cronogramas matrículas.");
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Agrega el Cronograma de Matrícula
        vm.addCronogramaMatricula = function () {
            vm.isBusy = true;   
            vm.errors = [];
            vm.newCronogramaMatricula.anioAcademicoId = vm.currentAnioAcademico;

            $http.post("/api/cronogramasMatriculas/crear", vm.newCronogramaMatricula)
                .then(function (response) {
                    // Success                                
                    vm.cronogramasMatriculas.push(response.data);
                    vm.newCronogramaMatricula = {}
                    
                    toastr.success("Se registró el cronograma de matrícula correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.fechasMessageValidation !== "undefined")
                        toastr.warning(vm.errors.fechasMessageValidation);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo registrar el cronograma de matrícula.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Elimina el Cronograma de Matrícula
        vm.deleteCronogramaMatricula = function (idCronogramaMatricula, nombre) {
            vm.isBusy = true;
            vm.currentCronogramaMatricula = { anioAcademicoId: idCronogramaMatricula, nombre: nombre };

            $http.post("/api/cronogramasMatriculas/eliminar", vm.currentCronogramaMatricula)
                .then(function (response) {
                    // Success    
                    var index = vm.cronogramasMatriculas.findIndex(obj => obj.id === vm.currentCronogramaMatricula.id);
                    vm.cronogramasMatriculas.splice(index, 1);

                    toastr.success("Se eliminó el cronograma de matrícula correctamente.");
                },
                function (error) {
                    // Failure
                    toastr.error("No se pudo eliminar el cronograma de matrícula.");                  
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        //Recupera los datos del Cronograma de Matrícula
        vm.getCronogramaMatricula = function (idAnioAcademico, nombre) {
            $http.get("/api/cronogramasMatriculas/" + idAnioAcademico + "/" + nombre)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.currentCronogramaMatricula);
                    vm.currentCronogramaMatricula.fechaInicio = new Date(vm.currentCronogramaMatricula.fechaInicio.replace(/-/g, '\/').replace(/T.+/, ''));
                    vm.currentCronogramaMatricula.fechaFin = new Date(vm.currentCronogramaMatricula.fechaFin.replace(/-/g, '\/').replace(/T.+/, ''));
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del cronograma de matrícula.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Actualiza del Año Académico
        vm.updateCronogramaMatricula = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.post("/api/cronogramasMatriculas/editar", vm.currentCronogramaMatricula)
                .then(function (response) {
                    // Success         
                    var index = vm.cronogramasMatriculas.findIndex(obj => obj.nombre === vm.currentCronogramaMatricula.nombre);
                    vm.cronogramasMatriculas[index] = response.data;
                    $('#datos-cronogramaMatricula').modal('hide');

                    toastr.success("Se actualizó el cronograma de matrícula correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.fechasMessageValidation !== "undefined")
                        toastr.warning(vm.errors.fechasMessageValidation);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo actualizar el cronograma de matrícula.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }
    }
})();