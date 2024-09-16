document.addEventListener("DOMContentLoaded", function() {
    let role = localStorage.getItem('userType') || 1;
    let usersSection = document.getElementById('users');

    if (role === '1') {
        usersSection.innerHTML = `
                <section>
                    <div class="content">
                        <div class="inner">
                            <h2>Gestión de Roles</h2>
                            <p>Accede a las herramientas de gestión de roles para administrar los permisos dentro del sistema.</p>
                            <ul class="actions">
                                <li><a href="/Users/ManageRoles" class="button">
                                <i class="fa-solid fa-users-gear large-icon"></i>Gestionar Roles</a></li>
                            </ul>
                        </div>
                    </div>
                </section>
                <section>
                    <div class="content">
                        <div class="inner">
                            <h2>Perfiles de Docentes</h2>
                            <p>Administra la información de los perfiles de los docentes, incluyendo datos personales y roles asignados.</p>
                            <ul class="actions">
                                <li><a href="/Users/ManageProfiles" class="button">
                                <i class="fa-solid fa-chalkboard-user large-icon"></i>Gestionar Perfiles de Docentes</a></li>
                            </ul>
                        </div>
                    </div>
                </section>

                <section>
                    <div class="content">
                        <div class="inner">
                            <h2>Expedientes de Niños</h2>
                            <p>Accede y gestiona los expedientes digitales de los niños, incluyendo información académica y médica relevante.</p>
                            <ul class="actions">
                                <li><a href="/Users/ManageRecords" class="button">
                                <i class="fa-solid fa-users-viewfinder large-icon"></i>Gestionar Expedientes de Niños</a></li>
                            </ul>
                        </div>
                    </div>
                </section>
            `;
    } else if (role === '2') {
        usersSection.innerHTML = `
                <section>
                    <div class="content">
                        <div class="inner">
                            <h2>Expedientes de Niños</h2>
                            <p>Accede y gestiona los expedientes digitales de los niños de tu grupo.</p>
                            <ul class="actions">
                                <li><a href="/Users/ManageRecords" class="button">
                                <i class="fa-solid fa-children large-icon"></i>Gestionar Expedientes de Niños</a></li>
                            </ul>
                        </div>
                    </div>
                </section>
            `;
    } else {
        usersSection.remove();
    }
});
