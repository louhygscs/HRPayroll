var AllowanceList = ({
    Control: {
        gvAllowance: ".gvAllowance",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(AllowanceList.Control.gvAllowance).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [2], "bSortable": false }
            ]
        }
    );

    $(document).on("click", AllowanceList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Allowance']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
