function setDatePicker(classNameWithoutDot) {
    $(document).ready(function () {
        $("." + classNameWithoutDot).datepicker({
            todayBtn: "linked",
            language: "tr",
            orientation: "bottom auto",
            autoclose: true,
            todayHighlight: true,
            clearBtn: true
        });
    });
}

function setBlazorDate(blazorNamespace, blazorFunction, input) {
    DotNet.invokeMethod(blazorNamespace, blazorFunction, input.value);
}