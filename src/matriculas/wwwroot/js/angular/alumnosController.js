// alumnosController.js

(function () {
    "use strict";
    // Llama al módulo prinicipal e incluye el controlador
    angular.module("app")
        .controller("alumnosController", alumnosController);

    // Interactua con la Api alumnosController.cs
    function alumnosController($http) {
        var vm = this;
        vm.alumnos = [];
        vm.isBusy = true;

        // Pagination
        vm.pageSize = 10;

        vm.actualCount; // Cantidad actual de alumnos
        vm.currentAlumno = {}; // Alumno actual para editar o eliminar
        vm.newAlumno = { apoderado: {} }; // Nuevo alumno para crear
        
        // Solicita la lista de alumnos disponibles (Los que están con estado 1)
        $http.get("/api/v2/alumnos")
            .then(function (response) {
                // Success   
                angular.copy(response.data, vm.alumnos);
            }, function (error) {
                // Failure
                toastr.error("No se pudo cargar la información de alumnos.");
            })
            .finally(function () {
                vm.isBusy = false;
            });

    				//Recupera los datos del alumno
        vm.getAlumno = function (id) {
        				$http.get("/api/v2/alumnos/" + id)
                .then(function (response) {
                				// Success   
                				angular.copy(response.data, vm.currentAlumno);
                				vm.currentAlumno.fechaNacimiento = new Date(vm.currentAlumno.fechaNacimiento);
                }, function (error) {
                				// Failure
                				vm.errorMessage = "No se pudo cargar la información del alumno.";
                })
                .finally(function () {
                				vm.isBusy = false;
                });
        }

        // Agrega el alumno
        vm.addAlumno = function () {
            vm.isBusy = true;
            vm.errors = [];
            
            $http.post("/api/v2/alumnos", vm.newAlumno)
                .then(function (response) {
                    // Success                                
                    vm.alumnos.push(response.data);                  
                    vm.newAlumno = {}

                    toastr.success("Se registró el alumno correctamente.");
                },
                function (error) {
                    // Failure         
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.otros !== "undefined")
                        toastr.warning(vm.errors.otros);

                    toastr.error("No se pudo registrar el alumno");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }

    				// Actualiza del alumno
        vm.updateAlumno = function () {
        				vm.isBusy = true;
        				vm.errors = [];

        				$http.put("/api/v2/alumnos/", vm.currentAlumno)
                .then(function (response) {
                				// Success  
                				var index = vm.alumnos.findIndex(obj => obj.id === vm.currentAlumno.id);
                				vm.alumnos[index] = response.data;
                				$('#datos-alumno').modal('hide');

                				toastr.success("Se actualizó el alumno correctamente.");
                },
                function (error) {
                				// Failure                      
                				angular.copy(error.data, vm.errors);

                				if (typeof vm.errors.dniMessageValidation !== "undefined")
                								toastr.warning(vm.errors.dniMessageValidation);

                				toastr.error("No se pudo actualizar el alumno.");
                })
                .finally(function (error) {
                				vm.isBusy = false;
                });
        }

        // Elimina el alumno
        vm.deleteAlumno = function (idAlumno) {
            vm.isBusy = true;
            vm.currentAlumno = { id: idAlumno }
            $http.post("/api/alumnos/eliminar", vm.currentAlumno)
                .then(function (response) {
                    // Success            
                    var index = vm.alumnos.findIndex(obj => obj.id === vm.currentAlumno.id);
                    vm.alumnos.splice(index, 1);

                    toastr.success("Se eliminó el alumno correctamente.");
                },
                function (error) {
                    // Failure
                    toastr.error("No se pudo eliminar el alumno.");
                })
                .finally(function (error) {
                    vm.isBusy = false;
                });
        }
    

        
    }
})();