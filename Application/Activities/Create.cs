using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);
                //Not adding Data to DB, just adding data to the memory only. Thus not accessing the DB at the moment.

                var result = await _context.SaveChangesAsync() > 0;
                //Above line will save changes to the DB.

                if (!result) return Result<Unit>.Failure("Failed to create activity");

                return Result<Unit>.Success(Unit.Value);
                //Unit is just an object that the mediator provides but doesn't really have any actual value, like returning nothing
                //But it will tell the API that the request is finished and it's gonna move on
                //When using mediator, it will be waiting for task to complete, and it will tell the api that our task is complete.
            }
        }
    }
}