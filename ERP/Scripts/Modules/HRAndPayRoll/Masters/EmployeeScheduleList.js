var EmployeeList = ({
    Control: {
        gvEmployee: ".gvEmpSchedule"
        //btnDelete: ".btnDelete",
        //btnPermenentDelete: ".btnPermenentDelete",
    },
    Variable: {
        DeletePermenentConfirm: "You are deleting this record permanently, are you sure ? You will loose his / her all records. ?",
    }

});

$(document).ready(function () {
    $(EmployeeList.Control.gvEmployee).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [5], "bSortable": false },
                { "aTargets": [6], "bSortable": false },
                { "aTargets": [7], "bSortable": false },
                { "aTargets": [8], "bSortable": false },
                { "aTargets": [9], "bSortable": false },
                { "aTargets": [10], "bSortable": false },
                { "aTargets": [11], "bSortable": false },
                { "aTargets": [12], "bSortable": false },
                { "aTargets": [13], "bSortable": false },
                { "aTargets": [14], "bSortable": false },
                { "aTargets": [15], "bSortable": false },
                { "aTargets": [16], "bSortable": false },
                { "aTargets": [17], "bSortable": false },
                { "aTargets": [18], "bSortable": false },
                { "aTargets": [19], "bSortable": false }
            ]
        }
    );

    //$(document).on("click", EmployeeList.Control.btnDelete, function () {
    //    var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Employee']));
    //    if (confirmAns == true)
    //        return true;
    //    else
    //        return false;
    //});


    //$(document).on("click", EmployeeList.Control.btnPermenentDelete, function () {
    //    var confirmPermenentAns = confirm(Common.ReplacementString(EmployeeList.Variable.DeletePermenentConfirm));
    //    if (confirmPermenentAns == true)
    //        return true;
    //    else
    //        return false;
    //});

});
