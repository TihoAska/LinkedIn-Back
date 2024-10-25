﻿using AutoMapper;
using LinkedIn.Data;
using LinkedIn.Models.Pages;
using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.Images;
using LinkedIn.Models.ProfileDetails.Languages;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Locations;
using LinkedIn.Models.ProfileDetails.Skills;
using LinkedIn.Repository.IRepository;
using LinkedIn.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.OpenApi.Validations;

namespace LinkedIn.Services
{
    public class ProfileService : IProfileService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _autoMapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProfileService(IUnitOfWork unitOfWork, IMapper autoMapper, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<UserEducation> CreateEducationForUser(EducationCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(createRequest.UserId, cancellationToken);

            if (userFromDb == null)
            {
                throw new Exception("User with the given ID was not found");
            }

            var newEducation = _autoMapper.Map<UserEducation>(createRequest);
            var institutionFromDb = await _unitOfWork.Institutions.GetByName(newEducation.Name ,cancellationToken);

            if(institutionFromDb == null)
            {
                var newInstitution = new Institution()
                {
                    Name = newEducation.Name,
                    SchoolImageUrl = "../../assets/images/pageLogos/education-default.png",
                };

                _unitOfWork.Institutions.Add(newInstitution);
                await _unitOfWork.SaveChangesAsync();

                newEducation.SchoolImageUrl = newInstitution.SchoolImageUrl;
                newEducation.Institution = newInstitution;
                newEducation.InstitutionId = newInstitution.Id;
                newEducation.User = userFromDb;
            } 
            else
            {
                newEducation.SchoolImageUrl = institutionFromDb.SchoolImageUrl;
                newEducation.Institution = institutionFromDb;
                newEducation.InstitutionId = institutionFromDb.Id;
                newEducation.User = userFromDb;
            }

            _unitOfWork.Educations.Add(newEducation);
            await _unitOfWork.SaveChangesAsync();

            return newEducation;
        }

        public async Task<bool> DeleteEducationForUser(int educationId, int userId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken);

            if (userFromDb.Education == null)
            {
                throw new Exception("User has no educations!");
            }

            var userEducation = userFromDb.Education.Find(education => education.Id == educationId);

            if(userEducation == null)
            {
                throw new Exception("User has no specified education");
            }

            userFromDb.Education.Remove(userEducation);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<Experience> CreateExperienceForUser(ExperienceCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(createRequest.UserId, cancellationToken);

            if(userFromDb == null)
            {
                throw new Exception("User with the given ID was not found");
            }

            var companyFromDb = await _unitOfWork.Pages.GetByName(createRequest.CompanyName, cancellationToken);

            if(companyFromDb == null)
            {
                throw new Exception("Company with the given name was not found!");
            }

            var companyLocationFromDb = await _unitOfWork.CompanyLocations.GetCompanyLocationByCityName(createRequest.Location.City, cancellationToken);
            var newExperience = _autoMapper.Map<Experience>(createRequest);

            newExperience.CompanyId = companyFromDb.Id;
            newExperience.CompanyLocationId = companyLocationFromDb.Id;
            newExperience.CompanyImageUrl = companyFromDb.ImageUrl;

            _unitOfWork.Experiences.Add(newExperience); 
            await _unitOfWork.SaveChangesAsync();

            return newExperience;
        }
        public async Task<bool> DeleteExperienceForUser(int experienceId, int userId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken);

            if(userFromDb == null)
            {
                throw new Exception("User with the given id was not found!");
            }

            if(userFromDb.Experience == null)
            {
                throw new Exception("User has no experience");
            }

            var userExperience = userFromDb.Experience.Find(experience => experience.Id == experienceId);

            if(userExperience == null )
            {
                throw new Exception("User has no specified experience");
            }

            userFromDb.Experience.Remove(userExperience);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }


        public async Task<IEnumerable<Institution>> GetAllInstitutions(CancellationToken cancellationToken)
        {
            var institutionsFromDb = await _unitOfWork.Institutions.GetAll(cancellationToken);

            if(institutionsFromDb == null)
            {
                return [];
            }
            
            return institutionsFromDb;
        }

