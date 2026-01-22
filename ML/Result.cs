using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public int n_Value { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Ex { get; set; }
        public bool Correct { get; set; }
        public object Object { get; set; }
        public List<Object> Objects { get; set; }

    }
}
