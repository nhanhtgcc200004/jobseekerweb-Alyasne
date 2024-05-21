$(document).ready(function () {
    var searchButton = document.getElementById("Search");
    searchButton.addEventListener("click", function () {
        var condition = document.getElementById("condition").value;
        var search_value = document.getElementById("search_value").value;
        console.log(condition);
        console.log(search_value);
        $.ajax({
            url: "/Search/Result",
            method: "Post",
            data: {
                search_value: search_value,
                condition: condition
            },
            success: function (response) {
                
                $('#post-container').html(response);
            },
            error: function () {

            }
        });
    });

});
   

