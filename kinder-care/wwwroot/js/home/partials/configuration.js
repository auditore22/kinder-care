document.addEventListener("DOMContentLoaded", function () {
    let role = localStorage.getItem('userType') || 1;
    let configurationOptionsContent = document.getElementById('configurationOptionsContent');
    
    if (role === '1') { 
        configurationOptionsContent.innerHTML = `
        <div class="col-6 col-12-small">
            <section>
                <ul class="actions">
                    <li><a href="/Configuration/SystemConfig" class="button scrolly"><i class="fa-solid fa-cog large-icon"></i>Configuración del Sistema</a></li>
                </ul>
                <p>Accede a las configuraciones avanzadas del sistema.</p>
            </section>
        </div>
        `;
    }
    
    configurationOptionsContent.innerHTML += `
    <div class="col-6 col-12-small">
        <section>
            <ul class="actions">
                <li><a href="/Configuration/UserProfile" class="button scrolly"><i class="fa-solid fa-user-cog large-icon"></i>Perfil de Usuario</a></li>
            </ul>
            <p>Gestiona y actualiza la información de tu perfil.</p>
        </section>
    </div>
    `;
});
