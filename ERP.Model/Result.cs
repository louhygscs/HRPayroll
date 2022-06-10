using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class Result<T>
    {
        public T Data { get; set; }
        public Boolean IsSuccess { get; set; }
        public String Message { get; set; }
        public Exception Exception { get; set; }
        public string Id { get; set; }
        public int TotalCount { get; set; }
    }
}
