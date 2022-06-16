var RoleList = ({
    Control: {
        gvRole: ".gvRole",
        btnDelete: ".btnDelete"
    }
});

$(document).ready(function () {

    $(RoleList.Control.gvRole).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
            ]
        }
    );

});