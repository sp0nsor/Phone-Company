using PhoneCompany.Infrastructure.Entities;
using PhoneCompany.Infrastructure.Enums;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.PhoneNumber
{
    public class GetFreePhoneNumberSpecification : Specification<PhoneNumberEntity>
    {
        public override Expression<Func<PhoneNumberEntity, bool>> ToExpression()
        {
            return pn => pn.Status == PhoneStatus.Inactive;
        }
    }
}
