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
    public partial class HolidaySave : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liHoliday_liHR_liHRMasters";

            if (!IsPostBack)
            {
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
                Holiday _Holiday = new Holiday();

                _Holiday.HolidayID = new Guid(hfId.Value);
                _Holiday.Title = txtTitle.Text.Trim();
                _Holiday.Description = txtDescription.Text.Trim();

                if (!string.IsNullOrEmpty(txtDateRange.Value.Trim()))
                {
                    string[] _SplitDate = txtDateRange.Value.Trim().Split('-');

                    if (_SplitDate.Length > 1)
                    {
                        _Holiday.StartDate = GlobalHelper.StringToDate(_SplitDate[0]);
                        _Holiday.EndDate = GlobalHelper.StringToDate(_SplitDate[1]);
                    }
                }

                IHolidayService _IHolidayService = new HolidayService();

                Result<Boolean> _Result = _IHolidayService.SaveHoliday(_Holiday, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Holiday");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_Holiday.HolidayID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<Holiday>(_Result.Id, TableType.HolidayMaster, OperationType.Insert, _Holiday, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<Holiday>(Convert.ToString(_Holiday.HolidayID), TableType.HolidayMaster, OperationType.Update, _Holiday, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/HolidayList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Holiday") + "');});", true);
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
                IHolidayService _IHolidayService = new HolidayService();

                Result<Holiday> _Result = _IHolidayService.GetHolidayById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    txtDescription.Text = _Result.Data.Description;
                    txtTitle.Text = _Result.Data.Title;
                    txtDateRange.Value = _Result.Data.StartDate.ToString("MM/dd/yyyy") + " - " + _Result.Data.EndDate.ToString("MM/dd/yyyy");
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