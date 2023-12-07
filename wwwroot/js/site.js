// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var baseUrl = window.location.origin;
var token = $('input[name="__RequestVerificationToken"]').val();

function enviarAccion(url, type, parametros,callback) {
    $.ajax({
        url: baseUrl+url,
        type: type,
        data: parametros,
        headers: {
            'RequestVerificationToken': token
        },
        processData: false,  // Importante cuando se usa FormData
        contentType: false,  // Importante cuando se usa FormData
        success: function (data) {
            console.log(data);
            callback(data);
        },
        error: function (error) {
            // Maneja el error
            console.error(error);
        }
    });
}
function mostrarListado(url, parametros, callback) {
    $.ajax({
        url: baseUrl + url,
        type: "GET",
        data: parametros,

        success: function (data) {
            //  console.error(data);
            callback(data);
        },
        error: function (log) {
            console.log("Error al llamar al servidor." + log);
        }
    });
}
function listarCombo(url, type, parametros,callback) {
    $.ajax({
        url: baseUrl+url,
        type: type,
        data: parametros,
    
        success: function (data) {
          //  console.error(data);
            callback(data);
        },
        error: function () {
          //  console.log("Error al llamar al servidor.");
        }
    });
}
function listarDistritos(idProvincia, control, value, callback) {
    listarCombo("/Distritoes/ListPorProvincia", "GET", { idProvincia: idProvincia }, function (resultado) {
        if (resultado) {
            var html = "";
            html = html + "<option value='" + 0 + "'>Seleccione</option>";
            $.each(resultado, function (index, elemento) {
                var select = "";
                if (value == elemento.id) select = "selected";
                html = html + "<option value='" + elemento.id + "'" + select + ">" + elemento.nombreDistrito + "</option>";
            });
            $("#" + control).html(html);
            callback(1);
        }
    })
}
function listarProvincias(idDepartamento, control, value, callback) {
    listarCombo("/Provinciums/ListarPorDepartamento", "GET", { idDepartamento: idDepartamento }, function (resultado) {
        if (resultado) {
            var html = "";
            html = html + "<option value='" + 0 + "'>Seleccione</option>";
            $.each(resultado, function (index, elemento) {
                var select = "";
                if (value == elemento.id) select = "selected";
                html = html + "<option value='" + elemento.id + "'" + select + ">" + elemento.nombreProvincia + "</option>";
            });
            $("#" + control).html(html);
            callback(1);
        }
    })
}
function listarDepartamentos(control, value, callback) {
    listarCombo("/Departamentoes/List", "GET", {}, function (resultado) {
        if (resultado) {
            var html = "";
            html = html + "<option value='" + 0 + "'>Seleccione</option>";
            $.each(resultado, function (index, elemento) {
                var select = "";
                if (value == elemento.id) select = "selected";
                html = html + "<option value='" + elemento.id + "'" + select + ">" + elemento.nombreDepartamento + "</option>";
            });
            $("#" + control).html(html);
            callback(1);
        }
    })
}