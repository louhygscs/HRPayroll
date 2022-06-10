var EmployeeSalaryProcessList = ({
    Control: {
        gvEmployeeCompletedSalaryProcess: ".gvEmployeeCompletedSalaryProcess",
        gvEmployeePendingSalaryProcess: ".gvEmployeePendingSalaryProcess",
        ddlMonth: ".ddlMonth",
        btnEdit: ".btnEdit",
        txtDate:".txtDate",
    },
    InitailGridDataTable: function () {
        $(EmployeeSalaryProcessList.Control.gvEmployeeCompletedSalaryProcess).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [7], "bSortable": false }
            ]
        });

        $(EmployeeSalaryProcessList.Control.gvEmployeePendingSalaryProcess).dataTable(
       {
           "aoColumnDefs": [
                   { "aTargets": [7], "bSortable": false }
           ]
       });
    },

    RediectSalarySave: function (empId) {
        window.location.href = "EmployeeSalaryProcessSave.aspx?monthyear=" + $(EmployeeSalaryProcessList.Control.ddlMonth).val() + "&employeeid=" + empId;
    },
});

