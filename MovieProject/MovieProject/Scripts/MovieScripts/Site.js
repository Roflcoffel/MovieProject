$(document).ready(function () {
    $("#reviewButton").click(function () {
        $("#hiddenReview").toggle()
    })

    $("#delivery").click(function () {
        $("#hiddenDelivery").toggle(!this.checked)
    })
})