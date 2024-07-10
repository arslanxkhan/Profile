using Data.Helper;
using Data.Services.BaseServices;
using Data.Services.Contract;
using Models.Db;
using Models.Dto;
using Models.VM;

namespace Data.Services.Implementation
{
    public class ProfileService : BaseService, IProfileService
    {

        public async Task<ProfileDto> GetPortfolio(string url)
        {
            var data = await UnitOfWork.Profile.GetAsync(x => x.PortfolioUrl.Equals(url));
            return Mapper.Map<ProfileDto>(data);
        }


        public List<ProfileDto> GetAll()
        {
            var list = UnitOfWork.Profile.GetAll();
            return Mapper.Map<List<ProfileDto>>(list.ToList());
        }


        public ProfileDto Get(Guid id)
        {
            var data = UnitOfWork.Profile.GetById(id);
            return Mapper.Map<ProfileDto>(data);
        }


        public async Task<Guid> Add(ProfileVM vm)
        {
            var model = Mapper.Map<Profile>(vm);

            model.PortfolioUrl = GeneralHelper.GenerateRandomAlphanumericString(6);
            UnitOfWork.Profile.Add(model);

            await UnitOfWork.SaveChangesAsync();
            return model.Id;
        }


        public async Task<bool> Update(Guid id, ProfileVM vm)
        {
            var model = UnitOfWork.Profile.GetById(id);

            if (model is null)
                throw new Exception("Profile not found");

            model.UpdatedAt = DateTime.Now;
            model.Address = vm.Address;
            model.Name = vm.Name;
            model.Phone = vm.Phone;


            return await UnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateImage(Guid id, string filePath)
        {
            var model = UnitOfWork.Profile.GetById(id);

            if (model is null)
                throw new Exception("Profile not found");

            model.UpdatedAt = DateTime.Now;
            model.ProfileImage = filePath;

            return await UnitOfWork.SaveChangesAsync();
        }





        public async Task<bool> Delete(Guid id)
        {
            var model = UnitOfWork.Profile.GetById(id);

            if (model is null)
                throw new Exception("Profile not found");

            UnitOfWork.Profile.Remove(model);

            return await UnitOfWork.SaveChangesAsync();
        }



        public bool CheckDuplicate(string email, Guid? id = null)
        {
            if (id is null)
                return UnitOfWork.Profile.Any(x => x.Email.Equals(email));
            else
                return UnitOfWork.Profile.Any(x => !x.Id.Equals(id) && x.Email.Equals(email));

        }

    }
}
