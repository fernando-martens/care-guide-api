using CareGuide.Models.DTOs.PersonFamilyHistory;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonFamilyHistoryService
    {
        Task<List<PersonFamilyHistoryDto>> GetAllByPersonAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PersonFamilyHistoryDto> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<PersonFamilyHistoryDto> CreateAsync(CreatePersonFamilyHistoryDto personFamilyHistory, CancellationToken cancellationToken);
        Task<PersonFamilyHistoryDto> UpdateAsync(Guid id, UpdatePersonFamilyHistoryDto personFamilyHistory, CancellationToken cancellationToken);
        Task DeleteAllByPersonAsync(CancellationToken cancellationToken);
        Task DeleteByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
