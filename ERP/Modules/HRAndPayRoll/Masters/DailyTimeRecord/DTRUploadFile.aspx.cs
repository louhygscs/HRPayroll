using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Modules.HRAndPayRoll.Masters.DailyTimeRecord
{
    public partial class DTRUploadFile : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liCutOff_liHR_liEmployeeProfile";

            if (!IsPostBack)
            {
                //fill up
                FillWorkLocation();
                FillCutOffPeriod_TimeSummary();
            }
        }
        #endregion

        #region Events
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                IPayrollService _iService = new PayrollService();

                Guid _ctOffId = Guid.Parse(drpCutOffPeriod.SelectedValue.ToString());
                string inputContent = string.Empty;

                if (fuPhoto.HasFile)
                {
                    using (StreamReader inpStrmRdr = new StreamReader(fuPhoto.PostedFile.InputStream))
                    {
                        inputContent = inpStrmRdr.ReadToEnd();
                    }
                }

                ParseUploadTimelogs(_ctOffId, inputContent);
                
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }

        }
        #endregion

        #region Methods

        private void ParseUploadTimelogs(Guid _cutOffPeriod, string inputContent)
        {
            IPayrollService _iService = new PayrollService();

            Result<bool> _Result = new Result<bool>();

            if (!string.IsNullOrEmpty(inputContent))
            {
                string[] strResults = inputContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                string _timeType = string.Empty;

                foreach(string strLine in strResults)
                {
                    if(_timeType != "Time In")
                    {
                        _timeType = "Time In";
                    } else
                    {
                        _timeType = "Time Out";
                    }

                    string[] strItemLine = strLine.Split(new string[] { "\t" }, StringSplitOptions.None);

                    PayrollDTRRawTimeLogModel _timeLog = new PayrollDTRRawTimeLogModel()
                    {
                        LineNo     = strItemLine[0],
                        StaffCode  = strItemLine[1],
                        EmpName    = strItemLine[2],
                        Department = strItemLine[3],
                        UserId     = strItemLine[4],
                        Week       = strItemLine[5],
                        StrDate    = DateTime.Parse(strItemLine[6]),
                        StrTime    = TimeSpan.Parse(strItemLine[7]),
                        MachineId  = strItemLine[8],
                        Remark     = strItemLine[9]
                    };

                    if(!string.IsNullOrEmpty(_timeLog.StaffCode))
                    {
                        Result<EmployeeProfileModel> _empMdl = _iService.GetEmployeeProfileByStaffCode(_timeLog.StaffCode);

                        if (_empMdl != null)
                        {
                            DTRRawModel _pyMdl = new DTRRawModel()
                            {
                                EmployeeId = _empMdl.Data.EmployeeId,
                                CutOffId   = _cutOffPeriod,
                                StaffCode  = _empMdl.Data.StaffCode,
                                TimeType   = _timeType,
                                ActualDate = _timeLog.StrDate,
                                ActualTime = _timeLog.StrTime,
                                FromType   = "Uploaded",
                                RawOrder   = int.Parse(_timeLog.LineNo)
                            };

                            _Result = _iService.SaveDTRRawModel(_pyMdl);
                        }
                    }
                }
            }
        }

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

        private void FillCutOffPeriod_TimeSummary()
        {
            drpCutOffPeriod.Items.Clear();

            Result<List<Item>> _Result = _ILookupService.GetCutOffPeriod();

            if (_Result.IsSuccess)
            {
                drpCutOffPeriod.DataTextField = "Text";
                drpCutOffPeriod.DataValueField = "Id";
                drpCutOffPeriod.DataSource = _Result.Data;
                drpCutOffPeriod.DataBind();
            }

            drpCutOffPeriod.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });
        }

        #endregion
    }
}