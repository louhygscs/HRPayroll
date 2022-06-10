var InterviewSave = ({
    Control: {
        txtJoinDate: ".txtJoinDate",
        txtInterviewDate: ".txtInterviewDate",
        txtInterviewTime: ".txtInterviewTime",
    },
    ShowHideDocument: function (divViewDocument, divUploadDocument) {
        alert(divViewDocument);
        $(divViewDocument).css("display", "none");;
        $(divUploadDocument).css("display", "block");
    },
});

$(document).ready(function () {
    $(InterviewSave.Control.txtInterviewDate).daterangepicker({ singleDatePicker: true, minDate: new Date() });
    $(InterviewSave.Control.txtJoinDate).daterangepicker({ singleDatePicker: true });
    $(InterviewSave.Control.txtInterviewTime).daterangepicker({
        timePicker: true,
        singleDatePicker: true,
        timePicker24Hour: false,
        timePickerIncrement: 1,
        pick12HourFormat: true ,
        locale: {
            format: 'HH:mm A'
        },
       StartDate: new Date(),
    }).on('show.daterangepicker', function (ev, picker) {
        picker.container.find(".calendar-table").hide();
    });
});
