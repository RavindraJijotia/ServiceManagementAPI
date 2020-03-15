using System;
using System.Collections.Generic;
using System.Text;

namespace SM.Domain.Interfaces
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
