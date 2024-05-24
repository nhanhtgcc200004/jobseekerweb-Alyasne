$(document).ready(function () {
    var editButton = document.getElementById("EditAccount");
    var changesroleselection = document.getElementById("changesRoleSelection");
    var updateButton = document.getElementById("updaterole");

    if (updateButton) {
        updateButton.addEventListener("click", function () {

            var role = changesroleselection.value;
            var user_id = editButton.getAttribute("data-user_id");

       
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
    var DeleteButton = document.getElementById("DeleteAccountButton");
    var Banbutton = document.getElementById("confirm_delete");
    if (Banbutton) {
        Banbutton.addEventListener("click", function () {

            var user_id = DeleteButton.getAttribute("data-user_id");
            var reason = document.getElementById("reason").value;

          
            $.ajax({
                url: "/Account/BanAccount",
                method: "POST",
                data: {
                    user_id: user_id,
                    reason: reason
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
