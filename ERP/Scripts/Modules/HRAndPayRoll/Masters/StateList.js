var StateList = ({
    Control: {
        gvState: ".gvState",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(StateList.Control.gvState).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
            ]
        }
    );

    $(document).on("click", StateList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['State']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});