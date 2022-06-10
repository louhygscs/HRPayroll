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
    public partial class CutOffSave : System.Web.UI.Page
    {
        #region Variables
        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liCutOff_liHR_liHRMasters";

            //drpMonthDay.Items.Clear();

            //for (int i = 1; i <= 31; i++)
            //{
            //    ListItem itm = new ListItem();
            //    itm.Text     = i.ToString();
            //    itm.Value    = i.ToString();

            //    drpMonthDay.Items.Add(itm);
            //}

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
                IPayrollService _iService = new PayrollService();

                PayrollCutOff _item = new PayrollCutOff();

                _item.PayrollCutOffId = new Guid(hfId.Value);
                _item.CutOffCode      = txtCutOffCode.Text;
                _item.StartDate       = DateTime.Parse(txtStartDate.Text.Trim());
                _item.EndDate         = DateTime.Parse(txtEndDate.Text.Trim());
                _item.IsActive        = rbtnActive.Checked;
                _item.Remarks         = txtRemarks.Text;
                _item.CreatedById     = SessionHelper.SessionDetail.UserID;
                _item.ActualDate      = DateTime.Parse(txtEndDate.Text.Trim()); ///int.Parse(drpMonthDay.SelectedValue.ToString());

                Result<Boolean> _Result = _iService.SaveCutOffPeriod(_item);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "CutOff Period");

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/CutOff/CutOffList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "CutOff Period") + "');});", true);
                }
            }
            catch (Exception _ex)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _ex);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }
        #endregion

        #region Methods
        private void FillControls(Guid p_Id)
        {
            try
            {
                IPayrollService _IService = new PayrollService();

                Result<PayrollCutOff> _Result = _IService.GetByCutOffPeriodId(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value         = Convert.ToString(p_Id);
                    txtCutOffCode.Text = _Result.Data.CutOffCode;
                    txtStartDate.Text  = _Result.Data.StartDate.ToString("yyyy-MM-dd");
                    txtEndDate.Text    = _Result.Data.EndDate.ToString("yyyy-MM-dd");
                    txtActualDate.Text = _Result.Data.ActualDate.ToString("yyyy-MM-dd");
                    txtRemarks.Text    = _Result.Data.Remarks;

                    if (_Result.Data.IsActive.HasValue)
                    {
                        if (_Result.Data.IsActive.Value)
                        {
                            rbtnActive.Checked = true;
                            rbtnInactive.Checked = false;
                        }
                        else
                        {
                            rbtnActive.Checked = false;
                            rbtnInactive.Checked = true;
                        }
                    }
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