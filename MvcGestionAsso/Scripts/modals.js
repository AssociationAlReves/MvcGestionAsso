$(function () {
    $("a[data-modal]").on("click", function () {
        $("#mainModalContent").load(this.href, function () {
            $("#mainModal").modal({ keyboard: true }, "show");
        });
        return false;
    });
});