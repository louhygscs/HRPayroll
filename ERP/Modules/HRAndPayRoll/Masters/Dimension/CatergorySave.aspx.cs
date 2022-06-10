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

namespace ERP.Modules.HRAndPayRoll.Masters.Dimension
{
    public partial class CatergorySave : System.Web.UI.Page
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
                FillDropList();

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
                IDimensionService _IService = new DimensionService();

                CategoryModel _item = new CategoryModel();

                Guid _id = Guid.Empty;

                bool isParse = Guid.TryParse(hfId.Value.ToString(), out _id);

                if (!isParse)
                {
                    _id = Guid.NewGuid();
                }

                _item.CategoryId    = _id;
                _item.CategoryTable = drpEntryTable.SelectedValue.Trim();
                _item.CategoryName  = txtCategoryName.Text.Trim();

                Result<bool> _Result = _IService.SaveCategory(_item);

                if (_Result.IsSuccess)
                {
                    SessionHelper.MessageSession = String.Format(GlobalMsg.SaveSuccessMsg, "Category");

                    Response.Redirect("~/Modules/HRAndPayRoll/Masters/Dimension/CategoryList.aspx", false);

                }
                else
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

        private void FillDropList()
        {
            drpEntryTable.Items.Clear();
            drpEntryTable.Items.Insert(0, new ListItem() { Text = "-- Select --", Value = "" });

            int i = 0;

            foreach (var item in Enum.GetValues(typeof(TableType)))
            {
                i++;
                drpEntryTable.Items.Insert(i, new ListItem() { Text = item.ToString(), Value = item.ToString() });
            }
        }

        private void FillControls(Guid p_Id)
        {
            try
            {
                IDimensionService _IService = new DimensionService();

                Result<CategoryModel> _Result = _IService.GetByCategoryId(p_Id);

                if (_Result.IsSuccess)
                {
                    hfId.Value                  = Convert.ToString(p_Id);
                    drpEntryTable.SelectedValue = _Result.Data.CategoryTable;
                    txtCategoryName.Text        = _Result.Data.CategoryName;
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