document.addEventListener("DOMContentLoaded", function() {
    let role = localStorage.getItem('userType') || 1;
    let payments = document.getElementById('payments');
    let featuresPaymentsContent = document.getElementById('featuresPaymentsContent');

    if (role === '1') { 
        featuresPaymentsContent.innerHTML = `
                <section>
                    <ul class="actions">
                        <li><a href="/Payments/ManagePayments" class="button scrolly"><i class="fa-solid fa-money-check-alt large-icon"></i>Gestión de Pagos</a></li>
                    </ul>
                    <p>Gestiona todos los pagos y mensualidades dentro del sistema.</p>
                </section>
                <section>
                    <ul class="actions">
                        <li><a href="/Payments/FinanceDetails" class="button scrolly"><i class="fa-solid fa-file-invoice-dollar large-icon"></i>Detalles Financieros</a></li>
                    </ul>
                    <p>Consulta y administra los detalles financieros completos de todos los estudiantes.</p>
                </section>
            `;
    } else if (role === '3') { 
        featuresPaymentsContent.innerHTML = `
                <section>
                    <ul class="actions">
                        <li><a href="/Payments/FinanceDetails" class="button scrolly"><i class="fa-solid fa-file-invoice-dollar large-icon"></i>Detalles Financieros</a></li>
                    </ul>
                    <p>Consulta los detalles financieros de tus pagos.</p>
                </section>
            `;
    } else {
        payments.remove();
    }
});