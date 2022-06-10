var CollectAttendance = ({
    Control: {
        txtDate: ".txtDate",
    },
});

$(document).ready(function () {
    $(CollectAttendance.Control.txtDate).daterangepicker();
});