using Database.Entity.Util;

namespace Database.Entity.Id;

public class MessageEntityId(Guid value) : TypedGuid<MessageEntityId>(value);