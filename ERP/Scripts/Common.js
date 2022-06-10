var Common = ({
    Control: {
        Loading: "#loadingPanel",
        liSubMenu: ".liSubMenu",
    },
    Variable: {
        Success: 'Success',
        Error: 'Error',
        Info: 'Info',
        Warning: 'Warning',
        DeleteConfirm: "Are you sure, you want to delete this {0} ?",
    },
    ShowLoading: function () {
        $(Common.Control.Loading).show();
    },
    HideLoading: function () {
        $(Common.Control.Loading).hide();
    },
    ShowToastrMessage: function (messageType, messageTitle, message) {
        toastr.options.positionClass = 'toast-top-full-width';
        toastr[messageType.toLowerCase()](message, messageTitle);
        Common.HideLoading();
    },
    ReplacementString: function (string, replacementArray) {
        return string.replace(/({\d})/g, function (j) {
            return replacementArray[j.replace(/{/, '').replace(/}/, '')];
        });
    },
    SelectCurrentMenu: function (currentListItem) {
        var arrow_class_open = 'fa fa-angle-down',
			arrow_class_closed = 'fa fa-angle-left';

        $(Common.Control.liSubMenu).removeClass('current');

        var currentListItemArray = currentListItem.split('_');

        for (var no = 0; no < currentListItemArray.length; no++) {
            $('.' + currentListItemArray[no]).addClass('current');

            if (no > 0) {
                $('.' + currentListItemArray[no]).children('a').children('i.arrow').removeClass(arrow_class_closed).addClass(arrow_class_open);
            }
        }
    },
    isNumericKey: function (evt, txtControl) {
        var charCode = (evt.which) ? evt.which : evt.keyCode

        if (charCode == 46) {
            var inputValue = $(txtControl).val()
            if (inputValue.indexOf('.') < 1) {
                return true;
            }
            return false;
        }
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    },
    isNumberKey: function (evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode

        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    },
});




$(document).ready(function () {
    $('.txtSpaceRemoveOnly').on('keyup', function (event) {
        this.value = this.value.replace(/\s/g, '');
    });


    $('.txtNumber').on('input', function (event) {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

});