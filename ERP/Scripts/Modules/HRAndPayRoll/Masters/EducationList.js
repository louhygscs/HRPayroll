var EducationList = ({
    Control: {
        gvEducation: ".gvEducation",
        btnDelete: ".btnDelete",
    }
});

$(document).ready(function () {
    $(EducationList.Control.gvEducation).dataTable(
        {
            "aoColumnDefs": [
                { "aTargets": [1], "bSortable": false }
            ]
        }
    );

    $(document).on("click", EducationList.Control.btnDelete, function () {
        var confirmAns = confirm(Common.ReplacementString(Common.Variable.DeleteConfirm, ['Education']));
        if (confirmAns == true)
            return true;
        else
            return false;
    });

});