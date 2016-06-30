namespace SurreyDigitalServices.Locate.Test.Interfaces
{
    public interface IAddress
    {
        string Property { get; }
        string Street { get; }
        string Locality { get; }
        string Town { get; }
        string Area { get; }
        string PostCode { get; }
        long Uprn { get; }
        string GssCode { get; }
    }
}
