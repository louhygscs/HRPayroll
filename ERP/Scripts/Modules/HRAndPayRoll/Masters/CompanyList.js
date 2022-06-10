var CompanyList = ({
    Control: {
        gvList:    ".gvCompany",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(CompanyList.Control.gvList).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
            ]
        }
    );

    $(document).on("click", CompanyList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Company']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});