$(document).ready(function () {
    var showreportButton = document.getElementById("ShowCreateReport");
    var reportButton = document.getElementById("CreateReport");

    reportButton.addEventListener("click", function () {

        var postId = showreportButton.getAttribute("data-id");
        var reason_str = document.getElementById("Reason").value;

        // Make an AJAX call to the CreateReport action
        $.ajax({
            url: "/Report/CreateReport",
            method: "POST",
            data: {
                post_id: postId,
                reason: reason_str
            },
            success: function (response) {
                console.log("CreateReport success");
                alert("report success");
                window.location.reload();
            },
            error: function (xhr, status, error) {
                // Handle error response
                console.error("CreateReport error: " + xhr.responseText);
            }
        });
    });

    var applyButton = document.getElementById("Apply");

    // Add click event listener to the Apply button
    applyButton.addEventListener("click", function () {
        // Get the post ID from the data attribute
        var postId = applyButton.getAttribute("data-post-id");
        

        // Make an AJAX call to the AppliedJob action
        $.ajax({
            url: "/Post/AppliedJob",
            method: "POST",
            data: { post_id: postId },
            success: function (response) {
                console.log("Report success");
                alert("applied success");
                window.location.reload();
               
            },
            error: function (xhr, status, error) {
                // Handle error response
                console.error("AppliedJob error: " + xhr.responseText);
            }
        });
    });

    
    
    var commentbutton = document.getElementById("CommentButton");
    commentbutton.addEventListener("click", function () {
        var comment_content = document.getElementById("comment").value;
        var post_Id = document.getElementById("comment").getAttribute("data-post_id");
        var rating = document.querySelector('input[name="rating"]:checked').value;
        $.ajax({
            url: "/Post/AddComment",
            method: "POST",
            data: {
                post_id: post_Id,
                content: comment_content,
                rating: rating, 
            },
            success: function (response) {
                window.location.reload();
                
            },
            error: function (xhr, status, error) {
                console.error("Error: ", error);
            }
        });
    });
    var FullButton = document.getElementById("Full");
    FullButton.addEventListener("click", function () {
        alert("this post job is reach limit");
    })

});