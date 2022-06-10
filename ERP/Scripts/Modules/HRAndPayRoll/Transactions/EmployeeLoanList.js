var EmployeeLoanList = ({
    Control: {
        gvEmployeeLoan: ".gvEmployeeLoan",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(EmployeeLoanList.Control.gvEmployeeLoan).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [8], "bSortable": false }
            ]
        }
    );

    $(document).on("click", EmployeeLoanList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Employee Loan']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
