$(document).ready(function () {
    $("form").submit(function (e) {
        var password = $("#Contrasena").val(); // Obtener la contraseña ingresada

        // Comprobar la longitud y requisitos de la contraseña
        if (!isPasswordValid(password)) {
            alert("La contraseña debe estar entre 8 y 25 caracteres, y contener mínimo una letra y un número."); // Mensaje de error
            e.preventDefault(); // Evitar el envío del formulario
        }
    });
});

// Función para validar la contraseña
function isPasswordValid(password) {
    return password.length >= 8 && password.length <= 25 && hasLetter(password) && hasDigit(password);
}

function hasLetter(str) {
    return /[a-zA-Z]/.test(str); // Comprobar si hay al menos una letra
}

function hasDigit(str) {
    return /\d/.test(str); // Comprobar si hay al menos un dígito
}
