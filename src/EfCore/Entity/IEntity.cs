using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.EfCore.Entity
{
    public interface IEntity<T>
    {
         T Id { get; set; }

         DateTime CreatedAtUtc { get; set; }

         DateTime? LastModifiedAtUtc { get; set; }
    }
}
