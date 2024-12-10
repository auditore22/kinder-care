document.addEventListener('DOMContentLoaded', function () {
    // Inicializa DataTable
    let table = new DataTable('#studentTable', {
        destroy: true,
        paging: true,           // Activa la paginaci�n
        pageLength: 10,         // Longitud de p�gina por defecto
        lengthChange: false,    // Desactiva la opci�n de cambiar longitud en el footer de la tabla
        info: false,            // Vamos a personalizar la informaci�n nosotros mismos
        columnDefs: [
            { orderable: false, targets: [0, 6] } // Desactiva el ordenamiento en las columnas de checkbox y bot�n de ver m�s
        ]
    });

    // Elemento para mostrar la informaci�n de las entradas
    let entriesInfo = document.getElementById('entriesInfo');

    // Funci�n para actualizar el texto de cu�ntas entradas se muestran
    function updateEntriesInfo() {
        let pageInfo = table.page.info();
        entriesInfo.textContent = `Mostrando ${pageInfo.start + 1} a ${pageInfo.end} de ${pageInfo.recordsTotal} estudiantes.`;
    }

    // Actualiza la informaci�n de las entradas al cargar la p�gina
    updateEntriesInfo();

    // Actualiza la informaci�n de las entradas cuando cambie la p�gina
    table.on('draw', function () {
        updateEntriesInfo();
    });

    // Actualiza el n�mero de entradas mostradas
    document.getElementById('entriesSelect').addEventListener('change', function () {
        let selectedValue = parseInt(this.value, 10);
        table.page.len(selectedValue).draw();
    });
});
