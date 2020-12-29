using EasySharp.EfCore.StoredProcedure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.EfCore.StoredProcedure
{
    public static class DbContextExtension
    {
        public static IProcBuilder TriggerStoredProc(this DbContext ctx, string name)
        {
            return new ProcBuilder(ctx, name);
        }
    }
}
