const form = document.querySelector("form");
const email = document.getElementById("email");
const subject = document.getElementById("subject");
const message = document.getElementById("message");
const captcha = document.getElementById("captcha");
const send = document.getElementById("submit");
const randomNumber = Math.floor(Math.random() * 90000) + 10000;

const emailError = document.querySelector("#email + span.emailError");
const captchaError = document.querySelector("#captcha + span.captchaError");

createSum();

email.addEventListener("input", (event) => {
    // Each time the user types something, we check if the
    // form fields are valid.

    if (email.validity.valid) {
        // In case there is an error message visible, if the field
        // is valid, we remove the error message.
        emailError.textContent = ""; // Reset the content of the message
        emailError.className = "error"; // Reset the visual state of the message
    } else {
        // If there is still an error, show the correct error
        showEmailError();
    }
});

captcha.addEventListener("input", (event) => {
    if (captcha.validity.valid) {
        captchaError.textContent = "";
        captchaError.className = "error";
    } else {
        showCaptchaError();
    }
});

function createSum() {
    const sum = randomNumber;
    const canvasText = document.getElementById("maths").getContext("2d");
    canvasText.font = "25px Consolas";
    canvasText.fillStyle = "azure";
    canvasText.fillText(sum, 92.5, 25);
}

function checkCaptcha(input) {
    input = parseInt(input);
    const expected = randomNumber;
    if (input === expected) {
        return true;
    }
    return false;
}

function submitForm(e) {
    // Then we prevent the form from being sent by canceling the event
    e.preventDefault();

    if (!email.validity.valid) {
        // If it isn't, we display an appropriate error message
        showEmailError();
        return;
    }

    if (!subject.validity.valid || !message.validity.valid) return;

    if (!checkCaptcha(captcha.value)) {
        showCaptchaError();
        return;
    }

    $.ajax('https://localhost:7291/api/DeveloperMessages', {
        data: JSON.stringify({
            email: email.value,
            subject: subject.value,
            messageText: message.value
        }),
        contentType: 'application/json',
        type: "post"
    });

    form.reset();
    alert("Message sent");
    return false;
}

function showEmailError() {
    if (email.validity.valueMissing) {
        // If the field is empty,
        // display the following error message.
        emailError.textContent = "You need to enter an e-mail address.";
    } else if (email.validity.typeMismatch) {
        // If the field doesn't contain an email address,
        // display the following error message.
        emailError.textContent = "Entered value needs to be an e-mail address.";
    } else if (email.validity.tooShort) {
        // If the data is too short,
        // display the following error message.
        emailError.textContent = `E-mail should be at least ${email.minLength} characters; you entered ${email.value.length}.`;
    }

    // Set the styling appropriately
    emailError.className = "error active";
}

function showCaptchaError() {
    if (captcha.validity.valid) {
        captchaError.textContent = `Captcha is incorrect`
        captcha.style.outline = '#c0231e solid 2px';
    } else {
        captchaError.textContent = `Entered value needs to be a number`
    }
    captchaError.className = "error active";
}