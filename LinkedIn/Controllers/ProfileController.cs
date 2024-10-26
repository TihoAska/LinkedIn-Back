﻿using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.Images;
using LinkedIn.Models.ProfileDetails.Languages;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Skills;
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

        [HttpDelete]
        public async Task<ActionResult> DeleteExperienceForUser(int experienceId, int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.DeleteExperienceForUser(experienceId, userId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllInstitutions(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.GetAllInstitutions(cancellationToken);
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

        [HttpPut]
        public async Task<ActionResult> EditEducationForUser(EducationUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.EditEducationForUser(updateRequest, cancellationToken);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEducationForUser(int educationId, int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.DeleteEducationForUser(educationId, userId, cancellationToken);
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

        [HttpPut]
        public async Task<ActionResult> EditLicenseOrCertificationForUser(LicenseUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.EditLicenseOrCertificationForUser(updateRequest, cancellationToken);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLicenseForUser(int userId, int licenseId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.DeleteLicenseForUser(userId, licenseId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSkills(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.GetAllSkills(cancellationToken);
                return Ok(result);
            }
            catch(Exception ex)
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

        [HttpPost]
        public async Task<ActionResult> CreateSkillForUser(SkillForUserCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.CreateSkillForUser(createRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserSkill(int userId, string skillName, string skillDescription, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.DeleteUserSkill(userId, skillName, skillDescription, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateSkill(SkillsCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.CreateSkill(createRequest, cancellationToken);
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

        [HttpPost]
        public async Task<ActionResult> CreateLanguageForUser(LanguagesCreateRequest createRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.CreateLanguageForUser(createRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> EditUserLanguage(LanguagesUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.EditLanguage(updateRequest, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLanguageForUser(int userId, int languageId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.DeleteLanguageForUser(userId, languageId, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> ChangeBackgroundImage(BackgroundImageUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profileService.ChangeBackgroundImage(updateRequest, cancellationToken);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
