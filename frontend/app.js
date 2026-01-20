// app.js

const form = document.getElementById("user-form");
const message = document.getElementById("message");
const salaryInput = document.getElementById("salary");
const salaryDisplay = document.getElementById("salary-display");

// Actualizar display de salario
if (salaryInput) {
    salaryInput.addEventListener("input", (e) => {
        const value = parseInt(e.target.value);
        salaryDisplay.textContent = `$${value.toLocaleString()}`;
    });
}

// Función para mostrar mensajes en el DOM
function showMessage(text, type) {
    message.textContent = text;
    message.className = type; // "error" o "success"
}

// Mock simple de backend para POC
async function submitUser(user) {
    // Simula una respuesta exitosa después de 100ms
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve({
                ok: true,
                json: () => ({ id: 1, ...user })
            });
        }, 100);
    });
}

// Listener del formulario
form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const name = document.getElementById("name").value.trim();
    const email = document.getElementById("email").value.trim();
    const ageValue = document.getElementById("age").value.trim();
    const phone = document.getElementById("phone").value.trim();
    const address = document.getElementById("address").value.trim();
    const country = document.getElementById("country").value;
    const gender = document.querySelector('input[name="gender"]:checked');
    const birthDate = document.getElementById("birthDate").value;
    
    // Campos profesionales
    const company = document.getElementById("company").value.trim();
    const position = document.getElementById("position").value.trim();
    const experienceValue = document.getElementById("experience").value.trim();
    const languages = Array.from(document.querySelectorAll('input[name="language"]:checked')).map(l => l.value);
    const salary = document.getElementById("salary").value;
    const availability = document.getElementById("availability").value;
    const contractType = document.querySelector('input[name="contractType"]:checked');
    const bio = document.getElementById("bio").value.trim();
    const skills = document.getElementById("skills").value.trim().split(",").map(s => s.trim()).filter(s => s);
    
    // Consentimientos
    const terms = document.getElementById("terms").checked;
    const newsletter = document.getElementById("newsletter").checked;

    // Validación: campos obligatorios básicos
    if (!name || !email || !ageValue || !phone || !address) {
        showMessage("Todos los campos obligatorios deben estar completos", "error");
        return;
    }

    // Validación: país
    if (!country) {
        showMessage("Debe seleccionar un país/ciudad", "error");
        return;
    }

    // Validación: género
    if (!gender) {
        showMessage("Debe seleccionar un género", "error");
        return;
    }

    // Validación: fecha de nacimiento
    if (!birthDate) {
        showMessage("Debe ingresar una fecha de nacimiento", "error");
        return;
    }

    // Validación: campos profesionales
    if (!company || !position) {
        showMessage("Los campos de empresa y puesto son obligatorios", "error");
        return;
    }

    if (experienceValue === "") {
        showMessage("Debe indicar la experiencia en años", "error");
        return;
    }

    if (languages.length === 0) {
        showMessage("Debe seleccionar al menos un idioma", "error");
        return;
    }

    if (!availability) {
        showMessage("Debe seleccionar una disponibilidad", "error");
        return;
    }

    if (!contractType) {
        showMessage("Debe seleccionar un tipo de contrato", "error");
        return;
    }

    if (!bio) {
        showMessage("La biografía no puede estar vacía", "error");
        return;
    }

    if (skills.length === 0) {
        showMessage("Debe agregar al menos una habilidad", "error");
        return;
    }

    // Validación: términos y condiciones
    if (!terms) {
        showMessage("Debe aceptar los términos y condiciones", "error");
        return;
    }

    const age = Number(ageValue);
    const experience = Number(experienceValue);

    // Validación: edad mínima
    if (age < 18) {
        showMessage("La edad debe ser mayor o igual a 18", "error");
        return;
    }

    // Validación: teléfono
    const phoneRegex = /^\d{7,}$/;
    if (!phoneRegex.test(phone.replace(/\D/g, ""))) {
        showMessage("El teléfono debe contener al menos 7 dígitos", "error");
        return;
    }

    // Validación: experiencia
    if (experience < 0) {
        showMessage("La experiencia no puede ser negativa", "error");
        return;
    }

    try {
        // En POC usamos mock
        const response = await submitUser({
            name,
            email,
            age,
            phone,
            address,
            country,
            gender: gender.value,
            birthDate,
            company,
            position,
            experience,
            languages,
            salary,
            availability,
            contractType: contractType.value,
            bio,
            skills,
            newsletter
        });

        if (!response.ok) {
            throw new Error("Error al crear el usuario");
        }

        showMessage("Usuario creado correctamente", "success");
        form.reset();
        salaryDisplay.textContent = "$50,000";
    } catch (error) {
        showMessage(error.message, "error");
    }
});
