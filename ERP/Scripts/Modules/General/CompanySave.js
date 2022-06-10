var CompanySave = ({
    Control: {
        btnDeleteLogo: ".btnDeleteLogo",
        divUploadLogo: ".divUploadLogo",
        divViewLogo: ".divViewLogo",
    },
});

$(document).ready(function () {

    $(document).on("click", CompanySave.Control.btnDeleteLogo, function () {
        $(CompanySave.Control.divUploadLogo).show();
        $(CompanySave.Control.divViewLogo).hide();
        ValidatorEnable($("[id$='rfvLogo']")[0], true);
        ValidatorEnable($("[id$='revLogo']")[0], true);
    });
});
