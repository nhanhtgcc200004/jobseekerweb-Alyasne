$(document).ready(function () {
    var editButtons = document.querySelectorAll("#EditAccount");
    var changesroleselection = document.getElementById("changesRoleSelection");
    var updateButton = document.getElementById("updaterole");
    var user_id;
    editButtons.forEach(function (editButton) {
        user_id = editButton.getAttribute("data-user_id");
    });
    if (updateButton) {
        updateButton.addEventListener("click", function () {
            var role = changesroleselection.value;
            
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

    var user_id_ban;
    var DeleteButtons = document.querySelectorAll("#DeleteAccountButton");
    var Banbutton = document.getElementById("BanAccount");
    DeleteButtons.forEach(function (DeleteButton) {
        user_id_ban = DeleteButton.getAttribute("data-user_id");
    });
    if (Banbutton) {
        Banbutton.addEventListener("click", function () {
            
            var reason = document.getElementById("reason").value;

          
            $.ajax({
                url: "/Account/BanAccount",
                method: "POST",
                data: {
                    user_id: user_id_ban,
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
