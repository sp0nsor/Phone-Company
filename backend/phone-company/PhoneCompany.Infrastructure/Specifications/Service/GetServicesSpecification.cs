using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.Service
{
    public class GetServicesSpecification : Specification<ServiceEntity>
    {
        public override Expression<Func<ServiceEntity, bool>> ToExpression()
        {
            return s => s is ServiceEntity;
        }
    }
}
