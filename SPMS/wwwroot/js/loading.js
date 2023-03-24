function showPreloader() {
    //const buttonToShow = document.getElementById("button-to-show");
    //buttonToShow.removeAttribute("hidden");

    //const hideButton = document.getElementById("hide-button");
    //hideButton.style.display = "none";

    const overlay = document.createElement("div");
    overlay.style.position = "fixed";
    overlay.style.top = "0";
    overlay.style.left = "0";
    overlay.style.width = "100%";
    overlay.style.height = "100%";
    overlay.style.backgroundColor = "rgba(0, 0, 0, 0.5)";
    overlay.style.zIndex = "9999";
    document.body.appendChild(overlay);

    const spinner = document.createElement("div");
    spinner.classList.add("spinner");

    const spinnerIcon = document.createElement("i");
    spinnerIcon.classList.add("fa", "fa-spinner", "fa-spin", "fa-5x"); 

    spinner.appendChild(spinnerIcon);
    overlay.appendChild(spinner);

    // Center the spinner
    spinner.style.position = "absolute";
    spinner.style.top = "50%";
    spinner.style.left = "50%";
    spinner.style.transform = "translate(-50%, -50%)";
}

// Add a click event listener to all buttons and links
const buttons = document.querySelectorAll('button:not([data-bs-toggle="modal"][data-bs-dismiss]):not(#noloader)');
const links = document.querySelectorAll('a:not([data-bs-toggle="modal"][data-bs-dismiss]):not(#noloader)');

buttons.forEach((button) => {
    button.addEventListener('click', function (event) {
        if (!event.target.hasAttribute('data-bs-toggle') && !event.target.hasAttribute('data-bs-dismiss') && !event.target.classList.contains('nav-text') && event.target.tagName !== 'I') {
            showPreloader();
        }
    });
});

links.forEach((link) => {
    link.addEventListener('click', function (event) {
        if (!event.target.hasAttribute('data-bs-toggle') && !event.target.hasAttribute('data-bs-dismiss') && !event.target.classList.contains('nav-text') && event.target.tagName !== 'I' && event.target.getAttribute('href') !== 'javascript:void()' && event.target.id !== 'noloader') {
            showPreloader();
        }
    });
});

