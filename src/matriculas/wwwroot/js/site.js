// Rellenar a la izquierda
// n: El caracter fijo
// width: El tamaño de la cadena
// z: El caracter de relleno
function lpad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function cleanForm() {
    var $form = $("form");
    var $validator = $form.validate();
    var $errors = $form.find(".field-validation-error span");
    $errors.each(function () { $validator.settings.success($(this)); })
    $validator.resetForm();

    $("form").find("select, input[type='text'], input[type='number'], input[type='date']").val("");
    $("form").find("input:checkbox").removeAttr("checked");

    var $myGroup = $('.custom-accordion');
    $myGroup.find('.collapse.in').collapse('hide');
}

function collapseAccordion() {
    var $myGroup = $('.custom-accordion');
    $myGroup.find('.collapse.in').collapse('hide');
}

jQuery(document).ready(function ($) {
				$('.counter').counterUp({
								delay: 10,
								time: 1000
				});
});