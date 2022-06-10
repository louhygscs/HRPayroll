using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.HRAndPayRoll.Masters.ClientProfile
{
    public partial class ClientProfileSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILookupService _ILookupService = new LookupService();
        #endregion

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liCountry_liHR_liHRMasters";

            if (!IsPostBack)
            {
                hfId.Value = Convert.ToString(Guid.Empty);

                FillCategory();
                FillCountry();

                drpCountry.SelectedValue = "815A5321-D34E-47F9-ADD8-0DE89B9F0556";

                FillState();

                if (Request.QueryString["id"] != null)
                {
                    Guid _id;

                    bool _Result = Guid.TryParse(Convert.ToString(Request.QueryString["id"]), out _id);

                    if (_Result)
                    {
                        FillControls(_id);
                    }
                }
            }
        }
        #endregion

        #region Events
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ICompanyService _iService = new CompanyService();

                Model.Company _model = new Model.Company();

                _model.CompanyName  = txtCompanyName.Text;
                _model.CategoryId   = Guid.Parse(drpCategory.SelectedValue.ToString());
                _model.EmailAddress = txtEmailAddress.Text;

                _model.CountryId = Guid.Parse(drpCountry.SelectedValue.ToString());
                _model.StateId   = Guid.Parse(drpState.SelectedValue.ToString());
                _model.City      = txtCity.Text;

                _model.Address   = txtAddress.Text;

                _model.MobileNo  = txtMobileNo.Text;
                _model.PhoneNo   = txtPhoneNo.Text;
                _model.HotLineNo = txtHotlineNo.Text;

                _model.FaxNo   = txtFaxNo.Text;
                _model.WebSite = txtWebsite.Text;
                _model.TINNo   = txtTINNo.Text;

                _model.BusinessPermitNo = txtBusinessPermitNo.Text;

                _model.Remarks = txtAddress.Text;

                Result<bool> _Result = _iService.SaveCompany(_model, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Client Profile");

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/Company/CompanyList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "CutOff Period") + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }
        #endregion

        #region Methods
        private void FillControls(Guid p_Id)
        {
            try
            {
                ICompanyService _IService = new CompanyService();

                Result<Model.Company> _Result = _IService.GetCompany(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);

                    txtCompanyName.Text       = _Result.Data.CompanyName;
                    drpCategory.SelectedValue = _Result.Data.CategoryId.ToString();
                    txtEmailAddress.Text      = _Result.Data.EmailAddress;

                    drpCountry.SelectedValue = _Result.Data.CountryId.ToString();
                    drpState.SelectedValue   = _Result.Data.StateId.ToString();
                    txtCity.Text             = _Result.Data.City;

                    txtAddress.Text = _Result.Data.Address;

                    txtMobileNo.Text  = _Result.Data.MobileNo;
                    txtPhoneNo.Text   = _Result.Data.PhoneNo;
                    txtHotlineNo.Text = _Result.Data.HotLineNo.Trim();

                    txtFaxNo.Text   = _Result.Data.FaxNo;
                    txtWebsite.Text = _Result.Data.WebSite;
                    txtTINNo.Text   = _Result.Data.TINNo;

                    txtBusinessPermitNo.Text = _Result.Data.BusinessPermitNo;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        private void FillCategory()
        {
            Result<List<Item>> _Result = _ILookupService.GetTableCategory("CompanyMaster");

            if (_Result.IsSuccess)
            {
                drpCategory.DataTextField = "Text";
                drpCategory.DataValueField = "Id";
                drpCategory.DataSource = _Result.Data;
                drpCategory.DataBind();
            }

            drpCategory.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillCountry()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllCountry();

            if (_Result.IsSuccess)
            {
                drpCountry.DataTextField = "Text";
                drpCountry.DataValueField = "Id";
                drpCountry.DataSource = _Result.Data;
                drpCountry.DataBind();
            }

            drpCountry.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillState()
        {
            drpState.Items.Clear();

            string _CountryId = "815A5321-D34E-47F9-ADD8-0DE89B9F0556";

            if (!string.IsNullOrEmpty(_CountryId))
            {
                Result<List<Item>> _Result = _ILookupService.GetAllStateByCountryId(new Guid(_CountryId));

                if (_Result.IsSuccess)
                {
                    drpState.DataTextField = "Text";
                    drpState.DataValueField = "Id";
                    drpState.DataSource = _Result.Data;
                    drpState.DataBind();
                }

                drpState.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
            }
        }

        #endregion
    }
}