using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.User
{
    public class GetUserByIdSpecification : Specification<UserEntity>
    {
        private readonly Guid _id;

        public GetUserByIdSpecification(Guid id)
        {
            _id = id;

            AddInclude(u => u.Role);
            AddInclude(u => u.RefreshToken);
        }

        public override Expression<Func<UserEntity, bool>> ToExpression()
        {
            return u => u.Id == _id;
        }
    }
}
