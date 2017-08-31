function EventValidation() {
    $('StartDate').datepicker({
        format: "dd/MM/yyyy",
        startDate: "today",
        orientation: "botton right",
        autoclose: true
    });

    $('EndDate').datepicker({
        format: "dd/MM/yyyy",
        startDate: "today",
        orientation: "botton right",
        autoclose: true
    });
}