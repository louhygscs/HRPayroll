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

namespace ERP.Modules.HRAndPayRoll.Masters.User
{
    public partial class UserSave : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liCutOff_liHR_liEmployeeProfile";

            if (!IsPostBack)
            {
                FillWorkLocation();
                FillEmployeeProfiles(Guid.Parse("84B59A87-D816-4887-86D5-23B7818A26A6"));
                FillRole();

                hfId.Value = Convert.ToString(Guid.Empty);

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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UserModel _userModel = new UserModel();

                if (Request.QueryString["id"] != null)
                {
                    Guid _id;

                    bool _ResultParse = Guid.TryParse(Convert.ToString(Request.QueryString["id"]), out _id);

                    if (_ResultParse)
                    {
                        _userModel.UserID = _id;
                    } 
                }

                _userModel.RoleId = Guid.Parse(drpRole.SelectedValue.ToString());
                _userModel.EmployeeId = Guid.Parse(drpEmployee.SelectedValue.ToString());
                _userModel.Username = txtUsername.Text.Trim();
                _userModel.Password = SecurityHelper.EncryptString(txtPassword.Text.Trim());

                IUserService _IUserService = new UserService();

                Result<Boolean> _Result = _IUserService.CreateUser(_userModel, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "User");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_userModel.UserID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<UserModel>(_Result.Id, TableType.UserMaster, OperationType.Insert, _userModel, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<UserModel>(Convert.ToString(_userModel.UserID), TableType.UserMaster, OperationType.Update, _userModel, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/User/UserList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Country") + "');});", true);
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

        private void FillWorkLocation()
        {
            drpWorkLocation.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetWorkLocation(Guid.Parse("F699B807-07E7-45FD-BAC6-0F3BFF1E34BA"));

            if (_Result.IsSuccess)
            {
                drpWorkLocation.DataTextField = "Text";
                drpWorkLocation.DataValueField = "Id";
                drpWorkLocation.DataSource = _Result.Data;
                drpWorkLocation.DataBind();
            }

            drpWorkLocation.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillRole()
        {
            drpRole.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetRoleList();

            if (_Result.IsSuccess)
            {
                drpRole.DataTextField = "Text";
                drpRole.DataValueField = "Id";
                drpRole.DataSource = _Result.Data;
                drpRole.DataBind();
            }

            drpRole.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillEmployeeProfiles(Guid p_WorkLocationId)
        {
            drpEmployee.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetAllActiveEmployeeProfile(p_WorkLocationId);

            if (_Result.IsSuccess)
            {
                drpEmployee.DataTextField = "Text";
                drpEmployee.DataValueField = "Id";
                drpEmployee.DataSource = _Result.Data;
                drpEmployee.DataBind();
            }

            drpEmployee.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillControls(Guid p_Id)
        {
            try
            {
                IUserService _IUserService = new UserService();

                Result<UserModel> _Result = _IUserService.GetUserById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value                    = Convert.ToString(p_Id);
                    drpWorkLocation.SelectedValue = _Result.Data.WorkLocationId.ToString();
                    drpEmployee.SelectedValue     = _Result.Data.EmployeeId.ToString();
                    drpRole.SelectedValue         = _Result.Data.RoleId.ToString();

                    txtUsername.Text = _Result.Data.Username;
                    txtPassword.Text = SecurityHelper.DecryptString(_Result.Data.Password);
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

        #endregion

    }
}