using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.PhoneNumber
{
    public class GetPhoneNumberByIdSpecification : Specification<PhoneNumberEntity>
    {
        private readonly Guid _phoneNumberId;

        public GetPhoneNumberByIdSpecification(Guid phoneNumberId)
        {
            _phoneNumberId = phoneNumberId;
         
            AddInclude(p => p.TariffPlan);
        }

        public override Expression<Func<PhoneNumberEntity, bool>> ToExpression()
        {
            return p => p.Id == _phoneNumberId;
        }
    }
}
