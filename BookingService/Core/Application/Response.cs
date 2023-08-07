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

    public bool isMappedException()
    {
        return Enum.GetValues(typeof(EErrorCodes)).Cast<EErrorCodes>().ToHashSet().Contains(this.ErrorCode);
    }
}
