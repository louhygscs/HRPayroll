var EmployeeWeeklySalaryProcessList = ({
    Control: {
        gvEmployeeCompletedSalaryProcess: ".gvEmployeeCompletedSalaryProcess",
        gvEmployeePendingSalaryProcess: ".gvEmployeePendingSalaryProcess",
        txtDate: ".txtDate",
        btnEdit: ".btnEdit",
    },
    InitailGridDataTable: function () {
        $(EmployeeWeeklySalaryProcessList.Control.gvEmployeeCompletedSalaryProcess).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [7], "bSortable": false }
            ]
        });

        $(EmployeeWeeklySalaryProcessList.Control.gvEmployeePendingSalaryProcess).dataTable(
       {
           "aoColumnDefs": [
                   { "aTargets": [7], "bSortable": false }
           ]
       });
    },

    RediectSalarySave: function (empId) {
        window.location.href = "EmployeeWeeklySalaryProcessSave.aspx?date=" + $(EmployeeWeeklySalaryProcessList.Control.txtDate).val() + "&employeeid=" + empId;
    },
});

