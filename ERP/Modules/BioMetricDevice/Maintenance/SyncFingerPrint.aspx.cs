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
using zkemkeeper;

namespace ERP.Modules.BioMetricDevice.Maintenance
{
    public partial class SyncFingerPrint : System.Web.UI.Page
    {
        #region Variables

        private readonly log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IDeviceService _IDeviceService = new DeviceService();
        IEmployeeService _IEmployeeService = new EmployeeService();
        IEmployeeDeviceMapService _IEmployeeDeviceMapService = new EmployeeDeviceMapService();
        CZKEM CtrlBioComm = new CZKEM();
        string _ErrorMessage = "";

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionHelper.SessionDetail == null)
            {
                Response.Redirect("~/Modules/Login.aspx", true);
            }

            SessionHelper.SelectMenuSession = "liSyncFingerPrint_liBioMetricDevice_liMaintenance";

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
                #region CONNECT TERMINALS

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

                //    string _NetWorkDevice = IPInfo.GetARPResult();

                //    if (_Result.Data.Where(q => _NetWorkDevice.Contains(Convert.ToString(q.IPAddress).Trim()) == true && Convert.ToString(q.IPAddress).Trim() != "").Count() != 0)
                //    {
                //        List<DeviceModel> _ListOfSelectDevice = _Result.Data.Where(q => _NetWorkDevice.Contains(Convert.ToString(q.IPAddress).Trim()) == true && Convert.ToString(q.IPAddress).Trim() != "").ToList();

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
                //        _ErrorMessage = "Unable to connect device"; ;
                //    } 

                #endregion

                if (_ErrorMessage == "")
                {
                    _ErrorMessage = "Sync Finger Print Successfully.";
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
            int _machinePrivilege = 0;
            string _enrollNo = "", _name = "", _password = "";
            bool _enabled = false;
            List<EmployeeDeviceMapModel> _ListOfEmployeeDevice = new List<EmployeeDeviceMapModel>();

            Result<List<EmployeeDeviceMapModel>> _ResultEmployeeDevice = _IEmployeeDeviceMapService.GetAllEmployeeDeviceAttendanceByDeviceId(p_Device.DeviceID);

            if (_ResultEmployeeDevice.IsSuccess)
            {
                _ListOfEmployeeDevice = _ResultEmployeeDevice.Data;
            }

            CtrlBioComm.EnableDevice(1, false);

            if (_ListOfEmployeeDevice != null && _ListOfEmployeeDevice.Count > 0)
            {
                foreach (EmployeeDeviceMapModel _EmployeeDeviceMap in _ListOfEmployeeDevice)
                {
                    EmployeeModel _Employee = new EmployeeModel();
                    _enrollNo = _EmployeeDeviceMap.EnrollmentNo;
                    _Employee.EmployeeId = _EmployeeDeviceMap.EmployeeId ?? Guid.NewGuid();

                    if (p_IsTFT)
                    {
                        if (CtrlBioComm.SSR_GetUserInfo(1, _enrollNo, out _name, out _password, out _machinePrivilege, out _enabled))
                        {
                            _Employee.EnrollNo = _enrollNo;

                            if (!string.IsNullOrEmpty(_password))
                            {
                                _Employee.Password = SecurityHelper.EncryptString(_password);
                            }

                            _Employee = GetEmployeeFingure(_Employee);
                            _Employee = GetEmployeeFace(_Employee);

                            Result<bool> _Result = _IEmployeeService.SaveEmployeeFingerPrint(_Employee);

                            if (!_Result.IsSuccess)
                            {
                                p_ErrorMessage = _Result.Message;
                            }
                        }
                    }
                    else
                    {
                        if (CtrlBioComm.GetUserInfo(1, Convert.ToInt32(_enrollNo), ref _name, ref _password, ref _machinePrivilege, ref _enabled))
                        {
                            _Employee.EnrollNo = _enrollNo;

                            if (!string.IsNullOrEmpty(_password))
                            {
                                _Employee.Password = SecurityHelper.EncryptString(_password);
                            }

                            _Employee = GetEmployeeFingure(_Employee);
                            _Employee = GetEmployeeFace(_Employee);

                            Result<bool> _Result = _IEmployeeService.SaveEmployeeFingerPrint(_Employee);

                            if (!_Result.IsSuccess)
                            {
                                p_ErrorMessage = _Result.Message;
                            }
                        }
                    }
                }
            }

            CtrlBioComm.EnableDevice(1, true);

            return p_ErrorMessage;
        }

