var ReportLoan = ({
    Control: {
        lbEmployees: ".lbEmployees",
    },
    InitializeControl: function () {
        $(ReportLoan.Control.lbEmployees).fSelect();
    }
});

$(document).ready(function () {

    $(ReportLoan.Control.lbEmployees).fSelect();

});