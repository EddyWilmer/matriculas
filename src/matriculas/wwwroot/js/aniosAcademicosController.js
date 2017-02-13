
// aniosAcademicosController.js

(function () {
    "use strict";
    
    // Llama al módulo principal e incluye el controlador
    angular.module("app")
        .controller("aniosAcademicosController", aniosAcademicosController);

    // Interactua con la Api aniosAcademicosController.cs
    function aniosAcademicosController($http) {
        var vm = this;
        vm.aniosAcademicos = [];
        vm.isBusy = true;

        // Pagination
        vm.pageSize = 10;

        vm.currentAnioAcademico = {}; // Año Académico actual para editar o eliminar
        vm.newAnioAcademico = {}; // Nuevo Año Académico para crear

        // Solicita los Años Académicos disponibles (Los que están con estado 1) 
        $http.get("/api/aniosAcademicos")
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.aniosAcademicos);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de años académicos.");
            })
            .finally(function () {
                vm.isBusy = false;
            });

        // Agrega el Año Académico
        vm.addAnioAcademico = function () {
            vm.isBusy = true;
            vm.errors = [];
            
            if (vm.newAnioAcademico.fechaInicio == null)
                vm.newAnioAcademico.fechaInicio = undefined;

            if (vm.newAnioAcademico.fechaFin == null)
                vm.newAnioAcademico.fechaFin = undefined;

            $http.post("/api/aniosAcademicos/crear", vm.newAnioAcademico)
                .then(function (response) {
                    // Success                                
                    vm.aniosAcademicos.push(response.data);
                    vm.newAnioAcademico = {};
                    
                    toastr.success("Se registró el año académico correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.fechasMessageValidation !== "undefined")
                        toastr.warning(vm.errors.fechasMessageValidation);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo registrar el año académico");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        // Elimina el Año Académico
        vm.deleteAnioAcademico = function (idAnioAcademico) {
            vm.isBusy = true;
            vm.currentAnioAcademico = { id: idAnioAcademico };

            $http.post("/api/aniosAcademicos/eliminar", vm.currentAnioAcademico)
                .then(function (response) {
                    // Success    
                    var index = vm.aniosAcademicos.findIndex(obj => obj.id === vm.currentAnioAcademico.id);
                    vm.aniosAcademicos.splice(index, 1);

                    toastr.success("Se eliminó el año académico correctamente.");
                },
                function (error) {
                    // Failure
                    toastr.error("No se pudo eliminar el año académico.");                  
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

        //Recupera los datos del Año Académico
        vm.getAnioAcademico = function (idAnioAcademico) {
            $http.get("/api/aniosAcademicos/" + idAnioAcademico)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.currentAnioAcademico);
                    vm.currentAnioAcademico.fechaInicio = new Date(vm.currentAnioAcademico.fechaInicio.replace(/-/g, '\/').replace(/T.+/, ''));
                    vm.currentAnioAcademico.fechaFin = new Date(vm.currentAnioAcademico.fechaFin.replace(/-/g, '\/').replace(/T.+/, ''));
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del año académico.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Actualiza del Año Académico
        vm.updateAnioAcademico = function () {
            vm.isBusy = true;
            vm.errors = [];

            $http.post("/api/aniosAcademicos/editar", vm.currentAnioAcademico)
                .then(function (response) {
                    // Success         
                    var index = vm.aniosAcademicos.findIndex(obj => obj.id === vm.currentAnioAcademico.id);
                    vm.aniosAcademicos[index] = response.data;
                    $('#datos-anioAcademico').modal('hide');

                    toastr.success("Se actualizó el año académico correctamente.");
                },
                function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);
                    
                    if (typeof vm.errors.fechasMessageValidation !== "undefined")
                        toastr.warning(vm.errors.fechasMessageValidation);

                    if (typeof vm.errors.nombreMessageValidation !== "undefined")
                        toastr.warning(vm.errors.nombreMessageValidation);

                    toastr.error("No se pudo actualizar el año académico.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }
    }
})();