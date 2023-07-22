using Application.Guest.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public abstract class Response
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public EErrorCodes ErrorCode { get; set; }
}
