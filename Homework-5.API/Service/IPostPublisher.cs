using Homework_5.API.Entites;

namespace Homework_5.API.Service
{
    public interface IPostPublisher
    {
        Task Publish(Post post, CancellationToken cancellationToken = default);
    }

}
