document.addEventListener("DOMContentLoaded", function() {
    let role = localStorage.getItem('userType') || 1;
    let featuresEventsContent = document.getElementById('featuresEventsContent');

    // Muestra la vista semanal en el div #weeklyCalendar con el mes, días de la semana, y los números de día
    let weeklyCalendar = document.getElementById('weeklyCalendar');
    let today = new Date();
    let daysOfWeek = ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'];
    let monthName = today.toLocaleString('default', { month: 'long' }).toUpperCase();

    // Calcular el primer día de la semana
    let startOfWeek = new Date(today);
    startOfWeek.setDate(today.getDate() - today.getDay());

    weeklyCalendar.innerHTML = `
    <div class="calendar-header">
        <h3>${monthName}</h3>
    </div>
    <table class="calendar-table">
        <thead>
            <tr>
                ${daysOfWeek.map((day, index) => {
        let currentDay = new Date(startOfWeek);
        currentDay.setDate(startOfWeek.getDate() + index);
        let dayNumber = currentDay.getDate();
        return `<th>${day} ${dayNumber}</th>`;
    }).join('')}
            </tr>
        </thead>
        <tbody>
            <tr>
                ${daysOfWeek.map((day, index) => {
        let currentDay = new Date(startOfWeek);
        currentDay.setDate(startOfWeek.getDate() + index);
        let isToday = currentDay.toDateString() === today.toDateString() ? 'today' : '';
        let isPast = currentDay < today ? 'past' : '';
        return `<td class="${isToday} ${isPast}">Evento ${index + 1}</td>`;
    }).join('')}
            </tr>
        </tbody>
    </table>
`;
    
    if (role === '1') {
        featuresEventsContent.innerHTML = `
        <div class="col-8 col-12-small">
            <section>
                <h3>Gestión de Eventos</h3>
                <p>Administra y programa nuevos eventos y actividades en el calendario.</p>
                <ul class="actions">
                    <li><a href="/Events/ManageEvents" class="button">
                    <i class="fa-solid fa-calendar-plus large-icon"></i>Gestionar Eventos</a></li>
                </ul>
            </section>
        </div>
    `;
    }
});