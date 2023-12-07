$(document).ready(function () {
    $("#btnModal").on("click", function () {
        abriModal();
    });
    $("#brnGrabar").on("click", function () {
        grabar();
    });
    $("#cboDepartamento").on("change", function () {
        listarProvincias($("#cboDepartamento").val(), "cboProvincia", 0, function (result) {
            listarDistritos($("#cboProvincia").val(), "cboDistrito", 0, function (result) {
                
            }); 
        });
    });
    $("#cboProvincia").on("change", function () {
        listarDistritos($("#cboProvincia").val(), "cboDistrito", 0, function (result) {
            if (result == 1) {
               
            }
        });
    });
});
function grabar() {
    var flag = $("#flag").val();
    var type = "POST";
    var formData = new FormData();
    formData.append('Id', $("#codigo").val());
    formData.append('TipoDocumento', $("#txtTipoDocumento").val());
    formData.append('NumeroDocumento', $("#txtNumeroDocumento").val());
    formData.append('Nombres', $("#txtNombres").val());
    formData.append('Sexo', $("#txtSexo").val());
    formData.append('IdDepartamento', $("#cboDepartamento").val());
    formData.append('IdProvincia', $("#cboProvincia").val());
    formData.append('IdDistrito', $("#cboDistrito").val());
    /*
    var datos = {
        TipoDocumento: $("#txtTipoDocumento").val(),
        NumeroDocumento: $("#txtNumeroDocumento").val(),
        Nombres: $("#txtNombres").val(),
        Sexo: $("#txtSexo").val(),
        IdDepartamento: $("#cboDepartamento").val(),
        IdProvincia: $("#cboProvincia").val(),
        IdDistrito: $("#cboDistrito").val(),
        Id: $("#codigo").val()
    };*/
    if (flag == 1) {
        type = "POST";
        url = "/Trabajadores/Create";
    } else {
        type = "PUT";
        url = "/Trabajadores/Edit";
    }
    enviarAccion(url, type, formData, function (result) {
        if (result.success) {
            alert(result.mensaje);
            recargar();
        } else {
            alert(result.mensaje);
        }
    })
}
function abriModal() {
    listarDepartamentos("cboDepartamento", 0, function (result) {
        if (result==1) {
            listarProvincias($("#cboDepartamento").val(), "cboProvincia", 0, function (result) {
                if (result == 1) {
                    listarDistritos($("#cboProvincia").val(), "cboDistrito", 0, function (result) {
                        if (result == 1) {
                            limpiarInputs();
                            $("#flag").val(1);
                        }
                    });
                }
            });
        }
    });
}
function limpiarInputs() {
    $("#txtSexo").val("");
    $("#txtNombres").val("");
    $("#txtNumeroDocumento").val("");
    $("#txtTipoDocumento").val("");
    $("#codigo").val(0);
}
function recargar() {
    location.reload();
}
function obtenerDatos(codigo) {
    mostrarListado("/Trabajadores/Edit", { id: codigo }
, function (resultado) {
        if (resultado) {
            $("#txtSexo").val(resultado.sexo);
            $("#txtNombres").val(resultado.nombres);
            $("#txtNumeroDocumento").val(resultado.numeroDocumento);
            $("#txtTipoDocumento").val(resultado.tipoDocumento);
            $("#codigo").val(resultado.id);

            listarDepartamentos("cboDepartamento", resultado.idDepartamento, function (result) {
                if (result == 1) {
                    listarProvincias($("#cboDepartamento").val(), "cboProvincia", resultado.idProvincia, function (result) {
                        if (result == 1) {
                            listarDistritos($("#cboProvincia").val(), "cboDistrito", resultado.idDistrito, function (result) {
                                if (result == 1) {
                                    $("#flag").val(0);
                                    $("#mdlRegistro").modal("show");
                                }
                            });
                        }
                    });
                }
            });
        }
    })
}
function eliminar(codigo) {
    var type = "PUT";
    var url = "/Trabajadores/Delete";
    var formData = new FormData();
    formData.append('Id', codigo);

    if (confirm("¿Está seguro de eliminar el registro ? ") == true) {
        enviarAccion(url, type, formData, function (result) {
            if (result.success) {
                alert(result.mensaje);
                recargar();
            } else {
                alert(result.mensaje);
            }
        })
    } 
}