using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IUserService
    {
        Result<SessionDetail> CheckLogin(string p_UserName, string p_Password);
        Result<List<UserModel>> GetAllUserList();
        Result<UserModel> GetUserById(Guid p_UserId);
        Result<String> CheckUserByUserName(string p_UserName);
        Result<Boolean> ResetPassword(Guid p_UserId,string p_Password);
        Result<Boolean> DeActivateUser(Guid p_UserId, bool isActive, Guid p_ModifiedId);
        Result<Boolean> CreateUser(UserModel p_User, Guid p_UserId);
        Result<Boolean> ChangePassword(Guid p_UserId, string p_OldPassword,string p_NewPassword);
        Result<List<String>> GetAllAdminEmail();
        Result<LoginResponse> UserLogin(LoginRequest p_LoginRequest);
        Result<bool> SaveDeviceRegistrationForNotification(DeviceRegistration p_DeviceRegistration);
    }
}
