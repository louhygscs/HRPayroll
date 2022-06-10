
var EmployeeAttendanceEntry = ({
    checkDecimal: function (el) {
        var ex = /^[0-9]+\.?[0-9]*$/;
        if (ex.test(el.value) == false || el.value > 24) {
            el.value = el.value.substring(0, el.value.length - 1);
            if (el.value > 24) {
                checkDec(el);
            }
        }
    },

    AddAttendance: function (el) {
        $(".hfDate").val($(el).data('date'));
        $(".hfEmployeeAttendanceID").val($(el).data('employeeattendanceid'));
    }
});
