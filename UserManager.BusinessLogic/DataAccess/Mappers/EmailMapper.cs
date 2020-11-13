using Dapper;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.DataAccess.Mappers
{
    public class EmailMapper : SqlMapper.StringTypeHandler<Email>
    {
        protected override string Format(Email email) => email.ToString();

        protected override Email Parse(string email) => new Email(email);
    }
}
