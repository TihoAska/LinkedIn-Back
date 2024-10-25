﻿using LinkedIn.Models.ProfileDetails.Experiences;

namespace LinkedIn.Repository.IRepository
{
    public interface IExperienceRepository : IGenericRepository<Experience>
    {
        Task<Experience> GetExperienceById(int id, CancellationToken cancellationToken);
    } 
}
