$(document).ready(function() {
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#avatar_img').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }

    $("#fileupload").change(function () {
        readURL(this);
    });

    //var showchangepassword = document.getElementById("modal_changepassword");
    //var UpdateButton = document.getElementById("updatepassword");

    //reportButton.addEventListener("click", function () {

    //    var userId = showchangepassword.getAttribute("data-user_id");
        

    //    // Make an AJAX call to the CreateReport action
    //    $.ajax({
    //        url: "/Report/CreateReport",
    //        method: "POST",
    //        data: {
    //            post_id: postId,
    //            reason: reason_str
    //        },
    //        success: function (response) {
    //            console.log("CreateReport success");
    //            window.location.reload();
    //            applyButton.disabled = true;
    //        },
    //        error: function (xhr, status, error) {
    //            // Handle error response
    //            console.error("CreateReport error: " + xhr.responseText);
    //        }
    //    });
    //});
    var downloadbutton = document.getElementById("download");
    downloadbutton.addEventListener("click", function () {
        $.post("/Profile/downloadUserCv", downloadbutton.getAttribute("data-user_id");
    })
});
