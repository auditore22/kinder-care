document.addEventListener('DOMContentLoaded', function () {
    // Inicializa DataTable
    let table = new DataTable('#studentTable', {
        destroy: true,
        paging: true,           // Activa la paginación
        pageLength: 10,         // Longitud de página por defecto
        lengthChange: false,    // Desactiva la opción de cambiar longitud en el footer de la tabla
        info: false,            // Vamos a personalizar la información nosotros mismos
        columnDefs: [
            { orderable: false, targets: [0, 6] } // Desactiva el ordenamiento en las columnas de checkbox y botón de ver más
        ]
    });

    // Elemento para mostrar la información de las entradas
    let entriesInfo = document.getElementById('entriesInfo');

    // Función para actualizar el texto de cuántas entradas se muestran
    function updateEntriesInfo() {
        let pageInfo = table.page.info();
        entriesInfo.textContent = `Mostrando ${pageInfo.start + 1} a ${pageInfo.end} de ${pageInfo.recordsTotal} estudiantes.`;
    }

    // Actualiza la información de las entradas al cargar la página
    updateEntriesInfo();

    // Actualiza la información de las entradas cuando cambie la página
    table.on('draw', function () {
        updateEntriesInfo();
    });

    // Actualiza el número de entradas mostradas
    document.getElementById('entriesSelect').addEventListener('change', function () {
        let selectedValue = parseInt(this.value, 10);
        table.page.len(selectedValue).draw();
    });
});
