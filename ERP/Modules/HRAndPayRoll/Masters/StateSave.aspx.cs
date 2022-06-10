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

namespace ERP.Modules.HRAndPayRoll.Masters
{
    public partial class StateSave : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liState_liHR_liHRMasters";

            if (!IsPostBack)
            {
                hfId.Value = Convert.ToString(Guid.Empty);

                FillCountry();

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
                State _State = new State();

                _State.StateID   = new Guid(hfId.Value);
                _State.CountryID = Guid.Parse(this.ddlCountry.SelectedValue.Trim());
                _State.StateName = txtState.Text.Trim();
                

                IStateService _IStateService = new StateService();

                Result<Boolean> _Result = _IStateService.SaveState(_State, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "State");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_State.StateID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<State>(_Result.Id, TableType.StateMaster, OperationType.Insert, _State, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<State>(Convert.ToString(_State.StateID), TableType.StateMaster, OperationType.Update, _State, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/StateList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Education") + "');});", true);
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

        private void FillCountry()
        {
            Result<List<Item>> _Result = _ILookupService.GetAllCountry();

            if (_Result.IsSuccess)
            {
                ddlCountry.DataTextField = "Text";
                ddlCountry.DataValueField = "Id";
                ddlCountry.DataSource = _Result.Data;
                ddlCountry.DataBind();
            }

            ddlCountry.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        private void FillControls(Guid p_Id)
        {
            try
            {
                IStateService _IStateService = new StateService();

                Result<State> _Result = _IStateService.GetStateById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value               = Convert.ToString(p_Id);
                    ddlCountry.SelectedValue = _Result.Data.CountryID.ToString();
                    txtState.Text            = _Result.Data.StateName;
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