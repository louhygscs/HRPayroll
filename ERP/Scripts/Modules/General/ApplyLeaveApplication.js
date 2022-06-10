var ApplyLeaveApplication = ({
    Control: {
        txtDateRange: ".txtDateRange"
    }
});

$(document).ready(function () {
   
    $(ApplyLeaveApplication.Control.txtDateRange).daterangepicker();
});
