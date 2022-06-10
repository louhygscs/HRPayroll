var DeviceList = ({
    Control: {
        gvDevice: ".gvDevice",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(DeviceList.Control.gvDevice).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [5], "bSortable": false }
            ]
        }
    );

    $(document).on("click", DeviceList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Device']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
