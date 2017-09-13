function GeneralConfig() {

    toastr.options = {
        "closeButton": false,
        "debug": true,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    $('#StartDate').datepicker({
        format: "dd/MM/yyyy",
        startDate: "today",
        orientation: "botton right",
        autoclose: true
    });

    $('#EndDate').datepicker({
        format: "dd/MM/yyyy",
        startDate: "tomorrow",
        orientation: "botton right",
        autoclose: true
    });

    //Address Exhibition Validation
    $(document).ready(function() {
        var $inputOnline = $("#Online");
        var $inputFree = $("#IsFree");
        ShowAddress(); //Edit mode
        ShowPrice(); //Edit mode

        $inputOnline.click(function() {
            ShowAddress();
        });

        $inputFree.click(function () {
            ShowPrice();
        });

        function ShowAddress() {
            if ($inputOnline.is(":checked"))
                $("#FormAddress").hide();
            else
                $("#FormAddress").show();
        }

        function ShowPrice() {
            if ($inputFree.is(":checked")) {
                $("#Price").prop("disabled", true);
            } else {
                $("#Price").prop("disabled", false);
            }
        }
    });
}

function AjaxModal() {
    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                    keyboard: true
                                },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });

        function bindForm(dialog) {
            $('form', dialog).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#myModal').modal('hide');
                            $('#AddressTarget').load(result.url); // Carrega o resultado HTML para a div demarcada
                        } else {
                            $('#myModalContent').html(result);
                            bindForm(dialog);
                        }
                    }
                });
                return false;
            });
        }
    });
}