namespace PpeManager.Infrastructure
{
    public class PpeManagerContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "ppemanager";
        private IDbContextTransaction? _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        private readonly IMediator? _mediator;

        public DbSet<Worker> Worker { get; set; }
        public DbSet<Ppe> Ppe { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<PpeCertification> PpeCertification { get; set; }
        public DbSet<PpePossession> PpePossession { get; set; }


        public PpeManagerContext(DbContextOptions<PpeManagerContext> options, IMediator mediator) : base(options)
        {         
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("PpeManagerContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PpeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PpePossessionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PpeCertificationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerEntityTypeConfiguration());

        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {

            await _mediator!.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

}

