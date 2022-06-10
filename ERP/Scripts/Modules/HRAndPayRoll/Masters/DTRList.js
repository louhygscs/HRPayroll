var DTRList = ({
    Control: {
        gvDTRDaily: ".gvDTRDaily",
        gvDTRDailyTimelogs: ".gvDTRDailyTimelogs",
        btnDelete: ".btnDelete",
        btnPermenentDelete: ".btnPermenentDelete",
    },
    Variable: {
        DeletePermenentConfirm: "You are deleting this record permanently, are you sure ? You will loose his / her all records. ?",
    }

});

$(document).ready(function () {
    $(DTRList.Control.gvDTRDaily).dataTable();

    $('#cphBody_btnGenerate').click(function () {
        $(DTRList.Control.gvDTRDailyTimelogs).dataTable();
    });

});
