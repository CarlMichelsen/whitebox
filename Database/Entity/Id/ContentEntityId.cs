using Database.Entity.Util;

namespace Database.Entity.Id;

public class ContentEntityId(Guid value) : TypedGuid<ContentEntityId>(value);