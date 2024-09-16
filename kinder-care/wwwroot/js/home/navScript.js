document.addEventListener('DOMContentLoaded', function () {
    let userType = localStorage.getItem('userType') || 1; 
    
    const navItems = ['navInicio', 'navUsers', 'navEnrollment', 'navPayments', 'navEvents', 'navReports', 'navConfig', 'navLogout'];
    navItems.forEach(item => document.getElementById(item).style.display = 'block');
    
    if (userType === "2") {
        document.getElementById('navPayments').style.display = 'none';
    }

    if (userType === "3") {
        document.getElementById('navUsers').style.display = 'none';
        document.getElementById('navReports').style.display = 'none';
    }
});
