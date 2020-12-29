using System;
using EasySharp.EfCore.StoredProcedure.Interface;

namespace CompanyService.Domain.Entities
{
    public class Branch : IProc<Guid>
    {
        public string Name      { get; set; }
        public string Location  { get; set; }
        public Guid   CompanyId { get; set; }
    }
}