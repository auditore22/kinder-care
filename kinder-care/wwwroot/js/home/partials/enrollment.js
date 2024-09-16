document.addEventListener("DOMContentLoaded", function() {
    let role = localStorage.getItem('userType') || 1;
    let featuresContent = document.getElementById('featuresContent');

    if (role === '1') { 
        featuresContent.innerHTML = `
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/ManageEnrollments" class="button scrolly"><i class="fa-solid fa-user-cog large-icon"></i> Gestión de Matrículas</a></li>
                    </ul>
                    <p>Gestiona las matrículas de todos los niños en el sistema.</p>
                </section>
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/StudentProfile" class="button scrolly"><i class="fa-solid fa-id-card large-icon"></i>Perfil de Estudiantes</a></li>
                    </ul>
                    <p>Accede a la información completa del perfil de cada niño.</p>
                </section>
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/AcademicRecord" class="button scrolly"><i class="fa-solid fa-book-open large-icon"></i>Registro Académico</a></li>
                    </ul>
                    <p>Administra y consulta el registro académico de todos los niños.</p>
                </section>
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/AttendanceRecord" class="button scrolly"><i class="fa-solid fa-calendar-check large-icon"></i>Registro de Asistencia</a></li>
                    </ul>
                    <p>Gestiona y revisa la asistencia de todos los niños.</p>
                </section>
            `;
    } else if (role === '2') {
        featuresContent.innerHTML = `
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/StudentProfile" class="button scrolly"><i class="fa-solid fa-id-badge large-icon"></i>Perfil de Estudiantes</a></li>
                    </ul>
                    <p>Consulta y gestiona el perfil de los niños de tu grupo.</p>
                </section>
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/AcademicRecord" class="button scrolly"><i class="fa-solid fa-chalkboard-teacher large-icon"></i>Registro Académico</a></li>
                    </ul>
                    <p>Gestiona y revisa el registro académico de tu grupo de estudiantes.</p>
                </section>
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/AttendanceRecord" class="button scrolly"><i class="fa-solid fa-user-check large-icon"></i>Registro de Asistencia</a></li>
                    </ul>
                    <p>Revisa y administra la asistencia de los niños en tu grupo.</p>
                </section>
            `;
    } else if (role === '3') {
        featuresContent.innerHTML = `
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/StudentProfile" class="button scrolly"><i class="fa-solid fa-user large-icon"></i>Perfil del Niño</a></li>
                    </ul>
                    <p>Consulta el perfil y la información de tus hijos.</p>
                </section>
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/AcademicRecord" class="button scrolly"><i class="fa-solid fa-graduation-cap large-icon"></i>Registro Académico</a></li>
                    </ul>
                    <p>Revisa el registro académico de tus hijos.</p>
                </section>
                <section>
                    <ul class="actions">
                        <li><a href="/Enrollment/AttendanceRecord" class="button scrolly"><i class="fa-solid fa-calendar-alt large-icon"></i>Registro de Asistencia</a></li>
                    </ul>
                    <p>Consulta la asistencia de tus hijos.</p>
                </section>
            `;
    } else {
        featuresContent.remove();
    }
});