        private EmployeeModel GetEmployeeFace(EmployeeModel p_Employee)
        {
            string FaceData = "";
            int iLength = 0;
            bool _IsFace = false;

            try
            {
                if (CtrlBioComm.GetUserFaceStr(1, p_Employee.EnrollNo, 50, ref FaceData, ref iLength))
                {
                    _IsFace = true;
                }
            }
            catch (Exception _Exception)
            {
                _Logger.Error(GlobalMsg.ExceptionErrMsg, _Exception);
            }

            p_Employee.FaceTemplate = FaceData;
            p_Employee.FaceLength = iLength;
            p_Employee.IsFace = _IsFace;

            return p_Employee;
        }

        private EmployeeModel GetEmployeeFingure(EmployeeModel p_Employee)
        {
            int _templatelength = 0, flag = 1;
            byte[] _enrollDataByte = new byte[1024 * 168];
            byte[] _enrollDataByte1 = new byte[1024 * 168];
            byte[] _enrollDataByte2 = new byte[1024 * 168];
            byte[] _enrollDataByte3 = new byte[1024 * 168];
            byte[] _enrollDataByte4 = new byte[1024 * 168];
            byte[] _enrollDataByte5 = new byte[1024 * 168];
            byte[] _enrollDataByte6 = new byte[1024 * 168];
            byte[] _enrollDataByte7 = new byte[1024 * 168];
            byte[] _enrollDataByte8 = new byte[1024 * 168];
            byte[] _enrollDataByte9 = new byte[1024 * 168];

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 0, out flag, out _enrollDataByte[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft = _enrollDataByte;
                }
                else
                {
                    p_Employee.finger_template_data_tft = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft = null;
            }

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 1, out flag, out _enrollDataByte1[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft1 = _enrollDataByte1;
                }
                else
                {
                    p_Employee.finger_template_data_tft1 = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft1 = null;
            }

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 2, out flag, out _enrollDataByte2[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft2 = _enrollDataByte2;
                }
                else
                {

                    p_Employee.finger_template_data_tft2 = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft2 = null;
            }

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 3, out flag, out _enrollDataByte3[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft3 = _enrollDataByte3;
                }
                else
                {
                    p_Employee.finger_template_data_tft3 = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft3 = null;
            }

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 4, out flag, out _enrollDataByte4[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft4 = _enrollDataByte4;
                }
                else
                {
                    p_Employee.finger_template_data_tft4 = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft4 = null;
            }

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 5, out flag, out _enrollDataByte5[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft5 = _enrollDataByte5;
                }
                else
                {
                    p_Employee.finger_template_data_tft5 = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft5 = null;
            }

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 6, out flag, out _enrollDataByte6[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft6 = _enrollDataByte6;
                }
                else
                {
                    p_Employee.finger_template_data_tft6 = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft6 = null;
            }

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 7, out flag, out _enrollDataByte7[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft7 = _enrollDataByte7;
                }
                else
                {
                    p_Employee.finger_template_data_tft7 = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft7 = null;
            }

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 8, out flag, out _enrollDataByte8[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft8 = _enrollDataByte8;
                }
                else
                {
                    p_Employee.finger_template_data_tft8 = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft8 = null;
            }

            try
            {
                if (CtrlBioComm.GetUserTmpEx(1, p_Employee.EnrollNo, 9, out flag, out _enrollDataByte9[0], out _templatelength))
                {
                    p_Employee.IsFinger = true;
                    p_Employee.finger_template_data_tft9 = _enrollDataByte9;
                }
                else
                {
                    p_Employee.finger_template_data_tft9 = null;
                }
            }
            catch
            {
                p_Employee.finger_template_data_tft9 = null;
            }

            p_Employee.IsFinger = true;
            if (p_Employee.finger_template_data_tft == null && p_Employee.finger_template_data_tft1 == null && p_Employee.finger_template_data_tft2 == null && p_Employee.finger_template_data_tft3 == null && p_Employee.finger_template_data_tft4 == null && p_Employee.finger_template_data_tft5 == null && p_Employee.finger_template_data_tft6 == null && p_Employee.finger_template_data_tft7 == null && p_Employee.finger_template_data_tft8 == null && p_Employee.finger_template_data_tft9 == null)
            {
                p_Employee.IsFinger = false;
            }

            return p_Employee;
        }

        #endregion
    }
}