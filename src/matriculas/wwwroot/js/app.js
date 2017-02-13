// app.js
(function () {
    "use strict";

    // Crea el módulo que incluye todos los controladores
    // angularUtils.directives.dirPagination: Incluye una directiva para crear paginaciones
    // simpleControls: Incluye directivas personalizadas
    angular.module("app", ['angularUtils.directives.dirPagination', 'simpleControls', 'checklist-model']);
})();