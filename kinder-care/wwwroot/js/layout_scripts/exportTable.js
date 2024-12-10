// ========================== Export Table Script ===========================
document.getElementById('exportOptions').addEventListener('change', function() {
    const format = this.value;
    const table = document.getElementById('studentTable');
    let data = [];
    const headers = [];

    // Get the table headers
    table.querySelectorAll('thead th').forEach(th => {
        headers.push(th.innerText.trim());
    });

    // Get the table rows
    table.querySelectorAll('tbody tr').forEach(tr => {
        const row = {};
        tr.querySelectorAll('td').forEach((td, index) => {
            row[headers[index]] = td.innerText.trim();
        });
        data.push(row);
    });

    if (format === 'csv') {
        downloadCSV(data);
    } else if (format === 'json') {
        downloadJSON(data);
    }
});

function downloadCSV(data) {
    const csv = data.map(row => Object.values(row).join(',')).join('\n');
    const blob = new Blob([csv], { type: 'text/csv' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'students.csv';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}

function downloadJSON(data) {
    const json = JSON.stringify(data, null, 2);
    const blob = new Blob([json], { type: 'application/json' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'students.json';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}
