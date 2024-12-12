namespace Database.Entity;

public interface ISoftDeletable
{
    DateTime? DeletedAtUtc { get; }
}