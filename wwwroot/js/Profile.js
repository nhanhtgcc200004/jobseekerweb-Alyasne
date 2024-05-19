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
        var currentPassword = document.getElementsByTagName("current_password");
        var newpassword = document.getElementsByTagName("new_password");
        var confirmpassword = document.getElementsByTagName("confirm_password");
        $.Ajax({
            url: "/...",
            method: "Post",
            data: {
                current_password=currentPassword,
                new_password=newpassword,
                confirm_password=confirmpassword
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
});
