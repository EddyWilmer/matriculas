// simpleControls.js

(function () {
    "use strict";
    
    // La directiva waitCursor oculta el Div cuando esté procesando la solicitud
    angular.module("simpleControls", [])
        .directive("waitCursor", function () {
            return {
                scope: {
                    show: "=displayWhen"  
                },
                restrict: "E",
                templateUrl: "/views/waitCursor.html"
            };
        });
})();