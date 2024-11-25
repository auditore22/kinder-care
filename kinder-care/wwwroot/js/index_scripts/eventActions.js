document.addEventListener('DOMContentLoaded', function () {
    // ============================ Event Deletion Start ==========================
    $('.delete-btn').on('click', function () {
        $(this).closest('.event-item').addClass('d-none');
    });
    // ============================ Event Deletion End ==========================
});
