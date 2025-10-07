﻿using CareGuide.Models.DTOs.PersonPhone;
using CareGuide.Models.DTOs.Phone;

namespace CareGuide.Core.Interfaces
{
    public interface IPersonPhoneService
    {
        Task<PersonPhoneDto> GetAllByPersonAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<PersonPhoneDto> GetAsync(Guid phoneId, CancellationToken cancellationToken);
        Task<PersonPhoneDto> CreateAsync(CreatePhoneDto personPhone, CancellationToken cancellationToken);
        Task<PersonPhoneDto> UpdateAsync(Guid id, UpdatePhoneDto personPhone, CancellationToken cancellationToken);
        Task DeleteAllByPersonAsync(CancellationToken cancellationToken);
        Task DeleteByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    }
}
