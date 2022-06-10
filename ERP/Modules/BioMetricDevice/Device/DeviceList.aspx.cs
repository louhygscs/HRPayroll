using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using zkemkeeper;

namespace ERP.Modules.BioMetricDevice.Device
{
    public partial class DeviceList : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IDeviceService _IDeviceService = new DeviceService();
        IHistoryService _IHistoryService = new HistoryService();
        private List<DeviceModel> _DeviceList = null;
        CZKEM CtrlBioComm = new CZKEM();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liDevice_liBioMetricDevice";

            FillDeviceList();

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionHelper.MessageSession))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + SessionHelper.MessageSession + "');});", true);
                    SessionHelper.RemoveMessageSession();
                }
            }
        }

        #endregion

        #region Events

        protected void gvDevice_PreRender(object sender, EventArgs e)
        {
            try
            {
                gvDevice.DataSource = _DeviceList;
                gvDevice.DataBind();

                if (gvDevice.Rows.Count > 0)
                {
                    gvDevice.UseAccessibleHeader = true;
                    gvDevice.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        protected void btnConnectDevice_Click(object sender, EventArgs e)
        {

            LinkButton _btnConnectDevice = (LinkButton)sender;

            Guid _DeviceId = new Guid(_btnConnectDevice.CommandArgument);

            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            int _RowIndex = clickedRow.RowIndex;
            string _DeviceStatus = gvDevice.Rows[_RowIndex].Cells[2].Text;
            string _IPAddress = Convert.ToString(gvDevice.Rows[_RowIndex].Cells[3].Text);
            Int32 _Port = Convert.ToInt32(gvDevice.Rows[_RowIndex].Cells[4].Text);
            GlobalHelper.Connect(Convert.ToString(_IPAddress));

            bool _Connected = false;
            try
            {
                if (_DeviceStatus == "DisConnected")
                {
                    _Connected = CtrlBioComm.Connect_Net(_IPAddress, _Port);

                    if (_Connected)
                    {
                        gvDevice.Rows[_RowIndex].Cells[2].Text = Convert.ToString(ConnectionStatusValue.Connected);

                        DeviceModel _Device = _DeviceList.Where(x => x.DeviceID == _DeviceId).FirstOrDefault();
                        if (_Device != null)
                        {
                            _Device.ConnectionStatus = Convert.ToString(ConnectionStatusValue.Connected);
                            DeviceSessionDetail _DeviceSessionDetail = new DeviceSessionDetail();
                            _DeviceSessionDetail.DeviceId = _Device.DeviceID;
                            _DeviceSessionDetail.IsConnected = true;
                            _DeviceSessionDetail.IPAddress = _Device.IPAddress;
                            _DeviceSessionDetail.Port = _Device.Port;
                            _DeviceSessionDetail.DeviceName = _Device.DeviceName;
                            SessionHelper.DeviceSessionDetail = _DeviceSessionDetail;
                        }
                    }
                }
                else
                {
                    gvDevice.Rows[_RowIndex].Cells[2].Text = Convert.ToString(ConnectionStatusValue.DisConnected);

                    DeviceModel _Device = _DeviceList.Where(x => x.DeviceID == _DeviceId).FirstOrDefault();
                    if (_Device != null)
                    {
                        _Device.ConnectionStatus = Convert.ToString(ConnectionStatusValue.DisConnected);
                        SessionHelper.RemoveDeviceSessionDetail();
                    }
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton _btnDelete = (LinkButton)sender;

                Guid _DeviceId = new Guid(_btnDelete.CommandArgument);

                Result<Boolean> _Result = _IDeviceService.DeleteDeviceById(_DeviceId, SessionHelper.SessionDetail.UserID);

                if (_Result.IsSuccess)
                {
                    DeviceModel _Device = _DeviceList.Where(x => x.DeviceID == _DeviceId).FirstOrDefault();
                    if (_Device != null)
                    {
                        _DeviceList.Remove(_Device);
                    }

                    SessionHelper.RemoveDeviceSessionDetail();

                    _IHistoryService.InsertHistory<Guid>(Convert.ToString(_DeviceId), TableType.DeviceMaster, OperationType.Delete, _DeviceId, SessionHelper.SessionDetail.UserID);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "DeletionSuccessMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + String.Format(GlobalMsg.DeletionSuccessMsg, "Device") + "');});", true);
                    // gvDevice_PreRender(gvDevice, new EventArgs());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "DeletionFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion

        #region Private Methods

        public void FillDeviceList()
        {
            try
            {
                SessionHelper.RemoveDeviceSessionDetail();

                Result<List<DeviceModel>> _Result = _IDeviceService.GetDeviceList();

                if (_Result.IsSuccess)
                {
                    _DeviceList = _Result.Data;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "GetFailMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _Result.Message + "');});", true);
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