using System;
using System.Collections.Generic;

namespace Tacmin.Core.Model
{
    public class ErrorViewModel
    {
        public string UserName { get; set; }

        public int HttpErrorCode { get; set; }

        public Exception Error { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
