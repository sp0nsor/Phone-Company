using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.PhoneNumber
{
    public class GetPhoneNumbersSpecification : Specification<PhoneNumberEntity>
    {
        public GetPhoneNumbersSpecification()
        {
            AddInclude(p => p.TariffPlan);
        }

        public override Expression<Func<PhoneNumberEntity, bool>> ToExpression()
        {
            return p => p is PhoneNumberEntity;
        }
    }
}
