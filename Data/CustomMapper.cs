using NPoco;

namespace TimeKeeperApp.Data
{
    class CustomMapper : DefaultMapper
    {
        public override Func<object, object> GetFromDbConverter(Type destType, Type sourceType)
        {
            // https://github.com/schotime/NPoco/issues/359#issuecomment-272171474
            if (destType == typeof(Guid) && sourceType == typeof(string))
                return (value) => Guid.Parse((string)value);
            if (destType == typeof(Guid) && sourceType == typeof(byte[]))
                return (value) => new Guid((byte[])value);
            if (destType == typeof(Guid?) && sourceType == typeof(string))
                return (value) => (value != null) ? Guid.Parse((string)value) : (Guid?)null;
            if (destType == typeof(Guid?) && sourceType == typeof(byte[]))
                return (value) => (value != null) ? new Guid((byte[])value) : (Guid?)null;
            return base.GetFromDbConverter(destType, sourceType);
        }
    }
}
