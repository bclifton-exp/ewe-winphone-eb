using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Cars
{
    public interface ICarResult
    {
        bool IsSelectable { get; }
    }
}
