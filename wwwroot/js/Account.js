$(document).ready(function () {
    var editButton = document.getElementById("EditAccount");
    var changesroleselection = document.getElementById("changesRoleSelection");
    var updateButton = document.getElementById("updaterole");

    if (updateButton) {
        updateButton.addEventListener("click", function () {

            var role = changesroleselection.value;
            var user_id = editButton.getAttribute("data-user_id");

            // Make an AJAX call to the CreateReport action
            $.ajax({
                url: "/Account/UpdateRole",
                method: "POST",
                data: {
                    user_id: user_id,
                    role: role
                },
                success: function (response) {
                    console.log("success");
                  
                    window.location.reload();
                },
                error: function (xhr, status, error) {
                    // Handle error response
                    console.error("fail");
                }
            });
        });
    }
});
