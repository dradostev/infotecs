using FluentValidation;

namespace Infotecs.Articles.Server.Application.Validators
{
    public class AddCommentRequestValidator : AbstractValidator<AddCommentRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCommentRequestValidator"/> class.
        /// </summary>
        public AddCommentRequestValidator()
        {
            RuleFor(x => x.ArticleId).NotEmpty();
            RuleFor(x => x.User).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Content).NotEmpty().MinimumLength(10);
        }
    }
}