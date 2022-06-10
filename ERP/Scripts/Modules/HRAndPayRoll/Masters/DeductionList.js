var DeductionList = ({
    Control: {
        gvDeduction: ".gvDeduction",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(DeductionList.Control.gvDeduction).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [2], "bSortable": false }
            ]
        }
    );

    $(document).on("click", DeductionList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Deduction']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
