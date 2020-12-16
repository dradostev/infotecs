using FluentValidation;

namespace Infotecs.Articles.Server.Application.Validators
{
    /// <summary>
    /// Validator for AddCommentRequest.
    /// </summary>
    public class AddCommentRequestValidator : AbstractValidator<AddCommentRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommentRequestValidator"/> class.
        /// </summary>
        public AddCommentRequestValidator()
        {
            this.RuleFor(x => x.ArticleId).NotEmpty();
            this.RuleFor(x => x.User).NotEmpty().MinimumLength(3);
            this.RuleFor(x => x.Content).NotEmpty().MinimumLength(10);
        }
    }
}