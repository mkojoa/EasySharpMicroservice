using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyService.Commands.Command;
using CompanyService.Domain;
using CompanyService.Domain.Entities;
using CompanyService.Domain.Events;
using CompanyService.Dtos.Companies;
using EasySharp.Core.Commands;
using EasySharp.Core.Events;
using EasySharp.Helpers;

namespace CompanyService.Commands.Handlers
{
    public class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand, CompanyCreatedDto>
    {
        private readonly CompanyDomainContext _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public CreateCompanyCommandHandler(CompanyDomainContext db)
        {
            _db = db;
            _db.Database.EnsureCreated();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CompanyCreatedDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            //command to entity object
            var company = new Company
                          {
                              CompanyName  = request.CompanyName,
                              Ceo          = request.Ceo,
                              Logo         = request.Logo,
                              Status       = request.Status,
                              NoOfEmployee = request.NoOfEmployee,
                              UserId       = request.UserId,
                              Id           = Guid.NewGuid()
                          };

            // ef add company
            await _db.AddAsync(company, cancellationToken);

            //map entity object to event
            var @event = Mapping.onMap<Company, CompanyCreatedEvent>(company);

            //save to ef and commit to rabbit mq as event.
            await _db.OnSaveChangesAndCommitEvent(@event);

            // return type
            var result = new CompanyCreatedDto
                         {
                             Id = company.Id
                         };

            return result;
        }
    }
}