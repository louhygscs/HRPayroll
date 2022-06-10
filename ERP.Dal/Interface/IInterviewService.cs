using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Dal.Interface
{
    public interface IInterviewService
    {
        Result<List<Interview>> GetInterviewList();

        Result<Interview> GetInterviewByInterviewId(Guid p_InterviewId);

        Result<bool> DeleteInterviewById(Guid p_InterviewId, Guid p_UserId);

        Result<int> GetTodayInterviewCount();

        Result<bool> SaveInterview(Interview p_Interview, Guid p_UserId);

    }
}
