
using Application.Common;

using MediatR;

namespace Application.Reviews.Comments;

public class CommentReviewCommandHandler : IRequestHandler<CommentReviewCommand, Result<ReviewDetailsResponse>>
{
    public Task<Result<ReviewDetailsResponse>> Handle(CommentReviewCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
