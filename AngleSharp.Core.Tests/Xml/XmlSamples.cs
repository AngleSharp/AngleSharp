namespace AngleSharp.Core.Tests.Xml
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System.Linq;

    /// <summary>
    /// Parsing the sample XML files taken from
    /// http://msdn.microsoft.com/en-us/library/bb387026.aspx
    /// Additional (maybe):
    /// http://tantek.com/XHTML/Test/minimal.html
    /// </summary>
    [TestFixture]
    public class XmlSamples
    {
        [Test]
        public void XmlTypicalPurchaseOrder()
        {
            var document = (@"<?xml version=""1.0""?>
<PurchaseOrder PurchaseOrderNumber=""99503"" OrderDate=""1999-10-20"">
  <Address Type=""Shipping"">
    <Name>Ellen Adams</Name>
    <Street>123 Maple Street</Street>
    <City>Mill Valley</City>
    <State>CA</State>
    <Zip>10999</Zip>
    <Country>USA</Country>
  </Address>
  <Address Type=""Billing"">
    <Name>Tai Yee</Name>
    <Street>8 Oak Avenue</Street>
    <City>Old Town</City>
    <State>PA</State>
    <Zip>95819</Zip>
    <Country>USA</Country>
  </Address>
  <DeliveryNotes>Please leave packages in shed by driveway.</DeliveryNotes>
  <Items>
    <Item PartNumber=""872-AA"">
      <ProductName>Lawnmower</ProductName>
      <Quantity>1</Quantity>
      <USPrice>148.95</USPrice>
      <Comment>Confirm this is electric</Comment>
    </Item>
    <Item PartNumber=""926-AA"">
      <ProductName>Baby Monitor</ProductName>
      <Quantity>2</Quantity>
      <USPrice>39.98</USPrice>
      <ShipDate>1999-05-21</ShipDate>
    </Item>
  </Items>
</PurchaseOrder>").ToXmlDocument();

            var documentPurchaseOrder1 = document.ChildNodes[0] as IElement;
            Assert.AreEqual(9, documentPurchaseOrder1.ChildNodes.Length);
            Assert.AreEqual(2, documentPurchaseOrder1.Attributes.Count());
            Assert.AreEqual("PurchaseOrder", documentPurchaseOrder1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1.NodeType);
            Assert.IsNotNull(documentPurchaseOrder1.GetAttribute("PurchaseOrderNumber"));
            Assert.AreEqual("99503", documentPurchaseOrder1.GetAttribute("PurchaseOrderNumber"));
            Assert.AreEqual("1999-10-20", documentPurchaseOrder1.GetAttribute("OrderDate"));

            var documentPurchaseOrder1Text0 = documentPurchaseOrder1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Text0.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrder1Text0.TextContent);

            var documentPurchaseOrder1Address1 = documentPurchaseOrder1.ChildNodes[1] as IElement;
            Assert.AreEqual(13, documentPurchaseOrder1Address1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrder1Address1.Attributes.Count());
            Assert.AreEqual("Address", documentPurchaseOrder1Address1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1.NodeType);
            Assert.AreEqual("Shipping", documentPurchaseOrder1Address1.GetAttribute("Type"));

            var documentPurchaseOrder1Address1Text0 = documentPurchaseOrder1Address1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text0.TextContent);

            var documentPurchaseOrder1Address1Name1 = documentPurchaseOrder1Address1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1Name1.Attributes.Count());
            Assert.AreEqual("Name", documentPurchaseOrder1Address1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1Name1.NodeType);

            var documentPurchaseOrder1Address1Name1Text0 = documentPurchaseOrder1Address1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Name1Text0.NodeType);
            Assert.AreEqual("Ellen Adams", documentPurchaseOrder1Address1Name1Text0.TextContent);

            var documentPurchaseOrder1Address1Text2 = documentPurchaseOrder1Address1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text2.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text2.TextContent);

            var documentPurchaseOrder1Address1Street3 = documentPurchaseOrder1Address1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address1Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1Street3.Attributes.Count());
            Assert.AreEqual("Street", documentPurchaseOrder1Address1Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1Street3.NodeType);

            var documentPurchaseOrder1Address1Street3Text0 = documentPurchaseOrder1Address1Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Street3Text0.NodeType);
            Assert.AreEqual("123 Maple Street", documentPurchaseOrder1Address1Street3Text0.TextContent);

            var documentPurchaseOrder1Address1Text4 = documentPurchaseOrder1Address1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text4.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text4.TextContent);

            var documentPurchaseOrder1Address1City5 = documentPurchaseOrder1Address1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address1City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1City5.Attributes.Count());
            Assert.AreEqual("City", documentPurchaseOrder1Address1City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1City5.NodeType);

            var documentPurchaseOrder1Address1City5Text0 = documentPurchaseOrder1Address1City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1City5Text0.NodeType);
            Assert.AreEqual("Mill Valley", documentPurchaseOrder1Address1City5Text0.TextContent);

            var documentPurchaseOrder1Address1Text6 = documentPurchaseOrder1Address1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text6.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text6.TextContent);

            var documentPurchaseOrder1Address1State7 = documentPurchaseOrder1Address1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address1State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1State7.Attributes.Count());
            Assert.AreEqual("State", documentPurchaseOrder1Address1State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1State7.NodeType);

            var documentPurchaseOrder1Address1State7Text0 = documentPurchaseOrder1Address1State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1State7Text0.NodeType);
            Assert.AreEqual("CA", documentPurchaseOrder1Address1State7Text0.TextContent);

            var documentPurchaseOrder1Address1Text8 = documentPurchaseOrder1Address1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text8.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text8.TextContent);

            var documentPurchaseOrder1Address1Zip9 = documentPurchaseOrder1Address1.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address1Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1Zip9.Attributes.Count());
            Assert.AreEqual("Zip", documentPurchaseOrder1Address1Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1Zip9.NodeType);

            var documentPurchaseOrder1Address1Zip9Text0 = documentPurchaseOrder1Address1Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Zip9Text0.NodeType);
            Assert.AreEqual("10999", documentPurchaseOrder1Address1Zip9Text0.TextContent);

            var documentPurchaseOrder1Address1Text10 = documentPurchaseOrder1Address1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text10.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text10.TextContent);

            var documentPurchaseOrder1Address1Country11 = documentPurchaseOrder1Address1.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address1Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1Country11.Attributes.Count());
            Assert.AreEqual("Country", documentPurchaseOrder1Address1Country11.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1Country11.NodeType);

            var documentPurchaseOrder1Address1Country11Text0 = documentPurchaseOrder1Address1Country11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Country11Text0.NodeType);
            Assert.AreEqual("USA", documentPurchaseOrder1Address1Country11Text0.TextContent);

            var documentPurchaseOrder1Address1Text12 = documentPurchaseOrder1Address1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text12.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrder1Address1Text12.TextContent);

            var documentPurchaseOrder1Text2 = documentPurchaseOrder1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Text2.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrder1Text2.TextContent);

            var documentPurchaseOrder1Address3 = documentPurchaseOrder1.ChildNodes[3] as IElement;
            Assert.AreEqual(13, documentPurchaseOrder1Address3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrder1Address3.Attributes.Count());
            Assert.AreEqual("Address", documentPurchaseOrder1Address3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3.NodeType);
            Assert.AreEqual("Billing", documentPurchaseOrder1Address3.GetAttribute("Type"));

            var documentPurchaseOrder1Address3Text0 = documentPurchaseOrder1Address3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text0.TextContent);

            var documentPurchaseOrder1Address3Name1 = documentPurchaseOrder1Address3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3Name1.Attributes.Count());
            Assert.AreEqual("Name", documentPurchaseOrder1Address3Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3Name1.NodeType);

            var documentPurchaseOrder1Address3Name1Text0 = documentPurchaseOrder1Address3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Name1Text0.NodeType);
            Assert.AreEqual("Tai Yee", documentPurchaseOrder1Address3Name1Text0.TextContent);

            var documentPurchaseOrder1Address3Text2 = documentPurchaseOrder1Address3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text2.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text2.TextContent);

            var documentPurchaseOrder1Address3Street3 = documentPurchaseOrder1Address3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address3Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3Street3.Attributes.Count());
            Assert.AreEqual("Street", documentPurchaseOrder1Address3Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3Street3.NodeType);

            var documentPurchaseOrder1Address3Street3Text0 = documentPurchaseOrder1Address3Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Street3Text0.NodeType);
            Assert.AreEqual("8 Oak Avenue", documentPurchaseOrder1Address3Street3Text0.TextContent);

            var documentPurchaseOrder1Address3Text4 = documentPurchaseOrder1Address3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text4.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text4.TextContent);

            var documentPurchaseOrder1Address3City5 = documentPurchaseOrder1Address3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address3City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3City5.Attributes.Count());
            Assert.AreEqual("City", documentPurchaseOrder1Address3City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3City5.NodeType);

            var documentPurchaseOrder1Address3City5Text0 = documentPurchaseOrder1Address3City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3City5Text0.NodeType);
            Assert.AreEqual("Old Town", documentPurchaseOrder1Address3City5Text0.TextContent);

            var documentPurchaseOrder1Address3Text6 = documentPurchaseOrder1Address3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text6.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text6.TextContent);

            var documentPurchaseOrder1Address3State7 = documentPurchaseOrder1Address3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address3State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3State7.Attributes.Count());
            Assert.AreEqual("State", documentPurchaseOrder1Address3State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3State7.NodeType);

            var documentPurchaseOrder1Address3State7Text0 = documentPurchaseOrder1Address3State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3State7Text0.NodeType);
            Assert.AreEqual("PA", documentPurchaseOrder1Address3State7Text0.TextContent);

            var documentPurchaseOrder1Address3Text8 = documentPurchaseOrder1Address3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text8.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text8.TextContent);

            var documentPurchaseOrder1Address3Zip9 = documentPurchaseOrder1Address3.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address3Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3Zip9.Attributes.Count());
            Assert.AreEqual("Zip", documentPurchaseOrder1Address3Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3Zip9.NodeType);

            var documentPurchaseOrder1Address3Zip9Text0 = documentPurchaseOrder1Address3Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Zip9Text0.NodeType);
            Assert.AreEqual("95819", documentPurchaseOrder1Address3Zip9Text0.TextContent);

            var documentPurchaseOrder1Address3Text10 = documentPurchaseOrder1Address3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text10.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text10.TextContent);

            var documentPurchaseOrder1Address3Country11 = documentPurchaseOrder1Address3.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Address3Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3Country11.Attributes.Count());
            Assert.AreEqual("Country", documentPurchaseOrder1Address3Country11.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3Country11.NodeType);

            var documentPurchaseOrder1Address3Country11Text0 = documentPurchaseOrder1Address3Country11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Country11Text0.NodeType);
            Assert.AreEqual("USA", documentPurchaseOrder1Address3Country11Text0.TextContent);

            var documentPurchaseOrder1Address3Text12 = documentPurchaseOrder1Address3.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text12.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrder1Address3Text12.TextContent);

            var documentPurchaseOrder1Text4 = documentPurchaseOrder1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Text4.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrder1Text4.TextContent);

            var documentPurchaseOrder1DeliveryNotes5 = documentPurchaseOrder1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1DeliveryNotes5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1DeliveryNotes5.Attributes.Count());
            Assert.AreEqual("DeliveryNotes", documentPurchaseOrder1DeliveryNotes5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1DeliveryNotes5.NodeType);

            var documentPurchaseOrder1DeliveryNotes5Text0 = documentPurchaseOrder1DeliveryNotes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1DeliveryNotes5Text0.NodeType);
            Assert.AreEqual("Please leave packages in shed by driveway.", documentPurchaseOrder1DeliveryNotes5Text0.TextContent);

            var documentPurchaseOrder1Text6 = documentPurchaseOrder1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Text6.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrder1Text6.TextContent);

            var documentPurchaseOrder1Items7 = documentPurchaseOrder1.ChildNodes[7] as IElement;
            Assert.AreEqual(5, documentPurchaseOrder1Items7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7.Attributes.Count());
            Assert.AreEqual("Items", documentPurchaseOrder1Items7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7.NodeType);

            var documentPurchaseOrder1Items7Text0 = documentPurchaseOrder1Items7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Items7Text0.TextContent);

            var documentPurchaseOrder1Items7Item1 = documentPurchaseOrder1Items7.ChildNodes[1] as IElement;
            Assert.AreEqual(9, documentPurchaseOrder1Items7Item1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1.Attributes.Count());
            Assert.AreEqual("Item", documentPurchaseOrder1Items7Item1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item1.NodeType);
            Assert.AreEqual("872-AA", documentPurchaseOrder1Items7Item1.GetAttribute("PartNumber"));

            var documentPurchaseOrder1Items7Item1Text0 = documentPurchaseOrder1Items7Item1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item1Text0.TextContent);

            var documentPurchaseOrder1Items7Item1ProductName1 = documentPurchaseOrder1Items7Item1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item1ProductName1.Attributes.Count());
            Assert.AreEqual("ProductName", documentPurchaseOrder1Items7Item1ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item1ProductName1.NodeType);

            var documentPurchaseOrder1Items7Item1ProductName1Text0 = documentPurchaseOrder1Items7Item1ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1ProductName1Text0.NodeType);
            Assert.AreEqual("Lawnmower", documentPurchaseOrder1Items7Item1ProductName1Text0.TextContent);

            var documentPurchaseOrder1Items7Item1Text2 = documentPurchaseOrder1Items7Item1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item1Text2.TextContent);

            var documentPurchaseOrder1Items7Item1Quantity3 = documentPurchaseOrder1Items7Item1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item1Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentPurchaseOrder1Items7Item1Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item1Quantity3.NodeType);

            var documentPurchaseOrder1Items7Item1Quantity3Text0 = documentPurchaseOrder1Items7Item1Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrder1Items7Item1Quantity3Text0.TextContent);

            var documentPurchaseOrder1Items7Item1Text4 = documentPurchaseOrder1Items7Item1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item1Text4.TextContent);

            var documentPurchaseOrder1Items7Item1USPrice5 = documentPurchaseOrder1Items7Item1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item1USPrice5.Attributes.Count());
            Assert.AreEqual("USPrice", documentPurchaseOrder1Items7Item1USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item1USPrice5.NodeType);

            var documentPurchaseOrder1Items7Item1USPrice5Text0 = documentPurchaseOrder1Items7Item1USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1USPrice5Text0.NodeType);
            Assert.AreEqual("148.95", documentPurchaseOrder1Items7Item1USPrice5Text0.TextContent);

            var documentPurchaseOrder1Items7Item1Text6 = documentPurchaseOrder1Items7Item1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item1Text6.TextContent);

            var documentPurchaseOrder1Items7Item1Comment7 = documentPurchaseOrder1Items7Item1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1Comment7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item1Comment7.Attributes.Count());
            Assert.AreEqual("Comment", documentPurchaseOrder1Items7Item1Comment7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item1Comment7.NodeType);

            var documentPurchaseOrder1Items7Item1Comment7Text0 = documentPurchaseOrder1Items7Item1Comment7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Comment7Text0.NodeType);
            Assert.AreEqual("Confirm this is electric", documentPurchaseOrder1Items7Item1Comment7Text0.TextContent);

            var documentPurchaseOrder1Items7Item1Text8 = documentPurchaseOrder1Items7Item1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Text8.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Items7Item1Text8.TextContent);

            var documentPurchaseOrder1Items7Text2 = documentPurchaseOrder1Items7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Text2.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Items7Text2.TextContent);

            var documentPurchaseOrder1Items7Item3 = documentPurchaseOrder1Items7.ChildNodes[3] as IElement;
            Assert.AreEqual(9, documentPurchaseOrder1Items7Item3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3.Attributes.Count());
            Assert.AreEqual("Item", documentPurchaseOrder1Items7Item3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item3.NodeType);
            Assert.IsNotNull(documentPurchaseOrder1Items7Item3.GetAttribute("PartNumber"));
            Assert.AreEqual("926-AA", documentPurchaseOrder1Items7Item3.GetAttribute("PartNumber"));

            var documentPurchaseOrder1Items7Item3Text0 = documentPurchaseOrder1Items7Item3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item3Text0.TextContent);

            var documentPurchaseOrder1Items7Item3ProductName1 = documentPurchaseOrder1Items7Item3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item3ProductName1.Attributes.Count());
            Assert.AreEqual("ProductName", documentPurchaseOrder1Items7Item3ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item3ProductName1.NodeType);

            var documentPurchaseOrder1Items7Item3ProductName1Text0 = documentPurchaseOrder1Items7Item3ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3ProductName1Text0.NodeType);
            Assert.AreEqual("Baby Monitor", documentPurchaseOrder1Items7Item3ProductName1Text0.TextContent);

            var documentPurchaseOrder1Items7Item3Text2 = documentPurchaseOrder1Items7Item3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item3Text2.TextContent);

            var documentPurchaseOrder1Items7Item3Quantity3 = documentPurchaseOrder1Items7Item3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item3Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentPurchaseOrder1Items7Item3Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item3Quantity3.NodeType);

            var documentPurchaseOrder1Items7Item3Quantity3Text0 = documentPurchaseOrder1Items7Item3Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Quantity3Text0.NodeType);
            Assert.AreEqual("2", documentPurchaseOrder1Items7Item3Quantity3Text0.TextContent);

            var documentPurchaseOrder1Items7Item3Text4 = documentPurchaseOrder1Items7Item3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item3Text4.TextContent);

            var documentPurchaseOrder1Items7Item3USPrice5 = documentPurchaseOrder1Items7Item3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item3USPrice5.Attributes.Count());
            Assert.AreEqual("USPrice", documentPurchaseOrder1Items7Item3USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item3USPrice5.NodeType);

            var documentPurchaseOrder1Items7Item3USPrice5Text0 = documentPurchaseOrder1Items7Item3USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3USPrice5Text0.NodeType);
            Assert.AreEqual("39.98", documentPurchaseOrder1Items7Item3USPrice5Text0.TextContent);

            var documentPurchaseOrder1Items7Item3Text6 = documentPurchaseOrder1Items7Item3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item3Text6.TextContent);

            var documentPurchaseOrder1Items7Item3ShipDate7 = documentPurchaseOrder1Items7Item3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3ShipDate7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item3ShipDate7.Attributes.Count());
            Assert.AreEqual("ShipDate", documentPurchaseOrder1Items7Item3ShipDate7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item3ShipDate7.NodeType);

            var documentPurchaseOrder1Items7Item3ShipDate7Text0 = documentPurchaseOrder1Items7Item3ShipDate7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3ShipDate7Text0.NodeType);
            Assert.AreEqual("1999-05-21", documentPurchaseOrder1Items7Item3ShipDate7Text0.TextContent);

            var documentPurchaseOrder1Items7Item3Text8 = documentPurchaseOrder1Items7Item3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Text8.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Items7Item3Text8.TextContent);

            var documentPurchaseOrder1Items7Text4 = documentPurchaseOrder1Items7.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Text4.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrder1Items7Text4.TextContent);

            var documentPurchaseOrder1Text8 = documentPurchaseOrder1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Text8.NodeType);
            Assert.AreEqual("\n", documentPurchaseOrder1Text8.TextContent);
        }

        [Test]
        public void XmlTypicalPurchaseOrderInNamespace()
        {
            var document = (@"<?xml version=""1.0""?>
<aw:PurchaseOrder
    aw:PurchaseOrderNumber=""99503""
    aw:OrderDate=""1999-10-20""
    xmlns:aw=""http://www.adventure-works.com"">
  <aw:Address aw:Type=""Shipping"">
    <aw:Name>Ellen Adams</aw:Name>
    <aw:Street>123 Maple Street</aw:Street>
    <aw:City>Mill Valley</aw:City>
    <aw:State>CA</aw:State>
    <aw:Zip>10999</aw:Zip>
    <aw:Country>USA</aw:Country>
  </aw:Address>
  <aw:Address aw:Type=""Billing"">
    <aw:Name>Tai Yee</aw:Name>
    <aw:Street>8 Oak Avenue</aw:Street>
    <aw:City>Old Town</aw:City>
    <aw:State>PA</aw:State>
    <aw:Zip>95819</aw:Zip>
    <aw:Country>USA</aw:Country>
  </aw:Address>
  <aw:DeliveryNotes>Please leave packages in shed by driveway.</aw:DeliveryNotes>
  <aw:Items>
    <aw:Item aw:PartNumber=""872-AA"">
      <aw:ProductName>Lawnmower</aw:ProductName>
      <aw:Quantity>1</aw:Quantity>
      <aw:USPrice>148.95</aw:USPrice>
      <aw:Comment>Confirm this is electric</aw:Comment>
    </aw:Item>
    <aw:Item aw:PartNumber=""926-AA"">
      <aw:ProductName>Baby Monitor</aw:ProductName>
      <aw:Quantity>2</aw:Quantity>
      <aw:USPrice>39.98</aw:USPrice>
      <aw:ShipDate>1999-05-21</aw:ShipDate>
    </aw:Item>
  </aw:Items>
</aw:PurchaseOrder>").ToXmlDocument();

            var documentawPurchaseOrder0 = document.ChildNodes[0] as IElement;
            Assert.AreEqual(9, documentawPurchaseOrder0.ChildNodes.Length);
            Assert.AreEqual(3, documentawPurchaseOrder0.Attributes.Count());
            Assert.AreEqual("aw:PurchaseOrder", documentawPurchaseOrder0.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0.NodeType);
            
            Assert.AreEqual("99503", documentawPurchaseOrder0.GetAttribute("aw:PurchaseOrderNumber"));
            Assert.AreEqual("1999-10-20", documentawPurchaseOrder0.GetAttribute("aw:OrderDate"));
            Assert.AreEqual("http://www.adventure-works.com", documentawPurchaseOrder0.GetAttribute("xmlns:aw"));

            var documentawPurchaseOrder0Text0 = documentawPurchaseOrder0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0Text0.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrder0Text0.TextContent);

            var documentawPurchaseOrder0awAddress1 = documentawPurchaseOrder0.ChildNodes[1] as IElement;
            Assert.AreEqual(13, documentawPurchaseOrder0awAddress1.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress1.Attributes.Count());
            Assert.AreEqual("aw:Address", documentawPurchaseOrder0awAddress1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress1.NodeType);
            
            Assert.AreEqual("Shipping", documentawPurchaseOrder0awAddress1.GetAttribute("aw:Type"));

            var documentawPurchaseOrder0awAddress1Text0 = documentawPurchaseOrder0awAddress1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1Text0.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress1Text0.TextContent);

            var documentawPurchaseOrder0awAddress1awName1 = documentawPurchaseOrder0awAddress1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress1awName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress1awName1.Attributes.Count());
            Assert.AreEqual("aw:Name", documentawPurchaseOrder0awAddress1awName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress1awName1.NodeType);

            var documentawPurchaseOrder0awAddress1awName1Text0 = documentawPurchaseOrder0awAddress1awName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1awName1Text0.NodeType);
            Assert.AreEqual("Ellen Adams", documentawPurchaseOrder0awAddress1awName1Text0.TextContent);

            var documentawPurchaseOrder0awAddress1Text2 = documentawPurchaseOrder0awAddress1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1Text2.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress1Text2.TextContent);

            var documentawPurchaseOrder0awAddress1awStreet3 = documentawPurchaseOrder0awAddress1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress1awStreet3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress1awStreet3.Attributes.Count());
            Assert.AreEqual("aw:Street", documentawPurchaseOrder0awAddress1awStreet3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress1awStreet3.NodeType);

            var documentawPurchaseOrder0awAddress1awStreet3Text0 = documentawPurchaseOrder0awAddress1awStreet3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1awStreet3Text0.NodeType);
            Assert.AreEqual("123 Maple Street", documentawPurchaseOrder0awAddress1awStreet3Text0.TextContent);

            var documentawPurchaseOrder0awAddress1Text4 = documentawPurchaseOrder0awAddress1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1Text4.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress1Text4.TextContent);

            var documentawPurchaseOrder0awAddress1awCity5 = documentawPurchaseOrder0awAddress1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress1awCity5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress1awCity5.Attributes.Count());
            Assert.AreEqual("aw:City", documentawPurchaseOrder0awAddress1awCity5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress1awCity5.NodeType);

            var documentawPurchaseOrder0awAddress1awCity5Text0 = documentawPurchaseOrder0awAddress1awCity5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1awCity5Text0.NodeType);
            Assert.AreEqual("Mill Valley", documentawPurchaseOrder0awAddress1awCity5Text0.TextContent);

            var documentawPurchaseOrder0awAddress1Text6 = documentawPurchaseOrder0awAddress1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1Text6.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress1Text6.TextContent);

            var documentawPurchaseOrder0awAddress1awState7 = documentawPurchaseOrder0awAddress1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress1awState7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress1awState7.Attributes.Count());
            Assert.AreEqual("aw:State", documentawPurchaseOrder0awAddress1awState7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress1awState7.NodeType);

            var documentawPurchaseOrder0awAddress1awState7Text0 = documentawPurchaseOrder0awAddress1awState7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1awState7Text0.NodeType);
            Assert.AreEqual("CA", documentawPurchaseOrder0awAddress1awState7Text0.TextContent);

            var documentawPurchaseOrder0awAddress1Text8 = documentawPurchaseOrder0awAddress1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1Text8.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress1Text8.TextContent);

            var documentawPurchaseOrder0awAddress1awZip9 = documentawPurchaseOrder0awAddress1.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress1awZip9.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress1awZip9.Attributes.Count());
            Assert.AreEqual("aw:Zip", documentawPurchaseOrder0awAddress1awZip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress1awZip9.NodeType);

            var documentawPurchaseOrder0awAddress1awZip9Text0 = documentawPurchaseOrder0awAddress1awZip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1awZip9Text0.NodeType);
            Assert.AreEqual("10999", documentawPurchaseOrder0awAddress1awZip9Text0.TextContent);

            var documentawPurchaseOrder0awAddress1Text10 = documentawPurchaseOrder0awAddress1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1Text10.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress1Text10.TextContent);

            var documentawPurchaseOrder0awAddress1awCountry11 = documentawPurchaseOrder0awAddress1.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress1awCountry11.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress1awCountry11.Attributes.Count());
            Assert.AreEqual("aw:Country", documentawPurchaseOrder0awAddress1awCountry11.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress1awCountry11.NodeType);

            var documentawPurchaseOrder0awAddress1awCountry11Text0 = documentawPurchaseOrder0awAddress1awCountry11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1awCountry11Text0.NodeType);
            Assert.AreEqual("USA", documentawPurchaseOrder0awAddress1awCountry11Text0.TextContent);

            var documentawPurchaseOrder0awAddress1Text12 = documentawPurchaseOrder0awAddress1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress1Text12.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrder0awAddress1Text12.TextContent);

            var documentawPurchaseOrder0Text2 = documentawPurchaseOrder0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0Text2.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrder0Text2.TextContent);

            var documentawPurchaseOrder0awAddress3 = documentawPurchaseOrder0.ChildNodes[3] as IElement;
            Assert.AreEqual(13, documentawPurchaseOrder0awAddress3.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress3.Attributes.Count());
            Assert.AreEqual("aw:Address", documentawPurchaseOrder0awAddress3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress3.NodeType);
            
            Assert.AreEqual("Billing", documentawPurchaseOrder0awAddress3.GetAttribute("aw:Type"));

            var documentawPurchaseOrder0awAddress3Text0 = documentawPurchaseOrder0awAddress3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3Text0.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress3Text0.TextContent);

            var documentawPurchaseOrder0awAddress3awName1 = documentawPurchaseOrder0awAddress3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress3awName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress3awName1.Attributes.Count());
            Assert.AreEqual("aw:Name", documentawPurchaseOrder0awAddress3awName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress3awName1.NodeType);

            var documentawPurchaseOrder0awAddress3awName1Text0 = documentawPurchaseOrder0awAddress3awName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3awName1Text0.NodeType);
            Assert.AreEqual("Tai Yee", documentawPurchaseOrder0awAddress3awName1Text0.TextContent);

            var documentawPurchaseOrder0awAddress3Text2 = documentawPurchaseOrder0awAddress3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3Text2.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress3Text2.TextContent);

            var documentawPurchaseOrder0awAddress3awStreet3 = documentawPurchaseOrder0awAddress3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress3awStreet3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress3awStreet3.Attributes.Count());
            Assert.AreEqual("aw:Street", documentawPurchaseOrder0awAddress3awStreet3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress3awStreet3.NodeType);

            var documentawPurchaseOrder0awAddress3awStreet3Text0 = documentawPurchaseOrder0awAddress3awStreet3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3awStreet3Text0.NodeType);
            Assert.AreEqual("8 Oak Avenue", documentawPurchaseOrder0awAddress3awStreet3Text0.TextContent);

            var documentawPurchaseOrder0awAddress3Text4 = documentawPurchaseOrder0awAddress3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3Text4.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress3Text4.TextContent);

            var documentawPurchaseOrder0awAddress3awCity5 = documentawPurchaseOrder0awAddress3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress3awCity5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress3awCity5.Attributes.Count());
            Assert.AreEqual("aw:City", documentawPurchaseOrder0awAddress3awCity5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress3awCity5.NodeType);

            var documentawPurchaseOrder0awAddress3awCity5Text0 = documentawPurchaseOrder0awAddress3awCity5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3awCity5Text0.NodeType);
            Assert.AreEqual("Old Town", documentawPurchaseOrder0awAddress3awCity5Text0.TextContent);

            var documentawPurchaseOrder0awAddress3Text6 = documentawPurchaseOrder0awAddress3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3Text6.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress3Text6.TextContent);

            var documentawPurchaseOrder0awAddress3awState7 = documentawPurchaseOrder0awAddress3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress3awState7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress3awState7.Attributes.Count());
            Assert.AreEqual("aw:State", documentawPurchaseOrder0awAddress3awState7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress3awState7.NodeType);

            var documentawPurchaseOrder0awAddress3awState7Text0 = documentawPurchaseOrder0awAddress3awState7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3awState7Text0.NodeType);
            Assert.AreEqual("PA", documentawPurchaseOrder0awAddress3awState7Text0.TextContent);

            var documentawPurchaseOrder0awAddress3Text8 = documentawPurchaseOrder0awAddress3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3Text8.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress3Text8.TextContent);

            var documentawPurchaseOrder0awAddress3awZip9 = documentawPurchaseOrder0awAddress3.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress3awZip9.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress3awZip9.Attributes.Count());
            Assert.AreEqual("aw:Zip", documentawPurchaseOrder0awAddress3awZip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress3awZip9.NodeType);

            var documentawPurchaseOrder0awAddress3awZip9Text0 = documentawPurchaseOrder0awAddress3awZip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3awZip9Text0.NodeType);
            Assert.AreEqual("95819", documentawPurchaseOrder0awAddress3awZip9Text0.TextContent);

            var documentawPurchaseOrder0awAddress3Text10 = documentawPurchaseOrder0awAddress3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3Text10.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awAddress3Text10.TextContent);

            var documentawPurchaseOrder0awAddress3awCountry11 = documentawPurchaseOrder0awAddress3.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awAddress3awCountry11.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awAddress3awCountry11.Attributes.Count());
            Assert.AreEqual("aw:Country", documentawPurchaseOrder0awAddress3awCountry11.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awAddress3awCountry11.NodeType);

            var documentawPurchaseOrder0awAddress3awCountry11Text0 = documentawPurchaseOrder0awAddress3awCountry11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3awCountry11Text0.NodeType);
            Assert.AreEqual("USA", documentawPurchaseOrder0awAddress3awCountry11Text0.TextContent);

            var documentawPurchaseOrder0awAddress3Text12 = documentawPurchaseOrder0awAddress3.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awAddress3Text12.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrder0awAddress3Text12.TextContent);

            var documentawPurchaseOrder0Text4 = documentawPurchaseOrder0.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0Text4.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrder0Text4.TextContent);

            var documentawPurchaseOrder0awDeliveryNotes5 = documentawPurchaseOrder0.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awDeliveryNotes5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awDeliveryNotes5.Attributes.Count());
            Assert.AreEqual("aw:DeliveryNotes", documentawPurchaseOrder0awDeliveryNotes5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awDeliveryNotes5.NodeType);

            var documentawPurchaseOrder0awDeliveryNotes5Text0 = documentawPurchaseOrder0awDeliveryNotes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awDeliveryNotes5Text0.NodeType);
            Assert.AreEqual("Please leave packages in shed by driveway.", documentawPurchaseOrder0awDeliveryNotes5Text0.TextContent);

            var documentawPurchaseOrder0Text6 = documentawPurchaseOrder0.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0Text6.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrder0Text6.TextContent);

            var documentawPurchaseOrder0awItems7 = documentawPurchaseOrder0.ChildNodes[7] as IElement;
            Assert.AreEqual(5, documentawPurchaseOrder0awItems7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awItems7.Attributes.Count());
            Assert.AreEqual("aw:Items", documentawPurchaseOrder0awItems7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7.NodeType);

            var documentawPurchaseOrder0awItems7Text0 = documentawPurchaseOrder0awItems7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7Text0.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awItems7Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem1 = documentawPurchaseOrder0awItems7.ChildNodes[1] as IElement;
            Assert.AreEqual(9, documentawPurchaseOrder0awItems7awItem1.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem1.Attributes.Count());
            Assert.AreEqual("aw:Item", documentawPurchaseOrder0awItems7awItem1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem1.NodeType);
            
            Assert.AreEqual("872-AA", documentawPurchaseOrder0awItems7awItem1.GetAttribute("aw:PartNumber"));

            var documentawPurchaseOrder0awItems7awItem1Text0 = documentawPurchaseOrder0awItems7awItem1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem1Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrder0awItems7awItem1Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem1awProductName1 = documentawPurchaseOrder0awItems7awItem1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem1awProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awItems7awItem1awProductName1.Attributes.Count());
            Assert.AreEqual("aw:ProductName", documentawPurchaseOrder0awItems7awItem1awProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem1awProductName1.NodeType);

            var documentawPurchaseOrder0awItems7awItem1awProductName1Text0 = documentawPurchaseOrder0awItems7awItem1awProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem1awProductName1Text0.NodeType);
            Assert.AreEqual("Lawnmower", documentawPurchaseOrder0awItems7awItem1awProductName1Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem1Text2 = documentawPurchaseOrder0awItems7awItem1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem1Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrder0awItems7awItem1Text2.TextContent);

            var documentawPurchaseOrder0awItems7awItem1awQuantity3 = documentawPurchaseOrder0awItems7awItem1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem1awQuantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awItems7awItem1awQuantity3.Attributes.Count());
            Assert.AreEqual("aw:Quantity", documentawPurchaseOrder0awItems7awItem1awQuantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem1awQuantity3.NodeType);

            var documentawPurchaseOrder0awItems7awItem1awQuantity3Text0 = documentawPurchaseOrder0awItems7awItem1awQuantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem1awQuantity3Text0.NodeType);
            Assert.AreEqual("1", documentawPurchaseOrder0awItems7awItem1awQuantity3Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem1Text4 = documentawPurchaseOrder0awItems7awItem1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem1Text4.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrder0awItems7awItem1Text4.TextContent);

            var documentawPurchaseOrder0awItems7awItem1awUSPrice5 = documentawPurchaseOrder0awItems7awItem1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem1awUSPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awItems7awItem1awUSPrice5.Attributes.Count());
            Assert.AreEqual("aw:USPrice", documentawPurchaseOrder0awItems7awItem1awUSPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem1awUSPrice5.NodeType);

            var documentawPurchaseOrder0awItems7awItem1awUSPrice5Text0 = documentawPurchaseOrder0awItems7awItem1awUSPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem1awUSPrice5Text0.NodeType);
            Assert.AreEqual("148.95", documentawPurchaseOrder0awItems7awItem1awUSPrice5Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem1Text6 = documentawPurchaseOrder0awItems7awItem1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem1Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrder0awItems7awItem1Text6.TextContent);

            var documentawPurchaseOrder0awItems7awItem1awComment7 = documentawPurchaseOrder0awItems7awItem1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem1awComment7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awItems7awItem1awComment7.Attributes.Count());
            Assert.AreEqual("aw:Comment", documentawPurchaseOrder0awItems7awItem1awComment7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem1awComment7.NodeType);

            var documentawPurchaseOrder0awItems7awItem1awComment7Text0 = documentawPurchaseOrder0awItems7awItem1awComment7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem1awComment7Text0.NodeType);
            Assert.AreEqual("Confirm this is electric", documentawPurchaseOrder0awItems7awItem1awComment7Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem1Text8 = documentawPurchaseOrder0awItems7awItem1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem1Text8.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awItems7awItem1Text8.TextContent);

            var documentawPurchaseOrder0awItems7Text2 = documentawPurchaseOrder0awItems7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7Text2.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awItems7Text2.TextContent);

            var documentawPurchaseOrder0awItems7awItem3 = documentawPurchaseOrder0awItems7.ChildNodes[3] as IElement;
            Assert.AreEqual(9, documentawPurchaseOrder0awItems7awItem3.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem3.Attributes.Count());
            Assert.AreEqual("aw:Item", documentawPurchaseOrder0awItems7awItem3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem3.NodeType);
            
            Assert.AreEqual("926-AA", documentawPurchaseOrder0awItems7awItem3.GetAttribute("aw:PartNumber"));

            var documentawPurchaseOrder0awItems7awItem3Text0 = documentawPurchaseOrder0awItems7awItem3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem3Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrder0awItems7awItem3Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem3awProductName1 = documentawPurchaseOrder0awItems7awItem3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem3awProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awItems7awItem3awProductName1.Attributes.Count());
            Assert.AreEqual("aw:ProductName", documentawPurchaseOrder0awItems7awItem3awProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem3awProductName1.NodeType);

            var documentawPurchaseOrder0awItems7awItem3awProductName1Text0 = documentawPurchaseOrder0awItems7awItem3awProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem3awProductName1Text0.NodeType);
            Assert.AreEqual("Baby Monitor", documentawPurchaseOrder0awItems7awItem3awProductName1Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem3Text2 = documentawPurchaseOrder0awItems7awItem3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem3Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrder0awItems7awItem3Text2.TextContent);

            var documentawPurchaseOrder0awItems7awItem3awQuantity3 = documentawPurchaseOrder0awItems7awItem3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem3awQuantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awItems7awItem3awQuantity3.Attributes.Count());
            Assert.AreEqual("aw:Quantity", documentawPurchaseOrder0awItems7awItem3awQuantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem3awQuantity3.NodeType);

            var documentawPurchaseOrder0awItems7awItem3awQuantity3Text0 = documentawPurchaseOrder0awItems7awItem3awQuantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem3awQuantity3Text0.NodeType);
            Assert.AreEqual("2", documentawPurchaseOrder0awItems7awItem3awQuantity3Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem3Text4 = documentawPurchaseOrder0awItems7awItem3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem3Text4.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrder0awItems7awItem3Text4.TextContent);

            var documentawPurchaseOrder0awItems7awItem3awUSPrice5 = documentawPurchaseOrder0awItems7awItem3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem3awUSPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awItems7awItem3awUSPrice5.Attributes.Count());
            Assert.AreEqual("aw:USPrice", documentawPurchaseOrder0awItems7awItem3awUSPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem3awUSPrice5.NodeType);

            var documentawPurchaseOrder0awItems7awItem3awUSPrice5Text0 = documentawPurchaseOrder0awItems7awItem3awUSPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem3awUSPrice5Text0.NodeType);
            Assert.AreEqual("39.98", documentawPurchaseOrder0awItems7awItem3awUSPrice5Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem3Text6 = documentawPurchaseOrder0awItems7awItem3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem3Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrder0awItems7awItem3Text6.TextContent);

            var documentawPurchaseOrder0awItems7awItem3awShipDate7 = documentawPurchaseOrder0awItems7awItem3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrder0awItems7awItem3awShipDate7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrder0awItems7awItem3awShipDate7.Attributes.Count());
            Assert.AreEqual("aw:ShipDate", documentawPurchaseOrder0awItems7awItem3awShipDate7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrder0awItems7awItem3awShipDate7.NodeType);

            var documentawPurchaseOrder0awItems7awItem3awShipDate7Text0 = documentawPurchaseOrder0awItems7awItem3awShipDate7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem3awShipDate7Text0.NodeType);
            Assert.AreEqual("1999-05-21", documentawPurchaseOrder0awItems7awItem3awShipDate7Text0.TextContent);

            var documentawPurchaseOrder0awItems7awItem3Text8 = documentawPurchaseOrder0awItems7awItem3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7awItem3Text8.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrder0awItems7awItem3Text8.TextContent);

            var documentawPurchaseOrder0awItems7Text4 = documentawPurchaseOrder0awItems7.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0awItems7Text4.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrder0awItems7Text4.TextContent);

            var documentawPurchaseOrder0Text8 = documentawPurchaseOrder0.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrder0Text8.NodeType);
            Assert.AreEqual("\n", documentawPurchaseOrder0Text8.TextContent);
        }

        [Test]
        public void XmlMultiplePurchaseOrder()
        {
            var document = (@"<?xml version=""1.0""?>
<PurchaseOrders>
  <PurchaseOrder PurchaseOrderNumber=""99503"" OrderDate=""1999-10-20"">
    <Address Type=""Shipping"">
      <Name>Ellen Adams</Name>
      <Street>123 Maple Street</Street>
      <City>Mill Valley</City>
      <State>CA</State>
      <Zip>10999</Zip>
      <Country>USA</Country>
    </Address>
    <Address Type=""Billing"">
      <Name>Tai Yee</Name>
      <Street>8 Oak Avenue</Street>
      <City>Old Town</City>
      <State>PA</State>
      <Zip>95819</Zip>
      <Country>USA</Country>
    </Address>
    <DeliveryNotes>Please leave packages in shed by driveway.</DeliveryNotes>
    <Items>
      <Item PartNumber=""872-AA"">
        <ProductName>Lawnmower</ProductName>
        <Quantity>1</Quantity>
        <USPrice>148.95</USPrice>
        <Comment>Confirm this is electric</Comment>
      </Item>
      <Item PartNumber=""926-AA"">
        <ProductName>Baby Monitor</ProductName>
        <Quantity>2</Quantity>
        <USPrice>39.98</USPrice>
        <ShipDate>1999-05-21</ShipDate>
      </Item>
    </Items>
  </PurchaseOrder>
  <PurchaseOrder PurchaseOrderNumber=""99505"" OrderDate=""1999-10-22"">
    <Address Type=""Shipping"">
      <Name>Cristian Osorio</Name>
      <Street>456 Main Street</Street>
      <City>Buffalo</City>
      <State>NY</State>
      <Zip>98112</Zip>
      <Country>USA</Country>
    </Address>
    <Address Type=""Billing"">
      <Name>Cristian Osorio</Name>
      <Street>456 Main Street</Street>
      <City>Buffalo</City>
      <State>NY</State>
      <Zip>98112</Zip>
      <Country>USA</Country>
    </Address>
    <DeliveryNotes>Please notify me before shipping.</DeliveryNotes>
    <Items>
      <Item PartNumber=""456-NM"">
        <ProductName>Power Supply</ProductName>
        <Quantity>1</Quantity>
        <USPrice>45.99</USPrice>
      </Item>
    </Items>
  </PurchaseOrder>
  <PurchaseOrder PurchaseOrderNumber=""99504"" OrderDate=""1999-10-22"">
    <Address Type=""Shipping"">
      <Name>Jessica Arnold</Name>
      <Street>4055 Madison Ave</Street>
      <City>Seattle</City>
      <State>WA</State>
      <Zip>98112</Zip>
      <Country>USA</Country>
    </Address>
    <Address Type=""Billing"">
      <Name>Jessica Arnold</Name>
      <Street>4055 Madison Ave</Street>
      <City>Buffalo</City>
      <State>NY</State>
      <Zip>98112</Zip>
      <Country>USA</Country>
    </Address>
    <Items>
      <Item PartNumber=""898-AZ"">
        <ProductName>Computer Keyboard</ProductName>
        <Quantity>1</Quantity>
        <USPrice>29.99</USPrice>
      </Item>
      <Item PartNumber=""898-AM"">
        <ProductName>Wireless Mouse</ProductName>
        <Quantity>1</Quantity>
        <USPrice>14.99</USPrice>
      </Item>
    </Items>
  </PurchaseOrder>
</PurchaseOrders>").ToXmlDocument();

            var documentPurchaseOrders0 = document.ChildNodes[0] as IElement;
            Assert.AreEqual(7, documentPurchaseOrders0.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0.Attributes.Count());
            Assert.AreEqual("PurchaseOrders", documentPurchaseOrders0.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0.NodeType);

            var documentPurchaseOrders0Text0 = documentPurchaseOrders0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0Text0.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrders0Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1 = documentPurchaseOrders0.ChildNodes[1] as IElement;
            Assert.AreEqual(9, documentPurchaseOrders0PurchaseOrder1.ChildNodes.Length);
            Assert.AreEqual(2, documentPurchaseOrders0PurchaseOrder1.Attributes.Count());
            Assert.AreEqual("PurchaseOrder", documentPurchaseOrders0PurchaseOrder1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1.NodeType);
            Assert.AreEqual("99503", documentPurchaseOrders0PurchaseOrder1.GetAttribute("PurchaseOrderNumber"));
            Assert.AreEqual("1999-10-20", documentPurchaseOrders0PurchaseOrder1.GetAttribute("OrderDate"));

            var documentPurchaseOrders0PurchaseOrder1Text0 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[1] as IElement;
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1.Attributes.Count());
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder1Address1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1.NodeType);
            Assert.AreEqual("Shipping", documentPurchaseOrders0PurchaseOrder1Address1.GetAttribute("Type"));

            var documentPurchaseOrders0PurchaseOrder1Address1Text0 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Name1 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1Name1.Attributes.Count());
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder1Address1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1Name1Text0 = documentPurchaseOrders0PurchaseOrder1Address1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Name1Text0.NodeType);
            Assert.AreEqual("Ellen Adams", documentPurchaseOrders0PurchaseOrder1Address1Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text2 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Street3 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1Street3.Attributes.Count());
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder1Address1Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1Street3Text0 = documentPurchaseOrders0PurchaseOrder1Address1Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Street3Text0.NodeType);
            Assert.AreEqual("123 Maple Street", documentPurchaseOrders0PurchaseOrder1Address1Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text4 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1City5 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1City5.Attributes.Count());
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder1Address1City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1City5Text0 = documentPurchaseOrders0PurchaseOrder1Address1City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1City5Text0.NodeType);
            Assert.AreEqual("Mill Valley", documentPurchaseOrders0PurchaseOrder1Address1City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text6 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1State7 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1State7.Attributes.Count());
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder1Address1State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1State7Text0 = documentPurchaseOrders0PurchaseOrder1Address1State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1State7Text0.NodeType);
            Assert.AreEqual("CA", documentPurchaseOrders0PurchaseOrder1Address1State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text8 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Zip9 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1Zip9.Attributes.Count());
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder1Address1Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1Zip9Text0 = documentPurchaseOrders0PurchaseOrder1Address1Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Zip9Text0.NodeType);
            Assert.AreEqual("10999", documentPurchaseOrders0PurchaseOrder1Address1Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text10 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Country11 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1Country11.Attributes.Count());
            Assert.AreEqual("Country", documentPurchaseOrders0PurchaseOrder1Address1Country11.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1Country11.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1Country11Text0 = documentPurchaseOrders0PurchaseOrder1Address1Country11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Country11Text0.NodeType);
            Assert.AreEqual("USA", documentPurchaseOrders0PurchaseOrder1Address1Country11Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text12 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text12.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder1Address1Text12.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Text2 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Text2.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[3] as IElement;
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3.Attributes.Count());
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder1Address3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3.NodeType);
            Assert.AreEqual("Billing", documentPurchaseOrders0PurchaseOrder1Address3.GetAttribute("Type"));

            var documentPurchaseOrders0PurchaseOrder1Address3Text0 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Name1 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3Name1.Attributes.Count());
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder1Address3Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3Name1Text0 = documentPurchaseOrders0PurchaseOrder1Address3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Name1Text0.NodeType);
            Assert.AreEqual("Tai Yee", documentPurchaseOrders0PurchaseOrder1Address3Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text2 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Street3 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3Street3.Attributes.Count());
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder1Address3Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3Street3Text0 = documentPurchaseOrders0PurchaseOrder1Address3Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Street3Text0.NodeType);
            Assert.AreEqual("8 Oak Avenue", documentPurchaseOrders0PurchaseOrder1Address3Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text4 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3City5 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3City5.Attributes.Count());
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder1Address3City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3City5Text0 = documentPurchaseOrders0PurchaseOrder1Address3City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3City5Text0.NodeType);
            Assert.AreEqual("Old Town", documentPurchaseOrders0PurchaseOrder1Address3City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text6 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3State7 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3State7.Attributes.Count());
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder1Address3State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3State7Text0 = documentPurchaseOrders0PurchaseOrder1Address3State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3State7Text0.NodeType);
            Assert.AreEqual("PA", documentPurchaseOrders0PurchaseOrder1Address3State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text8 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Zip9 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3Zip9.Attributes.Count());
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder1Address3Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3Zip9Text0 = documentPurchaseOrders0PurchaseOrder1Address3Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Zip9Text0.NodeType);
            Assert.AreEqual("95819", documentPurchaseOrders0PurchaseOrder1Address3Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text10 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Country11 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3Country11.Attributes.Count());
            Assert.AreEqual("Country", documentPurchaseOrders0PurchaseOrder1Address3Country11.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3Country11.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3Country11Text0 = documentPurchaseOrders0PurchaseOrder1Address3Country11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Country11Text0.NodeType);
            Assert.AreEqual("USA", documentPurchaseOrders0PurchaseOrder1Address3Country11Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text12 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text12.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder1Address3Text12.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Text4 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Text4.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1DeliveryNotes5 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.Attributes.Count());
            Assert.AreEqual("DeliveryNotes", documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1DeliveryNotes5Text0 = documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1DeliveryNotes5Text0.NodeType);
            Assert.AreEqual("Please leave packages in shed by driveway.", documentPurchaseOrders0PurchaseOrder1DeliveryNotes5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Text6 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Text6.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[7] as IElement;
            Assert.AreEqual(5, documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7.Attributes.Count());
            Assert.AreEqual("Items", documentPurchaseOrders0PurchaseOrder1Items7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Text0 = documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Items7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1 = documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes[1] as IElement;
            Assert.AreEqual(9, documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1.Attributes.Count());
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder1Items7Item1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item1.NodeType);
            Assert.AreEqual("872-AA", documentPurchaseOrders0PurchaseOrder1Items7Item1.GetAttribute("PartNumber"));

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.Attributes.Count());
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1Text0.NodeType);
            Assert.AreEqual("Lawnmower", documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Text2 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Text4 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.Attributes.Count());
            Assert.AreEqual("USPrice", documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5Text0.NodeType);
            Assert.AreEqual("148.95", documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Text6 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Text6.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7.Attributes.Count());
            Assert.AreEqual("Comment", documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7Text0.NodeType);
            Assert.AreEqual("Confirm this is electric", documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Text8 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Items7Item1Text8.TextContent);


            var documentPurchaseOrders0PurchaseOrder1Items7Text2 = documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Items7Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3 = documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes[3] as IElement;
            Assert.AreEqual(9, documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3.Attributes.Count());
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder1Items7Item3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item3.NodeType);
            Assert.AreEqual("926-AA", documentPurchaseOrders0PurchaseOrder1Items7Item3.GetAttribute("PartNumber"));

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.Attributes.Count());
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1Text0.NodeType);
            Assert.AreEqual("Baby Monitor", documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Text2 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3Text0.NodeType);
            Assert.AreEqual("2", documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3Text0.TextContent);


            var documentPurchaseOrders0PurchaseOrder1Items7Item3Text4 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.Attributes.Count());
            Assert.AreEqual("USPrice", documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5Text0.NodeType);
            Assert.AreEqual("39.98", documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Text6 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Text6.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7.Attributes.Count());
            Assert.AreEqual("ShipDate", documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7Text0.NodeType);
            Assert.AreEqual("1999-05-21", documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Text8 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Items7Item3Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Text4 = documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Text4.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder1Items7Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Text8 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Text8.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrders0PurchaseOrder1Text8.TextContent);

            var documentPurchaseOrders0Text2 = documentPurchaseOrders0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0Text2.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrders0Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder3 = documentPurchaseOrders0.ChildNodes[3] as IElement;
            Assert.AreEqual(9, documentPurchaseOrders0PurchaseOrder3.ChildNodes.Length);
            Assert.AreEqual(2, documentPurchaseOrders0PurchaseOrder3.Attributes.Count());
            Assert.AreEqual("PurchaseOrder", documentPurchaseOrders0PurchaseOrder3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3.NodeType);
            Assert.AreEqual("99505", documentPurchaseOrders0PurchaseOrder3.GetAttribute("PurchaseOrderNumber"));
            Assert.AreEqual("1999-10-22", documentPurchaseOrders0PurchaseOrder3.GetAttribute("OrderDate"));

            var documentPurchaseOrders0PurchaseOrder3Text0 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[1] as IElement;
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1.Attributes.Count());
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder3Address1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1.NodeType);
            Assert.AreEqual("Shipping", documentPurchaseOrders0PurchaseOrder3Address1.GetAttribute("Type"));

            var documentPurchaseOrders0PurchaseOrder3Address1Text0 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Name1 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1Name1.Attributes.Count());
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder3Address1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1Name1Text0 = documentPurchaseOrders0PurchaseOrder3Address1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Name1Text0.NodeType);
            Assert.AreEqual("Cristian Osorio", documentPurchaseOrders0PurchaseOrder3Address1Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text2 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Street3 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1Street3.Attributes.Count());
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder3Address1Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1Street3Text0 = documentPurchaseOrders0PurchaseOrder3Address1Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Street3Text0.NodeType);
            Assert.AreEqual("456 Main Street", documentPurchaseOrders0PurchaseOrder3Address1Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text4 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1City5 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1City5.Attributes.Count());
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder3Address1City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1City5Text0 = documentPurchaseOrders0PurchaseOrder3Address1City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1City5Text0.NodeType);
            Assert.AreEqual("Buffalo", documentPurchaseOrders0PurchaseOrder3Address1City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text6 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1State7 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1State7.Attributes.Count());
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder3Address1State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1State7Text0 = documentPurchaseOrders0PurchaseOrder3Address1State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1State7Text0.NodeType);
            Assert.AreEqual("NY", documentPurchaseOrders0PurchaseOrder3Address1State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text8 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Zip9 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1Zip9.Attributes.Count());
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder3Address1Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1Zip9Text0 = documentPurchaseOrders0PurchaseOrder3Address1Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Zip9Text0.NodeType);
            Assert.AreEqual("98112", documentPurchaseOrders0PurchaseOrder3Address1Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text10 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Country11 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1Country11.Attributes.Count());
            Assert.AreEqual("Country", documentPurchaseOrders0PurchaseOrder3Address1Country11.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1Country11.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1Country11Text0 = documentPurchaseOrders0PurchaseOrder3Address1Country11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Country11Text0.NodeType);
            Assert.AreEqual("USA", documentPurchaseOrders0PurchaseOrder3Address1Country11Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text12 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text12.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder3Address1Text12.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Text2 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Text2.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[3] as IElement;
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3.Attributes.Count());
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder3Address3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3.NodeType);
            Assert.AreEqual("Billing", documentPurchaseOrders0PurchaseOrder3Address3.GetAttribute("Type"));

            var documentPurchaseOrders0PurchaseOrder3Address3Text0 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Name1 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3Name1.Attributes.Count());
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder3Address3Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3Name1Text0 = documentPurchaseOrders0PurchaseOrder3Address3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Name1Text0.NodeType);
            Assert.AreEqual("Cristian Osorio", documentPurchaseOrders0PurchaseOrder3Address3Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text2 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Street3 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3Street3.Attributes.Count());
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder3Address3Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3Street3Text0 = documentPurchaseOrders0PurchaseOrder3Address3Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Street3Text0.NodeType);
            Assert.AreEqual("456 Main Street", documentPurchaseOrders0PurchaseOrder3Address3Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text4 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3City5 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3City5.Attributes.Count());
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder3Address3City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3City5Text0 = documentPurchaseOrders0PurchaseOrder3Address3City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3City5Text0.NodeType);
            Assert.AreEqual("Buffalo", documentPurchaseOrders0PurchaseOrder3Address3City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text6 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3State7 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3State7.Attributes.Count());
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder3Address3State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3State7Text0 = documentPurchaseOrders0PurchaseOrder3Address3State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3State7Text0.NodeType);
            Assert.AreEqual("NY", documentPurchaseOrders0PurchaseOrder3Address3State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text8 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Zip9 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3Zip9.Attributes.Count());
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder3Address3Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3Zip9Text0 = documentPurchaseOrders0PurchaseOrder3Address3Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Zip9Text0.NodeType);
            Assert.AreEqual("98112", documentPurchaseOrders0PurchaseOrder3Address3Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text10 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Country11 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3Country11.Attributes.Count());
            Assert.AreEqual("Country", documentPurchaseOrders0PurchaseOrder3Address3Country11.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3Country11.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3Country11Text0 = documentPurchaseOrders0PurchaseOrder3Address3Country11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Country11Text0.NodeType);
            Assert.AreEqual("USA", documentPurchaseOrders0PurchaseOrder3Address3Country11Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text12 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text12.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder3Address3Text12.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Text4 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Text4.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder3DeliveryNotes5 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.Attributes.Count());
            Assert.AreEqual("DeliveryNotes", documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.NodeType);

            var documentPurchaseOrders0PurchaseOrder3DeliveryNotes5Text0 = documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3DeliveryNotes5Text0.NodeType);
            Assert.AreEqual("Please notify me before shipping.", documentPurchaseOrders0PurchaseOrder3DeliveryNotes5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Text6 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Text6.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[7] as IElement;
            Assert.AreEqual(3, documentPurchaseOrders0PurchaseOrder3Items7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Items7.Attributes.Count());
            Assert.AreEqual("Items", documentPurchaseOrders0PurchaseOrder3Items7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Items7.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Items7Text0 = documentPurchaseOrders0PurchaseOrder3Items7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Items7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1 = documentPurchaseOrders0PurchaseOrder3Items7.ChildNodes[1] as IElement;
            Assert.AreEqual(7, documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Items7Item1.Attributes.Count());
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder3Items7Item1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Items7Item1.NodeType);
            Assert.AreEqual("456-NM", documentPurchaseOrders0PurchaseOrder3Items7Item1.GetAttribute("PartNumber"));

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Text0 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder3Items7Item1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.Attributes.Count());
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1Text0 = documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1Text0.NodeType);
            Assert.AreEqual("Power Supply", documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Text2 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder3Items7Item1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3Text0 = documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Text4 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder3Items7Item1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5.Attributes.Count());
            Assert.AreEqual("USPrice", documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5Text0 = documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5Text0.NodeType);
            Assert.AreEqual("45.99", documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Text6 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Items7Item1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Text2 = documentPurchaseOrders0PurchaseOrder3Items7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Text2.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder3Items7Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Text8 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Text8.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrders0PurchaseOrder3Text8.TextContent);

            var documentPurchaseOrders0Text4 = documentPurchaseOrders0.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0Text4.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrders0Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5 = documentPurchaseOrders0.ChildNodes[5] as IElement;
            Assert.AreEqual(7, documentPurchaseOrders0PurchaseOrder5.ChildNodes.Length);
            Assert.AreEqual(2, documentPurchaseOrders0PurchaseOrder5.Attributes.Count());
            Assert.AreEqual("PurchaseOrder", documentPurchaseOrders0PurchaseOrder5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5.NodeType);
            Assert.AreEqual("99504", documentPurchaseOrders0PurchaseOrder5.GetAttribute("PurchaseOrderNumber"));
            Assert.AreEqual("1999-10-22", documentPurchaseOrders0PurchaseOrder5.GetAttribute("OrderDate"));

            var documentPurchaseOrders0PurchaseOrder5Text0 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[1] as IElement;
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1.Attributes.Count());
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder5Address1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1.NodeType);
            Assert.AreEqual("Shipping", documentPurchaseOrders0PurchaseOrder5Address1.GetAttribute("Type"));

            var documentPurchaseOrders0PurchaseOrder5Address1Text0 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Name1 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1Name1.Attributes.Count());
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder5Address1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1Name1Text0 = documentPurchaseOrders0PurchaseOrder5Address1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Name1Text0.NodeType);
            Assert.AreEqual("Jessica Arnold", documentPurchaseOrders0PurchaseOrder5Address1Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text2 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Street3 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1Street3.Attributes.Count());
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder5Address1Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1Street3Text0 = documentPurchaseOrders0PurchaseOrder5Address1Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Street3Text0.NodeType);
            Assert.AreEqual("4055 Madison Ave", documentPurchaseOrders0PurchaseOrder5Address1Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text4 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1City5 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1City5.Attributes.Count());
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder5Address1City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1City5Text0 = documentPurchaseOrders0PurchaseOrder5Address1City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1City5Text0.NodeType);
            Assert.AreEqual("Seattle", documentPurchaseOrders0PurchaseOrder5Address1City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text6 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1State7 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1State7.Attributes.Count());
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder5Address1State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1State7Text0 = documentPurchaseOrders0PurchaseOrder5Address1State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1State7Text0.NodeType);
            Assert.AreEqual("WA", documentPurchaseOrders0PurchaseOrder5Address1State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text8 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Zip9 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1Zip9.Attributes.Count());
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder5Address1Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1Zip9Text0 = documentPurchaseOrders0PurchaseOrder5Address1Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Zip9Text0.NodeType);
            Assert.AreEqual("98112", documentPurchaseOrders0PurchaseOrder5Address1Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text10 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Country11 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1Country11.Attributes.Count());
            Assert.AreEqual("Country", documentPurchaseOrders0PurchaseOrder5Address1Country11.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1Country11.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1Country11Text0 = documentPurchaseOrders0PurchaseOrder5Address1Country11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Country11Text0.NodeType);
            Assert.AreEqual("USA", documentPurchaseOrders0PurchaseOrder5Address1Country11Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text12 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text12.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder5Address1Text12.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Text2 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Text2.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder5Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[3] as IElement;
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3.Attributes.Count());
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder5Address3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3.NodeType);
            Assert.AreEqual("Billing", documentPurchaseOrders0PurchaseOrder5Address3.GetAttribute("Type"));

            var documentPurchaseOrders0PurchaseOrder5Address3Text0 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Name1 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3Name1.Attributes.Count());
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder5Address3Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3Name1Text0 = documentPurchaseOrders0PurchaseOrder5Address3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Name1Text0.NodeType);
            Assert.AreEqual("Jessica Arnold", documentPurchaseOrders0PurchaseOrder5Address3Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text2 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Street3 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3Street3.Attributes.Count());
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder5Address3Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3Street3Text0 = documentPurchaseOrders0PurchaseOrder5Address3Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Street3Text0.NodeType);
            Assert.AreEqual("4055 Madison Ave", documentPurchaseOrders0PurchaseOrder5Address3Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text4 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3City5 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3City5.Attributes.Count());
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder5Address3City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3City5Text0 = documentPurchaseOrders0PurchaseOrder5Address3City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3City5Text0.NodeType);
            Assert.AreEqual("Buffalo", documentPurchaseOrders0PurchaseOrder5Address3City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text6 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3State7 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3State7.Attributes.Count());
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder5Address3State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3State7Text0 = documentPurchaseOrders0PurchaseOrder5Address3State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3State7Text0.NodeType);
            Assert.AreEqual("NY", documentPurchaseOrders0PurchaseOrder5Address3State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text8 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Zip9 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3Zip9.Attributes.Count());
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder5Address3Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3Zip9Text0 = documentPurchaseOrders0PurchaseOrder5Address3Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Zip9Text0.NodeType);
            Assert.AreEqual("98112", documentPurchaseOrders0PurchaseOrder5Address3Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text10 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Country11 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3Country11.Attributes.Count());
            Assert.AreEqual("Country", documentPurchaseOrders0PurchaseOrder5Address3Country11.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3Country11.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3Country11Text0 = documentPurchaseOrders0PurchaseOrder5Address3Country11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Country11Text0.NodeType);
            Assert.AreEqual("USA", documentPurchaseOrders0PurchaseOrder5Address3Country11Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text12 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text12.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder5Address3Text12.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Text4 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Text4.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder5Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[5] as IElement;
            Assert.AreEqual(5, documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5.Attributes.Count());
            Assert.AreEqual("Items", documentPurchaseOrders0PurchaseOrder5Items5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Text0 = documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Items5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1 = documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes[1] as IElement;
            Assert.AreEqual(7, documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item1.Attributes.Count());
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder5Items5Item1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item1.NodeType);
            Assert.AreEqual("898-AZ", documentPurchaseOrders0PurchaseOrder5Items5Item1.GetAttribute("PartNumber"));

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.Attributes.Count());
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1Text0.NodeType);
            Assert.AreEqual("Computer Keyboard", documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Text2 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Text4 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5.Attributes.Count());
            Assert.AreEqual("USPrice", documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5Text0.NodeType);
            Assert.AreEqual("29.99", documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Text6 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Items5Item1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Text2 = documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Items5Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3 = documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes[3] as IElement;
            Assert.AreEqual(7, documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item3.Attributes.Count());
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder5Items5Item3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item3.NodeType);
            Assert.AreEqual("898-AM", documentPurchaseOrders0PurchaseOrder5Items5Item3.GetAttribute("PartNumber"));

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.Attributes.Count());
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1Text0.NodeType);
            Assert.AreEqual("Wireless Mouse", documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Text2 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Text4 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5.Attributes.Count());
            Assert.AreEqual("USPrice", documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5Text0.NodeType);
            Assert.AreEqual("14.99", documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Text6 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Items5Item3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Text4 = documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Text4.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder5Items5Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Text6 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Text6.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrders0PurchaseOrder5Text6.TextContent);

            var documentPurchaseOrders0Text6 = documentPurchaseOrders0.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0Text6.NodeType);
            Assert.AreEqual("\n", documentPurchaseOrders0Text6.TextContent);
        }

        [Test]
        public void XmlMultiplePurchaseOrderInNamespace()
        {
            var document = (@"<?xml version=""1.0"" encoding=""utf-8""?>
<aw:PurchaseOrders xmlns:aw=""http://www.adventure-works.com"">
  <aw:PurchaseOrder aw:PurchaseOrderNumber=""99503"" aw:OrderDate=""1999-10-20"">
    <aw:Address aw:Type=""Shipping"">
      <aw:Name>Ellen Adams</aw:Name>
      <aw:Street>123 Maple Street</aw:Street>
      <aw:City>Mill Valley</aw:City>
      <aw:State>CA</aw:State>
      <aw:Zip>10999</aw:Zip>
      <aw:Country>USA</aw:Country>
    </aw:Address>
    <aw:Address aw:Type=""Billing"">
      <aw:Name>Tai Yee</aw:Name>
      <aw:Street>8 Oak Avenue</aw:Street>
      <aw:City>Old Town</aw:City>
      <aw:State>PA</aw:State>
      <aw:Zip>95819</aw:Zip>
      <aw:Country>USA</aw:Country>
    </aw:Address>
    <aw:DeliveryNotes>Please leave packages in shed by driveway.</aw:DeliveryNotes>
    <aw:Items>
      <aw:Item aw:PartNumber=""872-AA"">
        <aw:ProductName>Lawnmower</aw:ProductName>
        <aw:Quantity>1</aw:Quantity>
        <aw:USPrice>148.95</aw:USPrice>
        <aw:Comment>Confirm this is electric</aw:Comment>
      </aw:Item>
      <aw:Item aw:PartNumber=""926-AA"">
        <aw:ProductName>Baby Monitor</aw:ProductName>
        <aw:Quantity>2</aw:Quantity>
        <aw:USPrice>39.98</aw:USPrice>
        <aw:ShipDate>1999-05-21</aw:ShipDate>
      </aw:Item>
    </aw:Items>
  </aw:PurchaseOrder>
  <aw:PurchaseOrder aw:PurchaseOrderNumber=""99505"" aw:OrderDate=""1999-10-22"">
    <aw:Address aw:Type=""Shipping"">
      <aw:Name>Cristian Osorio</aw:Name>
      <aw:Street>456 Main Street</aw:Street>
      <aw:City>Buffalo</aw:City>
      <aw:State>NY</aw:State>
      <aw:Zip>98112</aw:Zip>
      <aw:Country>USA</aw:Country>
    </aw:Address>
    <aw:Address aw:Type=""Billing"">
      <aw:Name>Cristian Osorio</aw:Name>
      <aw:Street>456 Main Street</aw:Street>
      <aw:City>Buffalo</aw:City>
      <aw:State>NY</aw:State>
      <aw:Zip>98112</aw:Zip>
      <aw:Country>USA</aw:Country>
    </aw:Address>
    <aw:DeliveryNotes>Please notify me before shipping.</aw:DeliveryNotes>
    <aw:Items>
      <aw:Item aw:PartNumber=""456-NM"">
        <aw:ProductName>Power Supply</aw:ProductName>
        <aw:Quantity>1</aw:Quantity>
        <aw:USPrice>45.99</aw:USPrice>
      </aw:Item>
    </aw:Items>
  </aw:PurchaseOrder>
  <aw:PurchaseOrder aw:PurchaseOrderNumber=""99504"" aw:OrderDate=""1999-10-22"">
    <aw:Address aw:Type=""Shipping"">
      <aw:Name>Jessica Arnold</aw:Name>
      <aw:Street>4055 Madison Ave</aw:Street>
      <aw:City>Seattle</aw:City>
      <aw:State>WA</aw:State>
      <aw:Zip>98112</aw:Zip>
      <aw:Country>USA</aw:Country>
    </aw:Address>
    <aw:Address aw:Type=""Billing"">
      <aw:Name>Jessica Arnold</aw:Name>
      <aw:Street>4055 Madison Ave</aw:Street>
      <aw:City>Buffalo</aw:City>
      <aw:State>NY</aw:State>
      <aw:Zip>98112</aw:Zip>
      <aw:Country>USA</aw:Country>
    </aw:Address>
    <aw:Items>
      <aw:Item aw:PartNumber=""898-AZ"">
        <aw:ProductName>Computer Keyboard</aw:ProductName>
        <aw:Quantity>1</aw:Quantity>
        <aw:USPrice>29.99</aw:USPrice>
      </aw:Item>
      <aw:Item aw:PartNumber=""898-AM"">
        <aw:ProductName>Wireless Mouse</aw:ProductName>
        <aw:Quantity>1</aw:Quantity>
        <aw:USPrice>14.99</aw:USPrice>
      </aw:Item>
    </aw:Items>
  </aw:PurchaseOrder>
</aw:PurchaseOrders>").ToXmlDocument();

            var documentawPurchaseOrders0 = document.ChildNodes[0] as IElement;
            Assert.AreEqual(7, documentawPurchaseOrders0.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0.Attributes.Count());
            Assert.AreEqual("aw:PurchaseOrders", documentawPurchaseOrders0.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0.NodeType);
            Assert.AreEqual("http://www.adventure-works.com", documentawPurchaseOrders0.GetAttribute("xmlns:aw"));

            var documentawPurchaseOrders0Text0 = documentawPurchaseOrders0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0Text0.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrders0Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1 = documentawPurchaseOrders0.ChildNodes[1] as IElement;
            Assert.AreEqual(9, documentawPurchaseOrders0awPurchaseOrder1.ChildNodes.Length);
            Assert.AreEqual(2, documentawPurchaseOrders0awPurchaseOrder1.Attributes.Count());
            Assert.AreEqual("aw:PurchaseOrder", documentawPurchaseOrders0awPurchaseOrder1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1.NodeType);
            Assert.AreEqual("99503", documentawPurchaseOrders0awPurchaseOrder1.GetAttribute("aw:PurchaseOrderNumber"));
            Assert.AreEqual("1999-10-20", documentawPurchaseOrders0awPurchaseOrder1.GetAttribute("aw:OrderDate"));

            var documentawPurchaseOrders0awPurchaseOrder1Text0 = documentawPurchaseOrders0awPurchaseOrder1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1Text0.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1 = documentawPurchaseOrders0awPurchaseOrder1.ChildNodes[1] as IElement;
            Assert.AreEqual(13, documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress1.Attributes.Count());
            Assert.AreEqual("aw:Address", documentawPurchaseOrders0awPurchaseOrder1awAddress1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress1.NodeType);
            Assert.AreEqual("Shipping", documentawPurchaseOrders0awPurchaseOrder1awAddress1.GetAttribute("aw:Type"));

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awName1 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress1awName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress1awName1.Attributes.Count());
            Assert.AreEqual("aw:Name", documentawPurchaseOrders0awPurchaseOrder1awAddress1awName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress1awName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awName1Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress1awName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1awName1Text0.NodeType);
            Assert.AreEqual("Ellen Adams", documentawPurchaseOrders0awPurchaseOrder1awAddress1awName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1Text2 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress1Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awStreet3 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress1awStreet3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress1awStreet3.Attributes.Count());
            Assert.AreEqual("aw:Street", documentawPurchaseOrders0awPurchaseOrder1awAddress1awStreet3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress1awStreet3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awStreet3Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress1awStreet3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1awStreet3Text0.NodeType);
            Assert.AreEqual("123 Maple Street", documentawPurchaseOrders0awPurchaseOrder1awAddress1awStreet3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1Text4 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1Text4.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress1Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awCity5 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress1awCity5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress1awCity5.Attributes.Count());
            Assert.AreEqual("aw:City", documentawPurchaseOrders0awPurchaseOrder1awAddress1awCity5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress1awCity5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awCity5Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress1awCity5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1awCity5Text0.NodeType);
            Assert.AreEqual("Mill Valley", documentawPurchaseOrders0awPurchaseOrder1awAddress1awCity5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1Text6 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress1Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awState7 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress1awState7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress1awState7.Attributes.Count());
            Assert.AreEqual("aw:State", documentawPurchaseOrders0awPurchaseOrder1awAddress1awState7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress1awState7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awState7Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress1awState7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1awState7Text0.NodeType);
            Assert.AreEqual("CA", documentawPurchaseOrders0awPurchaseOrder1awAddress1awState7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1Text8 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1Text8.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress1Text8.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awZip9 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress1awZip9.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress1awZip9.Attributes.Count());
            Assert.AreEqual("aw:Zip", documentawPurchaseOrders0awPurchaseOrder1awAddress1awZip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress1awZip9.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awZip9Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress1awZip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1awZip9Text0.NodeType);
            Assert.AreEqual("10999", documentawPurchaseOrders0awPurchaseOrder1awAddress1awZip9Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1Text10 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1Text10.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress1Text10.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awCountry11 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress1awCountry11.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress1awCountry11.Attributes.Count());
            Assert.AreEqual("aw:Country", documentawPurchaseOrders0awPurchaseOrder1awAddress1awCountry11.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress1awCountry11.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1awCountry11Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress1awCountry11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1awCountry11Text0.NodeType);
            Assert.AreEqual("USA", documentawPurchaseOrders0awPurchaseOrder1awAddress1awCountry11Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress1Text12 = documentawPurchaseOrders0awPurchaseOrder1awAddress1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress1Text12.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder1awAddress1Text12.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1Text2 = documentawPurchaseOrders0awPurchaseOrder1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1Text2.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder1Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3 = documentawPurchaseOrders0awPurchaseOrder1.ChildNodes[3] as IElement;
            Assert.AreEqual(13, documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress3.Attributes.Count());
            Assert.AreEqual("aw:Address", documentawPurchaseOrders0awPurchaseOrder1awAddress3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress3.NodeType);
            Assert.AreEqual("Billing", documentawPurchaseOrders0awPurchaseOrder1awAddress3.GetAttribute("aw:Type"));

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awName1 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress3awName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress3awName1.Attributes.Count());
            Assert.AreEqual("aw:Name", documentawPurchaseOrders0awPurchaseOrder1awAddress3awName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress3awName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awName1Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress3awName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3awName1Text0.NodeType);
            Assert.AreEqual("Tai Yee", documentawPurchaseOrders0awPurchaseOrder1awAddress3awName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3Text2 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress3Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awStreet3 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress3awStreet3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress3awStreet3.Attributes.Count());
            Assert.AreEqual("aw:Street", documentawPurchaseOrders0awPurchaseOrder1awAddress3awStreet3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress3awStreet3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awStreet3Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress3awStreet3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3awStreet3Text0.NodeType);
            Assert.AreEqual("8 Oak Avenue", documentawPurchaseOrders0awPurchaseOrder1awAddress3awStreet3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3Text4 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3Text4.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress3Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awCity5 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress3awCity5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress3awCity5.Attributes.Count());
            Assert.AreEqual("aw:City", documentawPurchaseOrders0awPurchaseOrder1awAddress3awCity5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress3awCity5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awCity5Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress3awCity5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3awCity5Text0.NodeType);
            Assert.AreEqual("Old Town", documentawPurchaseOrders0awPurchaseOrder1awAddress3awCity5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3Text6 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress3Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awState7 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress3awState7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress3awState7.Attributes.Count());
            Assert.AreEqual("aw:State", documentawPurchaseOrders0awPurchaseOrder1awAddress3awState7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress3awState7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awState7Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress3awState7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3awState7Text0.NodeType);
            Assert.AreEqual("PA", documentawPurchaseOrders0awPurchaseOrder1awAddress3awState7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3Text8 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3Text8.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress3Text8.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awZip9 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress3awZip9.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress3awZip9.Attributes.Count());
            Assert.AreEqual("aw:Zip", documentawPurchaseOrders0awPurchaseOrder1awAddress3awZip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress3awZip9.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awZip9Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress3awZip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3awZip9Text0.NodeType);
            Assert.AreEqual("95819", documentawPurchaseOrders0awPurchaseOrder1awAddress3awZip9Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3Text10 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3Text10.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awAddress3Text10.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awCountry11 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awAddress3awCountry11.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awAddress3awCountry11.Attributes.Count());
            Assert.AreEqual("aw:Country", documentawPurchaseOrders0awPurchaseOrder1awAddress3awCountry11.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awAddress3awCountry11.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3awCountry11Text0 = documentawPurchaseOrders0awPurchaseOrder1awAddress3awCountry11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3awCountry11Text0.NodeType);
            Assert.AreEqual("USA", documentawPurchaseOrders0awPurchaseOrder1awAddress3awCountry11Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awAddress3Text12 = documentawPurchaseOrders0awPurchaseOrder1awAddress3.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awAddress3Text12.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder1awAddress3Text12.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1Text4 = documentawPurchaseOrders0awPurchaseOrder1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1Text4.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder1Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awDeliveryNotes5 = documentawPurchaseOrders0awPurchaseOrder1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awDeliveryNotes5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awDeliveryNotes5.Attributes.Count());
            Assert.AreEqual("aw:DeliveryNotes", documentawPurchaseOrders0awPurchaseOrder1awDeliveryNotes5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awDeliveryNotes5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awDeliveryNotes5Text0 = documentawPurchaseOrders0awPurchaseOrder1awDeliveryNotes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awDeliveryNotes5Text0.NodeType);
            Assert.AreEqual("Please leave packages in shed by driveway.", documentawPurchaseOrders0awPurchaseOrder1awDeliveryNotes5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1Text6 = documentawPurchaseOrders0awPurchaseOrder1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1Text6.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder1Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7 = documentawPurchaseOrders0awPurchaseOrder1.ChildNodes[7] as IElement;
            Assert.AreEqual(5, documentawPurchaseOrders0awPurchaseOrder1awItems7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awItems7.Attributes.Count());
            Assert.AreEqual("aw:Items", documentawPurchaseOrders0awPurchaseOrder1awItems7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awItems7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1 = documentawPurchaseOrders0awPurchaseOrder1awItems7.ChildNodes[1] as IElement;
            Assert.AreEqual(9, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.Attributes.Count());
            Assert.AreEqual("aw:Item", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.NodeType);
            Assert.AreEqual("872-AA", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.GetAttribute("aw:PartNumber"));

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text0.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awProductName1 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awProductName1.Attributes.Count());
            Assert.AreEqual("aw:ProductName", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awProductName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awProductName1Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awProductName1Text0.NodeType);
            Assert.AreEqual("Lawnmower", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awProductName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text2 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text2.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awQuantity3 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awQuantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awQuantity3.Attributes.Count());
            Assert.AreEqual("aw:Quantity", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awQuantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awQuantity3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awQuantity3Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awQuantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awQuantity3Text0.NodeType);
            Assert.AreEqual("1", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awQuantity3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text4 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text4.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awUSPrice5 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awUSPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awUSPrice5.Attributes.Count());
            Assert.AreEqual("aw:USPrice", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awUSPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awUSPrice5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awUSPrice5Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awUSPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awUSPrice5Text0.NodeType);
            Assert.AreEqual("148.95", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awUSPrice5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text6 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text6.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awComment7 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awComment7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awComment7.Attributes.Count());
            Assert.AreEqual("aw:Comment", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awComment7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awComment7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awComment7Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awComment7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awComment7Text0.NodeType);
            Assert.AreEqual("Confirm this is electric", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1awComment7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text8 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text8.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem1Text8.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7Text2 = documentawPurchaseOrders0awPurchaseOrder1awItems7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awItems7Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3 = documentawPurchaseOrders0awPurchaseOrder1awItems7.ChildNodes[3] as IElement;
            Assert.AreEqual(9, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.Attributes.Count());
            Assert.AreEqual("aw:Item", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.NodeType);
            Assert.AreEqual("926-AA", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.GetAttribute("aw:PartNumber"));

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text0.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awProductName1 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awProductName1.Attributes.Count());
            Assert.AreEqual("aw:ProductName", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awProductName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awProductName1Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awProductName1Text0.NodeType);
            Assert.AreEqual("Baby Monitor", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awProductName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text2 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text2.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awQuantity3 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awQuantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awQuantity3.Attributes.Count());
            Assert.AreEqual("aw:Quantity", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awQuantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awQuantity3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awQuantity3Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awQuantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awQuantity3Text0.NodeType);
            Assert.AreEqual("2", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awQuantity3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text4 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text4.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awUSPrice5 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awUSPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awUSPrice5.Attributes.Count());
            Assert.AreEqual("aw:USPrice", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awUSPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awUSPrice5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awUSPrice5Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awUSPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awUSPrice5Text0.NodeType);
            Assert.AreEqual("39.98", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awUSPrice5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text6 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text6.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awShipDate7 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awShipDate7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awShipDate7.Attributes.Count());
            Assert.AreEqual("aw:ShipDate", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awShipDate7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awShipDate7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awShipDate7Text0 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awShipDate7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awShipDate7Text0.NodeType);
            Assert.AreEqual("1999-05-21", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3awShipDate7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text8 = documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text8.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder1awItems7awItem3Text8.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1awItems7Text4 = documentawPurchaseOrders0awPurchaseOrder1awItems7.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1awItems7Text4.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder1awItems7Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder1Text8 = documentawPurchaseOrders0awPurchaseOrder1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder1Text8.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrders0awPurchaseOrder1Text8.TextContent);

            var documentawPurchaseOrders0Text2 = documentawPurchaseOrders0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0Text2.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrders0Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3 = documentawPurchaseOrders0.ChildNodes[3] as IElement;
            Assert.AreEqual(9, documentawPurchaseOrders0awPurchaseOrder3.ChildNodes.Length);
            Assert.AreEqual(2, documentawPurchaseOrders0awPurchaseOrder3.Attributes.Count());
            Assert.AreEqual("aw:PurchaseOrder", documentawPurchaseOrders0awPurchaseOrder3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3.NodeType);
            Assert.AreEqual("99505", documentawPurchaseOrders0awPurchaseOrder3.GetAttribute("aw:PurchaseOrderNumber"));
            Assert.AreEqual("1999-10-22", documentawPurchaseOrders0awPurchaseOrder3.GetAttribute("aw:OrderDate"));

            var documentawPurchaseOrders0awPurchaseOrder3Text0 = documentawPurchaseOrders0awPurchaseOrder3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3Text0.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1 = documentawPurchaseOrders0awPurchaseOrder3.ChildNodes[1] as IElement;
            Assert.AreEqual(13, documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress1.Attributes.Count());
            Assert.AreEqual("aw:Address", documentawPurchaseOrders0awPurchaseOrder3awAddress1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress1.NodeType);
            Assert.AreEqual("Shipping", documentawPurchaseOrders0awPurchaseOrder3awAddress1.GetAttribute("aw:Type"));

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awName1 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress1awName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress1awName1.Attributes.Count());
            Assert.AreEqual("aw:Name", documentawPurchaseOrders0awPurchaseOrder3awAddress1awName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress1awName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awName1Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress1awName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1awName1Text0.NodeType);
            Assert.AreEqual("Cristian Osorio", documentawPurchaseOrders0awPurchaseOrder3awAddress1awName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1Text2 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress1Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awStreet3 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress1awStreet3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress1awStreet3.Attributes.Count());
            Assert.AreEqual("aw:Street", documentawPurchaseOrders0awPurchaseOrder3awAddress1awStreet3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress1awStreet3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awStreet3Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress1awStreet3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1awStreet3Text0.NodeType);
            Assert.AreEqual("456 Main Street", documentawPurchaseOrders0awPurchaseOrder3awAddress1awStreet3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1Text4 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1Text4.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress1Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awCity5 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress1awCity5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress1awCity5.Attributes.Count());
            Assert.AreEqual("aw:City", documentawPurchaseOrders0awPurchaseOrder3awAddress1awCity5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress1awCity5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awCity5Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress1awCity5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1awCity5Text0.NodeType);
            Assert.AreEqual("Buffalo", documentawPurchaseOrders0awPurchaseOrder3awAddress1awCity5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1Text6 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress1Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awState7 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress1awState7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress1awState7.Attributes.Count());
            Assert.AreEqual("aw:State", documentawPurchaseOrders0awPurchaseOrder3awAddress1awState7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress1awState7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awState7Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress1awState7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1awState7Text0.NodeType);
            Assert.AreEqual("NY", documentawPurchaseOrders0awPurchaseOrder3awAddress1awState7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1Text8 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1Text8.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress1Text8.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awZip9 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress1awZip9.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress1awZip9.Attributes.Count());
            Assert.AreEqual("aw:Zip", documentawPurchaseOrders0awPurchaseOrder3awAddress1awZip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress1awZip9.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awZip9Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress1awZip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1awZip9Text0.NodeType);
            Assert.AreEqual("98112", documentawPurchaseOrders0awPurchaseOrder3awAddress1awZip9Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1Text10 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1Text10.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress1Text10.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awCountry11 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress1awCountry11.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress1awCountry11.Attributes.Count());
            Assert.AreEqual("aw:Country", documentawPurchaseOrders0awPurchaseOrder3awAddress1awCountry11.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress1awCountry11.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1awCountry11Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress1awCountry11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1awCountry11Text0.NodeType);
            Assert.AreEqual("USA", documentawPurchaseOrders0awPurchaseOrder3awAddress1awCountry11Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress1Text12 = documentawPurchaseOrders0awPurchaseOrder3awAddress1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress1Text12.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder3awAddress1Text12.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3Text2 = documentawPurchaseOrders0awPurchaseOrder3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3Text2.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder3Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3 = documentawPurchaseOrders0awPurchaseOrder3.ChildNodes[3] as IElement;
            Assert.AreEqual(13, documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress3.Attributes.Count());
            Assert.AreEqual("aw:Address", documentawPurchaseOrders0awPurchaseOrder3awAddress3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress3.NodeType);
            Assert.AreEqual("Billing", documentawPurchaseOrders0awPurchaseOrder3awAddress3.GetAttribute("aw:Type"));

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awName1 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress3awName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress3awName1.Attributes.Count());
            Assert.AreEqual("aw:Name", documentawPurchaseOrders0awPurchaseOrder3awAddress3awName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress3awName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awName1Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress3awName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3awName1Text0.NodeType);
            Assert.AreEqual("Cristian Osorio", documentawPurchaseOrders0awPurchaseOrder3awAddress3awName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3Text2 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress3Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awStreet3 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress3awStreet3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress3awStreet3.Attributes.Count());
            Assert.AreEqual("aw:Street", documentawPurchaseOrders0awPurchaseOrder3awAddress3awStreet3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress3awStreet3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awStreet3Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress3awStreet3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3awStreet3Text0.NodeType);
            Assert.AreEqual("456 Main Street", documentawPurchaseOrders0awPurchaseOrder3awAddress3awStreet3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3Text4 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3Text4.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress3Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awCity5 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress3awCity5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress3awCity5.Attributes.Count());
            Assert.AreEqual("aw:City", documentawPurchaseOrders0awPurchaseOrder3awAddress3awCity5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress3awCity5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awCity5Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress3awCity5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3awCity5Text0.NodeType);
            Assert.AreEqual("Buffalo", documentawPurchaseOrders0awPurchaseOrder3awAddress3awCity5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3Text6 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress3Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awState7 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress3awState7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress3awState7.Attributes.Count());
            Assert.AreEqual("aw:State", documentawPurchaseOrders0awPurchaseOrder3awAddress3awState7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress3awState7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awState7Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress3awState7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3awState7Text0.NodeType);
            Assert.AreEqual("NY", documentawPurchaseOrders0awPurchaseOrder3awAddress3awState7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3Text8 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3Text8.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress3Text8.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awZip9 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress3awZip9.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress3awZip9.Attributes.Count());
            Assert.AreEqual("aw:Zip", documentawPurchaseOrders0awPurchaseOrder3awAddress3awZip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress3awZip9.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awZip9Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress3awZip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3awZip9Text0.NodeType);
            Assert.AreEqual("98112", documentawPurchaseOrders0awPurchaseOrder3awAddress3awZip9Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3Text10 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3Text10.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awAddress3Text10.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awCountry11 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awAddress3awCountry11.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awAddress3awCountry11.Attributes.Count());
            Assert.AreEqual("aw:Country", documentawPurchaseOrders0awPurchaseOrder3awAddress3awCountry11.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awAddress3awCountry11.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3awCountry11Text0 = documentawPurchaseOrders0awPurchaseOrder3awAddress3awCountry11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3awCountry11Text0.NodeType);
            Assert.AreEqual("USA", documentawPurchaseOrders0awPurchaseOrder3awAddress3awCountry11Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awAddress3Text12 = documentawPurchaseOrders0awPurchaseOrder3awAddress3.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awAddress3Text12.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder3awAddress3Text12.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3Text4 = documentawPurchaseOrders0awPurchaseOrder3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3Text4.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder3Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awDeliveryNotes5 = documentawPurchaseOrders0awPurchaseOrder3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awDeliveryNotes5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awDeliveryNotes5.Attributes.Count());
            Assert.AreEqual("aw:DeliveryNotes", documentawPurchaseOrders0awPurchaseOrder3awDeliveryNotes5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awDeliveryNotes5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awDeliveryNotes5Text0 = documentawPurchaseOrders0awPurchaseOrder3awDeliveryNotes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awDeliveryNotes5Text0.NodeType);
            Assert.AreEqual("Please notify me before shipping.", documentawPurchaseOrders0awPurchaseOrder3awDeliveryNotes5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3Text6 = documentawPurchaseOrders0awPurchaseOrder3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3Text6.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder3Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7 = documentawPurchaseOrders0awPurchaseOrder3.ChildNodes[7] as IElement;
            Assert.AreEqual(3, documentawPurchaseOrders0awPurchaseOrder3awItems7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awItems7.Attributes.Count());
            Assert.AreEqual("aw:Items", documentawPurchaseOrders0awPurchaseOrder3awItems7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awItems7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7Text0 = documentawPurchaseOrders0awPurchaseOrder3awItems7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awItems7Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awItems7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1 = documentawPurchaseOrders0awPurchaseOrder3awItems7.ChildNodes[1] as IElement;
            Assert.AreEqual(7, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.Attributes.Count());
            Assert.AreEqual("aw:Item", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.NodeType);
            Assert.AreEqual("456-NM", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.GetAttribute("aw:PartNumber"));

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text0 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text0.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awProductName1 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awProductName1.Attributes.Count());
            Assert.AreEqual("aw:ProductName", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awProductName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awProductName1Text0 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awProductName1Text0.NodeType);
            Assert.AreEqual("Power Supply", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awProductName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text2 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text2.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awQuantity3 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awQuantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awQuantity3.Attributes.Count());
            Assert.AreEqual("aw:Quantity", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awQuantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awQuantity3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awQuantity3Text0 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awQuantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awQuantity3Text0.NodeType);
            Assert.AreEqual("1", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awQuantity3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text4 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text4.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awUSPrice5 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awUSPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awUSPrice5.Attributes.Count());
            Assert.AreEqual("aw:USPrice", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awUSPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awUSPrice5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awUSPrice5Text0 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awUSPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awUSPrice5Text0.NodeType);
            Assert.AreEqual("45.99", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1awUSPrice5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text6 = documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder3awItems7awItem1Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3awItems7Text2 = documentawPurchaseOrders0awPurchaseOrder3awItems7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3awItems7Text2.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder3awItems7Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder3Text8 = documentawPurchaseOrders0awPurchaseOrder3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder3Text8.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrders0awPurchaseOrder3Text8.TextContent);

            var documentawPurchaseOrders0Text4 = documentawPurchaseOrders0.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0Text4.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrders0Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5 = documentawPurchaseOrders0.ChildNodes[5] as IElement;
            Assert.AreEqual(7, documentawPurchaseOrders0awPurchaseOrder5.ChildNodes.Length);
            Assert.AreEqual(2, documentawPurchaseOrders0awPurchaseOrder5.Attributes.Count());
            Assert.AreEqual("aw:PurchaseOrder", documentawPurchaseOrders0awPurchaseOrder5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5.NodeType);
            Assert.AreEqual("99504", documentawPurchaseOrders0awPurchaseOrder5.GetAttribute("aw:PurchaseOrderNumber"));
            Assert.AreEqual("1999-10-22", documentawPurchaseOrders0awPurchaseOrder5.GetAttribute("aw:OrderDate"));

            var documentawPurchaseOrders0awPurchaseOrder5Text0 = documentawPurchaseOrders0awPurchaseOrder5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5Text0.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1 = documentawPurchaseOrders0awPurchaseOrder5.ChildNodes[1] as IElement;
            Assert.AreEqual(13, documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress1.Attributes.Count());
            Assert.AreEqual("aw:Address", documentawPurchaseOrders0awPurchaseOrder5awAddress1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress1.NodeType);
            Assert.AreEqual("Shipping", documentawPurchaseOrders0awPurchaseOrder5awAddress1.GetAttribute("aw:Type"));

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awName1 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress1awName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress1awName1.Attributes.Count());
            Assert.AreEqual("aw:Name", documentawPurchaseOrders0awPurchaseOrder5awAddress1awName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress1awName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awName1Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress1awName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1awName1Text0.NodeType);
            Assert.AreEqual("Jessica Arnold", documentawPurchaseOrders0awPurchaseOrder5awAddress1awName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1Text2 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress1Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awStreet3 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress1awStreet3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress1awStreet3.Attributes.Count());
            Assert.AreEqual("aw:Street", documentawPurchaseOrders0awPurchaseOrder5awAddress1awStreet3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress1awStreet3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awStreet3Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress1awStreet3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1awStreet3Text0.NodeType);
            Assert.AreEqual("4055 Madison Ave", documentawPurchaseOrders0awPurchaseOrder5awAddress1awStreet3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1Text4 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1Text4.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress1Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awCity5 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress1awCity5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress1awCity5.Attributes.Count());
            Assert.AreEqual("aw:City", documentawPurchaseOrders0awPurchaseOrder5awAddress1awCity5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress1awCity5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awCity5Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress1awCity5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1awCity5Text0.NodeType);
            Assert.AreEqual("Seattle", documentawPurchaseOrders0awPurchaseOrder5awAddress1awCity5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1Text6 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress1Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awState7 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress1awState7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress1awState7.Attributes.Count());
            Assert.AreEqual("aw:State", documentawPurchaseOrders0awPurchaseOrder5awAddress1awState7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress1awState7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awState7Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress1awState7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1awState7Text0.NodeType);
            Assert.AreEqual("WA", documentawPurchaseOrders0awPurchaseOrder5awAddress1awState7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1Text8 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1Text8.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress1Text8.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awZip9 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress1awZip9.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress1awZip9.Attributes.Count());
            Assert.AreEqual("aw:Zip", documentawPurchaseOrders0awPurchaseOrder5awAddress1awZip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress1awZip9.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awZip9Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress1awZip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1awZip9Text0.NodeType);
            Assert.AreEqual("98112", documentawPurchaseOrders0awPurchaseOrder5awAddress1awZip9Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1Text10 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1Text10.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress1Text10.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awCountry11 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress1awCountry11.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress1awCountry11.Attributes.Count());
            Assert.AreEqual("aw:Country", documentawPurchaseOrders0awPurchaseOrder5awAddress1awCountry11.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress1awCountry11.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1awCountry11Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress1awCountry11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1awCountry11Text0.NodeType);
            Assert.AreEqual("USA", documentawPurchaseOrders0awPurchaseOrder5awAddress1awCountry11Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress1Text12 = documentawPurchaseOrders0awPurchaseOrder5awAddress1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress1Text12.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder5awAddress1Text12.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5Text2 = documentawPurchaseOrders0awPurchaseOrder5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5Text2.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder5Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3 = documentawPurchaseOrders0awPurchaseOrder5.ChildNodes[3] as IElement;
            Assert.AreEqual(13, documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress3.Attributes.Count());
            Assert.AreEqual("aw:Address", documentawPurchaseOrders0awPurchaseOrder5awAddress3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress3.NodeType);
            Assert.AreEqual("Billing", documentawPurchaseOrders0awPurchaseOrder5awAddress3.GetAttribute("aw:Type"));

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awName1 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress3awName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress3awName1.Attributes.Count());
            Assert.AreEqual("aw:Name", documentawPurchaseOrders0awPurchaseOrder5awAddress3awName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress3awName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awName1Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress3awName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3awName1Text0.NodeType);
            Assert.AreEqual("Jessica Arnold", documentawPurchaseOrders0awPurchaseOrder5awAddress3awName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3Text2 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress3Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awStreet3 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress3awStreet3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress3awStreet3.Attributes.Count());
            Assert.AreEqual("aw:Street", documentawPurchaseOrders0awPurchaseOrder5awAddress3awStreet3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress3awStreet3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awStreet3Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress3awStreet3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3awStreet3Text0.NodeType);
            Assert.AreEqual("4055 Madison Ave", documentawPurchaseOrders0awPurchaseOrder5awAddress3awStreet3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3Text4 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3Text4.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress3Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awCity5 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress3awCity5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress3awCity5.Attributes.Count());
            Assert.AreEqual("aw:City", documentawPurchaseOrders0awPurchaseOrder5awAddress3awCity5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress3awCity5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awCity5Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress3awCity5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3awCity5Text0.NodeType);
            Assert.AreEqual("Buffalo", documentawPurchaseOrders0awPurchaseOrder5awAddress3awCity5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3Text6 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress3Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awState7 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress3awState7.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress3awState7.Attributes.Count());
            Assert.AreEqual("aw:State", documentawPurchaseOrders0awPurchaseOrder5awAddress3awState7.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress3awState7.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awState7Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress3awState7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3awState7Text0.NodeType);
            Assert.AreEqual("NY", documentawPurchaseOrders0awPurchaseOrder5awAddress3awState7Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3Text8 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3Text8.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress3Text8.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awZip9 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[9] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress3awZip9.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress3awZip9.Attributes.Count());
            Assert.AreEqual("aw:Zip", documentawPurchaseOrders0awPurchaseOrder5awAddress3awZip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress3awZip9.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awZip9Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress3awZip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3awZip9Text0.NodeType);
            Assert.AreEqual("98112", documentawPurchaseOrders0awPurchaseOrder5awAddress3awZip9Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3Text10 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3Text10.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awAddress3Text10.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awCountry11 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[11] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awAddress3awCountry11.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awAddress3awCountry11.Attributes.Count());
            Assert.AreEqual("aw:Country", documentawPurchaseOrders0awPurchaseOrder5awAddress3awCountry11.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awAddress3awCountry11.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3awCountry11Text0 = documentawPurchaseOrders0awPurchaseOrder5awAddress3awCountry11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3awCountry11Text0.NodeType);
            Assert.AreEqual("USA", documentawPurchaseOrders0awPurchaseOrder5awAddress3awCountry11Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awAddress3Text12 = documentawPurchaseOrders0awPurchaseOrder5awAddress3.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awAddress3Text12.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder5awAddress3Text12.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5Text4 = documentawPurchaseOrders0awPurchaseOrder5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5Text4.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder5Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5 = documentawPurchaseOrders0awPurchaseOrder5.ChildNodes[5] as IElement;
            Assert.AreEqual(5, documentawPurchaseOrders0awPurchaseOrder5awItems5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awItems5.Attributes.Count());
            Assert.AreEqual("aw:Items", documentawPurchaseOrders0awPurchaseOrder5awItems5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awItems5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5Text0 = documentawPurchaseOrders0awPurchaseOrder5awItems5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5Text0.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awItems5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1 = documentawPurchaseOrders0awPurchaseOrder5awItems5.ChildNodes[1] as IElement;
            Assert.AreEqual(7, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.Attributes.Count());
            Assert.AreEqual("aw:Item", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.NodeType);
            Assert.AreEqual("898-AZ", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.GetAttribute("aw:PartNumber"));

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text0 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text0.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awProductName1 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awProductName1.Attributes.Count());
            Assert.AreEqual("aw:ProductName", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awProductName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awProductName1Text0 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awProductName1Text0.NodeType);
            Assert.AreEqual("Computer Keyboard", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awProductName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text2 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text2.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awQuantity3 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awQuantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awQuantity3.Attributes.Count());
            Assert.AreEqual("aw:Quantity", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awQuantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awQuantity3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awQuantity3Text0 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awQuantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awQuantity3Text0.NodeType);
            Assert.AreEqual("1", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awQuantity3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text4 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text4.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awUSPrice5 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awUSPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awUSPrice5.Attributes.Count());
            Assert.AreEqual("aw:USPrice", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awUSPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awUSPrice5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awUSPrice5Text0 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awUSPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awUSPrice5Text0.NodeType);
            Assert.AreEqual("29.99", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1awUSPrice5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text6 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem1Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5Text2 = documentawPurchaseOrders0awPurchaseOrder5awItems5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5Text2.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awItems5Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3 = documentawPurchaseOrders0awPurchaseOrder5awItems5.ChildNodes[3] as IElement;
            Assert.AreEqual(7, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.ChildNodes.Length);
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.Attributes.Count());
            Assert.AreEqual("aw:Item", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.NodeType);
            Assert.AreEqual("898-AM", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.GetAttribute("aw:PartNumber"));

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text0 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text0.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awProductName1 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awProductName1.Attributes.Count());
            Assert.AreEqual("aw:ProductName", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awProductName1.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awProductName1Text0 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awProductName1Text0.NodeType);
            Assert.AreEqual("Wireless Mouse", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awProductName1Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text2 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text2.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text2.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awQuantity3 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awQuantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awQuantity3.Attributes.Count());
            Assert.AreEqual("aw:Quantity", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awQuantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awQuantity3.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awQuantity3Text0 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awQuantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awQuantity3Text0.NodeType);
            Assert.AreEqual("1", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awQuantity3Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text4 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text4.NodeType);
            Assert.AreEqual("\n        ", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awUSPrice5 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awUSPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awUSPrice5.Attributes.Count());
            Assert.AreEqual("aw:USPrice", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awUSPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awUSPrice5.NodeType);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awUSPrice5Text0 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awUSPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awUSPrice5Text0.NodeType);
            Assert.AreEqual("14.99", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3awUSPrice5Text0.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text6 = documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text6.NodeType);
            Assert.AreEqual("\n      ", documentawPurchaseOrders0awPurchaseOrder5awItems5awItem3Text6.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5awItems5Text4 = documentawPurchaseOrders0awPurchaseOrder5awItems5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5awItems5Text4.NodeType);
            Assert.AreEqual("\n    ", documentawPurchaseOrders0awPurchaseOrder5awItems5Text4.TextContent);

            var documentawPurchaseOrders0awPurchaseOrder5Text6 = documentawPurchaseOrders0awPurchaseOrder5.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0awPurchaseOrder5Text6.NodeType);
            Assert.AreEqual("\n  ", documentawPurchaseOrders0awPurchaseOrder5Text6.TextContent);

            var documentawPurchaseOrders0Text6 = documentawPurchaseOrders0.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentawPurchaseOrders0Text6.NodeType);
            Assert.AreEqual("\n", documentawPurchaseOrders0Text6.TextContent);
        }

        [Test]
        public void XmlTestConfiguration()
        {
            var document = (@"<?xml version=""1.0""?>
<Tests>
  <Test TestId=""0001"" TestType=""CMD"">
    <Name>Convert number to string</Name>
    <CommandLine>Examp1.EXE</CommandLine>
    <Input>1</Input>
    <Output>One</Output>
  </Test>
  <Test TestId=""0002"" TestType=""CMD"">
    <Name>Find succeeding characters</Name>
    <CommandLine>Examp2.EXE</CommandLine>
    <Input>abc</Input>
    <Output>def</Output>
  </Test>
  <Test TestId=""0003"" TestType=""GUI"">
    <Name>Convert multiple numbers to strings</Name>
    <CommandLine>Examp2.EXE /Verbose</CommandLine>
    <Input>123</Input>
    <Output>One Two Three</Output>
  </Test>
  <Test TestId=""0004"" TestType=""GUI"">
    <Name>Find correlated key</Name>
    <CommandLine>Examp3.EXE</CommandLine>
    <Input>a1</Input>
    <Output>b1</Output>
  </Test>
  <Test TestId=""0005"" TestType=""GUI"">
    <Name>Count characters</Name>
    <CommandLine>FinalExamp.EXE</CommandLine>
    <Input>This is a test</Input>
    <Output>14</Output>
  </Test>
  <Test TestId=""0006"" TestType=""GUI"">
    <Name>Another Test</Name>
    <CommandLine>Examp2.EXE</CommandLine>
    <Input>Test Input</Input>
    <Output>10</Output>
  </Test>
</Tests>").ToXmlDocument();

            var documentTests0 = document.ChildNodes[0] as IElement;
            Assert.AreEqual(13, documentTests0.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0.Attributes.Count());
            Assert.AreEqual("Tests", documentTests0.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0.NodeType);

            var documentTests0Text0 = documentTests0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Text0.NodeType);
            Assert.AreEqual("\n  ", documentTests0Text0.TextContent);

            var documentTests0Test1 = documentTests0.ChildNodes[1] as IElement;
            Assert.AreEqual(9, documentTests0Test1.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test1.Attributes.Count());
            Assert.AreEqual("Test", documentTests0Test1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test1.NodeType);
            Assert.AreEqual("0001", documentTests0Test1.GetAttribute("TestId"));
            Assert.AreEqual("CMD", documentTests0Test1.GetAttribute("TestType"));

            var documentTests0Test1Text0 = documentTests0Test1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test1Text0.TextContent);

            var documentTests0Test1Name1 = documentTests0Test1.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentTests0Test1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test1Name1.Attributes.Count());
            Assert.AreEqual("Name", documentTests0Test1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test1Name1.NodeType);

            var documentTests0Test1Name1Text0 = documentTests0Test1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Name1Text0.NodeType);
            Assert.AreEqual("Convert number to string", documentTests0Test1Name1Text0.TextContent);

            var documentTests0Test1Text2 = documentTests0Test1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test1Text2.TextContent);

            var documentTests0Test1CommandLine3 = documentTests0Test1.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentTests0Test1CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test1CommandLine3.Attributes.Count());
            Assert.AreEqual("CommandLine", documentTests0Test1CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test1CommandLine3.NodeType);

            var documentTests0Test1CommandLine3Text0 = documentTests0Test1CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test1CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp1.EXE", documentTests0Test1CommandLine3Text0.TextContent);

            var documentTests0Test1Text4 = documentTests0Test1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test1Text4.TextContent);

            var documentTests0Test1Input5 = documentTests0Test1.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentTests0Test1Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test1Input5.Attributes.Count());
            Assert.AreEqual("Input", documentTests0Test1Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test1Input5.NodeType);

            var documentTests0Test1Input5Text0 = documentTests0Test1Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Input5Text0.NodeType);
            Assert.AreEqual("1", documentTests0Test1Input5Text0.TextContent);

            var documentTests0Test1Text6 = documentTests0Test1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test1Text6.TextContent);

            var documentTests0Test1Output7 = documentTests0Test1.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentTests0Test1Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test1Output7.Attributes.Count());
            Assert.AreEqual("Output", documentTests0Test1Output7.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test1Output7.NodeType);

            var documentTests0Test1Output7Text0 = documentTests0Test1Output7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Output7Text0.NodeType);
            Assert.AreEqual("One", documentTests0Test1Output7Text0.TextContent);

            var documentTests0Test1Text8 = documentTests0Test1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Text8.NodeType);
            Assert.AreEqual("\n  ", documentTests0Test1Text8.TextContent);

            var documentTests0Text2 = documentTests0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Text2.NodeType);
            Assert.AreEqual("\n  ", documentTests0Text2.TextContent);

            var documentTests0Test3 = documentTests0.ChildNodes[3] as IElement;
            Assert.AreEqual(9, documentTests0Test3.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test3.Attributes.Count());
            Assert.AreEqual("Test", documentTests0Test3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test3.NodeType);
            Assert.AreEqual("0002", documentTests0Test3.GetAttribute("TestId"));
            Assert.AreEqual("CMD", documentTests0Test3.GetAttribute("TestType"));

            var documentTests0Test3Text0 = documentTests0Test3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test3Text0.TextContent);

            var documentTests0Test3Name1 = documentTests0Test3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentTests0Test3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test3Name1.Attributes.Count());
            Assert.AreEqual("Name", documentTests0Test3Name1.NodeName);

            Assert.AreEqual(NodeType.Element, documentTests0Test3Name1.NodeType);
            var documentTests0Test3Name1Text0 = documentTests0Test3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Name1Text0.NodeType);
            Assert.AreEqual("Find succeeding characters", documentTests0Test3Name1Text0.TextContent);

            var documentTests0Test3Text2 = documentTests0Test3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test3Text2.TextContent);

            var documentTests0Test3CommandLine3 = documentTests0Test3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentTests0Test3CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test3CommandLine3.Attributes.Count());
            Assert.AreEqual("CommandLine", documentTests0Test3CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test3CommandLine3.NodeType);

            var documentTests0Test3CommandLine3Text0 = documentTests0Test3CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test3CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp2.EXE", documentTests0Test3CommandLine3Text0.TextContent);

            var documentTests0Test3Text4 = documentTests0Test3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test3Text4.TextContent);

            var documentTests0Test3Input5 = documentTests0Test3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentTests0Test3Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test3Input5.Attributes.Count());
            Assert.AreEqual("Input", documentTests0Test3Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test3Input5.NodeType);

            var documentTests0Test3Input5Text0 = documentTests0Test3Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Input5Text0.NodeType);
            Assert.AreEqual("abc", documentTests0Test3Input5Text0.TextContent);

            var documentTests0Test3Text6 = documentTests0Test3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test3Text6.TextContent);

            var documentTests0Test3Output7 = documentTests0Test3.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentTests0Test3Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test3Output7.Attributes.Count());
            Assert.AreEqual("Output", documentTests0Test3Output7.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test3Output7.NodeType);

            var documentTests0Test3Output7Text0 = documentTests0Test3Output7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Output7Text0.NodeType);
            Assert.AreEqual("def", documentTests0Test3Output7Text0.TextContent);

            var documentTests0Test3Text8 = documentTests0Test3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Text8.NodeType);
            Assert.AreEqual("\n  ", documentTests0Test3Text8.TextContent);

            var documentTests0Text4 = documentTests0.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Text4.NodeType);
            Assert.AreEqual("\n  ", documentTests0Text4.TextContent);

            var documentTests0Test5 = documentTests0.ChildNodes[5] as IElement;
            Assert.AreEqual(9, documentTests0Test5.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test5.Attributes.Count());
            Assert.AreEqual("Test", documentTests0Test5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test5.NodeType);
            Assert.AreEqual("0003", documentTests0Test5.GetAttribute("TestId"));
            Assert.AreEqual("GUI", documentTests0Test5.GetAttribute("TestType"));

            var documentTests0Test5Text0 = documentTests0Test5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test5Text0.TextContent);

            var documentTests0Test5Name1 = documentTests0Test5.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentTests0Test5Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test5Name1.Attributes.Count());
            Assert.AreEqual("Name", documentTests0Test5Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test5Name1.NodeType);

            var documentTests0Test5Name1Text0 = documentTests0Test5Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Name1Text0.NodeType);
            Assert.AreEqual("Convert multiple numbers to strings", documentTests0Test5Name1Text0.TextContent);

            var documentTests0Test5Text2 = documentTests0Test5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test5Text2.TextContent);

            var documentTests0Test5CommandLine3 = documentTests0Test5.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentTests0Test5CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test5CommandLine3.Attributes.Count());
            Assert.AreEqual("CommandLine", documentTests0Test5CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test5CommandLine3.NodeType);

            var documentTests0Test5CommandLine3Text0 = documentTests0Test5CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test5CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp2.EXE /Verbose", documentTests0Test5CommandLine3Text0.TextContent);

            var documentTests0Test5Text4 = documentTests0Test5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test5Text4.TextContent);

            var documentTests0Test5Input5 = documentTests0Test5.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentTests0Test5Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test5Input5.Attributes.Count());
            Assert.AreEqual("Input", documentTests0Test5Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test5Input5.NodeType);

            var documentTests0Test5Input5Text0 = documentTests0Test5Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Input5Text0.NodeType);
            Assert.AreEqual("123", documentTests0Test5Input5Text0.TextContent);

            var documentTests0Test5Text6 = documentTests0Test5.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test5Text6.TextContent);

            var documentTests0Test5Output7 = documentTests0Test5.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentTests0Test5Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test5Output7.Attributes.Count());
            Assert.AreEqual("Output", documentTests0Test5Output7.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test5Output7.NodeType);

            var documentTests0Test5Output7Text0 = documentTests0Test5Output7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Output7Text0.NodeType);
            Assert.AreEqual("One Two Three", documentTests0Test5Output7Text0.TextContent);

            var documentTests0Test5Text8 = documentTests0Test5.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Text8.NodeType);
            Assert.AreEqual("\n  ", documentTests0Test5Text8.TextContent);

            var documentTests0Text6 = documentTests0.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Text6.NodeType);
            Assert.AreEqual("\n  ", documentTests0Text6.TextContent);

            var documentTests0Test7 = documentTests0.ChildNodes[7] as IElement;
            Assert.AreEqual(9, documentTests0Test7.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test7.Attributes.Count());
            Assert.AreEqual("Test", documentTests0Test7.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test7.NodeType);
            Assert.AreEqual("0004", documentTests0Test7.GetAttribute("TestId"));
            Assert.AreEqual("GUI", documentTests0Test7.GetAttribute("TestType"));

            var documentTests0Test7Text0 = documentTests0Test7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test7Text0.TextContent);

            var documentTests0Test7Name1 = documentTests0Test7.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentTests0Test7Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test7Name1.Attributes.Count());
            Assert.AreEqual("Name", documentTests0Test7Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test7Name1.NodeType);

            var documentTests0Test7Name1Text0 = documentTests0Test7Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Name1Text0.NodeType);
            Assert.AreEqual("Find correlated key", documentTests0Test7Name1Text0.TextContent);

            var documentTests0Test7Text2 = documentTests0Test7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test7Text2.TextContent);

            var documentTests0Test7CommandLine3 = documentTests0Test7.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentTests0Test7CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test7CommandLine3.Attributes.Count());
            Assert.AreEqual("CommandLine", documentTests0Test7CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test7CommandLine3.NodeType);

            var documentTests0Test7CommandLine3Text0 = documentTests0Test7CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test7CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp3.EXE", documentTests0Test7CommandLine3Text0.TextContent);

            var documentTests0Test7Text4 = documentTests0Test7.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test7Text4.TextContent);

            var documentTests0Test7Input5 = documentTests0Test7.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentTests0Test7Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test7Input5.Attributes.Count());
            Assert.AreEqual("Input", documentTests0Test7Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test7Input5.NodeType);

            var documentTests0Test7Input5Text0 = documentTests0Test7Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Input5Text0.NodeType);
            Assert.AreEqual("a1", documentTests0Test7Input5Text0.TextContent);

            var documentTests0Test7Text6 = documentTests0Test7.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test7Text6.TextContent);

            var documentTests0Test7Output7 = documentTests0Test7.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentTests0Test7Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test7Output7.Attributes.Count());
            Assert.AreEqual("Output", documentTests0Test7Output7.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test7Output7.NodeType);

            var documentTests0Test7Output7Text0 = documentTests0Test7Output7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Output7Text0.NodeType);
            Assert.AreEqual("b1", documentTests0Test7Output7Text0.TextContent);

            var documentTests0Test7Text8 = documentTests0Test7.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Text8.NodeType);
            Assert.AreEqual("\n  ", documentTests0Test7Text8.TextContent);

            var documentTests0Text8 = documentTests0.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentTests0Text8.NodeType);
            Assert.AreEqual("\n  ", documentTests0Text8.TextContent);

            var documentTests0Test9 = documentTests0.ChildNodes[9] as IElement;
            Assert.AreEqual(9, documentTests0Test9.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test9.Attributes.Count());
            Assert.AreEqual("Test", documentTests0Test9.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test9.NodeType);
            Assert.AreEqual("0005", documentTests0Test9.GetAttribute("TestId"));
            Assert.AreEqual("GUI", documentTests0Test9.GetAttribute("TestType"));

            var documentTests0Test9Text0 = documentTests0Test9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test9Text0.TextContent);

            var documentTests0Test9Name1 = documentTests0Test9.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentTests0Test9Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test9Name1.Attributes.Count());
            Assert.AreEqual("Name", documentTests0Test9Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test9Name1.NodeType);

            var documentTests0Test9Name1Text0 = documentTests0Test9Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Name1Text0.NodeType);
            Assert.AreEqual("Count characters", documentTests0Test9Name1Text0.TextContent);

            var documentTests0Test9Text2 = documentTests0Test9.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test9Text2.TextContent);

            var documentTests0Test9CommandLine3 = documentTests0Test9.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentTests0Test9CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test9CommandLine3.Attributes.Count());
            Assert.AreEqual("CommandLine", documentTests0Test9CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test9CommandLine3.NodeType);

            var documentTests0Test9CommandLine3Text0 = documentTests0Test9CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test9CommandLine3Text0.NodeType);
            Assert.AreEqual("FinalExamp.EXE", documentTests0Test9CommandLine3Text0.TextContent);

            var documentTests0Test9Text4 = documentTests0Test9.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test9Text4.TextContent);

            var documentTests0Test9Input5 = documentTests0Test9.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentTests0Test9Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test9Input5.Attributes.Count());
            Assert.AreEqual("Input", documentTests0Test9Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test9Input5.NodeType);

            var documentTests0Test9Input5Text0 = documentTests0Test9Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Input5Text0.NodeType);
            Assert.AreEqual("This is a test", documentTests0Test9Input5Text0.TextContent);

            var documentTests0Test9Text6 = documentTests0Test9.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test9Text6.TextContent);

            var documentTests0Test9Output7 = documentTests0Test9.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentTests0Test9Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test9Output7.Attributes.Count());
            Assert.AreEqual("Output", documentTests0Test9Output7.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test9Output7.NodeType);

            var documentTests0Test9Output7Text0 = documentTests0Test9Output7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Output7Text0.NodeType);
            Assert.AreEqual("14", documentTests0Test9Output7Text0.TextContent);

            var documentTests0Test9Text8 = documentTests0Test9.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Text8.NodeType);
            Assert.AreEqual("\n  ", documentTests0Test9Text8.TextContent);

            var documentTests0Text10 = documentTests0.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentTests0Text10.NodeType);
            Assert.AreEqual("\n  ", documentTests0Text10.TextContent);

            var documentTests0Test11 = documentTests0.ChildNodes[11] as IElement;
            Assert.AreEqual(9, documentTests0Test11.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test11.Attributes.Count());
            Assert.AreEqual("Test", documentTests0Test11.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test11.NodeType);
            Assert.AreEqual("0006", documentTests0Test11.GetAttribute("TestId"));
            Assert.AreEqual("GUI", documentTests0Test11.GetAttribute("TestType"));

            var documentTests0Test11Text0 = documentTests0Test11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test11Text0.TextContent);

            var documentTests0Test11Name1 = documentTests0Test11.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentTests0Test11Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test11Name1.Attributes.Count());
            Assert.AreEqual("Name", documentTests0Test11Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test11Name1.NodeType);

            var documentTests0Test11Name1Text0 = documentTests0Test11Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Name1Text0.NodeType);
            Assert.AreEqual("Another Test", documentTests0Test11Name1Text0.TextContent);

            var documentTests0Test11Text2 = documentTests0Test11.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test11Text2.TextContent);

            var documentTests0Test11CommandLine3 = documentTests0Test11.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentTests0Test11CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test11CommandLine3.Attributes.Count());
            Assert.AreEqual("CommandLine", documentTests0Test11CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test11CommandLine3.NodeType);

            var documentTests0Test11CommandLine3Text0 = documentTests0Test11CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test11CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp2.EXE", documentTests0Test11CommandLine3Text0.TextContent);

            var documentTests0Test11Text4 = documentTests0Test11.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test11Text4.TextContent);

            var documentTests0Test11Input5 = documentTests0Test11.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentTests0Test11Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test11Input5.Attributes.Count());
            Assert.AreEqual("Input", documentTests0Test11Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test11Input5.NodeType);

            var documentTests0Test11Input5Text0 = documentTests0Test11Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Input5Text0.NodeType);
            Assert.AreEqual("Test Input", documentTests0Test11Input5Text0.TextContent);

            var documentTests0Test11Text6 = documentTests0Test11.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test11Text6.TextContent);

            var documentTests0Test11Output7 = documentTests0Test11.ChildNodes[7] as IElement;
            Assert.AreEqual(1, documentTests0Test11Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test11Output7.Attributes.Count());
            Assert.AreEqual("Output", documentTests0Test11Output7.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test11Output7.NodeType);

            var documentTests0Test11Output7Text0 = documentTests0Test11Output7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Output7Text0.NodeType);
            Assert.AreEqual("10", documentTests0Test11Output7Text0.TextContent);

            var documentTests0Test11Text8 = documentTests0Test11.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Text8.NodeType);
            Assert.AreEqual("\n  ", documentTests0Test11Text8.TextContent);

            var documentTests0Text12 = documentTests0.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentTests0Text12.NodeType);
            Assert.AreEqual("\n", documentTests0Text12.TextContent);
        }

        [Test]
        public void XmlCustomersAndOrders()
        {
            var document = (@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema"">
  <xs:element name='Root'>
    <xs:complexType>
      <xs:sequence>
        <xs:element name='Customers'>
          <xs:complexType>
            <xs:sequence>
              <xs:element name='Customer' type='CustomerType' minOccurs='0' maxOccurs='unbounded' />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name='Orders'>
          <xs:complexType>
            <xs:sequence>
              <xs:element name='Order' type='OrderType' minOccurs='0' maxOccurs='unbounded' />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
    <xs:key name='CustomerIDKey'>
      <xs:selector xpath='Customers/Customer'/>
      <xs:field xpath='@CustomerID'/>
    </xs:key>
    <xs:keyref name='CustomerIDKeyRef' refer='CustomerIDKey'>
      <xs:selector xpath='Orders/Order'/>
      <xs:field xpath='CustomerID'/>
    </xs:keyref>
  </xs:element>
  <xs:complexType name='CustomerType'>
    <xs:sequence>
      <xs:element name='CompanyName' type='xs:string'/>
      <xs:element name='ContactName' type='xs:string'/>
      <xs:element name='ContactTitle' type='xs:string'/>
      <xs:element name='Phone' type='xs:string'/>
      <xs:element name='Fax' minOccurs='0' type='xs:string'/>
      <xs:element name='FullAddress' type='AddressType'/>
    </xs:sequence>
    <xs:attribute name='CustomerID' type='xs:token'/>
  </xs:complexType>
  <xs:complexType name='AddressType'>
    <xs:sequence>
      <xs:element name='Address' type='xs:string'/>
      <xs:element name='City' type='xs:string'/>
      <xs:element name='Region' type='xs:string'/>
      <xs:element name='PostalCode' type='xs:string' />
      <xs:element name='Country' type='xs:string'/>
    </xs:sequence>
    <xs:attribute name='CustomerID' type='xs:token'/>
  </xs:complexType>
  <xs:complexType name='OrderType'>
    <xs:sequence>
      <xs:element name='CustomerID' type='xs:token'/>
      <xs:element name='EmployeeID' type='xs:token'/>
      <xs:element name='OrderDate' type='xs:dateTime'/>
      <xs:element name='RequiredDate' type='xs:dateTime'/>
      <xs:element name='ShipInfo' type='ShipInfoType'/>
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name='ShipInfoType'>
    <xs:sequence>
      <xs:element name='ShipVia' type='xs:integer'/>
      <xs:element name='Freight' type='xs:decimal'/>
      <xs:element name='ShipName' type='xs:string'/>
      <xs:element name='ShipAddress' type='xs:string'/>
      <xs:element name='ShipCity' type='xs:string'/>
      <xs:element name='ShipRegion' type='xs:string'/>
      <xs:element name='ShipPostalCode' type='xs:string'/>
      <xs:element name='ShipCountry' type='xs:string'/>
    </xs:sequence>
    <xs:attribute name='ShippedDate' type='xs:dateTime'/>
  </xs:complexType>
</xs:schema>").ToXmlDocument();
            var documentxsschema0 = document.ChildNodes[0] as IElement;
            Assert.AreEqual(11, documentxsschema0.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0.Attributes.Count());
            Assert.AreEqual("xs:schema", documentxsschema0.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0.NodeType);
            Assert.AreEqual("http://www.w3.org/2001/XMLSchema", documentxsschema0.GetAttribute("xmlns:xs"));

            var documentxsschema0Text0 = documentxsschema0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0Text0.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0Text0.TextContent);

            var documentxsschema0xselement1 = documentxsschema0.ChildNodes[1] as IElement;
            Assert.AreEqual(7, documentxsschema0xselement1.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xselement1.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xselement1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1.NodeType);
            Assert.AreEqual("Root", documentxsschema0xselement1.GetAttribute("name"));

            var documentxsschema0xselement1Text0 = documentxsschema0xselement1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1Text0.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xselement1Text0.TextContent);

            var documentxsschema0xselement1xscomplexType1 = documentxsschema0xselement1.ChildNodes[1] as IElement;
            Assert.AreEqual(3, documentxsschema0xselement1xscomplexType1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xselement1xscomplexType1.Attributes.Count());
            Assert.AreEqual("xs:complexType", documentxsschema0xselement1xscomplexType1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1.NodeType);

            var documentxsschema0xselement1xscomplexType1Text0 = documentxsschema0xselement1xscomplexType1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1Text0.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xselement1xscomplexType1Text0.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1 = documentxsschema0xselement1xscomplexType1.ChildNodes[1] as IElement;
            Assert.AreEqual(5, documentxsschema0xselement1xscomplexType1xssequence1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xselement1xscomplexType1xssequence1.Attributes.Count());
            Assert.AreEqual("xs:sequence", documentxsschema0xselement1xscomplexType1xssequence1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1xssequence1.NodeType);

            var documentxsschema0xselement1xscomplexType1xssequence1Text0 = documentxsschema0xselement1xscomplexType1xssequence1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1Text0.NodeType);
            Assert.AreEqual("\n        ", documentxsschema0xselement1xscomplexType1xssequence1Text0.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1 = documentxsschema0xselement1xscomplexType1xssequence1.ChildNodes[1] as IElement;
            Assert.AreEqual(3, documentxsschema0xselement1xscomplexType1xssequence1xselement1.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xselement1xscomplexType1xssequence1xselement1.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xselement1xscomplexType1xssequence1xselement1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1xssequence1xselement1.NodeType);
            Assert.AreEqual("Customers", documentxsschema0xselement1xscomplexType1xssequence1xselement1.GetAttribute("name"));

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1Text0 = documentxsschema0xselement1xscomplexType1xssequence1xselement1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement1Text0.NodeType);
            Assert.AreEqual("\n          ", documentxsschema0xselement1xscomplexType1xssequence1xselement1Text0.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1 = documentxsschema0xselement1xscomplexType1xssequence1xselement1.ChildNodes[1] as IElement;
            Assert.AreEqual(3, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1.Attributes.Count());
            Assert.AreEqual("xs:complexType", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1.NodeType);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1Text0 = documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1Text0.NodeType);
            Assert.AreEqual("\n            ", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1Text0.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1 = documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1.ChildNodes[1] as IElement;
            Assert.AreEqual(3, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1.Attributes.Count());
            Assert.AreEqual("xs:sequence", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1.NodeType);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1Text0 = documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1Text0.NodeType);
            Assert.AreEqual("\n              ", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1Text0.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1xselement1 = documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1.ChildNodes[1] as IElement;
            Assert.AreEqual(0, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1xselement1.ChildNodes.Length);
            Assert.AreEqual(4, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1xselement1.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1xselement1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1xselement1.NodeType);
            Assert.AreEqual("Customer", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1xselement1.GetAttribute("name"));
            Assert.AreEqual("CustomerType", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1xselement1.GetAttribute("type"));
            Assert.AreEqual("0", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1xselement1.GetAttribute("minOccurs"));
            Assert.AreEqual("unbounded", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1xselement1.GetAttribute("maxOccurs"));

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1Text2 = documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1Text2.NodeType);
            Assert.AreEqual("\n            ", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1xssequence1Text2.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1Text2 = documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1Text2.NodeType);
            Assert.AreEqual("\n          ", documentxsschema0xselement1xscomplexType1xssequence1xselement1xscomplexType1Text2.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement1Text2 = documentxsschema0xselement1xscomplexType1xssequence1xselement1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement1Text2.NodeType);
            Assert.AreEqual("\n        ", documentxsschema0xselement1xscomplexType1xssequence1xselement1Text2.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1Text2 = documentxsschema0xselement1xscomplexType1xssequence1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1Text2.NodeType);
            Assert.AreEqual("\n        ", documentxsschema0xselement1xscomplexType1xssequence1Text2.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3 = documentxsschema0xselement1xscomplexType1xssequence1.ChildNodes[3] as IElement;
            Assert.AreEqual(3, documentxsschema0xselement1xscomplexType1xssequence1xselement3.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xselement1xscomplexType1xssequence1xselement3.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xselement1xscomplexType1xssequence1xselement3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1xssequence1xselement3.NodeType);
            Assert.AreEqual("Orders", documentxsschema0xselement1xscomplexType1xssequence1xselement3.GetAttribute("name"));

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3Text0 = documentxsschema0xselement1xscomplexType1xssequence1xselement3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement3Text0.NodeType);
            Assert.AreEqual("\n          ", documentxsschema0xselement1xscomplexType1xssequence1xselement3Text0.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1 = documentxsschema0xselement1xscomplexType1xssequence1xselement3.ChildNodes[1] as IElement;
            Assert.AreEqual(3, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1.Attributes.Count());
            Assert.AreEqual("xs:complexType", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1.NodeType);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1Text0 = documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1Text0.NodeType);
            Assert.AreEqual("\n            ", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1Text0.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1 = documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1.ChildNodes[1] as IElement;
            Assert.AreEqual(3, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1.Attributes.Count());
            Assert.AreEqual("xs:sequence", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1.NodeType);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1Text0 = documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1Text0.NodeType);
            Assert.AreEqual("\n              ", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1Text0.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1xselement1 = documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1.ChildNodes[1] as IElement;
            Assert.AreEqual(0, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1xselement1.ChildNodes.Length);
            Assert.AreEqual(4, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1xselement1.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1xselement1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1xselement1.NodeType);
            Assert.AreEqual("Order", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1xselement1.GetAttribute("name"));
            Assert.AreEqual("OrderType", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1xselement1.GetAttribute("type"));
            Assert.AreEqual("0", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1xselement1.GetAttribute("minOccurs"));
            Assert.AreEqual("unbounded", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1xselement1.GetAttribute("maxOccurs"));

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1Text2 = documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1Text2.NodeType);
            Assert.AreEqual("\n            ", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1xssequence1Text2.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1Text2 = documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1Text2.NodeType);
            Assert.AreEqual("\n          ", documentxsschema0xselement1xscomplexType1xssequence1xselement3xscomplexType1Text2.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1xselement3Text2 = documentxsschema0xselement1xscomplexType1xssequence1xselement3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1xselement3Text2.NodeType);
            Assert.AreEqual("\n        ", documentxsschema0xselement1xscomplexType1xssequence1xselement3Text2.TextContent);

            var documentxsschema0xselement1xscomplexType1xssequence1Text4 = documentxsschema0xselement1xscomplexType1xssequence1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1xssequence1Text4.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xselement1xscomplexType1xssequence1Text4.TextContent);

            var documentxsschema0xselement1xscomplexType1Text2 = documentxsschema0xselement1xscomplexType1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xscomplexType1Text2.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xselement1xscomplexType1Text2.TextContent);

            var documentxsschema0xselement1Text2 = documentxsschema0xselement1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1Text2.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xselement1Text2.TextContent);

            var documentxsschema0xselement1xskey3 = documentxsschema0xselement1.ChildNodes[3] as IElement;
            Assert.AreEqual(5, documentxsschema0xselement1xskey3.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xselement1xskey3.Attributes.Count());
            Assert.AreEqual("xs:key", documentxsschema0xselement1xskey3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xskey3.NodeType);
            Assert.AreEqual("CustomerIDKey", documentxsschema0xselement1xskey3.GetAttribute("name"));

            var documentxsschema0xselement1xskey3Text0 = documentxsschema0xselement1xskey3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xskey3Text0.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xselement1xskey3Text0.TextContent);

            var documentxsschema0xselement1xskey3xsselector1 = documentxsschema0xselement1xskey3.ChildNodes[1] as IElement;
            Assert.AreEqual(0, documentxsschema0xselement1xskey3xsselector1.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xselement1xskey3xsselector1.Attributes.Count());
            Assert.AreEqual("xs:selector", documentxsschema0xselement1xskey3xsselector1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xskey3xsselector1.NodeType);
            Assert.AreEqual("Customers/Customer", documentxsschema0xselement1xskey3xsselector1.GetAttribute("xpath"));

            var documentxsschema0xselement1xskey3Text2 = documentxsschema0xselement1xskey3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xskey3Text2.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xselement1xskey3Text2.TextContent);

            var documentxsschema0xselement1xskey3xsfield3 = documentxsschema0xselement1xskey3.ChildNodes[3] as IElement;
            Assert.AreEqual(0, documentxsschema0xselement1xskey3xsfield3.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xselement1xskey3xsfield3.Attributes.Count());
            Assert.AreEqual("xs:field", documentxsschema0xselement1xskey3xsfield3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xskey3xsfield3.NodeType);
            Assert.AreEqual("@CustomerID", documentxsschema0xselement1xskey3xsfield3.GetAttribute("xpath"));

            var documentxsschema0xselement1xskey3Text4 = documentxsschema0xselement1xskey3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xskey3Text4.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xselement1xskey3Text4.TextContent);

            var documentxsschema0xselement1Text4 = documentxsschema0xselement1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1Text4.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xselement1Text4.TextContent);

            var documentxsschema0xselement1xskeyref5 = documentxsschema0xselement1.ChildNodes[5] as IElement;
            Assert.AreEqual(5, documentxsschema0xselement1xskeyref5.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xselement1xskeyref5.Attributes.Count());
            Assert.AreEqual("xs:keyref", documentxsschema0xselement1xskeyref5.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xskeyref5.NodeType);
            Assert.AreEqual("CustomerIDKeyRef", documentxsschema0xselement1xskeyref5.GetAttribute("name"));
            Assert.AreEqual("CustomerIDKey", documentxsschema0xselement1xskeyref5.GetAttribute("refer"));

            var documentxsschema0xselement1xskeyref5Text0 = documentxsschema0xselement1xskeyref5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xskeyref5Text0.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xselement1xskeyref5Text0.TextContent);

            var documentxsschema0xselement1xskeyref5xsselector1 = documentxsschema0xselement1xskeyref5.ChildNodes[1] as IElement;
            Assert.AreEqual(0, documentxsschema0xselement1xskeyref5xsselector1.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xselement1xskeyref5xsselector1.Attributes.Count());
            Assert.AreEqual("xs:selector", documentxsschema0xselement1xskeyref5xsselector1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xskeyref5xsselector1.NodeType);
            Assert.AreEqual("Orders/Order", documentxsschema0xselement1xskeyref5xsselector1.GetAttribute("xpath"));

            var documentxsschema0xselement1xskeyref5Text2 = documentxsschema0xselement1xskeyref5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xskeyref5Text2.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xselement1xskeyref5Text2.TextContent);

            var documentxsschema0xselement1xskeyref5xsfield3 = documentxsschema0xselement1xskeyref5.ChildNodes[3] as IElement;
            Assert.AreEqual(0, documentxsschema0xselement1xskeyref5xsfield3.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xselement1xskeyref5xsfield3.Attributes.Count());
            Assert.AreEqual("xs:field", documentxsschema0xselement1xskeyref5xsfield3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xselement1xskeyref5xsfield3.NodeType);
            Assert.AreEqual("CustomerID", documentxsschema0xselement1xskeyref5xsfield3.GetAttribute("xpath"));

            var documentxsschema0xselement1xskeyref5Text4 = documentxsschema0xselement1xskeyref5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1xskeyref5Text4.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xselement1xskeyref5Text4.TextContent);

            var documentxsschema0xselement1Text6 = documentxsschema0xselement1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentxsschema0xselement1Text6.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0xselement1Text6.TextContent);

            var documentxsschema0Text2 = documentxsschema0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0Text2.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0Text2.TextContent);

            var documentxsschema0xscomplexType3 = documentxsschema0.ChildNodes[3] as IElement;
            Assert.AreEqual(5, documentxsschema0xscomplexType3.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xscomplexType3.Attributes.Count());
            Assert.AreEqual("xs:complexType", documentxsschema0xscomplexType3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType3.NodeType);
            Assert.AreEqual("CustomerType", documentxsschema0xscomplexType3.GetAttribute("name"));

            var documentxsschema0xscomplexType3Text0 = documentxsschema0xscomplexType3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3Text0.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType3Text0.TextContent);

            var documentxsschema0xscomplexType3xssequence1 = documentxsschema0xscomplexType3.ChildNodes[1] as IElement;
            Assert.AreEqual(13, documentxsschema0xscomplexType3xssequence1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xscomplexType3xssequence1.Attributes.Count());
            Assert.AreEqual("xs:sequence", documentxsschema0xscomplexType3xssequence1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType3xssequence1.NodeType);

            var documentxsschema0xscomplexType3xssequence1Text0 = documentxsschema0xscomplexType3xssequence1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3xssequence1Text0.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType3xssequence1Text0.TextContent);

            var documentxsschema0xscomplexType3xssequence1xselement1 = documentxsschema0xscomplexType3xssequence1.ChildNodes[1] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType3xssequence1xselement1.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType3xssequence1xselement1.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType3xssequence1xselement1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType3xssequence1xselement1.NodeType);
            Assert.AreEqual("CompanyName", documentxsschema0xscomplexType3xssequence1xselement1.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType3xssequence1xselement1.GetAttribute("type"));

            var documentxsschema0xscomplexType3xssequence1Text2 = documentxsschema0xscomplexType3xssequence1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3xssequence1Text2.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType3xssequence1Text2.TextContent);

            var documentxsschema0xscomplexType3xssequence1xselement3 = documentxsschema0xscomplexType3xssequence1.ChildNodes[3] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType3xssequence1xselement3.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType3xssequence1xselement3.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType3xssequence1xselement3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType3xssequence1xselement3.NodeType);
            Assert.AreEqual("ContactName", documentxsschema0xscomplexType3xssequence1xselement3.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType3xssequence1xselement3.GetAttribute("type"));

            var documentxsschema0xscomplexType3xssequence1Text4 = documentxsschema0xscomplexType3xssequence1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3xssequence1Text4.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType3xssequence1Text4.TextContent);

            var documentxsschema0xscomplexType3xssequence1xselement5 = documentxsschema0xscomplexType3xssequence1.ChildNodes[5] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType3xssequence1xselement5.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType3xssequence1xselement5.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType3xssequence1xselement5.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType3xssequence1xselement5.NodeType);
            Assert.AreEqual("ContactTitle", documentxsschema0xscomplexType3xssequence1xselement5.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType3xssequence1xselement5.GetAttribute("type"));

            var documentxsschema0xscomplexType3xssequence1Text6 = documentxsschema0xscomplexType3xssequence1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3xssequence1Text6.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType3xssequence1Text6.TextContent);

            var documentxsschema0xscomplexType3xssequence1xselement7 = documentxsschema0xscomplexType3xssequence1.ChildNodes[7] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType3xssequence1xselement7.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType3xssequence1xselement7.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType3xssequence1xselement7.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType3xssequence1xselement7.NodeType);
            Assert.AreEqual("Phone", documentxsschema0xscomplexType3xssequence1xselement7.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType3xssequence1xselement7.GetAttribute("type"));

            var documentxsschema0xscomplexType3xssequence1Text8 = documentxsschema0xscomplexType3xssequence1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3xssequence1Text8.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType3xssequence1Text8.TextContent);

            var documentxsschema0xscomplexType3xssequence1xselement9 = documentxsschema0xscomplexType3xssequence1.ChildNodes[9] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType3xssequence1xselement9.ChildNodes.Length);
            Assert.AreEqual(3, documentxsschema0xscomplexType3xssequence1xselement9.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType3xssequence1xselement9.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType3xssequence1xselement9.NodeType);
            Assert.AreEqual("Fax", documentxsschema0xscomplexType3xssequence1xselement9.GetAttribute("name"));
            Assert.AreEqual("0", documentxsschema0xscomplexType3xssequence1xselement9.GetAttribute("minOccurs"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType3xssequence1xselement9.GetAttribute("type"));

            var documentxsschema0xscomplexType3xssequence1Text10 = documentxsschema0xscomplexType3xssequence1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3xssequence1Text10.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType3xssequence1Text10.TextContent);

            var documentxsschema0xscomplexType3xssequence1xselement11 = documentxsschema0xscomplexType3xssequence1.ChildNodes[11] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType3xssequence1xselement11.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType3xssequence1xselement11.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType3xssequence1xselement11.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType3xssequence1xselement11.NodeType);
            Assert.AreEqual("FullAddress", documentxsschema0xscomplexType3xssequence1xselement11.GetAttribute("name"));
            Assert.AreEqual("AddressType", documentxsschema0xscomplexType3xssequence1xselement11.GetAttribute("type"));

            var documentxsschema0xscomplexType3xssequence1Text12 = documentxsschema0xscomplexType3xssequence1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3xssequence1Text12.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType3xssequence1Text12.TextContent);

            var documentxsschema0xscomplexType3Text2 = documentxsschema0xscomplexType3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3Text2.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType3Text2.TextContent);

            var documentxsschema0xscomplexType3xsattribute3 = documentxsschema0xscomplexType3.ChildNodes[3] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType3xsattribute3.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType3xsattribute3.Attributes.Count());
            Assert.AreEqual("xs:attribute", documentxsschema0xscomplexType3xsattribute3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType3xsattribute3.NodeType);
            Assert.AreEqual("CustomerID", documentxsschema0xscomplexType3xsattribute3.GetAttribute("name"));
            Assert.AreEqual("xs:token", documentxsschema0xscomplexType3xsattribute3.GetAttribute("type"));

            var documentxsschema0xscomplexType3Text4 = documentxsschema0xscomplexType3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType3Text4.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0xscomplexType3Text4.TextContent);

            var documentxsschema0Text4 = documentxsschema0.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0Text4.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0Text4.TextContent);

            var documentxsschema0xscomplexType5 = documentxsschema0.ChildNodes[5] as IElement;
            Assert.AreEqual(5, documentxsschema0xscomplexType5.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xscomplexType5.Attributes.Count());
            Assert.AreEqual("xs:complexType", documentxsschema0xscomplexType5.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType5.NodeType);
            Assert.AreEqual("AddressType", documentxsschema0xscomplexType5.GetAttribute("name"));

            var documentxsschema0xscomplexType5Text0 = documentxsschema0xscomplexType5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType5Text0.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType5Text0.TextContent);

            var documentxsschema0xscomplexType5xssequence1 = documentxsschema0xscomplexType5.ChildNodes[1] as IElement;
            Assert.AreEqual(11, documentxsschema0xscomplexType5xssequence1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xscomplexType5xssequence1.Attributes.Count());
            Assert.AreEqual("xs:sequence", documentxsschema0xscomplexType5xssequence1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType5xssequence1.NodeType);

            var documentxsschema0xscomplexType5xssequence1Text0 = documentxsschema0xscomplexType5xssequence1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType5xssequence1Text0.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType5xssequence1Text0.TextContent);

            var documentxsschema0xscomplexType5xssequence1xselement1 = documentxsschema0xscomplexType5xssequence1.ChildNodes[1] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType5xssequence1xselement1.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType5xssequence1xselement1.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType5xssequence1xselement1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType5xssequence1xselement1.NodeType);
            Assert.AreEqual("Address", documentxsschema0xscomplexType5xssequence1xselement1.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType5xssequence1xselement1.GetAttribute("type"));

            var documentxsschema0xscomplexType5xssequence1Text2 = documentxsschema0xscomplexType5xssequence1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType5xssequence1Text2.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType5xssequence1Text2.TextContent);

            var documentxsschema0xscomplexType5xssequence1xselement3 = documentxsschema0xscomplexType5xssequence1.ChildNodes[3] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType5xssequence1xselement3.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType5xssequence1xselement3.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType5xssequence1xselement3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType5xssequence1xselement3.NodeType);
            Assert.AreEqual("City", documentxsschema0xscomplexType5xssequence1xselement3.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType5xssequence1xselement3.GetAttribute("type"));

            var documentxsschema0xscomplexType5xssequence1Text4 = documentxsschema0xscomplexType5xssequence1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType5xssequence1Text4.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType5xssequence1Text4.TextContent);

            var documentxsschema0xscomplexType5xssequence1xselement5 = documentxsschema0xscomplexType5xssequence1.ChildNodes[5] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType5xssequence1xselement5.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType5xssequence1xselement5.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType5xssequence1xselement5.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType5xssequence1xselement5.NodeType);
            Assert.AreEqual("Region", documentxsschema0xscomplexType5xssequence1xselement5.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType5xssequence1xselement5.GetAttribute("type"));

            var documentxsschema0xscomplexType5xssequence1Text6 = documentxsschema0xscomplexType5xssequence1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType5xssequence1Text6.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType5xssequence1Text6.TextContent);

            var documentxsschema0xscomplexType5xssequence1xselement7 = documentxsschema0xscomplexType5xssequence1.ChildNodes[7] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType5xssequence1xselement7.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType5xssequence1xselement7.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType5xssequence1xselement7.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType5xssequence1xselement7.NodeType);
            Assert.AreEqual("PostalCode", documentxsschema0xscomplexType5xssequence1xselement7.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType5xssequence1xselement7.GetAttribute("type"));

            var documentxsschema0xscomplexType5xssequence1Text8 = documentxsschema0xscomplexType5xssequence1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType5xssequence1Text8.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType5xssequence1Text8.TextContent);

            var documentxsschema0xscomplexType5xssequence1xselement9 = documentxsschema0xscomplexType5xssequence1.ChildNodes[9] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType5xssequence1xselement9.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType5xssequence1xselement9.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType5xssequence1xselement9.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType5xssequence1xselement9.NodeType);
            Assert.AreEqual("Country", documentxsschema0xscomplexType5xssequence1xselement9.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType5xssequence1xselement9.GetAttribute("type"));

            var documentxsschema0xscomplexType5xssequence1Text10 = documentxsschema0xscomplexType5xssequence1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType5xssequence1Text10.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType5xssequence1Text10.TextContent);

            var documentxsschema0xscomplexType5Text2 = documentxsschema0xscomplexType5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType5Text2.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType5Text2.TextContent);

            var documentxsschema0xscomplexType5xsattribute3 = documentxsschema0xscomplexType5.ChildNodes[3] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType5xsattribute3.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType5xsattribute3.Attributes.Count());
            Assert.AreEqual("xs:attribute", documentxsschema0xscomplexType5xsattribute3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType5xsattribute3.NodeType);
            Assert.AreEqual("CustomerID", documentxsschema0xscomplexType5xsattribute3.GetAttribute("name"));
            Assert.AreEqual("xs:token", documentxsschema0xscomplexType5xsattribute3.GetAttribute("type"));

            var documentxsschema0xscomplexType5Text4 = documentxsschema0xscomplexType5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType5Text4.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0xscomplexType5Text4.TextContent);

            var documentxsschema0Text6 = documentxsschema0.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentxsschema0Text6.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0Text6.TextContent);

            var documentxsschema0xscomplexType7 = documentxsschema0.ChildNodes[7] as IElement;
            Assert.AreEqual(3, documentxsschema0xscomplexType7.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xscomplexType7.Attributes.Count());
            Assert.AreEqual("xs:complexType", documentxsschema0xscomplexType7.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType7.NodeType);
            Assert.AreEqual("OrderType", documentxsschema0xscomplexType7.GetAttribute("name"));

            var documentxsschema0xscomplexType7Text0 = documentxsschema0xscomplexType7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType7Text0.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType7Text0.TextContent);

            var documentxsschema0xscomplexType7xssequence1 = documentxsschema0xscomplexType7.ChildNodes[1] as IElement;
            Assert.AreEqual(11, documentxsschema0xscomplexType7xssequence1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xscomplexType7xssequence1.Attributes.Count());
            Assert.AreEqual("xs:sequence", documentxsschema0xscomplexType7xssequence1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType7xssequence1.NodeType);

            var documentxsschema0xscomplexType7xssequence1Text0 = documentxsschema0xscomplexType7xssequence1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType7xssequence1Text0.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType7xssequence1Text0.TextContent);

            var documentxsschema0xscomplexType7xssequence1xselement1 = documentxsschema0xscomplexType7xssequence1.ChildNodes[1] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType7xssequence1xselement1.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType7xssequence1xselement1.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType7xssequence1xselement1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType7xssequence1xselement1.NodeType);
            Assert.AreEqual("CustomerID", documentxsschema0xscomplexType7xssequence1xselement1.GetAttribute("name"));
            Assert.AreEqual("xs:token", documentxsschema0xscomplexType7xssequence1xselement1.GetAttribute("type"));

            var documentxsschema0xscomplexType7xssequence1Text2 = documentxsschema0xscomplexType7xssequence1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType7xssequence1Text2.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType7xssequence1Text2.TextContent);

            var documentxsschema0xscomplexType7xssequence1xselement3 = documentxsschema0xscomplexType7xssequence1.ChildNodes[3] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType7xssequence1xselement3.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType7xssequence1xselement3.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType7xssequence1xselement3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType7xssequence1xselement3.NodeType);
            Assert.AreEqual("EmployeeID", documentxsschema0xscomplexType7xssequence1xselement3.GetAttribute("name"));
            Assert.AreEqual("xs:token", documentxsschema0xscomplexType7xssequence1xselement3.GetAttribute("type"));

            var documentxsschema0xscomplexType7xssequence1Text4 = documentxsschema0xscomplexType7xssequence1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType7xssequence1Text4.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType7xssequence1Text4.TextContent);

            var documentxsschema0xscomplexType7xssequence1xselement5 = documentxsschema0xscomplexType7xssequence1.ChildNodes[5] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType7xssequence1xselement5.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType7xssequence1xselement5.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType7xssequence1xselement5.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType7xssequence1xselement5.NodeType);
            Assert.AreEqual("OrderDate", documentxsschema0xscomplexType7xssequence1xselement5.GetAttribute("name"));
            Assert.AreEqual("xs:dateTime", documentxsschema0xscomplexType7xssequence1xselement5.GetAttribute("type"));

            var documentxsschema0xscomplexType7xssequence1Text6 = documentxsschema0xscomplexType7xssequence1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType7xssequence1Text6.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType7xssequence1Text6.TextContent);

            var documentxsschema0xscomplexType7xssequence1xselement7 = documentxsschema0xscomplexType7xssequence1.ChildNodes[7] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType7xssequence1xselement7.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType7xssequence1xselement7.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType7xssequence1xselement7.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType7xssequence1xselement7.NodeType);
            Assert.AreEqual("RequiredDate", documentxsschema0xscomplexType7xssequence1xselement7.GetAttribute("name"));
            Assert.AreEqual("xs:dateTime", documentxsschema0xscomplexType7xssequence1xselement7.GetAttribute("type"));

            var documentxsschema0xscomplexType7xssequence1Text8 = documentxsschema0xscomplexType7xssequence1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType7xssequence1Text8.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType7xssequence1Text8.TextContent);

            var documentxsschema0xscomplexType7xssequence1xselement9 = documentxsschema0xscomplexType7xssequence1.ChildNodes[9] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType7xssequence1xselement9.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType7xssequence1xselement9.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType7xssequence1xselement9.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType7xssequence1xselement9.NodeType);
            Assert.AreEqual("ShipInfo", documentxsschema0xscomplexType7xssequence1xselement9.GetAttribute("name"));
            Assert.AreEqual("ShipInfoType", documentxsschema0xscomplexType7xssequence1xselement9.GetAttribute("type"));

            var documentxsschema0xscomplexType7xssequence1Text10 = documentxsschema0xscomplexType7xssequence1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType7xssequence1Text10.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType7xssequence1Text10.TextContent);

            var documentxsschema0xscomplexType7Text2 = documentxsschema0xscomplexType7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType7Text2.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0xscomplexType7Text2.TextContent);

            var documentxsschema0Text8 = documentxsschema0.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentxsschema0Text8.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0Text8.TextContent);

            var documentxsschema0xscomplexType9 = documentxsschema0.ChildNodes[9] as IElement;
            Assert.AreEqual(5, documentxsschema0xscomplexType9.ChildNodes.Length);
            Assert.AreEqual(1, documentxsschema0xscomplexType9.Attributes.Count());
            Assert.AreEqual("xs:complexType", documentxsschema0xscomplexType9.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9.NodeType);
            Assert.AreEqual("ShipInfoType", documentxsschema0xscomplexType9.GetAttribute("name"));

            var documentxsschema0xscomplexType9Text0 = documentxsschema0xscomplexType9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9Text0.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType9Text0.TextContent);

            var documentxsschema0xscomplexType9xssequence1 = documentxsschema0xscomplexType9.ChildNodes[1] as IElement;
            Assert.AreEqual(17, documentxsschema0xscomplexType9xssequence1.ChildNodes.Length);
            Assert.AreEqual(0, documentxsschema0xscomplexType9xssequence1.Attributes.Count());
            Assert.AreEqual("xs:sequence", documentxsschema0xscomplexType9xssequence1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xssequence1.NodeType);

            var documentxsschema0xscomplexType9xssequence1Text0 = documentxsschema0xscomplexType9xssequence1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9xssequence1Text0.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType9xssequence1Text0.TextContent);

            var documentxsschema0xscomplexType9xssequence1xselement1 = documentxsschema0xscomplexType9xssequence1.ChildNodes[1] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType9xssequence1xselement1.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType9xssequence1xselement1.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType9xssequence1xselement1.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xssequence1xselement1.NodeType);
            Assert.AreEqual("ShipVia", documentxsschema0xscomplexType9xssequence1xselement1.GetAttribute("name"));
            Assert.AreEqual("xs:integer", documentxsschema0xscomplexType9xssequence1xselement1.GetAttribute("type"));

            var documentxsschema0xscomplexType9xssequence1Text2 = documentxsschema0xscomplexType9xssequence1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9xssequence1Text2.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType9xssequence1Text2.TextContent);

            var documentxsschema0xscomplexType9xssequence1xselement3 = documentxsschema0xscomplexType9xssequence1.ChildNodes[3] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType9xssequence1xselement3.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType9xssequence1xselement3.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType9xssequence1xselement3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xssequence1xselement3.NodeType);
            Assert.AreEqual("Freight", documentxsschema0xscomplexType9xssequence1xselement3.GetAttribute("name"));
            Assert.AreEqual("xs:decimal", documentxsschema0xscomplexType9xssequence1xselement3.GetAttribute("type"));

            var documentxsschema0xscomplexType9xssequence1Text4 = documentxsschema0xscomplexType9xssequence1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9xssequence1Text4.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType9xssequence1Text4.TextContent);

            var documentxsschema0xscomplexType9xssequence1xselement5 = documentxsschema0xscomplexType9xssequence1.ChildNodes[5] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType9xssequence1xselement5.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType9xssequence1xselement5.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType9xssequence1xselement5.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xssequence1xselement5.NodeType);
            Assert.AreEqual("ShipName", documentxsschema0xscomplexType9xssequence1xselement5.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType9xssequence1xselement5.GetAttribute("type"));

            var documentxsschema0xscomplexType9xssequence1Text6 = documentxsschema0xscomplexType9xssequence1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9xssequence1Text6.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType9xssequence1Text6.TextContent);

            var documentxsschema0xscomplexType9xssequence1xselement7 = documentxsschema0xscomplexType9xssequence1.ChildNodes[7] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType9xssequence1xselement7.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType9xssequence1xselement7.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType9xssequence1xselement7.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xssequence1xselement7.NodeType);
            Assert.AreEqual("ShipAddress", documentxsschema0xscomplexType9xssequence1xselement7.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType9xssequence1xselement7.GetAttribute("type"));

            var documentxsschema0xscomplexType9xssequence1Text8 = documentxsschema0xscomplexType9xssequence1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9xssequence1Text8.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType9xssequence1Text8.TextContent);

            var documentxsschema0xscomplexType9xssequence1xselement9 = documentxsschema0xscomplexType9xssequence1.ChildNodes[9] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType9xssequence1xselement9.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType9xssequence1xselement9.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType9xssequence1xselement9.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xssequence1xselement9.NodeType);
            Assert.AreEqual("ShipCity", documentxsschema0xscomplexType9xssequence1xselement9.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType9xssequence1xselement9.GetAttribute("type"));

            var documentxsschema0xscomplexType9xssequence1Text10 = documentxsschema0xscomplexType9xssequence1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9xssequence1Text10.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType9xssequence1Text10.TextContent);

            var documentxsschema0xscomplexType9xssequence1xselement11 = documentxsschema0xscomplexType9xssequence1.ChildNodes[11] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType9xssequence1xselement11.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType9xssequence1xselement11.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType9xssequence1xselement11.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xssequence1xselement11.NodeType);
            Assert.AreEqual("ShipRegion", documentxsschema0xscomplexType9xssequence1xselement11.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType9xssequence1xselement11.GetAttribute("type"));

            var documentxsschema0xscomplexType9xssequence1Text12 = documentxsschema0xscomplexType9xssequence1.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9xssequence1Text12.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType9xssequence1Text12.TextContent);

            var documentxsschema0xscomplexType9xssequence1xselement13 = documentxsschema0xscomplexType9xssequence1.ChildNodes[13] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType9xssequence1xselement13.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType9xssequence1xselement13.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType9xssequence1xselement13.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xssequence1xselement13.NodeType);
            Assert.AreEqual("ShipPostalCode", documentxsschema0xscomplexType9xssequence1xselement13.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType9xssequence1xselement13.GetAttribute("type"));

            var documentxsschema0xscomplexType9xssequence1Text14 = documentxsschema0xscomplexType9xssequence1.ChildNodes[14];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9xssequence1Text14.NodeType);
            Assert.AreEqual("\n      ", documentxsschema0xscomplexType9xssequence1Text14.TextContent);

            var documentxsschema0xscomplexType9xssequence1xselement15 = documentxsschema0xscomplexType9xssequence1.ChildNodes[15] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType9xssequence1xselement15.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType9xssequence1xselement15.Attributes.Count());
            Assert.AreEqual("xs:element", documentxsschema0xscomplexType9xssequence1xselement15.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xssequence1xselement15.NodeType);
            Assert.AreEqual("ShipCountry", documentxsschema0xscomplexType9xssequence1xselement15.GetAttribute("name"));
            Assert.AreEqual("xs:string", documentxsschema0xscomplexType9xssequence1xselement15.GetAttribute("type"));

            var documentxsschema0xscomplexType9xssequence1Text16 = documentxsschema0xscomplexType9xssequence1.ChildNodes[16];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9xssequence1Text16.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType9xssequence1Text16.TextContent);

            var documentxsschema0xscomplexType9Text2 = documentxsschema0xscomplexType9.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9Text2.NodeType);
            Assert.AreEqual("\n    ", documentxsschema0xscomplexType9Text2.TextContent);

            var documentxsschema0xscomplexType9xsattribute3 = documentxsschema0xscomplexType9.ChildNodes[3] as IElement;
            Assert.AreEqual(0, documentxsschema0xscomplexType9xsattribute3.ChildNodes.Length);
            Assert.AreEqual(2, documentxsschema0xscomplexType9xsattribute3.Attributes.Count());
            Assert.AreEqual("xs:attribute", documentxsschema0xscomplexType9xsattribute3.NodeName);
            Assert.AreEqual(NodeType.Element, documentxsschema0xscomplexType9xsattribute3.NodeType);
            Assert.AreEqual("ShippedDate", documentxsschema0xscomplexType9xsattribute3.GetAttribute("name"));
            Assert.AreEqual("xs:dateTime", documentxsschema0xscomplexType9xsattribute3.GetAttribute("type"));

            var documentxsschema0xscomplexType9Text4 = documentxsschema0xscomplexType9.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentxsschema0xscomplexType9Text4.NodeType);
            Assert.AreEqual("\n  ", documentxsschema0xscomplexType9Text4.TextContent);

            var documentxsschema0Text10 = documentxsschema0.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentxsschema0Text10.NodeType);
            Assert.AreEqual("\n", documentxsschema0Text10.TextContent);
        }

        [Test]
        public void XmlNumericalData()
        {
            var document = (@"<Root>
  <TaxRate>7.25</TaxRate>
  <Data>
    <Category>A</Category>
    <Quantity>3</Quantity>
    <Price>24.50</Price>
  </Data>
  <Data>
    <Category>B</Category>
    <Quantity>1</Quantity>
    <Price>89.99</Price>
  </Data>
  <Data>
    <Category>A</Category>
    <Quantity>5</Quantity>
    <Price>4.95</Price>
  </Data>
  <Data>
    <Category>A</Category>
    <Quantity>3</Quantity>
    <Price>66.00</Price>
  </Data>
  <Data>
    <Category>B</Category>
    <Quantity>10</Quantity>
    <Price>.99</Price>
  </Data>
  <Data>
    <Category>A</Category>
    <Quantity>15</Quantity>
    <Price>29.00</Price>
  </Data>
  <Data>
    <Category>B</Category>
    <Quantity>8</Quantity>
    <Price>6.99</Price>
  </Data>
</Root>").ToXmlDocument();

            var documentRoot0 = document.ChildNodes[0] as IElement;
            Assert.AreEqual(17, documentRoot0.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0.Attributes.Count());
            Assert.AreEqual("Root", documentRoot0.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0.NodeType);

            var documentRoot0Text0 = documentRoot0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Text0.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text0.TextContent);

            var documentRoot0TaxRate1 = documentRoot0.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentRoot0TaxRate1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0TaxRate1.Attributes.Count());
            Assert.AreEqual("TaxRate", documentRoot0TaxRate1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0TaxRate1.NodeType);

            var documentRoot0TaxRate1Text0 = documentRoot0TaxRate1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0TaxRate1Text0.NodeType);
            Assert.AreEqual("7.25", documentRoot0TaxRate1Text0.TextContent);

            var documentRoot0Text2 = documentRoot0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Text2.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text2.TextContent);

            var documentRoot0Data3 = documentRoot0.ChildNodes[3] as IElement;
            Assert.AreEqual(7, documentRoot0Data3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data3.Attributes.Count());
            Assert.AreEqual("Data", documentRoot0Data3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data3.NodeType);

            var documentRoot0Data3Text0 = documentRoot0Data3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data3Text0.TextContent);

            var documentRoot0Data3Category1 = documentRoot0Data3.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentRoot0Data3Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data3Category1.Attributes.Count());
            Assert.AreEqual("Category", documentRoot0Data3Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data3Category1.NodeType);

            var documentRoot0Data3Category1Text0 = documentRoot0Data3Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Category1Text0.NodeType);
            Assert.AreEqual("A", documentRoot0Data3Category1Text0.TextContent);

            var documentRoot0Data3Text2 = documentRoot0Data3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data3Text2.TextContent);

            var documentRoot0Data3Quantity3 = documentRoot0Data3.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentRoot0Data3Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data3Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentRoot0Data3Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data3Quantity3.NodeType);

            var documentRoot0Data3Quantity3Text0 = documentRoot0Data3Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Quantity3Text0.NodeType);
            Assert.AreEqual("3", documentRoot0Data3Quantity3Text0.TextContent);

            var documentRoot0Data3Text4 = documentRoot0Data3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data3Text4.TextContent);

            var documentRoot0Data3Price5 = documentRoot0Data3.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentRoot0Data3Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data3Price5.Attributes.Count());
            Assert.AreEqual("Price", documentRoot0Data3Price5.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data3Price5.NodeType);

            var documentRoot0Data3Price5Text0 = documentRoot0Data3Price5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Price5Text0.NodeType);
            Assert.AreEqual("24.50", documentRoot0Data3Price5Text0.TextContent);

            var documentRoot0Data3Text6 = documentRoot0Data3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Text6.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Data3Text6.TextContent);

            var documentRoot0Text4 = documentRoot0.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Text4.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text4.TextContent);

            var documentRoot0Data5 = documentRoot0.ChildNodes[5] as IElement;
            Assert.AreEqual(7, documentRoot0Data5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data5.Attributes.Count());
            Assert.AreEqual("Data", documentRoot0Data5.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data5.NodeType);

            var documentRoot0Data5Text0 = documentRoot0Data5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data5Text0.TextContent);

            var documentRoot0Data5Category1 = documentRoot0Data5.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentRoot0Data5Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data5Category1.Attributes.Count());
            Assert.AreEqual("Category", documentRoot0Data5Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data5Category1.NodeType);

            var documentRoot0Data5Category1Text0 = documentRoot0Data5Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Category1Text0.NodeType);
            Assert.AreEqual("B", documentRoot0Data5Category1Text0.TextContent);

            var documentRoot0Data5Text2 = documentRoot0Data5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data5Text2.TextContent);

            var documentRoot0Data5Quantity3 = documentRoot0Data5.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentRoot0Data5Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data5Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentRoot0Data5Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data5Quantity3.NodeType);

            var documentRoot0Data5Quantity3Text0 = documentRoot0Data5Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentRoot0Data5Quantity3Text0.TextContent);

            var documentRoot0Data5Text4 = documentRoot0Data5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data5Text4.TextContent);

            var documentRoot0Data5Price5 = documentRoot0Data5.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentRoot0Data5Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data5Price5.Attributes.Count());
            Assert.AreEqual("Price", documentRoot0Data5Price5.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data5Price5.NodeType);

            var documentRoot0Data5Price5Text0 = documentRoot0Data5Price5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Price5Text0.NodeType);
            Assert.AreEqual("89.99", documentRoot0Data5Price5Text0.TextContent);

            var documentRoot0Data5Text6 = documentRoot0Data5.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Text6.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Data5Text6.TextContent);

            var documentRoot0Text6 = documentRoot0.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentRoot0Text6.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text6.TextContent);

            var documentRoot0Data7 = documentRoot0.ChildNodes[7] as IElement;
            Assert.AreEqual(7, documentRoot0Data7.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data7.Attributes.Count());
            Assert.AreEqual("Data", documentRoot0Data7.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data7.NodeType);

            var documentRoot0Data7Text0 = documentRoot0Data7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data7Text0.TextContent);

            var documentRoot0Data7Category1 = documentRoot0Data7.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentRoot0Data7Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data7Category1.Attributes.Count());
            Assert.AreEqual("Category", documentRoot0Data7Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data7Category1.NodeType);

            var documentRoot0Data7Category1Text0 = documentRoot0Data7Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Category1Text0.NodeType);
            Assert.AreEqual("A", documentRoot0Data7Category1Text0.TextContent);

            var documentRoot0Data7Text2 = documentRoot0Data7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data7Text2.TextContent);

            var documentRoot0Data7Quantity3 = documentRoot0Data7.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentRoot0Data7Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data7Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentRoot0Data7Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data7Quantity3.NodeType);

            var documentRoot0Data7Quantity3Text0 = documentRoot0Data7Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Quantity3Text0.NodeType);
            Assert.AreEqual("5", documentRoot0Data7Quantity3Text0.TextContent);

            var documentRoot0Data7Text4 = documentRoot0Data7.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data7Text4.TextContent);

            var documentRoot0Data7Price5 = documentRoot0Data7.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentRoot0Data7Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data7Price5.Attributes.Count());
            Assert.AreEqual("Price", documentRoot0Data7Price5.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data7Price5.NodeType);

            var documentRoot0Data7Price5Text0 = documentRoot0Data7Price5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Price5Text0.NodeType);
            Assert.AreEqual("4.95", documentRoot0Data7Price5Text0.TextContent);

            var documentRoot0Data7Text6 = documentRoot0Data7.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Text6.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Data7Text6.TextContent);

            var documentRoot0Text8 = documentRoot0.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentRoot0Text8.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text8.TextContent);

            var documentRoot0Data9 = documentRoot0.ChildNodes[9] as IElement;
            Assert.AreEqual(7, documentRoot0Data9.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data9.Attributes.Count());
            Assert.AreEqual("Data", documentRoot0Data9.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data9.NodeType);

            var documentRoot0Data9Text0 = documentRoot0Data9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data9Text0.TextContent);

            var documentRoot0Data9Category1 = documentRoot0Data9.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentRoot0Data9Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data9Category1.Attributes.Count());
            Assert.AreEqual("Category", documentRoot0Data9Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data9Category1.NodeType);

            var documentRoot0Data9Category1Text0 = documentRoot0Data9Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Category1Text0.NodeType);
            Assert.AreEqual("A", documentRoot0Data9Category1Text0.TextContent);

            var documentRoot0Data9Text2 = documentRoot0Data9.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data9Text2.TextContent);

            var documentRoot0Data9Quantity3 = documentRoot0Data9.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentRoot0Data9Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data9Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentRoot0Data9Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data9Quantity3.NodeType);

            var documentRoot0Data9Quantity3Text0 = documentRoot0Data9Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Quantity3Text0.NodeType);
            Assert.AreEqual("3", documentRoot0Data9Quantity3Text0.TextContent);

            var documentRoot0Data9Text4 = documentRoot0Data9.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data9Text4.TextContent);

            var documentRoot0Data9Price5 = documentRoot0Data9.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentRoot0Data9Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data9Price5.Attributes.Count());
            Assert.AreEqual("Price", documentRoot0Data9Price5.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data9Price5.NodeType);

            var documentRoot0Data9Price5Text0 = documentRoot0Data9Price5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Price5Text0.NodeType);
            Assert.AreEqual("66.00", documentRoot0Data9Price5Text0.TextContent);

            var documentRoot0Data9Text6 = documentRoot0Data9.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Text6.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Data9Text6.TextContent);

            var documentRoot0Text10 = documentRoot0.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentRoot0Text10.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text10.TextContent);

            var documentRoot0Data11 = documentRoot0.ChildNodes[11] as IElement;
            Assert.AreEqual(7, documentRoot0Data11.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data11.Attributes.Count());
            Assert.AreEqual("Data", documentRoot0Data11.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data11.NodeType);

            var documentRoot0Data11Text0 = documentRoot0Data11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data11Text0.TextContent);

            var documentRoot0Data11Category1 = documentRoot0Data11.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentRoot0Data11Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data11Category1.Attributes.Count());
            Assert.AreEqual("Category", documentRoot0Data11Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data11Category1.NodeType);

            var documentRoot0Data11Category1Text0 = documentRoot0Data11Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Category1Text0.NodeType);
            Assert.AreEqual("B", documentRoot0Data11Category1Text0.TextContent);

            var documentRoot0Data11Text2 = documentRoot0Data11.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data11Text2.TextContent);

            var documentRoot0Data11Quantity3 = documentRoot0Data11.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentRoot0Data11Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data11Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentRoot0Data11Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data11Quantity3.NodeType);

            var documentRoot0Data11Quantity3Text0 = documentRoot0Data11Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Quantity3Text0.NodeType);
            Assert.AreEqual("10", documentRoot0Data11Quantity3Text0.TextContent);

            var documentRoot0Data11Text4 = documentRoot0Data11.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data11Text4.TextContent);

            var documentRoot0Data11Price5 = documentRoot0Data11.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentRoot0Data11Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data11Price5.Attributes.Count());
            Assert.AreEqual("Price", documentRoot0Data11Price5.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data11Price5.NodeType);

            var documentRoot0Data11Price5Text0 = documentRoot0Data11Price5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Price5Text0.NodeType);
            Assert.AreEqual(".99", documentRoot0Data11Price5Text0.TextContent);

            var documentRoot0Data11Text6 = documentRoot0Data11.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Text6.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Data11Text6.TextContent);

            var documentRoot0Text12 = documentRoot0.ChildNodes[12];
            Assert.AreEqual(NodeType.Text, documentRoot0Text12.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text12.TextContent);

            var documentRoot0Data13 = documentRoot0.ChildNodes[13] as IElement;
            Assert.AreEqual(7, documentRoot0Data13.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data13.Attributes.Count());
            Assert.AreEqual("Data", documentRoot0Data13.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data13.NodeType);

            var documentRoot0Data13Text0 = documentRoot0Data13.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data13Text0.TextContent);

            var documentRoot0Data13Category1 = documentRoot0Data13.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentRoot0Data13Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data13Category1.Attributes.Count());
            Assert.AreEqual("Category", documentRoot0Data13Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data13Category1.NodeType);

            var documentRoot0Data13Category1Text0 = documentRoot0Data13Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Category1Text0.NodeType);
            Assert.AreEqual("A", documentRoot0Data13Category1Text0.TextContent);

            var documentRoot0Data13Text2 = documentRoot0Data13.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data13Text2.TextContent);

            var documentRoot0Data13Quantity3 = documentRoot0Data13.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentRoot0Data13Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data13Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentRoot0Data13Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data13Quantity3.NodeType);

            var documentRoot0Data13Quantity3Text0 = documentRoot0Data13Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Quantity3Text0.NodeType);
            Assert.AreEqual("15", documentRoot0Data13Quantity3Text0.TextContent);

            var documentRoot0Data13Text4 = documentRoot0Data13.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data13Text4.TextContent);

            var documentRoot0Data13Price5 = documentRoot0Data13.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentRoot0Data13Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data13Price5.Attributes.Count());
            Assert.AreEqual("Price", documentRoot0Data13Price5.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data13Price5.NodeType);

            var documentRoot0Data13Price5Text0 = documentRoot0Data13Price5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Price5Text0.NodeType);
            Assert.AreEqual("29.00", documentRoot0Data13Price5Text0.TextContent);

            var documentRoot0Data13Text6 = documentRoot0Data13.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Text6.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Data13Text6.TextContent);

            var documentRoot0Text14 = documentRoot0.ChildNodes[14];
            Assert.AreEqual(NodeType.Text, documentRoot0Text14.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text14.TextContent);

            var documentRoot0Data15 = documentRoot0.ChildNodes[15] as IElement;
            Assert.AreEqual(7, documentRoot0Data15.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data15.Attributes.Count());
            Assert.AreEqual("Data", documentRoot0Data15.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data15.NodeType);

            var documentRoot0Data15Text0 = documentRoot0Data15.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data15Text0.TextContent);

            var documentRoot0Data15Category1 = documentRoot0Data15.ChildNodes[1] as IElement;
            Assert.AreEqual(1, documentRoot0Data15Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data15Category1.Attributes.Count());
            Assert.AreEqual("Category", documentRoot0Data15Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data15Category1.NodeType);

            var documentRoot0Data15Category1Text0 = documentRoot0Data15Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Category1Text0.NodeType);
            Assert.AreEqual("B", documentRoot0Data15Category1Text0.TextContent);

            var documentRoot0Data15Text2 = documentRoot0Data15.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data15Text2.TextContent);

            var documentRoot0Data15Quantity3 = documentRoot0Data15.ChildNodes[3] as IElement;
            Assert.AreEqual(1, documentRoot0Data15Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data15Quantity3.Attributes.Count());
            Assert.AreEqual("Quantity", documentRoot0Data15Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data15Quantity3.NodeType);

            var documentRoot0Data15Quantity3Text0 = documentRoot0Data15Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Quantity3Text0.NodeType);
            Assert.AreEqual("8", documentRoot0Data15Quantity3Text0.TextContent);

            var documentRoot0Data15Text4 = documentRoot0Data15.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data15Text4.TextContent);

            var documentRoot0Data15Price5 = documentRoot0Data15.ChildNodes[5] as IElement;
            Assert.AreEqual(1, documentRoot0Data15Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data15Price5.Attributes.Count());
            Assert.AreEqual("Price", documentRoot0Data15Price5.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data15Price5.NodeType);

            var documentRoot0Data15Price5Text0 = documentRoot0Data15Price5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Price5Text0.NodeType);
            Assert.AreEqual("6.99", documentRoot0Data15Price5Text0.TextContent);

            var documentRoot0Data15Text6 = documentRoot0Data15.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Text6.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Data15Text6.TextContent);

            var documentRoot0Text16 = documentRoot0.ChildNodes[16];
            Assert.AreEqual(NodeType.Text, documentRoot0Text16.NodeType);
            Assert.AreEqual("\n", documentRoot0Text16.TextContent);
        }
    }
}
