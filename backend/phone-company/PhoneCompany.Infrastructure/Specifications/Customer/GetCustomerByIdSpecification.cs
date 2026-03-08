using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.Customer
{
    public class GetCustomerByIdSpecification : Specification<CustomerEntity>
    {
        private readonly Guid _customerId;

        public GetCustomerByIdSpecification(Guid customerId)
        {
            _customerId = customerId;

            AddInclude(c => c.PhoneNumber);
        }

        public override Expression<Func<CustomerEntity, bool>> ToExpression()
        {
            return c => c.Id == _customerId;
        }
    }
}
