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
                console.log(response);
                window.location.reload();
            },
                error: function (xhr, status, error) {
                    var errorMsg = xhr.responseText || "An error occurred.";
                    document.getElementById("error").innerText = errorMsg;
                
            }
        });
    })
    
    document.getElementById("submit-profile").addEventListener("click", function (event) {
        event.preventDefault(); // Prevent the default anchor behavior
        document.getElementById("profile-form").submit(); // Submit the form
    });
});
