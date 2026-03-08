using System.Linq.Expressions;

namespace PhoneCompany.Infrastructure.Specifications
{
    public abstract class Specification<T>
    {
        public List<Expression<Func<T, object>>> Includes { get; set; } = [];

        protected void AddInclude(Expression<Func<T, object>> includeExpression) =>
            Includes.Add(includeExpression);

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
