var EmployeeShiftList = ({
    Control: {
        gvEmployeeShift: ".gvEmployeeShift"
    }
});

$(document).ready(function () {
    $(EmployeeShiftList.Control.gvEmployeeShift).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [6], "bSortable": false }
            ]
        }
    );
});