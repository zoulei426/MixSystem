using FluentValidation;

namespace Mix.Library.Entities.Model.Validator
{
    /// <summary>
    /// 账户验证器
    /// </summary>
    public class UserValidator : AbstractValidator<User>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不能为空")
                .Length(2, 30).WithMessage("用户名长度在2-30个字符之间");
        }
    }
}