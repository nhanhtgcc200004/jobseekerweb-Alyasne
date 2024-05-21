$(document).ready(function () {
    var acceptbuttons = document.querySelectorAll("#appliedbutton");
    var appliedjob_id;
    acceptbuttons.forEach(function (acceptbutton) {
        acceptbutton.addEventListener("click", function () {
            console.log("OK")
            appliedjob_id = acceptbutton.getAttribute("data-applied-id");
           
            $.ajax({
                url: "/Candidate/AcceptApplied",
                method: "Post",
                data: {
                    appliedjob_id: appliedjob_id
                },
                success: function () {
                    window.location.reload();
                },
                error: function () {
                    console.log("something wrong");
                } 
            })
        })
    })
    var Refusebuttons = document.querySelectorAll("#refusebutton");
    var confirmdelete = document.getElementById("delete_applied_modal");
    var appliedjob_id_refuse;
    Refusebuttons.forEach(function (refusebutton) {
       refusebutton.addEventListener("click", function () {
            console.log("OK");
            appliedjob_id_refuse = refusebutton.getAttribute("data-applied-id");
        })
        confirmdelete.addEventListener("click", function () {
            $.ajax({
                url: "/Candidate/RefuseApplied",
                method: "Post",
                data: {
                    appliedjob_id: appliedjob_id_refuse
                },
                success: function () {
                    window.location.reload();
                },
                error: function () {
                    console.log("something wrong");
                }
            })
        })
        })
        
});
