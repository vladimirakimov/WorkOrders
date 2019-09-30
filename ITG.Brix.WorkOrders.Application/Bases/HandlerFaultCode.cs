namespace ITG.Brix.WorkOrders.Application.Bases
{
    public class HandlerFaultCode
    {

        public static readonly HandlerFaultCode UpstreamAccessBiztalk = new HandlerFaultCode("upstream-access-biztalk");
        public static readonly HandlerFaultCode UpstreamAccessPlato = new HandlerFaultCode("upstream-access-plato");
        public static readonly HandlerFaultCode NotMet = new HandlerFaultCode("not-met");
        public static readonly HandlerFaultCode NotFound = new HandlerFaultCode("not-found");
        public static readonly HandlerFaultCode Conflict = new HandlerFaultCode("conflict");
        public static readonly HandlerFaultCode InvalidQueryFilter = new HandlerFaultCode("invalid-query-filter");
        public static readonly HandlerFaultCode InvalidCredentials = new HandlerFaultCode("invalid-credentials");

        public string Name { get; private set; }

        private HandlerFaultCode() { }

        public HandlerFaultCode(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as HandlerFaultCode;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Name.Equals(otherValue.Name);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
