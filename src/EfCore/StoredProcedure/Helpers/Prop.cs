using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.EfCore.StoredProcedure.Helpers
{
    struct Prop
    {
        public int ColumnOrdinal { get; set; }
        public Action<object, object> Setter { get; set; }
    }
}
