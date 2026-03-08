using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace PhoneCompany.Infrastructure.Specifications.Service
{
    public class GetServiceByIdSpecification : Specification<ServiceEntity>
    {
        private readonly Guid _serviceId;

        public GetServiceByIdSpecification(Guid serviceId)
        {
            _serviceId = serviceId;

            AddInclude(s => s.TariffPlans);
        }

        public override Expression<Func<ServiceEntity, bool>> ToExpression()
        {
            return s => s.Id == _serviceId;
        }
    }
}
