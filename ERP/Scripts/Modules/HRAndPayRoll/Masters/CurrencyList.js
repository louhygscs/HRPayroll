var CurrencyList = ({
    Control: {
        gvList   : ".gvCurrency",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(CurrencyList.Control.gvList).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
            ]
        }
    );

    $(document).on("click", CurrencyList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Currency']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});