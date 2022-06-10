var prm = Sys.WebForms.PageRequestManager.getInstance();
prm.add_beginRequest(BeginRequestHandler);
prm.add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args) {
    Common.ShowLoading();
}
function EndRequestHandler(sender, args) {
    Common.HideLoading();
}