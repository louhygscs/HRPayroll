var ReportLeave = ({
    Control: {
        lbEmployees: ".lbEmployees",
    },
    InitializeControl: function () {
        $(ReportLeave.Control.lbEmployees).fSelect();
    }
});

$(document).ready(function () {

    $(ReportLeave.Control.lbEmployees).fSelect();

});