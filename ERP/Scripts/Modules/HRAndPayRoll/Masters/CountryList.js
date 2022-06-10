var CountryList = ({
    Control: {
        gvCountry: ".gvCountry",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(CountryList.Control.gvCountry).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
            ]
        }
    );

    $(document).on("click", CountryList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Country']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});