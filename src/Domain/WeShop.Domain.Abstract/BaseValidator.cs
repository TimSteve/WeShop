using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace WeShop.Domain.Abstract
{
    /// <summary>
    /// 验证器
    /// </summary>
    public abstract class BaseValidator
    {
        public ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// 验证是否有效
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValid();

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <returns></returns>
        public string GetErrors()
        {
            if (!ValidationResult.Errors.Any())
            {
                return string.Empty;
            }

            var errors = new StringBuilder();
            foreach (var failure in ValidationResult.Errors)
            {
                errors.Append($"{failure.ErrorMessage}");
            }

            return errors.ToString();
        }
    }
}