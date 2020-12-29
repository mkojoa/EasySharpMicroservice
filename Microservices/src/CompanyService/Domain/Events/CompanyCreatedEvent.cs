using System;
using EasySharp.Core.Events;

namespace CompanyService.Domain.Events
{
    public class CompanyCreatedEvent : Event
    {
        public Guid   CompanyId    { get; set; }
        public string CompanyName  { get; set; }
        public string Ceo          { get; set; }
        public string Logo         { get; set; }
        public bool   Status       { get; set; }
        public int    NoOfEmployee { get; set; }
        public Guid   UserId       { get; set; }
    }
}