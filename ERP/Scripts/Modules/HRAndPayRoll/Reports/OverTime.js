var ReportOverTime = ({
    Control: {
        lbEmployees: ".lbEmployees",
        lbMonth: ".lbMonth",
    },
    InitializeControl: function () {
        $(ReportOverTime.Control.lbEmployees).fSelect();

        $(ReportOverTime.Control.lbMonth).fSelect();
    }
});

$(document).ready(function () {

    $(ReportOverTime.Control.lbEmployees).fSelect();

    $(ReportOverTime.Control.lbMonth).fSelect();

});