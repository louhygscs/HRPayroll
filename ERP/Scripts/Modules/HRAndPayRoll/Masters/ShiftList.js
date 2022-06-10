var ShiftList = ({
    Control: {
        gvShift: ".gvShift",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(ShiftList.Control.gvShift).dataTable(
        {
        "aoColumnDefs": [
                { "aTargets": [3], "bSortable": false }
        ]
        }
    );

    $(document).on("click", ShiftList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Shift']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
