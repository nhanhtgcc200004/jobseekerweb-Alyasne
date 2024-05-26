$(document).ready(function () {
    var acceptbuttons = document.querySelectorAll("#accept_report_button");
    var report_id;
    acceptbuttons.forEach(function (acceptbutton) {
        acceptbutton.addEventListener("click", function () {

            report_id = acceptbutton.getAttribute("data-report-id");
            $.ajax({
                url: "/Report/AcceptReport",
                method: "Post",
                data: {
                    report_id: report_id
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

    var refusebuttons = document.querySelectorAll("#refuse_report_button");
    var report_id;
    refusebuttons.forEach(function (refusebutton) {
        report_id = refusebutton.getAttribute("data-report_id");
        refusebutton.addEventListener("click", function () {

            
            $.ajax({
                url: "/Report/RefuseReport",
                method: "Post",
                data: {
                    report_id: report_id
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