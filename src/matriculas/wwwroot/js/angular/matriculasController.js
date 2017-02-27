// matriculasController.js

(function () {
    "use strict";
    // Llama al módulo prinicipal e incluye el controlador
    angular.module("app")
        .controller("matriculasController", matriculasController);

    // Interactua con la Api matriculasController.cs
    function matriculasController($http) {
        var vm = this;
        vm.alumnos = [];
        vm.notas = [];
        vm.deudas = [];
        vm.isBusy = true;

        // Pagination
        vm.pageSize = 10;

        vm.actualCount; // Cantidad actual de alumnos
        vm.currentAlumno = {}; // Alumno actual para editar o eliminar
        vm.newAlumno = { apoderado: {} }; // Nuevo alumno para crear
        vm.alumno = {};
        vm.matricula = {};
        vm.nextGrado = {};
        vm.nextCursos = [];
        vm.isMatriculado = false;

        $(document).ready(function () {
            //Wizard
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {

                var $target = $(e.target);

                if ($target.parent().hasClass('disabled')) {
                    return false;
                }
            })

            $("#btn-step1").click(function (e) {
                if (vm.notas.length > 0) {
                    enableNextTab();
                }
                else if (typeof (vm.alumno.dni) !== "undefined") {
                    var $active = $('.wizard .nav-tabs li.active');
                    $active.next().next().next().removeClass('disabled');
                    nextTab($active.next().next());
                }
            });

            $("#btn-step2").click(function (e) {
                if (vm.deudas.length > 0) {
                    enableNextTab();
                }
            });

            $("#btn-step3").click(function (e) {
                enableNextTab();
            });
        });

        function enableNextTab() {
            var $active = $('.wizard .nav-tabs li.active');
            $active.next().removeClass('disabled');
            nextTab($active);
        }

        function nextTab(elem) {
            $(elem).next().find('a[data-toggle="tab"]').click();
        }
        function prevTab(elem) {
            $(elem).prev().find('a[data-toggle="tab"]').click();
        }

        //Notas del alumno 
        vm.getMatricula = function (idAlumno) {
            $http.get("/api/v2/alumnos/matricula/" + idAlumno)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.matricula);
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del alumno.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        //Notas del alumno 
        vm.getNextGrado = function (idAlumno) {
            $http.get("/api/alumnos/nextGrado/" + idAlumno)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.nextGrado);

                    $http.get("/api/grados/cursos/" + vm.nextGrado.id)
                        .then(function (response) {
                            // Success   
                            angular.copy(response.data, vm.nextCursos);
                        }, function (error) {
                            // Failure
                            vm.errorMessage = "No se pudo cargar la información del alumno.";
                        })
                        .finally(function () {
                            vm.isBusy = false;
                        });
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del alumno.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        //Notas del alumno 
        vm.getNotas = function (idAlumno) {
            $http.get("/api/alumnos/notas/" + idAlumno)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.notas);
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del alumno.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        //Notas del alumno 
        vm.getDeudas = function (idAlumno) {
            $http.get("/api/alumnos/deudas/" + idAlumno)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.deudas);
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del alumno.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        //Recupera los datos del alumno
        vm.getAlumno = function (id) {
            $http.get("/api/alumnos/" + id)
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

        //Recupera los datos del alumno
        vm.getAlumnoByDni = function () {
            vm.alumno = {};
            vm.notas = [];
            vm.deudas = [];
            $http.get("/api/v2/alumnos/dni/" + vm.search)
                .then(function (response) {
                    // Success   
                    angular.copy(response.data, vm.alumno);
                    vm.alumno.fechaNacimiento = new Date(vm.alumno.fechaNacimiento);

                    vm.getMatricula(vm.alumno.id);
                    vm.getNotas(vm.alumno.id);
                    vm.getDeudas(vm.alumno.id);
                    vm.getNextGrado(vm.alumno.id);
                }, function (error) {
                    // Failure
                    vm.errorMessage = "No se pudo cargar la información del alumno.";
                })
                .finally(function () {
                    vm.isBusy = false;
                    // Reset los tabs
                    var $active = $('.wizard .nav-tabs li');
                    $active.addClass('disabled');
                    $('.wizard .nav-tabs li:first-child').removeClass('disabled');
                    vm.isMatriculado = false;
                });
        }

        //Recupera los datos del alumno
        vm.procesarMatricula = function (id) {
            vm.isBusy = true;
            vm.errors = [];
            vm.isMatriculado = false;

            $http.get("/api/alumnos/matriculas/" + id)
                .then(function (response) {
                    // Success   
                    toastr.success("Se matriculó al alumno correctamente.");
                    vm.isMatriculado = true;
                }, function (error) {
                    // Failure
                    angular.copy(error.data, vm.errors);

                    if (typeof vm.errors.seccionesDisponiblesMessage !== "undefined")
                        toastr.warning(vm.errors.seccionesDisponiblesMessage);

                    if (typeof vm.errors.alumnoMatriculadoMessage !== "undefined")
                        toastr.warning(vm.errors.alumnoMatriculadoMessage);

                    if (typeof vm.errors.cronogramaMessage !== "undefined")
                        toastr.warning(vm.errors.cronogramaMessage)

                    toastr.error("No se pudo registrar la matricula.");
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

        // Actualiza del alumno
        vm.updateAlumno = function () {
            vm.isBusy = true;
            vm.errors = [];
            
            $http.post("/api/alumnos/editar", vm.currentAlumno)
                .then(function (response) {
                    // Success  
                    vm.alumno = response.data;
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
    }
})();