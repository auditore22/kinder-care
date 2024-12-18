document.addEventListener('DOMContentLoaded', function () {
    const table = document.getElementById('studentTable');
    
    const searchByName = document.getElementById('searchByName');
    const searchByCedula = document.getElementById('searchByCedula');
    
    const paginationContainer = document.querySelector('.pagination');
    const infoContainer = document.querySelector('#infoContainer');
    const entriesSelect = document.getElementById('entriesSelect');

    let rows = Array.from(document.querySelectorAll('#studentTable tbody tr')).filter(row => !row.id || row.id !== 'noResultsRow');
    let pageSize = parseInt(entriesSelect.value);
    let currentPage = 1;
    let filteredRows = [...rows];
    
    function filterRows() {
        const nameValue = searchByName.value.toLowerCase();
        const cedulaValue = searchByCedula.value.toLowerCase();

        filteredRows = rows.filter(row => {
            const nameColumn = row.querySelector('[data-column="name"]')?.textContent?.toLowerCase() || "";
            const cedulaColumn = row.querySelector('[data-column="cedula"]')?.textContent?.toLowerCase() || ""
            return (nameColumn.includes(nameValue) || nameValue === "") &&
                (cedulaColumn.includes(cedulaValue) || cedulaValue === "");
        });

        currentPage = 1;
        renderTable();
    }
    
    function renderTable() {
        const totalFilteredItems = filteredRows.length;
        const totalPages = Math.ceil(totalFilteredItems / pageSize);
        const start = (currentPage - 1) * pageSize;
        const end = start + pageSize;
        const noResultsRow = document.getElementById('noResultsRow');
        
        if (totalFilteredItems === 0) {
            noResultsRow.classList.remove('d-none');
        } else {
            noResultsRow.classList.add('d-none');
        }

        rows.forEach(row => row.style.display = 'none');
        filteredRows.slice(start, end).forEach(row => row.style.display = '');
        
        infoContainer.textContent = totalFilteredItems > 0
            ? `Mostrando ${start + 1} a ${Math.min(end, totalFilteredItems)} de ${totalFilteredItems} registros`
            : "No se encontraron resultados.";
        
        renderPagination(totalPages);
        
        table.style.display = 'table';
    }
    
    function renderPagination(totalPages) {
        paginationContainer.innerHTML = '';
        
        if (totalPages === 0) return;

        if (currentPage > 1) {
            const prevButton = document.createElement('li');
            prevButton.className = 'page-item';
            prevButton.innerHTML = `<a class="page-link" href="#">Anterior</a>`;
            prevButton.querySelector('a').addEventListener('click', (e) => {
                e.preventDefault();
                currentPage -= 1;
                renderTable();
            });
            paginationContainer.appendChild(prevButton);
        }

        for (let i = 1; i <= totalPages; i++) {
            const pageButton = document.createElement('li');
            pageButton.className = `page-item ${i === currentPage ? 'active' : ''}`;
            pageButton.innerHTML = `<a class="page-link" href="#">${i}</a>`;
            pageButton.querySelector('a').addEventListener('click', (e) => {
                e.preventDefault();
                currentPage = i;
                renderTable();
            });
            paginationContainer.appendChild(pageButton);
        }

        if (currentPage < totalPages) {
            const nextButton = document.createElement('li');
            nextButton.className = 'page-item';
            nextButton.innerHTML = `<a class="page-link" href="#">Siguiente</a>`;
            nextButton.querySelector('a').addEventListener('click', (e) => {
                e.preventDefault();
                currentPage += 1;
                renderTable();
            });
            paginationContainer.appendChild(nextButton);
        }
    }

    entriesSelect.addEventListener('change', () => {
        pageSize = parseInt(entriesSelect.value);
        currentPage = 1;
        renderTable();
    });

    searchByName.addEventListener('keyup', filterRows);
    searchByCedula.addEventListener('keyup', filterRows);

    renderTable();
});