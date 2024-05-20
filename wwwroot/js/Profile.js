$(document).ready(function () {
    function readURL(input) {
        console.log('here')
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

    var UpdatePasswordButton = document.getElementById("updatepassword");
    UpdatePasswordButton.addEventListener("click", function () {
        var currentPassword = document.getElementById("current_password").value;
        var newpassword = document.getElementById("new_password").value;
        var confirmpassword = document.getElementById("confirm_password").value;
        $.ajax({
            url: "/Authentication/ChangePassword",
            method: "Post",
            data: {
                current_password: currentPassword,
                new_password: newpassword,
                confirm_password: confirmpassword
            },
            success: function (response) {
                console.log("success");
                window.location.reload();
            },
            erorr: function () {
                console.log("fail");
            }
        })
    })
    var downloadbutton = document.getElementById("download");
    downloadbutton.addEventListener("click", function () {
        var profile_user_id = downloadbutton.getAttribute("data-user_id");
        $.ajax({
            url: "/Profile/DownloadUserCv",
            method: "Post",
            data: {
                user_profile_id: user_profile_id
            },
            success: function () {
                window.location.reload();
            },
            error: function () {
                console.log("something wrong")
            }
        })
    })
});
