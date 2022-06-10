var EmployeeSalarySave = ({
    Control: {
        txtBasic: ".txtBasic",
        txtAllowance: ".txtAllowance",
        txtDeduction: ".txtDeduction",
        lblTotalAllowance: ".lblTotalAllowance",
        lblTotalDeduction: ".lblTotalDeduction",
        lblTotalSalary: ".lblTotalSalary",
        hfTotalSalary:"#hfTotalSalary",
        hfTotalAllowance:"#hfTotalAllowance",
        hfTotalDeduction: "#hfTotalDeduction",
    },

    CalculateTotal: function () {
        var Total = 0;
        var TotalAllowance = 0;
        var TotalDeduction = 0;

        if ($(EmployeeSalarySave.Control.txtBasic).val() != '') {
            if (!isNaN(parseFloat($(EmployeeSalarySave.Control.txtBasic).val()))) {
                Total = parseFloat($(EmployeeSalarySave.Control.txtBasic).val());
            }
        }

        $(EmployeeSalarySave.Control.txtAllowance).each(function () {
            if ($(this).val() != '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    TotalAllowance = TotalAllowance + parseFloat($(this).val());
                }
            }
        });

        $(EmployeeSalarySave.Control.txtDeduction).each(function () {
            if ($(this).val() != '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    TotalDeduction = TotalDeduction + parseFloat($(this).val());
                }
            }
        });
        $(EmployeeSalarySave.Control.lblTotalAllowance).text(TotalAllowance.toFixed(2));
        $(EmployeeSalarySave.Control.hfTotalAllowance).val(TotalAllowance.toFixed(2));
        $(EmployeeSalarySave.Control.lblTotalDeduction).text(TotalDeduction.toFixed(2));
        $(EmployeeSalarySave.Control.hfTotalDeduction).val(TotalDeduction.toFixed(2));
        $(EmployeeSalarySave.Control.lblTotalSalary).text((Total + TotalAllowance - TotalDeduction).toFixed(2));
        $(EmployeeSalarySave.Control.hfTotalSalary).val((Total + TotalAllowance - TotalDeduction).toFixed(2));
    }
});
