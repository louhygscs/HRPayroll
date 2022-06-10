var EmployeeHourlySalaryProcessList = ({
    Control: {
        gvEmployeeCompletedSalaryProcess: ".gvEmployeeCompletedSalaryProcess",
        gvEmployeePendingSalaryProcess: ".gvEmployeePendingSalaryProcess",
        ddlMonth: ".ddlMonth",
        btnEdit: ".btnEdit",
    },
    InitailGridDataTable: function () {
        $(EmployeeHourlySalaryProcessList.Control.gvEmployeeCompletedSalaryProcess).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [7], "bSortable": false }
            ]
        });

        $(EmployeeHourlySalaryProcessList.Control.gvEmployeePendingSalaryProcess).dataTable(
       {
           "aoColumnDefs": [
                   { "aTargets": [7], "bSortable": false }
           ]
       });
    },

    RediectSalarySave: function (empId) {
        window.location.href = "EmployeeHourlySalaryProcessSave.aspx?monthyear=" + $(EmployeeHourlySalaryProcessList.Control.ddlMonth).val() + "&employeeid=" + empId;
    },
});

