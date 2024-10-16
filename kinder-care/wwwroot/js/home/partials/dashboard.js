document.addEventListener("DOMContentLoaded", function () {
    // Obtener el ID del rol desde el input oculto en la vista Razor
    let role = document.getElementById('userRoleId').value;
    let dashboardContent = document.getElementById('dashboardContent');

    if (role === '2') {
        dashboardContent.innerHTML = `
            <h1>Bienvenido Maestro</h1>
            <p>Este es tu panel de control. Aquí puedes gestionar las asistencias y el progreso académico de tus alumnos.</p>
            <ul class="actions">
                <li><a href="/Enrollment/AttendanceRecord" class="button scrolly">Gestionar Asistencias</a></li>
                <li><a href="/Enrollment/AcademicRecord" class="button scrolly">Gestionar Evaluaciones</a></li>
            </ul>
        `;
    } else if (role === '3') {
        dashboardContent.innerHTML = `
            <h1>Bienvenido Padre</h1>
            <p>Este es tu panel de control. Aquí puedes ver la información de tus hijos y sus actividades.</p>
            <ul class="actions">
                <li><a href="/Enrollment/StudentProfile" class="button scrolly">Ver Perfil de Hijos</a></li>
                <li><a href="/Events/Calendar" class="button scrolly">Ver Actividades</a></li>
                <li><a href="/Payments/FinanceDetails" class="button scrolly">Ver Pagos</a></li>
            </ul>
        `;
    } else {
        dashboardContent.innerHTML = `
            <h1>Bienvenido Administrador</h1>
            <p>Este es tu panel de control. Aquí puedes gestionar roles, expedientes y pagos.</p>
            <ul class="actions">
                <li><a href="/Users/ManageRoles" class="button scrolly">Gestionar Roles</a></li>
                <li><a href="/Users/ManageRecords" class="button scrolly">Gestionar Expedientes</a></li>
                <li><a href="/Payments/ManagePayments" class="button scrolly">Gestionar Pagos</a></li>
            </ul>
        `;
    }
});
