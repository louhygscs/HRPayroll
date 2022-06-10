var EmployeeGradeList = ({
    Control: {
        gvEmployeeGrade: ".gvEmployeeGrade",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(EmployeeGradeList.Control.gvEmployeeGrade).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [1], "bSortable": false }
            ]
        }
    );

    $(document).on("click", EmployeeGradeList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Employee Grade']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
