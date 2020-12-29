using System.Threading.Tasks;
using CompanyService.Domain.Entities;
using CompanyService.Domain.Entities.EntityConfigurations;
using EasySharp.Core.Events;
using Microsoft.EntityFrameworkCore;

namespace CompanyService.Domain
{
    public class CompanyDomainContext : DbContext
    {
        private readonly IEventBus _eventBus;

        public CompanyDomainContext(DbContextOptions<CompanyDomainContext> options, IEventBus eventBus = null) : base(options)
        {
            _eventBus = eventBus;
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Branch>  Branches  { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            CompanyConfiguration.Build(builder);
            AddressConfiguration.Build(builder);
            BranchConfiguration.Build(builder);

        }

        /// <summary>
        /// Save db changes and commit the event to rabbit mq
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task OnSaveChangesAndCommitEvent(IEvent @event)
        {
            using (var transaction = Database.BeginTransaction())
            {
                await SaveChangesAsync();

                await _eventBus.Commit(@event);

                await transaction.CommitAsync();
            }
        }
    }
}