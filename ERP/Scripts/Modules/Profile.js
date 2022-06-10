var Profile = ({
    Control: {
        txtBirthDate: ".txtBirthDate",
        btnDeletePhoto: ".btnDeletePhoto",
        divUploadPhoto: ".divUploadPhoto",
        divViewPhoto: ".divViewPhoto",
    },
});

$(document).ready(function () {
    $(Profile.Control.txtBirthDate).daterangepicker({
        singleDatePicker: true
    });

    $(document).on("click", Profile.Control.btnDeletePhoto, function () {
        $(Profile.Control.divUploadPhoto).show();
        $(Profile.Control.divViewPhoto).hide();
        ValidatorEnable($("[id$='rfvPhoto']")[0], true);
        ValidatorEnable($("[id$='revPhoto']")[0], true);
    });
});
