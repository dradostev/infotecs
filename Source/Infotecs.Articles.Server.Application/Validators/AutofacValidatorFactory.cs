using System;
using Autofac;
using FluentValidation;

namespace Infotecs.Articles.Server.Application.Validators
{
    /// <inheritdoc />
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacValidatorFactory"/> class.
        /// </summary>
        /// <param name="context">Component context.</param>
        public AutofacValidatorFactory(IComponentContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public override IValidator CreateInstance(Type validatorType)
        {
            if (this.context.TryResolve(validatorType, out var instance))
            {
                return instance as IValidator;
            }

            return null;
        }
    }
}