$(function () {
    $("a[data-modal]").on("click", function () {
        $("#mainModalContent").load(this.href, function () {
            $("#mainModal").modal({ keyboard: true }, "show");

            $("#modalForm").submit(function () {
                if ($("#modalForm").valid()) {
                    $.ajax({
                        url: this.action,
                        type: this.method,
                        data: $(this).serialize(),
                        success: function (result) {
                            if (result.success) {
                                $("#modalForm").modal("hide");
                                location.reload();
                            } else {
                                $("#MessageToClient").text(result.message);
                            }
                        },
                        error: function () {
                            $("#MessageToClient").text("Le serveur a rencontré une erreur.")
                        }
                    });
                    return false;
                }
            });
        });
        return false;
    });
});