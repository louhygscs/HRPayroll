var EmployeeSave = ({
    Control: {
        txtBirthDate: ".txtBirthDate",
        txtJoinDate: ".txtJoinDate",
        btnDeletePhoto: ".btnDeletePhoto",
        divUploadPhoto: ".divUploadPhoto",
        divViewPhoto: ".divViewPhoto",
    },
    ShowHideDocument: function (divViewDocument, divUploadDocument) {
        $(divViewDocument).hide();
        $(divUploadDocument).show();
    },
});

$(document).ready(function () {
    $(EmployeeSave.Control.txtBirthDate).datepicker({ format: "mm/dd/yyyy" });
    $(EmployeeSave.Control.txtJoinDate).datepicker({ format: "mm/dd/yyyy" }).datepicker("setDate", new Date());

    $(document).on("click", EmployeeSave.Control.btnDeletePhoto, function () {
        $(EmployeeSave.Control.divUploadPhoto).show();
        $(EmployeeSave.Control.divViewPhoto).hide();
        ValidatorEnable($("[id$='rfvPhoto']")[0], true);
        ValidatorEnable($("[id$='revPhoto']")[0], true);
    });
});
