using Database.Entity.Util;

namespace Database.Entity.Id;

public class UsageEntityId(Guid value) : TypedGuid<UsageEntityId>(value);