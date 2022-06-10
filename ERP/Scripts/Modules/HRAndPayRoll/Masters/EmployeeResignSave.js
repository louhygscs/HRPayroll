var EmployeeResignSave = ({
    Control: {
        txtResignDate: ".txtResignDate"
    },
    ShowHideDocument: function (divViewDocument, divUploadDocument) {
        $(divViewDocument).hide();
        $(divUploadDocument).show();
    },
});

$(document).ready(function () {

    $(EmployeeResignSave.Control.txtResignDate).daterangepicker({
        singleDatePicker: true
    });
   
});



