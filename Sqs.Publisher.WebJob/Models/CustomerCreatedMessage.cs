namespace Sqs.Publisher.WebJob.Models
{
    public sealed class CustomerCreatedMessage
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string GitHubUserName { get; init; }
    }
}
