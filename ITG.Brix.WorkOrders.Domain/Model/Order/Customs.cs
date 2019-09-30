namespace ITG.Brix.WorkOrders.Domain
{
    public class Customs
    {
        public Customs()
        {
            Document = new Document();
        }
        public string CertificateOfOrigin { get; set; }
        public Document Document { get; set; }
    }
}
