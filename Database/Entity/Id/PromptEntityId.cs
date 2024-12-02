using Database.Entity.Util;

namespace Database.Entity.Id;

public class PromptEntityId(Guid value) : TypedGuid<PromptEntityId>(value);