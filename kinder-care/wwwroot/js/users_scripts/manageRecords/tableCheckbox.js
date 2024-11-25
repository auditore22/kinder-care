document.addEventListener('DOMContentLoaded', function () {
    // ============================ Checkbox Select All Start ============================
    document.getElementById('selectAll').addEventListener('change', function () {
        document.querySelectorAll('.form-check .form-check-input').forEach(checkbox => {
            checkbox.checked = this.checked;
        });
    });
    // ============================ Checkbox Select All End ============================
});
