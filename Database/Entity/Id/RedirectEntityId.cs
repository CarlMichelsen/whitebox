using Database.Entity.Util;

namespace Database.Entity.Id;

public class RedirectEntityId(Guid value) : TypedGuid<RedirectEntityId>(value);