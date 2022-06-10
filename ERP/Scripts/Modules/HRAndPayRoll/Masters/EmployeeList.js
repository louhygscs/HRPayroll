var EmployeeList = ({
    Control: {
        gvEmployee: ".gvEmployee",
        btnDelete: ".btnDelete",
        btnPermenentDelete: ".btnPermenentDelete",
    },
    Variable: {
        DeletePermenentConfirm: "You are deleting this record permanently, are you sure ? You will loose his / her all records. ?",
    }

});

$(document).ready(function () {
    $(EmployeeList.Control.gvEmployee).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [6], "bSortable": false },
                { "aTargets": [7], "bSortable": false }
            ]
        }
    );

    $(document).on("click", EmployeeList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Employee']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });


    $(document).on("click", EmployeeList.Control.btnPermenentDelete, function () {
        var confirmPermenentAns = confirm(Common.ReplacementString(EmployeeList.Variable.DeletePermenentConfirm));
        if (confirmPermenentAns == true)
            return true;
        else
            return false;
    });

});
