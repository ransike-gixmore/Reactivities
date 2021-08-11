using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);
                //Not adding Data to DB, just adding data to the memory only. Thus not accessing the DB at the moment.

                await _context.SaveChangesAsync();
                //Above line will save changes to the DB.

                return Unit.Value;
                //Unit is just an object that the mediator provides but doesn't really have any actual value, like returning nothing
                //But it will tell the API that the request is finished and it's gonna move on
                //When using mediator, it will be waiting for task to complete, and it will tell the api that our task is complete.
            }
        }
    }
}