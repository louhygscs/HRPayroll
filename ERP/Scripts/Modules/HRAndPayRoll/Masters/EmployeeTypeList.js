var EmployeeTypeList = ({
    Control: {
        gvEmployeeType: ".gvEmployeeType",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(EmployeeTypeList.Control.gvEmployeeType).dataTable(
        {
        "aoColumnDefs": [
                { "aTargets": [2], "bSortable": false }
        ]
        }
    );

    $(document).on("click", EmployeeTypeList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Employee Type']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
