using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.Customer
{
    public class GetCustomersSpecification : Specification<CustomerEntity>
    {
        public GetCustomersSpecification()
        {
            AddInclude(c => c.PhoneNumber);
        }

        public override Expression<Func<CustomerEntity, bool>> ToExpression()
        {
            return customer => customer is CustomerEntity;
        }
    }
}
