// Rellenar a la izquierda
// n: El caracter fijo
// width: El tamaño de la cadena
// z: El caracter de relleno
function lpad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function collapseAccordion() {
    var $myGroup = $('.custom-accordion');
    $myGroup.find('.collapse.in').collapse('hide');
}

function resetFormMatricula() {
    $("#toggle-button").bootstrapSwitch('state', false);
}

//Configuración de toastr plugin
jQuery(document).ready(function ($) {
    $('.cancel').on('click', function(){
        var $form = $("form");
        var $validator = $form.validate();
        var $errors = $form.find(".field-validation-error span");
        $errors.each(function () { $validator.settings.success($(this)); })
        $validator.resetForm();
    });

    $('.reset').on('click', function () {
        var $form = $("form");
        var $validator = $form.validate();
        var $errors = $form.find(".field-validation-error span");
        $errors.each(function () { $validator.settings.success($(this)); })
        $validator.resetForm();

        $("form").find("select, input[type='text'], input[type='number'], input[type='date']").val("");
        $("form").find("input:checkbox").removeAttr("checked");

        var $myGroup = $('.custom-accordion');
        $myGroup.find('.collapse.in').collapse('hide');
    });

    $(document).keyup(function (e) {
        if (e.keyCode === 27) {
            $('.cancel').click();
        }
    });

	$('.counter').counterUp({
		delay: 10,
		time: 1000
	});

	toastr.options = {
	    "closeButton": false,
	    "debug": false,
	    "newestOnTop": true,
	    "progressBar": false,
	    "positionClass": "toast-bottom-right",
	    "preventDuplicates": false,
	    "showDuration": "300",
	    "hideDuration": "1000",
	    "timeOut": "5000",
	    "extendedTimeOut": "1000",
	    "showEasing": "swing",
	    "hideEasing": "linear",
	    "showMethod": "fadeIn",
	    "hideMethod": "fadeOut"
	}

	$("#toggle-button").bootstrapSwitch();
	$("#toggle-button").bootstrapSwitch('onColor', 'retroorange');
	
	$('#current-grado').prop('disabled', true);
	var elementFormMatricula = $("#form-matricula input[type='text']:not('#current-grado'), #form-matricula select, #form-matricula input[type='date']");
    elementFormMatricula.prop('disabled', true);

	$("#toggle-button").on('switchChange.bootstrapSwitch', function (event, state) {
	    elementFormMatricula.prop('disabled', !state);
	});
});

