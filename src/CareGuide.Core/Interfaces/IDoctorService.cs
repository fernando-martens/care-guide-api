using CareGuide.Models.DTOs.Doctor;

namespace CareGuide.Core.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllByPersonAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<DoctorDto> GetAsync(Guid id, CancellationToken cancellationToken);
        Task<DoctorDto> CreateAsync(CreateDoctorDto doctor, CancellationToken cancellationToken);
        Task<DoctorDto> UpdateAsync(Guid id, UpdateDoctorDto doctor, CancellationToken cancellationToken);
        Task DeleteAllByPersonAsync(CancellationToken cancellationToken);
        Task DeleteByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
