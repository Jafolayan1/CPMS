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
            if (data === "success") {
                // redirect to home/good page
                window.location.href = "/staff/project/proposal";
            } else {
                // handle failure case
                console.log("Remark save failed");
            }
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
            if (data === "success") {
                // redirect to home/good page
                window.location.href = "/staff/project/proposal";
            } else {
                // handle failure case
                console.log("CRemark save failed");
            }
        }
    });
}
