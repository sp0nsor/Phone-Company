using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.TariffPlan
{
    public class GetTariffPlanByIdSpecification : Specification<TariffPlanEntity>
    {
        private readonly Guid _tariffPlanId;

        public GetTariffPlanByIdSpecification(Guid tariffPlanId)
        {
            _tariffPlanId = tariffPlanId;

            AddInclude(t => t.Numbers);
            AddInclude(t => t.Services);
        }

        public override Expression<Func<TariffPlanEntity, bool>> ToExpression()
        {
            return t => t.Id == _tariffPlanId;
        }
    }
}
