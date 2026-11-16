using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Domain.Shared
{
    public record Error(
         string Code,
         string Message,
         int StatusCode);



}
