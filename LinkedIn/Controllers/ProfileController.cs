using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IUserService _userService;
        private IProfileService _profileService;

        public ProfileController(IUserService userService, IProfileService profileService) { 
            _userService = userService;
            _profileService = profileService;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllExperiencesByUserId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.GetAllExperiencesByUserId(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateExperienceForUser(ExperienceCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.CreateExperienceForUser(createRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> EditExperienceForUser(ExperienceUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.EditExperienceForUser(updateRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEducationsByUserId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.GetAllEducationsByUserId(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateEducationForUser(EducationCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.CreateEducationForUser(createRequest, cancellationToken);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllLicensesAndCertificationsByUserId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.GetAllLicensesAndCertificationsByUserId(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateLicenseForUser(LicensesCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.CreateLicensesAndCertificationsForUser(createRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSkillsByUserId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.GetAllSkillsByUserId(id, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllLanguagesByUserId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.GetAllLanguagesByUserId(id, cancellationToken);  
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
