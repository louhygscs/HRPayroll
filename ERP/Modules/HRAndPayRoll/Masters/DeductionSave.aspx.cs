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
    public partial class DeductionSave : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liDeduction_liHR_liHRMasters";

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
                Deduction _Deduction = new Deduction();

                _Deduction.DeductionID = new Guid(hfId.Value);
                _Deduction.DeductionName = txtDeduction.Text.Trim();
                _Deduction.IsConsider = chkIsConsider.Checked;

                IDeductionService _IDeductionService = new DeductionService();
                
                Result<Boolean> _Result = _IDeductionService.SaveDeduction(_Deduction, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Deduction");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_Deduction.DeductionID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<Deduction>(_Result.Id, TableType.DeductionMaster, OperationType.Insert, _Deduction, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<Deduction>(Convert.ToString(_Deduction.DeductionID), TableType.DeductionMaster, OperationType.Update, _Deduction, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/DeductionList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Deduction") + "');});", true);
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
                IDeductionService _IDeductionService = new DeductionService();

                Result<Deduction> _Result = _IDeductionService.GetDeductionById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    txtDeduction.Text = _Result.Data.DeductionName;
                    chkIsConsider.Checked = _Result.Data.IsConsider;
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