using System;
using Autofac;
using FluentValidation;

namespace Infotecs.Articles.Server.Application.Validators
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext context;

        public AutofacValidatorFactory(IComponentContext context)
        {
            this.context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (context.TryResolve(validatorType, out var instance))
            {
                return instance as IValidator;
            }

            return null;
        }
    }
}