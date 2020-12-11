using FluentValidation;

namespace Infotecs.Articles.Server.Application.Validators
{
    public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleRequestValidator"/> class.
        /// </summary>
        public CreateArticleRequestValidator()
        {
            RuleFor(x => x.User).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Title).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Content).NotEmpty().MinimumLength(10);
            RuleFor(x => x.ThumbnailImage).NotNull().NotEmpty();
        }
    }
}