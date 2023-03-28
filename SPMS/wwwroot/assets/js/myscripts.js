function downloadDocument() {
    var viewer = document.getElementById('pdfviewer').ej2_instances[0];
    viewer.saveAsBlob().then(function (value) {
        var data = value;
        var reader = new FileReader();
        reader.readAsDataURL(data);
        reader.onload = () => {
            var base64data = reader.result;
            console.log(base64data);
            console.log("You hit me up");
            viewer.load(base64data, null);

            //// Prompt the user to select a file path
            //var filePath = window.prompt("Enter the file path to save the PDF:");

            //var fileName = '@ViewBag.fileName';

            //// Save the PDF to the specified path with the original file name
            //var link = document.createElement("a");
            //link.setAttribute("href", base64data);
            //link.setAttribute("download", fileName);
            //link.click();
        };
    });
}

function saveRemark() {
    var projectId = $("#projectId").val();
    var remark = $("#rTextarea").val();
    var status = $("#status").val();

    $.ajax({
        type: "POST",
        url: "staff/project/remark",
        data: {
            projectId: projectId,
            remark: remark,
            status: status
        },
        success: function (data) {
            // check if data indicates success
                window.location.href = "/staff/project/proposal";
            //if (data === "success") {
            //    // redirect to home/good page
            //    console.log(data);
            //} else {
            //    // handle failure case
            //    console.log("Remark save failed");
            //}
        }
    });
}



function saveCRemark() {
    var chapterId = $("#chapterId").val();
    var remark = $("#rTextarea").val();
    var status = $("#status").val();

    $.ajax({
        type: "POST",
        url: "staff/project/cremark",
        data: {
            chapterId: chapterId,
            remark: remark,
            status: status
        },
        success: function (data) {
            // check if data indicates success
            window.location.href = "/milestone";

            //if (data === "success") {
            //    // redirect to home/good page
            //    window.location.href = "/staff/project/proposal";
            //} else {
            //    // handle failure case
            //    console.log("CRemark save failed");
            //}
        }
    });
}

function readNoti(notificationId) {
    $.ajax({
        url: "/cascadehelp/notification?notificationId=" + notificationId,
        type: "GET",
        success: function (response) {
            // Handle success response
        },
        error: function (xhr) {
            // Handle error response
        }
    });
}


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
