// Default kullanım için DataTable'a dönüştürülmek istenen table id'si olarak "datatable" verilmelidir.

var _idWithoutSharp = "datatable";

$(document).ready(function () {
    //$(".menulink").click(function() { // çalışmadığı için aşağıdaki şekilde yazıyoruz
    //    removeDataTable(_idWithoutSharp);
    //});
    $(document).on("click", ".menulink", function () {
        removeDataTable(_idWithoutSharp);
    });
});

function addDataTable(idWithoutSharp = "datatable") {
    $(document).ready(function() {
        _idWithoutSharp = idWithoutSharp;
        $('#' + _idWithoutSharp).DataTable({
            "language": {
                "url": "/json/dataTablesTurkish.json"
            }
        });
    });
}

function removeDataTable(idWithoutSharp = "datatable") {
    $(document).ready(function() {
        _idWithoutSharp = idWithoutSharp;
        $('#' + _idWithoutSharp + '_wrapper').remove();
    });
}

function destroyDataTable(idWithoutSharp = "datatable") {
    $(document).ready(function () {
        _idWithoutSharp = idWithoutSharp;
        $('#' + _idWithoutSharp).DataTable().destroy();
    });
}
