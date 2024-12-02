using Database.Entity.Util;

namespace Database.Entity.Id;

public class ConversationEntityId(Guid value) : TypedGuid<ConversationEntityId>(value);