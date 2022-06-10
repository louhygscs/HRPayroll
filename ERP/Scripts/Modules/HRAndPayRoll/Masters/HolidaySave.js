var HolidaySave = ({
    Control: {
        txtDateRange: ".txtDateRange"
    }
});

$(document).ready(function () {
    $(HolidaySave.Control.txtDateRange).daterangepicker();
});
