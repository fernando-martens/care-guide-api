using CareGuide.Models.DTOs.Phone;

namespace CareGuide.Core.Interfaces
{
    public interface IPhoneService
    {
        Task<List<PhoneDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PhoneDto> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<PhoneDto> CreateAsync(CreatePhoneDto createPhoneDto, CancellationToken cancellationToken);
        Task<PhoneDto> UpdateAsync(Guid id, UpdatePhoneDto updatePhoneDto, CancellationToken cancellationToken);
        Task DeleteByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
