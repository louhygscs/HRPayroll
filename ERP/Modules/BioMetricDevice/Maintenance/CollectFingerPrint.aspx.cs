using ERP.Common;
using ERP.Dal.Implemention;
using ERP.Dal.Interface;
using ERP.Helpers;
using ERP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using zkemkeeper;

namespace ERP.Modules.BioMetricDevice.Maintenance
{
    public partial class CollectFingerPrint : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liCollectFingerPrint_liBioMetricDevice_liMaintenance";

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

                if (_ErrorMessage == "")
                {
                    _ErrorMessage = "Collect Finger Print Successfully.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "SaveSuccessMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, '" + _ErrorMessage + "');});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ErrorMsg", "$(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, '" + _ErrorMessage + "');});", true);
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
            List<EmployeeDeviceMapModel> _ListOfEmployeeDevice = null;

            Result<List<EmployeeDeviceMapModel>> _Result = _IEmployeeDeviceMapService.GetAllEmployeeDeviceAttendance();

            if (_Result.IsSuccess)
            {
                _ListOfEmployeeDevice = _Result.Data;
            }

            DataTable dtdevice = new DataTable();
            dtdevice.Columns.Add("_enrollNo", typeof(Int32));
            dtdevice.Columns.Add("_name");
            dtdevice.Columns.Add("_password");
            dtdevice.Columns.Add("_machinePrivilege");
            dtdevice.Columns.Add("_enabled");

            int _errorCode = 0, _machinePrivilege = 0;
            string _enrollNo = "", _name = "", _password = "";
            bool _enabled = false;

            #region FOR MEMBER CHECK

            if (CtrlBioComm.ReadAllUserID(1))
            {
                CtrlBioComm.GetLastError(ref _errorCode);

                while (_errorCode != 0)
                {
                    CtrlBioComm.ReadAllUserID(1);

                    if (p_IsTFT)
                    {
                        while (CtrlBioComm.SSR_GetAllUserInfo(1, out _enrollNo, out _name, out _password, out _machinePrivilege, out _enabled))
                        {
                            DataRow drow = dtdevice.NewRow();
                            drow["_enrollNo"] = _enrollNo;
                            drow["_name"] = _name;
                            drow["_password"] = _password;
                            drow["_machinePrivilege"] = _machinePrivilege;
                            drow["_enabled"] = _enabled;
                            dtdevice.Rows.Add(drow);
                        }
                    }
                    else
                    {
                        int _enrollNoInt = 0;
                        while (CtrlBioComm.GetAllUserInfo(1, ref _enrollNoInt, ref _name, ref _password, ref _machinePrivilege, ref _enabled))
                        {
                            DataRow drow = dtdevice.NewRow();
                            drow["_enrollNo"] = Convert.ToString(_enrollNoInt);
                            drow["_name"] = _name;
                            drow["_password"] = _password;
                            drow["_machinePrivilege"] = _machinePrivilege;
                            drow["_enabled"] = _enabled;
                            dtdevice.Rows.Add(drow);
                        }
                    }

                    CtrlBioComm.GetLastError(ref _errorCode);
                }
            }

            if (dtdevice != null)
            {
                foreach (DataRow item in dtdevice.Rows)
                {
                    if (_ListOfEmployeeDevice != null && _ListOfEmployeeDevice.Count > 0)
                    {
                        EmployeeDeviceMapModel _EmployeeDeviceMap = _ListOfEmployeeDevice.Where(st => st.EnrollmentNo == Convert.ToString(item["_enrollNo"]) && st.DeviceId == p_Device.DeviceID).FirstOrDefault();

                        if (_EmployeeDeviceMap != null)
                        {
                            EmployeeModel _Employee = new EmployeeModel();

                            _Employee.EmployeeId = _EmployeeDeviceMap.EmployeeId ?? Guid.NewGuid();

                            #region FOR UPDATE FINGUREPRINT TEMPLATE

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
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 0, out flag, out _enrollDataByte[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft = _enrollDataByte;
                                }
                                else
                                {
                                    _Employee.finger_template_data_tft = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft = null;
                            }

                            try
                            {
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 1, out flag, out _enrollDataByte1[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft1 = _enrollDataByte1;
                                }
                                else
                                {
                                    _Employee.finger_template_data_tft1 = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft1 = null;
                            }

                            try
                            {
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 2, out flag, out _enrollDataByte2[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft2 = _enrollDataByte2;
                                }
                                else
                                {

                                    _Employee.finger_template_data_tft2 = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft2 = null;
                            }

                            try
                            {
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 3, out flag, out _enrollDataByte3[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft3 = _enrollDataByte3;
                                }
                                else
                                {
                                    _Employee.finger_template_data_tft3 = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft3 = null;
                            }

                            try
                            {
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 4, out flag, out _enrollDataByte4[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft4 = _enrollDataByte4;
                                }
                                else
                                {
                                    _Employee.finger_template_data_tft4 = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft4 = null;
                            }

                            try
                            {
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 5, out flag, out _enrollDataByte5[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft5 = _enrollDataByte5;
                                }
                                else
                                {
                                    _Employee.finger_template_data_tft5 = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft5 = null;
                            }

                            try
                            {
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 6, out flag, out _enrollDataByte6[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft6 = _enrollDataByte6;
                                }
                                else
                                {
                                    _Employee.finger_template_data_tft6 = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft6 = null;
                            }

                            try
                            {
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 7, out flag, out _enrollDataByte7[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft7 = _enrollDataByte7;
                                }
                                else
                                {
                                    _Employee.finger_template_data_tft7 = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft7 = null;
                            }

                            try
                            {
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 8, out flag, out _enrollDataByte8[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft8 = _enrollDataByte8;
                                }
                                else
                                {
                                    _Employee.finger_template_data_tft8 = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft8 = null;
                            }

                            try
                            {
                                if (CtrlBioComm.GetUserTmpEx(1, Convert.ToString(item["_enrollNo"]), 9, out flag, out _enrollDataByte9[0], out _templatelength))
                                {
                                    _Employee.finger_template_data_tft9 = _enrollDataByte9;
                                }
                                else
                                {
                                    _Employee.finger_template_data_tft9 = null;
                                }
                            }
                            catch
                            {
                                _Employee.finger_template_data_tft9 = null;
                            }

                            if (_Employee.finger_template_data_tft == null && _Employee.finger_template_data_tft1 == null && _Employee.finger_template_data_tft2 == null && _Employee.finger_template_data_tft3 == null && _Employee.finger_template_data_tft4 == null && _Employee.finger_template_data_tft5 == null && _Employee.finger_template_data_tft6 == null && _Employee.finger_template_data_tft7 == null && _Employee.finger_template_data_tft8 == null && _Employee.finger_template_data_tft9 == null)
                            {
                                _Employee.IsFinger = false;
                            }
                            else
                            {
                                _Employee.IsFinger = true;
                                try
                                {
                                    Result<bool> _ResultFingerPrint = _IEmployeeService.SaveEmployeeFingerPrint(_Employee);
                                    if (!_ResultFingerPrint.IsSuccess)
                                    {
                                        _ErrorMessage += ",not updated";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _ErrorMessage += ex.Message + ",not updated";
                                }
                            }
                            #endregion
                        }
                    }
                }
            }

            #endregion

            return p_ErrorMessage;
        }

        #endregion
    }
}