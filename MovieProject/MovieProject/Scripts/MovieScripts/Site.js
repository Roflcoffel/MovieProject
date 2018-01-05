$(document).ready(function () {
    $("#reviewButton").click(function () {
        $("#hiddenReview").toggle()
    })

    $("#delivery").click(function () {
        $("#hiddenDelivery").toggle(!this.checked)
    })

    $(".test").on("mouseover", function () {
        $(this).addClass("border-success");
    })
})