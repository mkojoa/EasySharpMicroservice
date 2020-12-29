using System;
using EasySharp.EfCore.StoredProcedure.Interface;

namespace CompanyService.Domain.Entities
{
    public class Address : IProc<Guid>
    {
        public string Email     { get; set; }
        public string Digital   { get; set; }
        public string BoxAddr   { get; set; }
        public Guid   CompanyId { get; set; }
    }
}