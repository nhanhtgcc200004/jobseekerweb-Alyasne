$(document).ready(function () {
    var deletebuttons = document.querySelectorAll("#deletebutton_id");
    var confirmdelete = document.getElementById("deletebuttonmodal");
    var deletebuttonId;
    deletebuttons.forEach(function (deletebutton) {
        deletebutton.addEventListener("click", function () {
            deletebuttonId = deletebutton.getAttribute("data-post-id");
        });
    });


    confirmdelete.addEventListener("click", function () {
        var post_id = deletebuttonId;
        $.ajax({
            url: "/Post/DeletePost",
            method: "Post",
            data: {
                post_id: post_id,
            },
            success: function (response) {
                console.log("CreateReport success");
                window.location.reload();
            },
            error: function (xhr, status, error) {
                // Handle error response
                console.error("CreateReport error: " + xhr.responseText);
            }
        });
    });
});
