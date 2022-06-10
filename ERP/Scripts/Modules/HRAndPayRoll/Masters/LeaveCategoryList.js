var LeaveCategoryList = ({
    Control: {
        gvLeaveCategory: ".gvLeaveCategory",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(LeaveCategoryList.Control.gvLeaveCategory).dataTable(
        {
        "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
        ]
        }
    );

    $(document).on("click", LeaveCategoryList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Leave Category']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
