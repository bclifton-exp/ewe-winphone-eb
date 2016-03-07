using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.User
{
    public enum FacebookLinking
    {
        Unknown,
        NotLinked,
        Existing,
        Linked,
        Error,
        ErrorLinkedWithOther
    }
}
