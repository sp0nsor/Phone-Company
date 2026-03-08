using PhoneCompany.Infrastructure.Entities;
using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications.RefreshToken
{
    public class GetRefreshTokenByIdSpecification : Specification<RefreshTokenEntity>
    {
        private readonly Guid _id;

        public GetRefreshTokenByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<RefreshTokenEntity, bool>> ToExpression()
        {
            return t => t.Id == _id;
        }
    }
}
