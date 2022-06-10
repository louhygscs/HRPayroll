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
    public partial class GenerateCutOff : System.Web.UI.Page
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

            //drpCutOffFirstDay.Items.Clear();
            //drpCutOffSecondDay.Items.Clear();

            for (int i = 1; i <= 31; i++)
            {
                ListItem itm = new ListItem();
                itm.Text = i.ToString();
                itm.Value = i.ToString();

                drpCutOffFirstDay.Items.Add(itm);
            }

            for (int i = 1; i <= 31; i++)
            {
                ListItem itm = new ListItem();
                itm.Text = i.ToString();
                itm.Value = i.ToString();

                drpCutOffSecondDay.Items.Add(itm);
            }
        }

        #endregion

        #region Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Result<Boolean> _Result = null;

                IPayrollService _iService = new PayrollService();

                //List<DateTime> lstDate = new List<DateTime>();

                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime endDate = DateTime.Parse(txtEndDate.Text);

                int firstDay = int.Parse(drpCutOffFirstDay.Text);
                int secondDay = int.Parse(drpCutOffSecondDay.Text);

                DateTime _tempDate1 = new DateTime(startDate.Year, startDate.Month, firstDay);
                DateTime _tempDate2 = new DateTime(startDate.Year, startDate.Month, secondDay);

                int days = _tempDate2.Subtract(_tempDate1).Days;
                
                while (startDate <= endDate)
                {
                    if (startDate.Day == firstDay)
                    {
                        PayrollCutOff _itmFirst = new PayrollCutOff();

                        DateTime _sDate = new DateTime();
                        DateTime _eDate = new DateTime();

                        _sDate = startDate.AddDays(days * -1);
                        _eDate = startDate.AddDays(-1);

                        _itmFirst.StartDate   = _sDate;
                        _itmFirst.EndDate     = _eDate;
                        _itmFirst.ActualDate  = _eDate.AddDays(1);
                        _itmFirst.Remarks     = string.Format("Auto Generated {0}",txtRemarks.Text);
                        _itmFirst.CreatedById = SessionHelper.SessionDetail.UserID;

                        _Result = _iService.SaveCutOffPeriod(_itmFirst);
                    }

                    if (startDate.Day == secondDay)
                    {
                        PayrollCutOff _itmSecond = new PayrollCutOff();

                        DateTime _sDate = new DateTime();
                        DateTime _eDate = new DateTime();

                        _sDate = startDate.AddDays(days * -1);
                        _eDate = startDate.AddDays(-1);

                        _itmSecond.StartDate       = _sDate.AddDays(1);
                        _itmSecond.EndDate         = _eDate;
                        _itmSecond.ActualDate      = _eDate.AddDays(1);
                        _itmSecond.Remarks         = string.Format("Auto Generated {0}", txtRemarks.Text);
                        _itmSecond.CreatedById     = SessionHelper.SessionDetail.UserID;

                        _Result = _iService.SaveCutOffPeriod(_itmSecond);
                    }

                    startDate = startDate.AddDays(1);
                }

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

        #endregion


    }
}