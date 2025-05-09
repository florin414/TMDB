using PopcornHub.Domain.Common;
using Volo.Abp.Domain.Entities;

namespace PopcornHub.Domain.Aggregates;

public class CommentExample: BaseEntity, IAggregateRoot<int>
{
    public object?[] GetKeys()
    {
        return [];
    }
}