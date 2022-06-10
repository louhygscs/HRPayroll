var CutOffList = ({
    Control: {
        gvList: ".gvList",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(CutOffList.Control.gvList).dataTable(
        {
            "aoColumnDefs": [
                /*{ "aTargets": [1], "bSortable": false }*/
            ]
        }
    );

    $(document).on("click", CutOffList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['CutOff']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});