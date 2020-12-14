namespace Infotecs.Articles.Server.Application.Validators
{
    using FluentValidation;

    /// <summary>
    /// Validator for CreateArticleRequest.
    /// </summary>
    public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleRequestValidator"/> class.
        /// </summary>
        public CreateArticleRequestValidator()
        {
            this.RuleFor(x => x.User).NotEmpty().MinimumLength(3);
            this.RuleFor(x => x.Title).NotEmpty().MinimumLength(3);
            this.RuleFor(x => x.Content).NotEmpty().MinimumLength(10);
            this.RuleFor(x => x.ThumbnailImage).NotNull().NotEmpty();
        }
    }
}