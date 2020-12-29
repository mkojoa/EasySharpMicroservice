using System;
using EasySharp.EfCore.StoredProcedure.Interface;

namespace CompanyService.Domain.Entities
{
    public class Company : IProc<Guid>
    {
        public string CompanyName  { get; set; }
        public string Ceo          { get; set; }
        public string Logo         { get; set; }
        public bool   Status       { get; set; }
        public int    NoOfEmployee { get; set; }
        public Guid   UserId       { get; set; }
    }
}