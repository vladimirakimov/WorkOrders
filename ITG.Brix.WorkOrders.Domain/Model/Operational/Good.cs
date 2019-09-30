using System;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Good : Entity
    {
        public Good(Guid id) : base(id) { }

        public Configuration Configuration { get; private set; }
        public string Code { get; private set; }
        public string Customer { get; private set; }
        public string Arrival { get; private set; }
        public string Article { get; private set; }
        public string ArticlePackagingCode { get; private set; }
        public string Name { get; private set; }
        public string Gtin { get; private set; }
        public string ProductType { get; private set; }
        public string MaterialType { get; private set; }
        public string Color { get; private set; }
        public string Shape { get; private set; }
        public string Lotbatch { get; private set; }
        public string Lotbatch2 { get; private set; }
        public string ClientReference { get; private set; }
        public string ClientReference2 { get; private set; }
        public DateOn BestBeforeDate { get; private set; }
        public DateOn DateFifo { get; private set; }
        public string CustomsDocument { get; private set; }
        public string StorageStatus { get; private set; }
        public string Stackheight { get; private set; }
        public string Length { get; private set; }
        public string Width { get; private set; }
        public string Height { get; private set; }
        public string OriginalContainer { get; private set; }
        public Quantity Quantity { get; private set; }
        public Weight WeightNet { get; private set; }
        public Weight WeightGross { get; private set; }

        public void SetConfiguration(Configuration configuration)
        {
            Configuration = configuration;
        }

        public void SetCode(string code)
        {
            Code = code;
        }

        public void SetCustomer(string customer)
        {
            Customer = customer;
        }

        public void SetArrival(string arrival)
        {
            Arrival = arrival;
        }

        public void SetArticle(string article)
        {
            Article = article;
        }

        public void SetArticlePackagingCode(string articlePackagingCode)
        {
            ArticlePackagingCode = articlePackagingCode;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetGtin(string gtin)
        {
            Gtin = gtin;
        }

        public void SetProductType(string productType)
        {
            ProductType = productType;
        }

        public void SetMaterialType(string materialType)
        {
            MaterialType = materialType;
        }

        public void SetColor(string color)
        {
            Color = color;
        }

        public void SetShape(string shape)
        {
            Shape = shape;
        }
        public void SetLotbatch(string lotbatch)
        {
            Lotbatch = lotbatch;
        }

        public void SetLotbatch2(string lotbatch2)
        {
            Lotbatch2 = lotbatch2;
        }

        public void SetClientReference(string clientReference)
        {
            ClientReference = clientReference;
        }

        public void SetClientReference2(string clientReference2)
        {
            ClientReference2 = clientReference2;
        }

        public void SetBestBeforeDate(DateOn bestBeforeDate)
        {
            BestBeforeDate = bestBeforeDate;
        }

        public void SetDateFifo(DateOn dateFifo)
        {
            DateFifo = dateFifo;
        }

        public void SetCustomsDocument(string customsDocument)
        {
            CustomsDocument = customsDocument;
        }

        public void SetStorageStatus(string storageStatus)
        {
            StorageStatus = storageStatus;
        }

        public void SetStackheight(string stackheight)
        {
            Stackheight = stackheight;
        }

        public void SetLength(string length)
        {
            Length = length;
        }

        public void SetWidth(string width)
        {
            Width = width;
        }

        public void SetHeight(string height)
        {
            Height = height;
        }

        public void SetOriginalContainer(string originalContainer)
        {
            OriginalContainer = originalContainer;
        }

        public void SetQuantity(Quantity quantity)
        {
            Quantity = quantity;
        }

        public void SetWeightNet(Weight weightNet)
        {
            WeightNet = weightNet;
        }

        public void SetWeightGross(Weight weightGross)
        {
            WeightGross = weightGross;
        }
    }
}
