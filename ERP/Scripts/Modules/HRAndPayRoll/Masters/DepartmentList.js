var DepartmentList = ({
    Control: {
        gvDepartment: ".gvDepartment",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(DepartmentList.Control.gvDepartment).dataTable(
        {
        "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
        ]
        }
    );

    $(document).on("click", DepartmentList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Department']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
