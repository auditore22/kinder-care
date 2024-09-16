document.addEventListener("DOMContentLoaded", function() {
    let role = localStorage.getItem('userType') || '1'; 
    let currentSection = localStorage.getItem('currentSection');

    document.querySelectorAll('#dynamicNav .nav-item').forEach(function(item) {
        if (!item.classList.contains('home-item')) {
            item.style.display = 'none';
        }
    });

    
    if (role === '1') { 
        if (currentSection.startsWith("ManageRoles") || currentSection.startsWith("ManageProfiles") || currentSection.startsWith("ManageRecords")) {
            document.querySelector('.roles-item').style.display = 'block';
            document.querySelector('.profiles-item').style.display = 'block';
            document.querySelector('.records-item').style.display = 'block';
        } else if (currentSection.startsWith("ManageEnrollments") || currentSection.startsWith("StudentProfile") || currentSection.startsWith("AcademicRecord") || currentSection.startsWith("AttendanceRecord")) {
            document.querySelector('.enrollment-manage-item').style.display = 'block';
            document.querySelector('.student-profile-item').style.display = 'block';
            document.querySelector('.academic-record-item').style.display = 'block';
            document.querySelector('.attendance-record-item').style.display = 'block';
        } else if (currentSection.startsWith("ManagePayments") || currentSection.startsWith("FinanceDetails")) {
            document.querySelector('.payments-item').style.display = 'block';
            document.querySelector('.finance-details-item').style.display = 'block';
        } else if (currentSection.startsWith("Calendar") || currentSection.startsWith("ManageEvents")) {
            document.querySelector('.events-item').style.display = 'block';
            document.querySelector('.manage-events-item').style.display = 'block';
        } else if (currentSection.startsWith("GenerateReports")) {
            document.querySelector('.reports-item').style.display = 'block';
        } else if (currentSection.startsWith("SystemConfig") || currentSection.startsWith("UserProfile")) {
            document.querySelector('.system-config-item').style.display = 'block';
            document.querySelector('.profile-item').style.display = 'block';
        }
    } else if (role === '2') {
        if (currentSection.startsWith("StudentProfile") || currentSection.startsWith("AcademicRecord") || currentSection.startsWith("AttendanceRecord")) {
            document.querySelector('.student-profile-item').style.display = 'block';
            document.querySelector('.academic-record-item').style.display = 'block';
            document.querySelector('.attendance-record-item').style.display = 'block';
        } else if (currentSection.startsWith("Calendar")) {
            document.querySelector('.events-item').style.display = 'block';
        } else if (currentSection.startsWith("GenerateReports")) {
            document.querySelector('.reports-item').style.display = 'block';
        } else if (currentSection.startsWith("UserProfile")) {
            document.querySelector('.profile-item').style.display = 'block';
        }
    } else if (role === '3') {
        if (currentSection.startsWith("StudentProfile") || currentSection.startsWith("AcademicRecord") || currentSection.startsWith("AttendanceRecord")) {
            document.querySelector('.student-profile-item').style.display = 'block';
            document.querySelector('.academic-record-item').style.display = 'block';
            document.querySelector('.attendance-record-item').style.display = 'block';
        } else if (currentSection.startsWith("FinanceDetails")) {
            document.querySelector('.finance-details-item').style.display = 'block';
        } else if (currentSection.startsWith("Calendar")) {
            document.querySelector('.events-item').style.display = 'block';
        } else if (currentSection.startsWith("UserProfile")) {
            document.querySelector('.profile-item').style.display = 'block';
        }
    }

    document.querySelectorAll('#dynamicNav .nav-item').forEach(function(item) {
        const link = item.querySelector('a');
        if (link && link.href.includes(currentSection)) {
            item.classList.add('active');
        }
    });
});
