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

namespace ERP.Modules.BioMetricDevice.Maintenance
{
    public partial class SendEmployee : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IDeviceService _IDeviceService = new DeviceService();
        IEmployeeService _IEmployeeService = new EmployeeService();
        IEmployeeDeviceMapService _IEmployeeDeviceMapService = new EmployeeDeviceMapService();
        CZKEM CtrlBioComm = new CZKEM();
        string _ErrorMessage = " ";

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liSendEmployeeToDevice_liBioMetricDevice_liMaintenance";

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

        protected void btnConfrim_Click(object sender, EventArgs e)
        {
            try
            {
                #region CONNECT DEVICE

                if (SessionHelper.DeviceSessionDetail != null)
                {
                    bool _Connected = false;

                    _Connected = CtrlBioComm.Connect_Net(SessionHelper.DeviceSessionDetail.IPAddress, SessionHelper.DeviceSessionDetail.Port);

                    if (_Connected)
                    {
                        Result<DeviceModel> _Result = _IDeviceService.GetDeViceById(SessionHelper.DeviceSessionDetail.DeviceId);

                        if (CtrlBioComm.IsTFTMachine(1))
                        {
                            _ErrorMessage = GetAndUpdateData(_Result.Data, true, _ErrorMessage);
                        }
                        else
                        {
                            _ErrorMessage = GetAndUpdateData(_Result.Data, false, _ErrorMessage);
                        }

                        CtrlBioComm.Disconnect();
                        SessionHelper.RemoveDeviceSessionDetail();
                    }
                    else
                    {
                        if (_ErrorMessage == "")
                        {
                            _ErrorMessage = "Unable to connect " + SessionHelper.DeviceSessionDetail.DeviceName + " device";
                        }
                        else
                        {
                            _ErrorMessage += "\nUnable to connect " + SessionHelper.DeviceSessionDetail.DeviceName + " device.";
                        }
                    }
                }
                else
                {
                    _ErrorMessage = "Unable to connect device , please Check Network / Firewall ";
                }

                #region Old Code

                //Result<List<DeviceModel>> _Result = _IDeviceService.GetDeviceList();

                //if (_Result.IsSuccess)
                //{
                //    foreach (DeviceModel _DeviceItem in _Result.Data)
                //    {
                //        GlobalHelper.Connect(_DeviceItem.IPAddress);
                //    }

                //    string _Test = IPInfo.GetARPResult();

                //    if (_Result.Data.Where(q => _Test.Contains(Convert.ToString(q.IPAddress).Trim()) == true && Convert.ToString(q.IPAddress).Trim() != "").Count() != 0)
                //    {
                //        List<DeviceModel> _ListOfSelectDevice = _Result.Data.Where(q => _Test.Contains(Convert.ToString(q.IPAddress).Trim()) == true && Convert.ToString(q.IPAddress).Trim() != "").ToList();

                //        foreach (DeviceModel _DeviceItem in _ListOfSelectDevice)
                //        {
                //            bool _Connected = false;

                //            _Connected = CtrlBioComm.Connect_Net(_DeviceItem.IPAddress, Convert.ToInt32(_DeviceItem.Port));

                //            if (_Connected)
                //            {
                //                if (CtrlBioComm.IsTFTMachine(1))
                //                {
                //                    _ErrorMessage = GetAndUpdateData(_DeviceItem, true, _ErrorMessage);
                //                }
                //                else
                //                {
                //                    _ErrorMessage = GetAndUpdateData(_DeviceItem, false, _ErrorMessage);
                //                }

                //                CtrlBioComm.Disconnect();
                //            }
                //            else
                //            {
                //                if (_ErrorMessage == "")
                //                {
                //                    _ErrorMessage = "Unable to connect " + _DeviceItem.DeviceName + " device";
                //                }
                //                else
                //                {
                //                    _ErrorMessage += "\nUnable to connect " + _DeviceItem.DeviceName + " device.";
                //                }
                //            }
                //        }
                //    }
                //    else
                //    {
                //        _ErrorMessage = "Unable to connect device";
                //    } 

                #endregion

                if (_ErrorMessage == " ")
                {
                    _ErrorMessage = "Send Employee Successfully.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + _ErrorMessage + "');});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ErrorMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _ErrorMessage + "');});", true);
                }

                #endregion
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "ExceptionMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + GlobalMsg.ExceptionErrMsg + "');});", true);
            }
        }

        #endregion

        #region Private Methods

        private string GetAndUpdateData(DeviceModel p_Device, bool p_IsTFT, string p_ErrorMessage)
        {
            int _errorCode = 0, _machinePrivilege = 0, _enrollid = 1;
            string _enrollNo = "", _name = "", _password = "";
            bool _enabled = false, _uploadedtodevice = false;
            List<int> _ListEnrollId = new List<int>();

            #region FOR MEMBER CHECK

            if (CtrlBioComm.ReadAllUserID(1))
            {
                CtrlBioComm.GetLastError(ref _errorCode);

                _ListEnrollId = new List<int>();

                while (_errorCode != 0)
                {
                    CtrlBioComm.ReadAllUserID(1);

                    if (p_IsTFT)
                    {
                        while (CtrlBioComm.SSR_GetAllUserInfo(1, out _enrollNo, out _name, out _password, out _machinePrivilege, out _enabled))
                        {
                            _ListEnrollId.Add(Convert.ToInt32(_enrollNo));
                        }
                    }
                    else
                    {
                        int _enrollNoInt = 0;
                        while (CtrlBioComm.GetAllUserInfo(1, ref _enrollNoInt, ref _name, ref _password, ref _machinePrivilege, ref _enabled))
                        {
                            _ListEnrollId.Add(_enrollNoInt);
                        }
                    }

                    CtrlBioComm.GetLastError(ref _errorCode);
                }

                _enrollid = _ListEnrollId.Max() + 1;
            }

            #endregion

            List<EmployeeModel> _ListOfEmployee = null;

            Result<List<EmployeeModel>> _ResultEmployee = _IEmployeeService.GetAllSendPendingEmployeeByDevice(p_Device.DeviceID);

            if (_ResultEmployee.IsSuccess)
            {
                _ListOfEmployee = _ResultEmployee.Data;

                if (_ListOfEmployee != null)
                {
                    foreach (EmployeeModel _Employee in _ListOfEmployee)
                    {
                        _uploadedtodevice = false;

                        string _Names = _Employee.FullName.Length >= 30 ? _Employee.FullName.Substring(0, 29) : _Employee.FullName;

                        if (CtrlBioComm.SSR_SetUserInfo(1, Convert.ToString(_enrollid), _Names, "123456", 0, true))
                        {
                            _uploadedtodevice = true;
                        }
                        else
                        {
                            if (CtrlBioComm.SetUserInfo(1, _enrollid, _Names, "123456", 0, true))
                            {
                                _uploadedtodevice = true;
                            }
                        }

                        if (_uploadedtodevice)
                        {

                            EmployeeDeviceMapModel _EmployeeDeviceMap = new EmployeeDeviceMapModel();

                            _EmployeeDeviceMap.DeviceId = p_Device.DeviceID;
                            _EmployeeDeviceMap.EmployeeId = _Employee.EmployeeId;
                            _EmployeeDeviceMap.EnrollmentNo = Convert.ToString(_enrollid);

                            Result<bool> _ResultSave = _IEmployeeDeviceMapService.InsertEmployeeDeviceAttendance(_EmployeeDeviceMap);

                            if (_ResultSave.IsSuccess)
                            {
                                _enrollid = _enrollid + 1;
                            }
                            else
                            {
                                p_ErrorMessage = _ResultSave.Message;
                            }
                        }
                    }
                }
            }

            return p_ErrorMessage;
        }

        #endregion
    }
}