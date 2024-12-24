using PruebaTecnica.Domain.Entities;
using PruebaTecnica.Domain.Repositories;
using AutoMapper;
using System.Net.Http;
using System.Text.Json;
using System.Linq;

namespace PruebaTecnica.Application.Handlers.Commands
{
    /// <summary>
    /// Handles the import of bank data from an external API and stores it in the database.
    /// </summary>
    public class ImportBankHandler : IRequestHandler<ImportBankCommand, ImportBankResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankRepository _bankRepository;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ImportBankHandler(
            IUnitOfWork unitOfWork,
            IBankRepository bankRepository,
            HttpClient httpClient,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bankRepository = bankRepository;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the command to import banks by fetching data from an external API.
        /// </summary>
        /// <param name="request">The command request.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response indicating success or failure of the import operation.</returns>
        public async Task<ImportBankResponse> Handle(ImportBankCommand request, CancellationToken cancellationToken)
        {
            var response = new ImportBankResponse();
            var apiUrl = "https://api.opendata.esett.com/EXP06/Banks";
            response.Success = true;

            try
            {
                using var httpRequest = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                using var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    response.Success = false;
                    response.Message = $"Failed to fetch data from API. HTTP Status: {httpResponse.StatusCode}";
                    return response;
                }

                var apiResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
                if (string.IsNullOrEmpty(apiResponse))
                {
                    response.Success = false;
                    response.Message = "No data returned from external API.";
                    return response;
                }

                var bankDtos = JsonSerializer.Deserialize<List<BankDto>>(apiResponse);
                var bankEntities = _mapper.Map<List<BankEntity>>(bankDtos);
                if (bankEntities == null || !bankEntities.Any())
                {
                    response.Success = false;
                    response.Message = "No valid data found in the API response.";
                    return response;
                }

                foreach (var bank in bankEntities)
                {
                    if (!bank.IsValid())
                    {
                        response.Errors.Add($"Invalid data for bank with BIC: {bank.BIC}");
                        continue;
                    }

                    var existingBank = await _bankRepository.FindByBicAsync(bank.BIC);
                    if (existingBank == null)
                    {
                        try
                        {
                            await _bankRepository.AddAsync(bank);
                        }
                        catch (Exception ex)
                        {
                            response.Errors.Add($"Failed to import bank with BIC: {bank.BIC}. Error: {ex.Message}");
                        }
                    }
                }

                 _unitOfWork.CommitAsync();

                response.Message = response.Success ? "Banks successfully imported." : "Some banks could not be imported. See details.";
            }
            catch (HttpRequestException httpEx)
            {
                _unitOfWork.Rollback();
                response.Success = false;
                response.Message = $"HTTP request error: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                response.Success = false;
                response.Message = $"Error importing banks: {ex.Message}";
            }

            return response;
        }
    }
}