        public async Task<UserLanguages> CreateLanguageForUser(LanguagesCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var newLanguage = _autoMapper.Map<UserLanguages>(createRequest);

            _unitOfWork.Languages.Add(newLanguage);
            await _unitOfWork.SaveChangesAsync();

            return newLanguage;
        }

        public Task<LicensesAndCertifications> CreateLicensesAndCertifications(LicensesCreateRequest createRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Skill>> GetAllSkills(CancellationToken cancellationToken)
        {
            var skillsFromDb = await _unitOfWork.Skills.GetAll(cancellationToken);

            if(skillsFromDb == null)
            {
                return [];
            }

            return skillsFromDb;
        }

        public async Task<bool> DeleteLicenseForUser(int userId, int licenseId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken) ?? throw new Exception("User was not found!");
            
            if(userFromDb.LicensesAndCertifications == null)
            {
                throw new Exception("User has no licenses");
            }

            var licenseToRemove = userFromDb.LicensesAndCertifications.Find(license => license.Id == licenseId);

            if(licenseToRemove == null)
            {
                throw new Exception("User has no specified license!");
            }

            userFromDb.LicensesAndCertifications.Remove(licenseToRemove);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<Skill> CreateSkill(SkillsCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var newSkill = _autoMapper.Map<Skill>(createRequest);

            _unitOfWork.Skills.Add(newSkill);
            await _unitOfWork.SaveChangesAsync();

            return newSkill;
        }

        public async Task<UserSkills> CreateSkillForUser(SkillForUserCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(createRequest.UserId, cancellationToken);
            var newSkill = _autoMapper.Map<UserSkills>(createRequest);
            var companyFromDb = await _unitOfWork.Pages.GetByName(createRequest.SkillDomain, cancellationToken);

            if(userFromDb == null)
            {
                throw new Exception("User with the given ID was not found!");
            }

            var result = userFromDb.Skills.Find(skill => skill.Name == createRequest.Name && skill.Description == createRequest.Description);
            if(result != null)
            {
                throw new Exception("This skill is already on your profile");
            }

            if(companyFromDb == null)
            {
                var institutionFromDb = await _unitOfWork.Institutions.GetByName(createRequest.SkillDomain, cancellationToken);

                if(institutionFromDb == null)
                {
                    throw new Exception("Institution with the given ID was not found!");
                }

                newSkill.SkillsImageUrl = institutionFromDb.SchoolImageUrl;
            }
            else
            {
                newSkill.SkillsImageUrl = companyFromDb.ImageUrl;
            }

            if(userFromDb.Skills == null)
            {
                userFromDb.Skills = new List<UserSkills>();  
            }
            
            userFromDb.Skills.Add(newSkill);
            await _unitOfWork.SaveChangesAsync();

            return newSkill;
        }

        public async Task<bool> DeleteUserSkill(int userId, string skillName, string skillDescription, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken);

            if(userFromDb.Skills != null)
            {
                var skillToDelete = userFromDb.Skills.Find(skill => skill.Name == skillName && skill.Description == skillDescription);

                if(skillToDelete != null)
                {
                    userFromDb.Skills.Remove(skillToDelete);
                    await _unitOfWork.SaveChangesAsync();

                    return true;
                } 
                else
                {
                    throw new Exception("Skill " + skillName  + " " + skillDescription + " was not found!");
                }
            }
            else
            {
                throw new Exception("User has no skills");
            }
        }

        public async Task<IEnumerable<UserEducation>> GetAllEducationsByUserId(int userId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken);

            if(userFromDb.Education == null)
            {
                return [];
            }

