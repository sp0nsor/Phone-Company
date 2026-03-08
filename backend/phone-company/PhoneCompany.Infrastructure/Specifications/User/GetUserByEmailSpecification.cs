using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.User
{
    public class GetUserByEmailSpecification : Specification<UserEntity>
    {
        private readonly string _email;

        public GetUserByEmailSpecification(string email)
        {
            _email = email;

            AddInclude(x => x.Role);
            AddInclude(x => x.RefreshToken);
        }

        public override Expression<Func<UserEntity, bool>> ToExpression()
        {
            return u => u.Email == _email;
        }
    }
}
