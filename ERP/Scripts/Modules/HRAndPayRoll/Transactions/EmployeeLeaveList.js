var EmployeeLeaveList = ({
    Control: {
        gvEmployeeLeave: ".gvEmployeeLeave",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(EmployeeLeaveList.Control.gvEmployeeLeave).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [9], "bSortable": false }
            ]
        }
    );

    $(document).on("click", EmployeeLeaveList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Employee Loan']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
