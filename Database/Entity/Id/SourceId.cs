using Database.Entity.Util;

namespace Database.Entity.Id;

public class SourceId(Guid value) : TypedGuid<SourceId>(value);