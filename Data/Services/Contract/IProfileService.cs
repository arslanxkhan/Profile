using Models.Dto;
using Models.VM;

namespace Data.Services.Contract
{
    public interface IProfileService
    {
        Task<ProfileDto> GetPortfolio(string url);
        List<ProfileDto> GetAll();
        ProfileDto Get(Guid id);
        Task<Guid> Add(ProfileVM vm);
        Task<bool> Update(Guid id, ProfileVM vm);
        Task<bool> UpdateImage(Guid id, string filePath);
        Task<bool> Delete(Guid id);

        bool CheckDuplicate(string email, Guid? id = null);
    }
}
