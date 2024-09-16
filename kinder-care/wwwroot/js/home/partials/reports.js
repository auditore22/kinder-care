document.addEventListener("DOMContentLoaded", function() {
    let role = localStorage.getItem('userType') || 1;
    let reportOptionsContent = document.getElementById('reportOptionsContent');
    
    if (role === '1' || role === '2') { 
        reportOptionsContent.innerHTML = `
        <div class="col-4 col-12-small">
            <section>
                <ul class="actions">
                    <li><a href="/Reports/GenerateReports" class="button scrolly"><i class="fa-solid fa-file-alt large-icon"></i>Generar Reporte General</a></li>
                </ul>
                <p>Genera un reporte completo de las actividades y el desempeño de los estudiantes.</p>
            </section>
        </div>
        `;
    } else {
        reportOptionsContent.remove();
    }
});
