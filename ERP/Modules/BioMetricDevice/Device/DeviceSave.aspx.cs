using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Web.UI;

namespace ERP.Modules.BioMetricDevice.Device
{
    public partial class DeviceSave : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IDeviceService _IDeviceService = new DeviceService();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            this.Form.DefaultButton = "btnSave";

            SessionHelper.SelectMenuSession = "liDevice_liBioMetricDevice";

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

        #region Private Events

        private void FillControls(Guid p_Id)
        {
            try
            {
                Result<DeviceModel> _Result = _IDeviceService.GetDeViceById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value = Convert.ToString(p_Id);
                    txtAddress.Text = _Result.Data.Address;
                    txtDeviceCode.Text = _Result.Data.DeviceCode;
                    txtDeviceName.Text = _Result.Data.DeviceName;
                    txtIPAddress.Text = _Result.Data.IPAddress;
                    txtPhone.Text = _Result.Data.PhoneNo;
                    txtPort.Text = Convert.ToString(_Result.Data.Port);
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

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DeviceModel _DeviceModel = new DeviceModel();

                _DeviceModel.DeviceID = new Guid(hfId.Value);
                _DeviceModel.DeviceName = txtDeviceName.Text.Trim();
                _DeviceModel.DeviceCode = txtDeviceCode.Text.Trim();
                _DeviceModel.Address = txtAddress.Text.Trim();
                _DeviceModel.PhoneNo = txtPhone.Text.Trim();
                _DeviceModel.IPAddress = txtIPAddress.Text.Trim();
                _DeviceModel.Port = Convert.ToInt32(txtPort.Text.Trim());

                Result<bool> _Result = _IDeviceService.SaveDevice(_DeviceModel, SessionHelper.SessionDetail.UserID);
                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Device");

                    IHistoryService _IHistoryService = new HistoryService();

                    if (_DeviceModel.DeviceID == Guid.Empty)
                    {
                        _IHistoryService.InsertHistory<DeviceModel>(_Result.Id, TableType.DeviceMaster, OperationType.Insert, _DeviceModel, SessionHelper.SessionDetail.UserID);
                    }
                    else
                    {
                        _IHistoryService.InsertHistory<DeviceModel>(Convert.ToString(_DeviceModel.DeviceID), TableType.DeviceMaster, OperationType.Update, _DeviceModel, SessionHelper.SessionDetail.UserID);
                    }

                    Response.Redirect("~/Modules/BioMetricDevice/Device/DeviceList.aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Device") + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion

    }
}