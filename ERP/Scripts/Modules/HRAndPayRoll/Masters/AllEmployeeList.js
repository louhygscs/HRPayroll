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
    $(EmployeeList.Control.gvEmployee).dataTable();
});
