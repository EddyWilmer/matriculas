
// cronogramasMatriculasController.js

(function () {
    "use strict";
    
    // Llama al módulo principal e incluye el controlador
    angular.module("app")
        .controller("cronogramasController", cronogramasController);

    // Interactua con la Api cronogramasMatriculasController.cs
    function cronogramasController($http) {
        var vm = this;
        vm.idAnioAcademico = $("#anioAcademicoId").val();

        vm.cronogramas = [];
        vm.isBusy = true;

        // Pagination
        vm.pageSize = 10;

        vm.currentCronograma = {}; // Cronograma Matrícula actual para editar o eliminar
        vm.newCronograma = {}; // Nuevo Cronograma Mátrícula para crear

        // Solicita los Cronogramas de Matrícula disponibles (Los que están con estado 1) de un Año Académico específico
        $http.get("/api/v2/aniosAcademicos/" + vm.idAnioAcademico + "/cronogramas")
       
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.cronogramas);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de cronogramas.");
            })
            .finally(function () {
                vm.isBusy = false;
                
            });

        //Recupera los datos del Cronograma de Matrícula
        vm.getCronograma = function (id) {
            $http.get("/api/v2/cronogramas/" + id)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.currentCronograma);
                    vm.currentCronograma.fechaInicio = new Date(vm.currentCronograma.fechaInicio.replace(/-/g, '\/').replace(/T.+/, ''));
                    vm.currentCronograma.fechaFin = new Date(vm.currentCronograma.fechaFin.replace(/-/g, '\/').replace(/T.+/, ''));
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del cronograma de matrícula.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Agrega el Cronograma de Matrícula
        vm.addCronograma = function () {
            vm.isBusy = true;   
            vm.errors = [];
            vm.newCronograma.anioAcademicoId = vm.idAnioAcademico;

            $http.post("/api/v2/cronogramas/", vm.newCronograma)
                .then(function (response) {
                    // Success                                
                    vm.cronogramas.push(response.data);
                    vm.newCronograma = {}
                    
                    toastr.success("Se registró el cronograma correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.fechasMessageValidation !== "undefined")
                        toastr.warning(vm.errors.fechasMessageValidation);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo registrar el cronograma.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Actualiza del Año Académico
        vm.updateCronograma = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.put("/api/v2/cronogramas/", vm.currentCronograma)
                .then(function (response) {
                    // Success         
                    var index = vm.cronogramas.findIndex(obj => obj.id === vm.currentCronograma.id);
                    vm.cronogramas[index] = response.data;
                    $('#datos-cronograma').modal('hide');

                    toastr.success("Se actualizó el cronograma correctamente.");
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

        // Elimina el Cronograma de Matrícula
        vm.deleteCronograma = function (id) {
            vm.isBusy = true;

            $http.delete("/api/v2/cronogramas/" + id)
                .then(function (response) {
                    // Success    
                    var index = vm.cronogramas.findIndex(obj => obj.id === vm.idAnioAcademico);
                    vm.cronogramas.splice(index, 1);

                    toastr.success("Se eliminó el cronograma.");
                },
                function (error) {
                    // Failure
                    toastr.error("No se pudo eliminar el cronograma.");                  
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }
    }
})();