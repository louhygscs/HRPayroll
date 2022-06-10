var DesignationList = ({
    Control: {
        gvDesignation: ".gvDesignation",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(DesignationList.Control.gvDesignation).dataTable(
        {
            "aoColumnDefs": [
                    { "aTargets": [1], "bSortable": false }
            ]
        }
    );

    $(document).on("click", DesignationList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Designation']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});
