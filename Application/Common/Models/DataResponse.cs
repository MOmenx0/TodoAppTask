using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Application.Common.Models
{
    public class DataResponse<T>
    {
        public bool IsSuccess { get; set; } = true;

        public HttpStatusCode? StatusCode { get; set; }

        public string? ResponseMessage { get; set; }

        public T? ResponseData { get; set; }

    }
}
