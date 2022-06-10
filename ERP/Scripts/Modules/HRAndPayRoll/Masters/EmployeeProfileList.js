var EmployeeList = ({
    Control: {
        gvEmployee: ".gvEmployee",
        btnDelete: ".btnDelete",
        btnPermenentDelete: ".btnPermenentDelete",
        lnkEditPopup: ".lnkEditPopup"
    },
    Variable: {
        DeletePermenentConfirm: "You are deleting this record permanently, are you sure ? You will loose his / her all records. ?",
    }

});

$(document).ready(function () {

    $('.timepicker').timepicker({
        timeFormat: 'h:mm:ss p',
        interval: 60,
        minTime: '01',
        maxTime: '12:00:00 PM',
        defaultTime: '07',
        startTime: '01:00:00 AM',
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });

    $(EmployeeList.Control.gvEmployee).dataTable(
        {
            "aoColumnDefs": [
                /*{ "aTargets": [3], "bSortable": false },*/
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

    $(document).on('click', EmployeeList.Control.lnkEditPopup, function () {
        alert('dasd');
    });
});
