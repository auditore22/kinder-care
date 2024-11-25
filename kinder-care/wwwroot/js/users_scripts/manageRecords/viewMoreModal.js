document.addEventListener('DOMContentLoaded', function () {
    // Escuchar el evento de clic en los botones "Ver Más"
    document.querySelectorAll('.view-more-btn').forEach(function (button) {
        button.addEventListener('click', function () {
            // Obtener los datos desde los atributos `data-*` del botón
            let nombre = button.getAttribute('data-nombre');
            let fechaNacimiento = button.getAttribute('data-fechanacimiento');
            let direccion = button.getAttribute('data-direccion');
            let poliza = button.getAttribute('data-poliza');
            let alergia = button.getAttribute('data-alergia');
            let condicion = button.getAttribute('data-condicion');
            let medicamento = button.getAttribute('data-medicamento');
            let dosis = button.getAttribute('data-dosis');
            let contacto = button.getAttribute('data-contacto');
            let telefono = button.getAttribute('data-telefono');
            let relacion = button.getAttribute('data-relacion');

            // Rellenar el modal con los datos
            document.getElementById('modalNombre').innerText = nombre || 'N/A';
            document.getElementById('modalFechaNacimiento').innerText = fechaNacimiento || 'N/A';
            document.getElementById('modalDireccion').innerText = direccion || 'N/A';
            document.getElementById('modalPoliza').innerText = poliza || 'N/A';
            document.getElementById('modalNombreAlergia').innerText = alergia || 'N/A';
            document.getElementById('modalNombreCondicion').innerText = condicion || 'N/A';
            document.getElementById('modalNombreMedicamento').innerText = medicamento || 'N/A';
            document.getElementById('modalDosis').innerText = dosis || 'N/A';
            document.getElementById('modalNombreContacto').innerText = contacto || 'N/A';
            document.getElementById('modalTelefonoContacto').innerText = telefono || 'N/A';
            document.getElementById('modalRelacionContacto').innerText = relacion || 'N/A';
        });
    });
});