            return userFromDb.Education.Select(education => new UserEducation
            {
                Id = education.Id,
                UserId = education.UserId,
                Name = education.Name,
                Degree = education.Degree,
                FieldOfStudy = education.FieldOfStudy,
                StartTime = education.StartTime,
                EndTime = education.EndTime,
                SchoolImageUrl = education.SchoolImageUrl,
            });
        }

        public async Task<IEnumerable<Experience>> GetAllExperiencesByUserId(int userId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken);

            if(userFromDb.Experience == null)
            {
                return [];
            }

            return userFromDb.Experience.Select(experience => new Experience
            {
                Id = experience.Id,
                UserId = experience.UserId,
                Position = experience.Position,
                Company = experience.Company,
                CompanyId = experience.CompanyId,
                CompanyLocationId = experience.CompanyLocationId,
                CompanyImageUrl = experience.CompanyImageUrl,
                EmploymentType = experience.EmploymentType,
                StartTime = experience.StartTime,
                EndTime = experience.EndTime,
                Location = experience.Location,
                LocationType = experience.LocationType,
            });
        }

        public async Task<IEnumerable<UserLanguages>> GetAllLanguagesByUserId(int userId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken);

            if (userFromDb.Languages == null)
            {
                return [];
            }

            return userFromDb.Languages.Select(language => new UserLanguages
            {
                Id = language.Id,
                Name = language.Name,
                Proficiency = language.Proficiency,
                UserId = language.UserId,
            });
        }

        public async Task<IEnumerable<LicensesAndCertifications>> GetAllLicensesAndCertificationsByUserId(int userId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken);

            if(userFromDb.LicensesAndCertifications == null)
            {
                return [];
            }

            return userFromDb.LicensesAndCertifications.Select(license => new LicensesAndCertifications
            {
                Id = license.Id,
                UserId = license.UserId,
                Name = license.Name,
                IssuingOrganization = license.IssuingOrganization,
                IssueDate = license.IssueDate,
                CredentialId = license.CredentialId,
                OrganizationImageUrl = license.OrganizationImageUrl,
            });
        }

        public async Task<LicensesAndCertifications> CreateLicensesAndCertificationsForUser(LicensesCreateRequest createRequest, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetById(createRequest.UserId, cancellationToken) ?? throw new Exception("User with the given ID was not found");
            var newLicense = _autoMapper.Map<LicensesAndCertifications>(createRequest);
            var companyFromDb = await _unitOfWork.Pages.GetByName(createRequest.IssuingOrganization, cancellationToken) ?? throw new Exception("Company with the given NAME was not found!");

            newLicense.UserId = userFromDb.Id;
            newLicense.OrganizationImageUrl = companyFromDb.ImageUrl;

            _unitOfWork.Licenses.Add(newLicense);
            await _unitOfWork.SaveChangesAsync();

            return newLicense;
        }

        public async Task<LicensesAndCertifications> EditLicenseOrCertificationForUser(LicenseUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var licenseFromDb = await _unitOfWork.Licenses.GetById(updateRequest.Id, cancellationToken);
            var organizationFromDb = await _unitOfWork.Pages.GetByName(updateRequest.IssuingOrganization, cancellationToken);

            if(licenseFromDb == null)
            {
                throw new Exception("License with the given ID was not found!");
            }

            licenseFromDb.Name = updateRequest.Name;
            licenseFromDb.IssuingOrganization = updateRequest.IssuingOrganization;
            licenseFromDb.IssueDate = updateRequest.IssueDate;
            licenseFromDb.CredentialId = updateRequest.CredentialId;
            licenseFromDb.OrganizationImageUrl = organizationFromDb.ImageUrl;

            await _unitOfWork.SaveChangesAsync();

            return licenseFromDb;
        }

        public async Task<IEnumerable<UserSkills>> GetAllSkillsByUserId(int userId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken);

            if(userFromDb.Skills == null)
            {
                return [];
            }

            return userFromDb.Skills.Select(skill => new UserSkills
            {
                Id = skill.Id,
                UserId = skill.UserId,
                Name = skill.Name,
                Description = skill.Description,
                SkillsImageUrl = skill.SkillsImageUrl,
            });
        }

        public async Task<Experience> EditExperienceForUser(ExperienceUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var experienceFromDb = await _unitOfWork.Experiences.GetExperienceById(updateRequest.Id, cancellationToken);
            var companyFromDb = await _unitOfWork.Pages.GetByName(updateRequest.CompanyName, cancellationToken);
            var companyLocationFromDb = await _unitOfWork.CompanyLocations.GetCompanyLocationByCityName(updateRequest.Location.City, cancellationToken);

            experienceFromDb.Position = updateRequest.Position;
            experienceFromDb.EmploymentType = updateRequest.EmploymentType;
            experienceFromDb.CompanyId = companyFromDb.Id;
            experienceFromDb.Company = companyFromDb;
            experienceFromDb.CompanyImageUrl = companyFromDb.ImageUrl;
            experienceFromDb.Location = companyLocationFromDb;
            experienceFromDb.LocationType = updateRequest.LocationType;
            experienceFromDb.StartTime = updateRequest.StartDate;
            experienceFromDb.EndTime = updateRequest.EndDate;

            await _unitOfWork.SaveChangesAsync();

            return experienceFromDb;
        }

        public async Task<UserEducation> EditEducationForUser(EducationUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var educationFromDb = await _unitOfWork.Educations.GetById(updateRequest.Id, cancellationToken);
            var institutionFromDb = await _unitOfWork.Institutions.GetByName(updateRequest.School, cancellationToken);

            educationFromDb.Name = updateRequest.School;
            educationFromDb.Degree = updateRequest.Degree;
            educationFromDb.FieldOfStudy = updateRequest.FieldOfStudy;
            educationFromDb.StartTime = updateRequest.StartDate;
            educationFromDb.EndTime = updateRequest.EndDate;
            educationFromDb.SchoolImageUrl = institutionFromDb.SchoolImageUrl;
            educationFromDb.InstitutionId = institutionFromDb.Id;
            educationFromDb.Institution = institutionFromDb;

            await _unitOfWork.SaveChangesAsync();

            return educationFromDb;
        }

        public async Task<UserLanguages> EditLanguage(LanguagesUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var languageFromDb = await _unitOfWork.Languages.GetById(updateRequest.Id, cancellationToken);

            if(languageFromDb == null)
            {
                throw new Exception("UserLanguage with the given ID was not found!");
            }

            languageFromDb.Name = updateRequest.Name;
            languageFromDb.Proficiency = updateRequest.Proficiency;

            await _unitOfWork.SaveChangesAsync();

            return languageFromDb;
        }

        public async Task<bool> DeleteLanguageForUser(int userId, int languageId, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(userId, cancellationToken);

            if(userFromDb.Languages == null)
            {
                throw new Exception("User has no languages!");
            }

            var userLanguage = userFromDb.Languages.Where(language => language.Id == languageId).FirstOrDefault();

            if(userLanguage == null)
            {
                throw new Exception("User has no language with the id " + languageId);
            }

            userFromDb.Languages.Remove(userLanguage);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<BackgroundImage> ChangeBackgroundImage(BackgroundImageUpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var userFromDb = await _unitOfWork.Users.GetByIdWithUserDetails(updateRequest.UserId, cancellationToken);

            if (userFromDb == null)
            {
                throw new Exception("User with the ID " + updateRequest.UserId + " was not found!");
            }

            if (string.IsNullOrEmpty(updateRequest.ImageData))
            {
                userFromDb.ProfileDetails.BannerImage = "/assets/profileMisc/timeline.png";
            }
            else
            {
                var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, userFromDb.ProfileDetails.BannerImage.TrimStart('/'));

                if (File.Exists(oldImagePath) && userFromDb.ProfileDetails.BannerImage != "timeline.png")
                {
                    File.Delete(oldImagePath);
                }

                var base64Data = updateRequest.ImageData.Substring(updateRequest.ImageData.IndexOf(",") + 1);
                var imageBytes = Convert.FromBase64String(base64Data);

                var guid = Guid.NewGuid();

                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "assets/profileMisc", $"image_{guid}.jpg");

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllBytes(filePath, imageBytes);

                userFromDb.ProfileDetails.BannerImage = "/assets/profileMisc/" + $"image_{guid}.jpg";
            }

            await _unitOfWork.SaveChangesAsync();

            return new BackgroundImage
            {
                UserId = userFromDb.Id,
                ImageUrl = userFromDb.ProfileDetails.BannerImage,
            };
        }
    }
}
