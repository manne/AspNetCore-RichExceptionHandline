using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Application
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TrivialController
    {
        [HttpGet("{name}")]
        public IEnumerable<string> Get(TrivialOptions options)
        {
            return new [] { "nothing special", options.Name, options.PageNumber?.ToString() ?? "not set" };
        }
    }

    public sealed class TrivialOptions
    {
        public TrivialOptions(string name, int? pageNumber)
        {
            Name = name;
            PageNumber = pageNumber;
        }

        public string Name { get; }

        public int? PageNumber { get; }
    }

    public class TrivialOptionsValidator : AbstractValidator<TrivialOptions>
    {
        public TrivialOptionsValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.PageNumber)
                .InclusiveBetween(0, 21)
                .WithMessage("'{PropertyName}' must be a value in the range of {From} to {To} inclusive. You used {PropertyValue}");
        }
    }
}
