$(document).ready(){
    $("#search_post").submit(function (eventObj) {
        var condition = document.querySelector('#exampleFormControlSelect1');
        $(this).append('<input type="hidden" name="condition" value= " '+condition.value+'"/>');
    });
}
