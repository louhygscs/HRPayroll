var EmployeeAttendance = ({
    Control: {
        txtDate: ".txtDate",
        txtFromToDate: ".txtFromToDate",
    }
});
$(document).ready(function () {
    $(EmployeeAttendance.Control.txtDate).daterangepicker({
        singleDatePicker: true
    });
    $(EmployeeAttendance.Control.txtFromToDate).daterangepicker();
});