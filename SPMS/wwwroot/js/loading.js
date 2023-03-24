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

// add a click event listener to all buttons and links
const buttons = document.queryselectorall('button:not([data-bs-toggle="modal"][data-bs-dismiss][data-dismiss]):not(#noloader)');
const links = document.queryselectorall('a:not([data-bs-toggle="modal"][data-bs-dismiss]):not(#noloader)');

buttons.foreach((button) => {
    button.addeventlistener('click', function (event) {
        if (!event.target.hasattribute('data-bs-toggle') && !event.target.hasattribute('data-bs-dismiss') && !event.target.hasattribute('data-dismiss') && !event.target.classlist.contains('nav-text') && event.target.tagname !== 'i') {
            showpreloader();
        }
    });
});

links.foreach((link) => {
    link.addeventlistener('click', function (event) {
        if (!event.target.hasattribute('data-bs-toggle') && !event.target.hasattribute('data-bs-dismiss') && !event.target.classlist.contains('nav-text') && event.target.tagname !== 'i' && event.target.getattribute('href') !== 'javascript:void()' && event.target.id !== 'noloader') {
            showpreloader();
        }
    });
});