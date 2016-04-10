using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Entities.Cars;

namespace Expedia.Services.Interfaces
{
    public interface ICarService
    {
        Task<CarResults> Search(CancellationToken ct, SearchCarsLocalParameters searchInput);
    }
}
