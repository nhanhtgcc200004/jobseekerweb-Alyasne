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

});