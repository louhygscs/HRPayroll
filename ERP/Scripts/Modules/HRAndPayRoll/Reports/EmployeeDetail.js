var EmployeeDetail = ({
    Control: {
        txtDateRange: ".txtDateRange"
    },
    InitializeControl: function () {
        $(EmployeeDetail.Control.txtDateRange).daterangepicker();
    }
});

$(document).ready(function () {
    $(EmployeeDetail.Control.txtDateRange).daterangepicker();
});
