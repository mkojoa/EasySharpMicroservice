using System;
using CompanyService.Dtos.Companies;
using EasySharp.Core.Commands;
using FluentValidation;

namespace CompanyService.Commands.Command
{
    public class CreateCompanyCommand : ICommand<CompanyCreatedDto>
    {
        public string CompanyName { get; set; }
        public string Ceo { get; set; }
        public string Logo { get; set; }
        public bool Status { get; set; }
        public int NoOfEmployee { get; set; }
        public Guid UserId { get; set; }


        public class Validator : AbstractValidator<CreateCompanyCommand>
        {
            public Validator()
            {
                RuleFor(cmd => cmd.CompanyName).NotEmpty();
                RuleFor(cmd => cmd.Ceo).NotEmpty();
                RuleFor(cmd => cmd.Logo).NotEmpty();
                RuleFor(cmd => cmd.Status).NotEmpty();
                RuleFor(cmd => cmd.NoOfEmployee).NotEmpty();
                RuleFor(cmd => cmd.UserId).NotEmpty();
            }
        }
    }
}