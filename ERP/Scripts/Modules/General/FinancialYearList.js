var FinancialYearList = ({
    Control: {
        gvFinancialYear: ".gvFinancialYear",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(FinancialYearList.Control.gvFinancialYear).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [2], "bSortable": false }
            ]
        }
    );

    $(document).on("click", FinancialYearList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Financial Year']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});