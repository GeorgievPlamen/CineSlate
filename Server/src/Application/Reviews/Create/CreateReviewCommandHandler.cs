using Application.Common;
using Domain.Movies.Reviews.ValueObjects;
using MediatR;

namespace Application.Reviews.Create;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<ReviewId>>
{
    public Task<Result<ReviewId>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
