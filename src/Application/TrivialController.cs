using System;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Application
{
    [Route("api/[controller]")]
    public class TrivialController
    {
        [HttpGet("{name}")]
        public IEnumerable<string> Get(TrivialOptions options)
        {
            if (options.PageNumber == 21)
            {
                throw new InvalidOperationException("oops");
            }

            return new string[] { "nothing special", options.Name, options.PageNumber.ToString() };
        }
    }

    public sealed class TrivialOptions
    {
        public string Name { get; set; }

        public int PageNumber { get; set; }
    }

    public class TrivialOptionsValidator : AbstractValidator<TrivialOptions>
    {
        public TrivialOptionsValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.PageNumber)
                .InclusiveBetween(0, 21)
                .WithMessage("Please specify a first name");
        }
    }
}
