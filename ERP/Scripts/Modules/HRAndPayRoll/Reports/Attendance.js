var ReportAttendance = ({
    Control: {
        lbEmployees: ".lbEmployees",
        lbMonth: ".lbMonth",
    },
    InitializeControl: function () {
        $(ReportAttendance.Control.lbEmployees).fSelect();

        $(ReportAttendance.Control.lbMonth).fSelect();
    }
});

$(document).ready(function () {

    $(ReportAttendance.Control.lbEmployees).fSelect();

    $(ReportAttendance.Control.lbMonth).fSelect();

});