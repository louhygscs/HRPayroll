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
    public partial class CurrencySave : System.Web.UI.Page
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

            SessionHelper.SelectMenuSession = "liCurrency_liHR_liHRMasters";

            if (!IsPostBack)
            {
                hfId.Value = Convert.ToString(Guid.Empty);

                if (Request.QueryString["id"] != null)
                {
                    int _id;

                    bool _Result = int.TryParse(Convert.ToString(Request.QueryString["id"]), out _id);

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
                ICurrencyService _iService = new CurrencyService();
                Currency _item = new Currency();

                int _id = -1;

                bool isParse = int.TryParse(hfId.Value.ToString(), out _id);

                if(!isParse)
                {
                    _id = -1;
                }
                
                _item.CurrencyID     = _id;
                _item.CurrencyCode   = txtCurrencyCode.Text.Trim();
                _item.CurrencySymbol = txtCurrencySymbol.Text.Trim();

                Result<Boolean> _Result = _iService.SaveEntity(_item);

                if(_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Currency");

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/CurrencyList.aspx", false);

                } else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "AlreadyExistsMsg", " $(document).ready(function() {Common.ShowToastrMessage(Common.Variable.Warning, Common.Variable.Warning, '" + String.Format(_Result.Message, "Currency") + "');});", true);
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
        private void FillControls(int p_Id)
        {
            try
            {
                ICurrencyService _IService = new CurrencyService();

                Result<Currency> _Result = _IService.GetById(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value             = Convert.ToString(p_Id);
                    txtCurrencyCode.Text   = _Result.Data.CurrencyCode;
                    txtCurrencySymbol.Text = _Result.Data.CurrencySymbol;
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