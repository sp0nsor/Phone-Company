using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.TariffPlan
{
    public class GetTariffPlansSpecification : Specification<TariffPlanEntity>
    {
        public GetTariffPlansSpecification()
        {
            AddInclude(t => t.Services);
        }

        public override Expression<Func<TariffPlanEntity, bool>> ToExpression()
        {
            return t => t is TariffPlanEntity;
        }
    }
}
