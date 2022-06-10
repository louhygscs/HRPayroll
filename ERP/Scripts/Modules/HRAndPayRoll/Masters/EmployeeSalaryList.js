var EmployeeSalaryList = ({
    Control: {
        gvEmployeeSalary: ".gvEmployeeSalary",
    }
});

$(document).ready(function () {
    $(EmployeeSalaryList.Control.gvEmployeeSalary).dataTable(
        {
        "aoColumnDefs": [
                { "aTargets": [6], "bSortable": false }
        ]
        }
    );

   
});
