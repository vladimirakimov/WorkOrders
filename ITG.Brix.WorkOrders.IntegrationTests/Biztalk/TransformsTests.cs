using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Xml;

namespace ITG.Brix.WorkOrders.IntegrationTests.Biztalk
{
    [TestClass]
    public class TransformsTests
    {
        [TestMethod]
        public void PlatoToBiztalkShouldSucceed()
        {
            string xml = @"<?xml version=""1.0"" encoding =""UTF-8"" standalone =""no"" ?>
<FullTrpInbound xmlns=""urn:microsoft-dynamics-nav/xmlports/x68112"" >
  <Transport>
    <Source>BKAL33+KBT T</Source>
    <ID>781415</ID>
    <TransportNo>UEN000001</TransportNo>
    <RelationType>Inbound</RelationType>
    <Operation>Unload into warehouse</Operation>
    <UnitPlanning>WHT</UnitPlanning>
    <TypePlanning>MAG</TypePlanning>
    <Customer>DBPLASTICS</Customer>
    <CustomerReference1>EORDER TEST</CustomerReference1>
    <CustomerReference2>STANDARD</CustomerReference2>
    <CustomerReference3>CR3</CustomerReference3>
    <CustomerReference4>CR4</CustomerReference4>
    <CustomerReference5>CR5X</CustomerReference5>
    <LoadingReference>LR</LoadingReference>
    <ARDReference1>CR3</ARDReference1>
    <ARDReference2>CR4</ARDReference2>
    <ARDReference3>CR5X</ARDReference3>
    <ARDReference4>EORDERTEST</ARDReference4>
    <ARDReference5>STANDARD</ARDReference5>
    <ARDReference6>SVEN1231231</ARDReference6>
    <ARDReference7 />
    <ARDReference8 />
    <ARDReference9 />
    <ARDReference10 />
    <ProductionSite>CANADA01</ProductionSite>
    <LoadingPlace>0000201449</LoadingPlace>
    <DeliveryPlace>LB1227</DeliveryPlace>
    <BillOfLading>BOL</BillOfLading>
    <BLNetWeight>1050</BLNetWeight>
    <BLGrossWeight>1075</BLGrossWeight>
    <CertificateOfOrigin>4</CertificateOfOrigin>
    <CarrierBooked>LKW</CarrierBooked>
    <CarrierArrived>LKW</CarrierArrived>
    <Kind>40DCI-W</Kind>
    <TransportType>CP</TransportType>
    <DriverWaits>true</DriverWaits>
    <Driver>DRIVER</Driver>
    <LicensePlateTruck>TRUCK</LicensePlateTruck>
    <LicensePlateTrailer>TRAILER</LicensePlateTrailer>
    <Container>SVEN1231231</Container>
    <Railcar />
    <Seal1>SEAL1</Seal1>
    <Seal2>SEAL2</Seal2>
    <Seal3>SEAL3</Seal3>
    <NetWeightWeighbridge>2480</NetWeightWeighbridge>
    <GrossWeightWeighbridge>2500</GrossWeightWeighbridge>
    <FreeUntilOnTerminal>2018-03-23</FreeUntilOnTerminal>
    <FreeUntilOnTerminalCustomer>2018-03-27</FreeUntilOnTerminalCustomer>
    <ADR />
    <CustomsDocument>COMMUNITY</CustomsDocument>
    <DocumentNumber>4444</DocumentNumber>
    <DocumentOffice>VOSSELAAR</DocumentOffice>
    <DocumentDate>2018-03-21</DocumentDate>
    <ETA>2017-06-14T12:01:49Z</ETA>
    <LTA />
    <Arrived />
    <Site>LB1227</Site>
    <OperationalDepartment>SVEN</OperationalDepartment>
    <DockingZone />
    <ContainerLocation>1000</ContainerLocation>
    <ContainerStackLocation>1001</ContainerStackLocation>
    <EorderPriority>KTN</EorderPriority>
    <Dispatch>
      <LoadingDock />
      <DispatchPriority>90</DispatchPriority>
      <DispatchedTo />
      <DispatchComment />
    </Dispatch>
    <Zone>UNKNOWN_</Zone>
    <ProductOverview>HE3490LS</ProductOverview>
    <LotbatchOverview>3010171</LotbatchOverview>
    <OperationalRemark>
      <Comment>test operational</Comment>
    </OperationalRemark>
    <OperationalRemark>
      <Comment>test operational 2</Comment>
    </OperationalRemark>
    <OperationalRemark>
      <Comment>test operational 3</Comment>
    </OperationalRemark>
    <ExtraActivities>
      <ExtraActivity>
        <ID>369367</ID>
        <Activity>CLEAN</Activity>
        <Description>Clean goods</Description>
        <IsExcecuted>0</IsExcecuted>
        <Quantity>5</Quantity>
        <Remarks>test remark clean goods</Remarks>
      </ExtraActivity>
    </ExtraActivities>
    <ProductEntries>
      <ProductEntry>
        <EntryNo>13710289</EntryNo>
        <Customer>DBPLASTICS</Customer>
        <Arrival>AEN000767</Arrival>
        <Article>418595</Article>
        <ArticlePackagingCode>55</ArticlePackagingCode>
        <ProductCode>HE3490LS</ProductCode>
        <Product>
          <GTIN>5555</GTIN>
          <ProductType>PE-HD</ProductType>
          <MaterialType>0</MaterialType>
          <Color>WHITE</Color>
          <Shape>GRANULS</Shape>
        </Product>
        <Configuration>
          <Code>55BGEPE25KG-PRS4LS</Code>
          <Description>55 LOGO PE BAG(S) 25 KG ON PRS4 PALLET+SHRUNK</Description>
          <Quantity>55</Quantity>
          <ConfigurationUnit>
            <UnitType>BGEPE</UnitType>
            <NetPerUnit>25</NetPerUnit>
            <NetPerUnitAlwaysDifferent>0</NetPerUnitAlwaysDifferent>
          </ConfigurationUnit>
        </Configuration>
        <LotBatch>3010171</LotBatch>
        <LotBatch2 />
        <ClientReference />
        <BestBeforeDate />
        <DateFifo />
        <PalletNo />
        <SSCCNo />
        <CustomsDocument>COMMUNITY</CustomsDocument>
        <StorageStatus>STORAGE</StorageStatus>
        <StackHeight>2</StackHeight>
        <Length>110</Length>
        <Width>130</Width>
        <Height>150</Height>
        <OriginalContainer>SVEN1231231</OriginalContainer>
        <IsPartial>0</IsPartial>
        <IsMixed>0</IsMixed>
        <MixedID>0</MixedID>
        <MixedPalletNo />
        <Quantity>550</Quantity>
        <QuantitySHU>10</QuantitySHU>
        <NetWeight>13750</NetWeight>
        <GrossWeight>14140.5</GrossWeight>
        <Location>
          <Warehouse>BLOK DB</Warehouse>
          <Gate>DBA</Gate>
          <Row>T01</Row>
          <Position>-</Position>
        </Location>
        <ProductRemarks>
          <ProductRemark>
            <ProductComment>product remark 1</ProductComment>
          </ProductRemark>
          <ProductRemark>
            <ProductComment>product remark 2</ProductComment>
          </ProductRemark>
        </ProductRemarks>
        <SafetyRemarks>
          <SafetyRemark>
            <SafetyComment>safety remark 1</SafetyComment>
          </SafetyRemark>
          <SafetyRemark>
            <SafetyComment>safety remark 2</SafetyComment>
          </SafetyRemark>
        </SafetyRemarks>
        <Notes>
          <Note>
            <NoteComment />
          </Note>
        </Notes>
      </ProductEntry>
    </ProductEntries>
  </Transport>
  <ErrorMessages>
    <ErrorMessage />
  </ErrorMessages>
</FullTrpInbound>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            var json = JsonConvert.SerializeXmlNode(doc);
        }
    }
}
