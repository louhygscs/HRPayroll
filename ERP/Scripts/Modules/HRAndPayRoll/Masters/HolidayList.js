var HolidayList = ({
    Control: {
        gvHoliday: ".gvHoliday",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(HolidayList.Control.gvHoliday).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [3], "bSortable": false }
            ]
        }
    );

    $(document).on("click", HolidayList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Holiday']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
