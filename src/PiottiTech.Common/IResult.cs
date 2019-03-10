using System.Collections.Generic;

namespace PiottiTech.Common
{
    public interface IResult
    {
        bool Success { get; set; }

        List<string> MessageList { get; set; }
    }
}