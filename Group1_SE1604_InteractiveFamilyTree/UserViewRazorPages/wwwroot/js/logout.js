document.getElementById("logoutButton").addEventListener("click", function () {
    // Clear the session "loginUser"
    sessionStorage.removeItem("loginCustomer");
    // Redirect the user to the logout page or perform any other desired action
    window.location.href = "/Dangptm/Login";
});