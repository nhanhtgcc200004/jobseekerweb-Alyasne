$(document).ready(function () {
    $("#search_post").submit(function (eventObj) {
        var condition = document.getElementById("condition_select").value;
        var search_value = document.getElementById("Search_value").value;
        $.ajax({
            url: "/Post/Search",
            method: "POST",
            data: {
                condition: condition,
                Search_value: search_value
            }
        })
    });

});
   

