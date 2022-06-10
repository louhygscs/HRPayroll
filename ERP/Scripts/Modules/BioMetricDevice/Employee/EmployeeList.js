var EmployeeList = ({
    Control: {
        gvEmployee: ".gvEmployee",
    }
});

$(document).ready(function () {
    $(EmployeeList.Control.gvEmployee).dataTable();
});
