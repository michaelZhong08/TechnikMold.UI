$(document).ready(function () {
    $("#test").on("click", function () {
        alert($("#aa\\.bb\\[0\\]").val());

    });

    $(".valuefield").on("focus", function () {
        alert(this);
    });
})