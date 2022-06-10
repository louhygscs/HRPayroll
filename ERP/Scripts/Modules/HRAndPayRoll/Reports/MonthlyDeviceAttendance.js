var ReportMonthlyDeviceAttendance = ({
    Control: {
        lbEmployees: ".lbEmployees",
        lbMonth: ".lbMonth",
    },
    InitializeControl: function () {
        $(ReportMonthlyDeviceAttendance.Control.lbEmployees).fSelect();

        $(ReportMonthlyDeviceAttendance.Control.lbMonth).fSelect();
    }
});

$(document).ready(function () {

    $(ReportMonthlyDeviceAttendance.Control.lbEmployees).fSelect();

    $(ReportMonthlyDeviceAttendance.Control.lbMonth).fSelect();

});