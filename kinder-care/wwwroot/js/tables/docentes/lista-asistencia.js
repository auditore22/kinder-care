document.addEventListener('DOMContentLoaded', function () {
    const paginationContainer = document.querySelector('.pagination');
    const infoContainer = document.getElementById('infoContainer');
    const entriesSelect = document.getElementById('entriesSelect');

    const rows = Array.from(document.querySelectorAll('#attendanceTable2 tbody tr'));
    let pageSize = parseInt(entriesSelect.value);
    let currentPage = 1;

    function renderTable() {
        const total = rows.length;
        const totalPages = Math.ceil(total / pageSize);
        const start = (currentPage - 1) * pageSize;
        const end = start + pageSize;

        rows.forEach(row => row.style.display = 'none');
        rows.slice(start, end).forEach(row => row.style.display = '');

        infoContainer.textContent = `Mostrando ${start + 1} a ${Math.min(end, total)} de ${total} registros`;

        renderPagination(totalPages);
    }

    function renderPagination(totalPages) {
        paginationContainer.innerHTML = '';

        if (currentPage > 1) {
            paginationContainer.innerHTML += `<li class="page-item"><a href="#" data-page="${currentPage - 1}">Anterior</a></li>`;
        }

        for (let i = 1; i <= totalPages; i++) {
            paginationContainer.innerHTML += `<li class="page-item ${i === currentPage ? 'active' : ''}">
                <a href="#" data-page="${i}">${i}</a></li>`;
        }

        if (currentPage < totalPages) {
            paginationContainer.innerHTML += `<li class="page-item"><a href="#" data-page="${currentPage + 1}">Siguiente</a></li>`;
        }

        paginationContainer.querySelectorAll('a').forEach(link => {
            link.addEventListener('click', function (e) {
                e.preventDefault();
                currentPage = parseInt(link.dataset.page);
                renderTable();
            });
        });
    }

    entriesSelect.addEventListener('change', () => {
        pageSize = parseInt(entriesSelect.value);
        currentPage = 1;
        renderTable();
    });

    renderTable();
});