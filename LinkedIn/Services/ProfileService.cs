using AutoMapper;
using LinkedIn.Data;
using LinkedIn.Models.Pages;
using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.Languages;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Locations;
using LinkedIn.Models.ProfileDetails.Skills;
using LinkedIn.Repository.IRepository;
using LinkedIn.Services.IServices;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LinkedIn.Services
{
    public class ProfileService : IProfileService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _autoMapper;

        public ProfileService(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
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

        public async Task<IEnumerable<Institution>> GetAllInstitutions(CancellationToken cancellationToken)
        {
            var institutionsFromDb = await _unitOfWork.Institutions.GetAll(cancellationToken);

            if(institutionsFromDb == null)
            {
                return [];
            }
            
            return institutionsFromDb;
        }

        public Task<UserLanguages> CreateLanguages(LanguagesCreateRequest createRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<LicensesAndCertifications> CreateLicensesAndCertifications(LicensesCreateRequest createRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserSkills> CreateSkills(SkillsCreateRequest createRequest, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

            experienceFromDb.Position = updateRequest.Position;
            experienceFromDb.EmploymentType = updateRequest.EmploymentType;
            experienceFromDb.CompanyId = companyFromDb.Id;
            experienceFromDb.Company = companyFromDb;
            experienceFromDb.CompanyImageUrl = companyFromDb.ImageUrl;
            experienceFromDb.Location = updateRequest.Location;
            experienceFromDb.LocationType = updateRequest.LocationType;
            experienceFromDb.StartTime = updateRequest.StartDate;
            experienceFromDb.EndTime = updateRequest.EndDate;

            await _unitOfWork.SaveChangesAsync();

            return experienceFromDb;
        }
    }
}
