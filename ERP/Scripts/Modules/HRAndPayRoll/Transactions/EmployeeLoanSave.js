var EmployeeLoanSave = ({
    Control: {
        txtLoanDate: ".txtLoanDate",
        txtAmount: ".txtAmount",
        txtTotalMonths: ".txtTotalMonths",
        lblInstallment: ".lblInstallment",
    },
    CalculateInstallment: function () {
        var Installment = 0;

        if ($(EmployeeLoanSave.Control.txtAmount).val() != '' && $(EmployeeLoanSave.Control.txtTotalMonths).val() != '') {
            if (!isNaN(parseFloat($(EmployeeLoanSave.Control.txtAmount).val())) && !isNaN(parseInt($(EmployeeLoanSave.Control.txtTotalMonths).val()))) {
                Installment = ($(EmployeeLoanSave.Control.txtAmount).val() / $(EmployeeLoanSave.Control.txtTotalMonths).val()).toFixed(2);
            }
        }

        $(EmployeeLoanSave.Control.lblInstallment).text(Installment);
    },
});

$(document).ready(function () {
    $(EmployeeLoanSave.Control.txtLoanDate).daterangepicker({
        singleDatePicker: true
    });
});
