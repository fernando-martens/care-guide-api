using CareGuide.Models.DTOs.DoctorPhone;
using CareGuide.Models.DTOs.Phone;

namespace CareGuide.Core.Interfaces
{
    public interface IDoctorPhoneService
    {
        Task<IReadOnlyCollection<DoctorPhoneDto>> GetAllByDoctorAsync(int page, int pageSize, Guid doctorId, CancellationToken cancellationToken);
        Task<DoctorPhoneDto> GetAsync(Guid phoneId, Guid doctorId, CancellationToken cancellationToken);
        Task<DoctorPhoneDto> CreateAsync(Guid doctorId, CreatePhoneDto phoneDto, CancellationToken cancellationToken);
        Task<DoctorPhoneDto> UpdateAsync(Guid id, Guid doctorId, UpdatePhoneDto phoneDto, CancellationToken cancellationToken);
        Task DeleteAllByDoctorAsync(Guid doctorId, CancellationToken cancellationToken);
        Task DeleteByIdsAsync(List<Guid> phoneIds, Guid doctorId, CancellationToken cancellationToken);
    }
}
