var EmployeeDailySalaryProcessList = ({
    Control: {
        gvEmployeeCompletedSalaryProcess: ".gvEmployeeCompletedSalaryProcess",
        gvEmployeePendingSalaryProcess: ".gvEmployeePendingSalaryProcess",
        txtDate: ".txtDate",
        btnEdit: ".btnEdit",
    },
    InitailGridDataTable: function () {
        $(EmployeeDailySalaryProcessList.Control.gvEmployeeCompletedSalaryProcess).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [7], "bSortable": false }
            ]
        });

        $(EmployeeDailySalaryProcessList.Control.gvEmployeePendingSalaryProcess).dataTable(
       {
           "aoColumnDefs": [
                   { "aTargets": [7], "bSortable": false }
           ]
       });
    },

    RediectSalarySave: function (empId) {
        window.location.href = "EmployeeDailySalaryProcessSave.aspx?date=" + $(EmployeeDailySalaryProcessList.Control.txtDate).val() + "&employeeid=" + empId;
    },
});

