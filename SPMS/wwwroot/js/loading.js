function showPreloader() {
    const buttonToShow = document.getElementById("button-to-show");
    buttonToShow.removeAttribute("hidden");
    const signInButton = document.getElementById("sign-in-button");
    signInButton.style.display = "none";

    const overlay = document.createElement("div");
    overlay.style.position = "fixed";
    overlay.style.top = "0";
    overlay.style.left = "0";
    overlay.style.width = "100%";
    overlay.style.height = "100%";
    overlay.style.backgroundColor = "rgba(0, 0, 0, 0.5)";
    overlay.style.zIndex = "9999";
    document.body.appendChild(overlay);
}
