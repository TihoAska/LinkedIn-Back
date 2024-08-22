using LinkedIn.Data;
using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LinkedIn.Repository
{
    public class InstitutionRepository : GenericRepository<Institution>, IInstitutionRepository
    {
        public InstitutionRepository(IDataContext dataContext) : base(dataContext)
        {
            
        }

        public Task<Institution> GetByName(string name, CancellationToken cancellationToken)
        {
            return _query.Where(institution => institution.Name == name).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
