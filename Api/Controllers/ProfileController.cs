using Data.Constant;
using Data.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.VM;
using System.Linq;

namespace Api.Controllers
{

    [Route("api/profile")]
    public class ProfileController : BaseController
    {
        private readonly IProfileService profileService;
        private readonly IFileService fileService;
        private readonly AppSettingModel options;
        private readonly IHostEnvironment hostEnvironment;

        public ProfileController(IProfileService profileService, IFileService fileService, IOptions<AppSettingModel> options, IHostEnvironment hostEnvironment)
        {
            this.profileService = profileService;
            this.fileService = fileService;
            this.options = options.Value;
            this.hostEnvironment = hostEnvironment;
        }


        [HttpGet("protfolio/{url}")]
        public async Task<ActionResult<ResponseModel>> GetPortfolio([FromRoute] string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                    return BadRequest(new ResponseModel(false, "Please provide url"));

                var data = await profileService.GetPortfolio(url);
                return Ok(new ResponseModel(data));
            }
            catch (Exception x)
            {
                return BadRequest(new ResponseModel(false, x.Message));
            }
        }




        [HttpGet("")]
        public ActionResult<ResponseModel> GetAll()
        {
            try
            {
                var list = profileService.GetAll();

                foreach (var item in list)
                    item.ProfileImage = $"{options.ApiBaseUrl}/{item.ProfileImage}";

                return Ok(new ResponseModel(list));
            }
            catch (Exception x)
            {
                return BadRequest(new ResponseModel(false, x.Message));
            }
        }



        [HttpGet("{id:guid}")]
        public ActionResult<ResponseModel> Get([FromRoute] Guid id)
        {
            try
            {
                var data = profileService.Get(id);
                return Ok(new ResponseModel(data));
            }
            catch (Exception x)
            {
                return BadRequest(new ResponseModel(false, x.Message));
            }
        }



        [HttpPost("")]
        public async Task<ActionResult<ResponseModel>> Add([FromBody] ProfileVM vm)
        {
            try
            {
                var check = profileService.CheckDuplicate(vm.Email);
                if (check)
                {
                    var errors = new Dictionary<string, string>
                    {
                           { "Email", "Email already exist" }
                    };
                    return BadRequest(new ResponseModel(errors));
                }

                var id = await profileService.Add(vm);
                return Ok(new ResponseModel(true, ResponseText.DataAdded, new { Id = id }));
            }
            catch (Exception x)
            {
                return BadRequest(new ResponseModel(false, x.Message));
            }
        }


        [HttpPost]
        [Route("image/{id:guid}")]
        public async Task<ActionResult<ResponseModel>> UpdateProfileImage([FromRoute] Guid id, IFormFile incomingFile)
        {
            if (incomingFile == null || incomingFile.Length == 0)
                return BadRequest(new ResponseModel(false, "Image is required"));

            var profile = profileService.Get(id);

            if (profile is null)
                return BadRequest(new ResponseModel(false, "Profile not found"));

            var validImageExtension = new[] { ".png", ".jpg", ".jpeg" };
            var fileEx = Path.GetExtension(incomingFile.FileName).ToString();
            if (!fileEx.Any(x => !validImageExtension.Contains(x.ToString())))
                return BadRequest(new ResponseModel(false, $"Only valid extension files for image. {string.Join(",", validImageExtension)}"));

            if (!string.IsNullOrEmpty(profile.ProfileImage))
                fileService.Delete(Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", profile.ProfileImage.Replace("/", "\\")));


            try
            {
                var fileName = await fileService.UploadFileAsync(incomingFile, Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "uploads"));

                if (!string.IsNullOrEmpty(fileName))
                {
                    var res = await profileService.UpdateImage(id, $"uploads/{fileName}");
                    return res
                    ? Ok(new ResponseModel(true, ResponseText.DataUpdate))
                    : BadRequest(new ResponseModel(false, ResponseText.DataUpdateFailed));
                }
            }
            catch (Exception x)
            {
                return BadRequest(new ResponseModel(false, x.Message));
            }

            return BadRequest(new ResponseModel(false, ResponseText.DataUpdateFailed));
        }





        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResponseModel>> Update([FromRoute] Guid id, [FromBody] ProfileVM vm)
        {
            try
            {
                var check = profileService.CheckDuplicate(vm.Email, id);
                if (check)
                {
                    var errors = new Dictionary<string, string>
                    {
                           { "Email", "Email already exist" }
                    };
                    return BadRequest(new ResponseModel(errors));
                }

                var res = await profileService.Update(id, vm);
                return res
                    ? Ok(new ResponseModel(true, ResponseText.DataUpdate))
                    : BadRequest(new ResponseModel(false, ResponseText.DataUpdateFailed));
            }
            catch (Exception x)
            {
                return BadRequest(new ResponseModel(false, x.Message));
            }
        }



        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ResponseModel>> Delete([FromRoute] Guid id)
        {
            try
            {
                var res = await profileService.Delete(id);
                return res
                    ? Ok(new ResponseModel(true, ResponseText.DataDelete))
                    : BadRequest(new ResponseModel(false, ResponseText.DataDeleteFailed));
            }
            catch (Exception x)
            {
                return BadRequest(new ResponseModel(false, x.Message));
            }
        }
    }
}
