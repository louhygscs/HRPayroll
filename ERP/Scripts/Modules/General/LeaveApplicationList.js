var LeaveApplicationList = ({
    Control: {
        gvLeaveApplication: ".gvLeaveApplication",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(LeaveApplicationList.Control.gvLeaveApplication).dataTable(
        {
        "aoColumnDefs": [
                { "aTargets": [6], "bSortable": false }
        ]
        }
    );

    $(document).on("click", LeaveApplicationList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Leave Application']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
