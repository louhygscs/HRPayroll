var CategoryList = ({
    Control: {
        gvList:    ".gvCategory",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(CategoryList.Control.gvList).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
            ]
        }
    );

    $(document).on("click", CategoryList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Category']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});