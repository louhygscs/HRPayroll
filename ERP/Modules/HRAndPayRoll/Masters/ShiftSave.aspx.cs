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
    public partial class ShiftSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion


        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liShift_liHR_liHRMasters";

            if (!IsPostBack)
            {
                hfId.Value = Convert.ToString(Guid.Empty);

                //txtFromTime.Text = DateTime.Now.ToString("hh:mm tt");
                //txtToTime.Text = DateTime.Now.AddHours(9).ToString("hh:mm tt");

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
                Shift _Shift = new Shift();

                _Shift.ShiftID = new Guid(hfId.Value);
                _Shift.ShiftName = txtShift.Text.Trim();
                _Shift.FromTime = ddlFromHour.SelectedValue + ":" + ddlFromMinute.SelectedValue + " " + ddlFromMeridiem.SelectedValue;
                _Shift.ToTime = ddlToHour.SelectedValue + ":" + ddlToMinute.SelectedValue + " " + ddlToMeridiem.SelectedValue;

                IShiftService _IShiftService = new ShiftService();

                Result<Boolean> _Result = _IShiftService.SaveShift(_Shift, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Shift");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_Shift.ShiftID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<Shift>(_Result.Id, TableType.ShiftMaster, OperationType.Insert, _Shift, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<Shift>(Convert.ToString(_Shift.ShiftID), TableType.ShiftMaster, OperationType.Update, _Shift, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/ShiftList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Shift") + "');});", true);
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
                IShiftService _IShiftService = new ShiftService();

                Result<Shift> _Result = _IShiftService.GetShiftById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    txtShift.Text = _Result.Data.ShiftName;
                    ddlFromHour.SelectedValue = _Result.Data.FromTime.Substring(0, 2);
                    ddlFromMinute.SelectedValue = _Result.Data.FromTime.Substring(3, 2);
                    ddlFromMeridiem.SelectedValue = _Result.Data.FromTime.Substring(6, 2);
                    ddlToHour.SelectedValue = _Result.Data.ToTime.Substring(0, 2);
                    ddlToMinute.SelectedValue = _Result.Data.ToTime.Substring(3, 2);
                    ddlToMeridiem.SelectedValue = _Result.Data.ToTime.Substring(6, 2);
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