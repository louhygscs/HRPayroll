var ReportSalary = ({
    Control: {
        lbEmployees: ".lbEmployees",
        lbMonth: ".lbMonth",
       
    },
    InitializeControl: function () {
        $(ReportSalary.Control.lbEmployees).fSelect();

        $(ReportSalary.Control.lbMonth).fSelect();
    }
});

$(document).ready(function () {

    $(ReportSalary.Control.lbEmployees).fSelect();

    $(ReportSalary.Control.lbMonth).fSelect();

});