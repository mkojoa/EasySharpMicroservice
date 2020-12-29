using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyService.Commands.Command;
using CompanyService.Dtos.Companies;
using EasySharp.Core.Commands;
using EasySharp.Core.Queries;
using EasySharp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {

        private readonly IQueryBus   _queryBus;
        private readonly ICommandBus _commandBus;

        public CompaniesController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus   = queryBus;
            _commandBus = commandBus;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public async Task<ActionResult<CompanyCreatedDto>> CreateCompanies([FromBody] CompanyCreateDto company, CancellationToken cancellationToken)
        {
            var command = Mapping.onMap<CompanyCreateDto, CreateCompanyCommand>(company);

            //who created this record?
            command.UserId = Guid.Empty;

            // send to command handler
            var result = await _commandBus.Send(command, cancellationToken);

            return Created("{id}", new { id = result.Id });
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<CompanyDto>> GetAllCompanies(CancellationToken cancellationToken)
        {

            return Ok("");
        }

        [HttpGet, Route("{CompanyId}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyBy(Guid CompanyId, CancellationToken cancellationToken)
        {

            return Ok("");
        }
    }
}