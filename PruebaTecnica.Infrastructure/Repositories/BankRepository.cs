namespace PruebaTecnica.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing BankEntity operations with the database.
    /// </summary>
    public class BankRepository : IBankRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankRepository"/> class with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string for the database.</param>
        public BankRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Establishes a new SQL connection using the repository's connection string.
        /// </summary>
        /// <returns>A new instance of <see cref="SqlConnection"/>.</returns>
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Finds a bank by its BIC (Bank Identifier Code).
        /// </summary>
        /// <param name="bic">The BIC of the bank to find.</param>
        /// <returns>A <see cref="BankEntity"/> object representing the bank, or null if not found.</returns>
        public async Task<BankEntity> FindByBicAsync(string bic)
        {
            using var connection = GetConnection();
            string query = "SELECT * FROM [Banks] WHERE BIC = @BIC";

            return await connection.QueryFirstOrDefaultAsync<BankEntity>(query, new { BIC = bic });
        }

        /// <summary>
        /// Fetches all bank records from the database.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{BankEntity}"/> representing all banks.</returns>
        public async Task<IEnumerable<BankEntity>> FindAllAsync()
        {
            using var connection = GetConnection();
            string query = "SELECT * FROM [Banks]";

            return await connection.QueryAsync<BankEntity>(query);
        }

        /// <summary>
        /// Adds a new bank entity to the database.
        /// </summary>
        /// <param name="bankEntity">A <see cref="BankEntity"/> object containing the bank's information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddAsync(BankEntity bankEntity)
        {
            using var connection = GetConnection();
            string query = @"
                INSERT INTO [Banks] (Name, BIC, Country)
                VALUES (@Name, @BIC, @Country)";

            await connection.ExecuteAsync(query, new
            {
                bankEntity.Name,
                bankEntity.BIC,
                bankEntity.Country
            });
        }
    }
}
