// ========================== Table Interactions Script ===========================
$('#selectAll').on('change', function () {
    $('.form-check .form-check-input').prop('checked', $(this).prop('checked'));
});

new DataTable('#studentTable', {
    searching: false,
    lengthChange: false,
    info: false,
    paging: false,
    "columnDefs": [
        { "orderable": false, "targets": [0, 6] }
    ]
});
