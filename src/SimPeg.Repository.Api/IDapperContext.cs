using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SimPeg.Repository.Api
{
    public interface IDapperContext : IDisposable
    {
        IDbConnection db { get; }
    }
}
