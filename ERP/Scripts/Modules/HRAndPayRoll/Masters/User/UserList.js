var UserList = ({
    Control: {
        gvUser: ".gvUser",
        btnDeActive: ".btnDeActive",
        btnResetPassword: ".btnResetPassword"
    }
});

$(document).ready(function () {
    $(UserList.Control.gvUser).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
            ]
        }
    );

    //$(document).on("click", UserList.Control.btnDeActive, function () {

    //    //var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Country']));
    //    //if (confirmAns == true)
    //    //    return true;
    //    //else
    //    //    return false;
    //});

});