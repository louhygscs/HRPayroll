var InterviewList = ({
    Control: {
        gvInterview: ".gvInterview",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(InterviewList.Control.gvInterview).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [9], "bSortable": false }
            ]
        }
    );

    $(document).on("click", InterviewList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Interview']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});