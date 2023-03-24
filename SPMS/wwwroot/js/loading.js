function showPreloader() {
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
const buttons = document.querySelectorAll('button:not([data-bs-toggle="modal"]):not([data-bs-dismiss]):not(#noloader)');
const links = document.querySelectorAll('a:not([data-bs-toggle="modal"]):not([data-bs-dismiss]):not([href="javascript:void()"]):not(.nav-text):not(i):not(#noloader)');

buttons.forEach((button) => {
    button.addEventListener('click', function (event) {
        if (event.target.getAttribute('data-bs-dismiss') === null && event.target.id !== 'noloader') {
            showPreloader();
        }
    });
});

links.forEach((link) => {
    link.addEventListener('click', function (event) {
        if (event.target.getAttribute('data-bs-dismiss') === null && event.target.id !== 'noloader') {
            showPreloader();
        }
    });
});
