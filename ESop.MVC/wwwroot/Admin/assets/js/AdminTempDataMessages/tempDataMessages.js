document.addEventListener("DOMContentLoaded", function () {
    var successMessage = document.getElementById("successMessage").innerText;
    var errorMessage = document.getElementById("errorMessage").innerText;
    var messageContainer = document.getElementById("messageContainer");

    if (messageContainer) {
        if (successMessage) {
            var successAlert = document.createElement("div");
            successAlert.className = "alert alert-success";
            successAlert.innerText = successMessage;
            messageContainer.appendChild(successAlert);
        }

        if (errorMessage) {
            var errorAlert = document.createElement("div");
            errorAlert.className = "alert alert-danger";
            errorAlert.innerText = errorMessage;
            messageContainer.appendChild(errorAlert);
        }
    }
});