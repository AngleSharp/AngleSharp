using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM.Xml;
using AngleSharp.DOM;

namespace UnitTests.Xml
{
    /// <summary>
    /// Parsing the sample XML files taken from
    /// http://msdn.microsoft.com/en-us/library/bb387026.aspx
    /// Additional (maybe):
    /// http://tantek.com/XHTML/Test/minimal.html
    /// </summary>
    [TestClass]
    public class XmlSamples
    {
        [TestMethod]
        public void XmlTypicalPurchaseOrder()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
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
</PurchaseOrder>");

            var documentPurchaseOrder1 = document.ChildNodes[0];
            Assert.AreEqual(9, documentPurchaseOrder1.ChildNodes.Length);
            Assert.AreEqual(2, documentPurchaseOrder1.Attributes.Length);
            Assert.AreEqual("PurchaseOrder", documentPurchaseOrder1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1.NodeType);
            Assert.IsNotNull(documentPurchaseOrder1.Attributes["PurchaseOrderNumber"]);
            Assert.AreEqual("PurchaseOrderNumber", documentPurchaseOrder1.Attributes["PurchaseOrderNumber"].Name);
            Assert.AreEqual("99503", documentPurchaseOrder1.Attributes["PurchaseOrderNumber"].Value);
            Assert.IsNotNull(documentPurchaseOrder1.Attributes["OrderDate"]);
            Assert.AreEqual("OrderDate", documentPurchaseOrder1.Attributes["OrderDate"].Name);
            Assert.AreEqual("1999-10-20", documentPurchaseOrder1.Attributes["OrderDate"].Value);

            var documentPurchaseOrder1Text0 = documentPurchaseOrder1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Text0.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrder1Text0.TextContent);

            var documentPurchaseOrder1Address1 = documentPurchaseOrder1.ChildNodes[1];
            Assert.AreEqual(13, documentPurchaseOrder1Address1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrder1Address1.Attributes.Length);
            Assert.AreEqual("Address", documentPurchaseOrder1Address1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1.NodeType);
            Assert.IsNotNull(documentPurchaseOrder1Address1.Attributes["Type"]);
            Assert.AreEqual("Type", documentPurchaseOrder1Address1.Attributes["Type"].Name);
            Assert.AreEqual("Shipping", documentPurchaseOrder1Address1.Attributes["Type"].Value);

            var documentPurchaseOrder1Address1Text0 = documentPurchaseOrder1Address1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text0.TextContent);

            var documentPurchaseOrder1Address1Name1 = documentPurchaseOrder1Address1.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrder1Address1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1Name1.Attributes.Length);
            Assert.AreEqual("Name", documentPurchaseOrder1Address1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1Name1.NodeType);

            var documentPurchaseOrder1Address1Name1Text0 = documentPurchaseOrder1Address1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Name1Text0.NodeType);
            Assert.AreEqual("Ellen Adams", documentPurchaseOrder1Address1Name1Text0.TextContent);

            var documentPurchaseOrder1Address1Text2 = documentPurchaseOrder1Address1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text2.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text2.TextContent);

            var documentPurchaseOrder1Address1Street3 = documentPurchaseOrder1Address1.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrder1Address1Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1Street3.Attributes.Length);
            Assert.AreEqual("Street", documentPurchaseOrder1Address1Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1Street3.NodeType);

            var documentPurchaseOrder1Address1Street3Text0 = documentPurchaseOrder1Address1Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Street3Text0.NodeType);
            Assert.AreEqual("123 Maple Street", documentPurchaseOrder1Address1Street3Text0.TextContent);

            var documentPurchaseOrder1Address1Text4 = documentPurchaseOrder1Address1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text4.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text4.TextContent);

            var documentPurchaseOrder1Address1City5 = documentPurchaseOrder1Address1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrder1Address1City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1City5.Attributes.Length);
            Assert.AreEqual("City", documentPurchaseOrder1Address1City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1City5.NodeType);

            var documentPurchaseOrder1Address1City5Text0 = documentPurchaseOrder1Address1City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1City5Text0.NodeType);
            Assert.AreEqual("Mill Valley", documentPurchaseOrder1Address1City5Text0.TextContent);

            var documentPurchaseOrder1Address1Text6 = documentPurchaseOrder1Address1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text6.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text6.TextContent);

            var documentPurchaseOrder1Address1State7 = documentPurchaseOrder1Address1.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrder1Address1State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1State7.Attributes.Length);
            Assert.AreEqual("State", documentPurchaseOrder1Address1State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1State7.NodeType);

            var documentPurchaseOrder1Address1State7Text0 = documentPurchaseOrder1Address1State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1State7Text0.NodeType);
            Assert.AreEqual("CA", documentPurchaseOrder1Address1State7Text0.TextContent);

            var documentPurchaseOrder1Address1Text8 = documentPurchaseOrder1Address1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text8.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text8.TextContent);

            var documentPurchaseOrder1Address1Zip9 = documentPurchaseOrder1Address1.ChildNodes[9];
            Assert.AreEqual(1, documentPurchaseOrder1Address1Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1Zip9.Attributes.Length);
            Assert.AreEqual("Zip", documentPurchaseOrder1Address1Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address1Zip9.NodeType);

            var documentPurchaseOrder1Address1Zip9Text0 = documentPurchaseOrder1Address1Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Zip9Text0.NodeType);
            Assert.AreEqual("10999", documentPurchaseOrder1Address1Zip9Text0.TextContent);

            var documentPurchaseOrder1Address1Text10 = documentPurchaseOrder1Address1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address1Text10.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address1Text10.TextContent);

            var documentPurchaseOrder1Address1Country11 = documentPurchaseOrder1Address1.ChildNodes[11];
            Assert.AreEqual(1, documentPurchaseOrder1Address1Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address1Country11.Attributes.Length);
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

            var documentPurchaseOrder1Address3 = documentPurchaseOrder1.ChildNodes[3];
            Assert.AreEqual(13, documentPurchaseOrder1Address3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrder1Address3.Attributes.Length);
            Assert.AreEqual("Address", documentPurchaseOrder1Address3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3.NodeType);
            Assert.IsNotNull(documentPurchaseOrder1Address3.Attributes["Type"]);
            Assert.AreEqual("Type", documentPurchaseOrder1Address3.Attributes["Type"].Name);
            Assert.AreEqual("Billing", documentPurchaseOrder1Address3.Attributes["Type"].Value);

            var documentPurchaseOrder1Address3Text0 = documentPurchaseOrder1Address3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text0.TextContent);

            var documentPurchaseOrder1Address3Name1 = documentPurchaseOrder1Address3.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrder1Address3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3Name1.Attributes.Length);
            Assert.AreEqual("Name", documentPurchaseOrder1Address3Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3Name1.NodeType);

            var documentPurchaseOrder1Address3Name1Text0 = documentPurchaseOrder1Address3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Name1Text0.NodeType);
            Assert.AreEqual("Tai Yee", documentPurchaseOrder1Address3Name1Text0.TextContent);

            var documentPurchaseOrder1Address3Text2 = documentPurchaseOrder1Address3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text2.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text2.TextContent);

            var documentPurchaseOrder1Address3Street3 = documentPurchaseOrder1Address3.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrder1Address3Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3Street3.Attributes.Length);
            Assert.AreEqual("Street", documentPurchaseOrder1Address3Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3Street3.NodeType);

            var documentPurchaseOrder1Address3Street3Text0 = documentPurchaseOrder1Address3Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Street3Text0.NodeType);
            Assert.AreEqual("8 Oak Avenue", documentPurchaseOrder1Address3Street3Text0.TextContent);

            var documentPurchaseOrder1Address3Text4 = documentPurchaseOrder1Address3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text4.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text4.TextContent);

            var documentPurchaseOrder1Address3City5 = documentPurchaseOrder1Address3.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrder1Address3City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3City5.Attributes.Length);
            Assert.AreEqual("City", documentPurchaseOrder1Address3City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3City5.NodeType);

            var documentPurchaseOrder1Address3City5Text0 = documentPurchaseOrder1Address3City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3City5Text0.NodeType);
            Assert.AreEqual("Old Town", documentPurchaseOrder1Address3City5Text0.TextContent);

            var documentPurchaseOrder1Address3Text6 = documentPurchaseOrder1Address3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text6.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text6.TextContent);

            var documentPurchaseOrder1Address3State7 = documentPurchaseOrder1Address3.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrder1Address3State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3State7.Attributes.Length);
            Assert.AreEqual("State", documentPurchaseOrder1Address3State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3State7.NodeType);

            var documentPurchaseOrder1Address3State7Text0 = documentPurchaseOrder1Address3State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3State7Text0.NodeType);
            Assert.AreEqual("PA", documentPurchaseOrder1Address3State7Text0.TextContent);

            var documentPurchaseOrder1Address3Text8 = documentPurchaseOrder1Address3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text8.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text8.TextContent);

            var documentPurchaseOrder1Address3Zip9 = documentPurchaseOrder1Address3.ChildNodes[9];
            Assert.AreEqual(1, documentPurchaseOrder1Address3Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3Zip9.Attributes.Length);
            Assert.AreEqual("Zip", documentPurchaseOrder1Address3Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Address3Zip9.NodeType);

            var documentPurchaseOrder1Address3Zip9Text0 = documentPurchaseOrder1Address3Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Zip9Text0.NodeType);
            Assert.AreEqual("95819", documentPurchaseOrder1Address3Zip9Text0.TextContent);

            var documentPurchaseOrder1Address3Text10 = documentPurchaseOrder1Address3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Address3Text10.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Address3Text10.TextContent);

            var documentPurchaseOrder1Address3Country11 = documentPurchaseOrder1Address3.ChildNodes[11];
            Assert.AreEqual(1, documentPurchaseOrder1Address3Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Address3Country11.Attributes.Length);
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

            var documentPurchaseOrder1DeliveryNotes5 = documentPurchaseOrder1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrder1DeliveryNotes5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1DeliveryNotes5.Attributes.Length);
            Assert.AreEqual("DeliveryNotes", documentPurchaseOrder1DeliveryNotes5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1DeliveryNotes5.NodeType);

            var documentPurchaseOrder1DeliveryNotes5Text0 = documentPurchaseOrder1DeliveryNotes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1DeliveryNotes5Text0.NodeType);
            Assert.AreEqual("Please leave packages in shed by driveway.", documentPurchaseOrder1DeliveryNotes5Text0.TextContent);

            var documentPurchaseOrder1Text6 = documentPurchaseOrder1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Text6.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrder1Text6.TextContent);

            var documentPurchaseOrder1Items7 = documentPurchaseOrder1.ChildNodes[7];
            Assert.AreEqual(5, documentPurchaseOrder1Items7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7.Attributes.Length);
            Assert.AreEqual("Items", documentPurchaseOrder1Items7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7.NodeType);

            var documentPurchaseOrder1Items7Text0 = documentPurchaseOrder1Items7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrder1Items7Text0.TextContent);

            var documentPurchaseOrder1Items7Item1 = documentPurchaseOrder1Items7.ChildNodes[1];
            Assert.AreEqual(9, documentPurchaseOrder1Items7Item1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1.Attributes.Length);
            Assert.AreEqual("Item", documentPurchaseOrder1Items7Item1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item1.NodeType);
            Assert.IsNotNull(documentPurchaseOrder1Items7Item1.Attributes["PartNumber"]);
            Assert.AreEqual("PartNumber", documentPurchaseOrder1Items7Item1.Attributes["PartNumber"].Name);
            Assert.AreEqual("872-AA", documentPurchaseOrder1Items7Item1.Attributes["PartNumber"].Value);

            var documentPurchaseOrder1Items7Item1Text0 = documentPurchaseOrder1Items7Item1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item1Text0.TextContent);

            var documentPurchaseOrder1Items7Item1ProductName1 = documentPurchaseOrder1Items7Item1.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item1ProductName1.Attributes.Length);
            Assert.AreEqual("ProductName", documentPurchaseOrder1Items7Item1ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item1ProductName1.NodeType);

            var documentPurchaseOrder1Items7Item1ProductName1Text0 = documentPurchaseOrder1Items7Item1ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1ProductName1Text0.NodeType);
            Assert.AreEqual("Lawnmower", documentPurchaseOrder1Items7Item1ProductName1Text0.TextContent);

            var documentPurchaseOrder1Items7Item1Text2 = documentPurchaseOrder1Items7Item1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item1Text2.TextContent);

            var documentPurchaseOrder1Items7Item1Quantity3 = documentPurchaseOrder1Items7Item1.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item1Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentPurchaseOrder1Items7Item1Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item1Quantity3.NodeType);

            var documentPurchaseOrder1Items7Item1Quantity3Text0 = documentPurchaseOrder1Items7Item1Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrder1Items7Item1Quantity3Text0.TextContent);

            var documentPurchaseOrder1Items7Item1Text4 = documentPurchaseOrder1Items7Item1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item1Text4.TextContent);

            var documentPurchaseOrder1Items7Item1USPrice5 = documentPurchaseOrder1Items7Item1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item1USPrice5.Attributes.Length);
            Assert.AreEqual("USPrice", documentPurchaseOrder1Items7Item1USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item1USPrice5.NodeType);

            var documentPurchaseOrder1Items7Item1USPrice5Text0 = documentPurchaseOrder1Items7Item1USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1USPrice5Text0.NodeType);
            Assert.AreEqual("148.95", documentPurchaseOrder1Items7Item1USPrice5Text0.TextContent);

            var documentPurchaseOrder1Items7Item1Text6 = documentPurchaseOrder1Items7Item1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item1Text6.TextContent);

            var documentPurchaseOrder1Items7Item1Comment7 = documentPurchaseOrder1Items7Item1.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item1Comment7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item1Comment7.Attributes.Length);
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

            var documentPurchaseOrder1Items7Item3 = documentPurchaseOrder1Items7.ChildNodes[3];
            Assert.AreEqual(9, documentPurchaseOrder1Items7Item3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3.Attributes.Length);
            Assert.AreEqual("Item", documentPurchaseOrder1Items7Item3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item3.NodeType);
            Assert.IsNotNull(documentPurchaseOrder1Items7Item3.Attributes["PartNumber"]);
            Assert.AreEqual("PartNumber", documentPurchaseOrder1Items7Item3.Attributes["PartNumber"].Name);
            Assert.AreEqual("926-AA", documentPurchaseOrder1Items7Item3.Attributes["PartNumber"].Value);

            var documentPurchaseOrder1Items7Item3Text0 = documentPurchaseOrder1Items7Item3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item3Text0.TextContent);

            var documentPurchaseOrder1Items7Item3ProductName1 = documentPurchaseOrder1Items7Item3.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item3ProductName1.Attributes.Length);
            Assert.AreEqual("ProductName", documentPurchaseOrder1Items7Item3ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item3ProductName1.NodeType);

            var documentPurchaseOrder1Items7Item3ProductName1Text0 = documentPurchaseOrder1Items7Item3ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3ProductName1Text0.NodeType);
            Assert.AreEqual("Baby Monitor", documentPurchaseOrder1Items7Item3ProductName1Text0.TextContent);

            var documentPurchaseOrder1Items7Item3Text2 = documentPurchaseOrder1Items7Item3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item3Text2.TextContent);

            var documentPurchaseOrder1Items7Item3Quantity3 = documentPurchaseOrder1Items7Item3.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item3Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentPurchaseOrder1Items7Item3Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item3Quantity3.NodeType);

            var documentPurchaseOrder1Items7Item3Quantity3Text0 = documentPurchaseOrder1Items7Item3Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Quantity3Text0.NodeType);
            Assert.AreEqual("2", documentPurchaseOrder1Items7Item3Quantity3Text0.TextContent);

            var documentPurchaseOrder1Items7Item3Text4 = documentPurchaseOrder1Items7Item3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item3Text4.TextContent);

            var documentPurchaseOrder1Items7Item3USPrice5 = documentPurchaseOrder1Items7Item3.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item3USPrice5.Attributes.Length);
            Assert.AreEqual("USPrice", documentPurchaseOrder1Items7Item3USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrder1Items7Item3USPrice5.NodeType);

            var documentPurchaseOrder1Items7Item3USPrice5Text0 = documentPurchaseOrder1Items7Item3USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3USPrice5Text0.NodeType);
            Assert.AreEqual("39.98", documentPurchaseOrder1Items7Item3USPrice5Text0.TextContent);

            var documentPurchaseOrder1Items7Item3Text6 = documentPurchaseOrder1Items7Item3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrder1Items7Item3Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrder1Items7Item3Text6.TextContent);

            var documentPurchaseOrder1Items7Item3ShipDate7 = documentPurchaseOrder1Items7Item3.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrder1Items7Item3ShipDate7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrder1Items7Item3ShipDate7.Attributes.Length);
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

        [TestMethod]
        public void XmlTypicalPurchaseOrderInNamespace()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
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
</aw:PurchaseOrder>");

            //TODO
        }

        [TestMethod]
        public void XmlMultiplePurchaseOrder()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
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
</PurchaseOrders>");

            var documentPurchaseOrders0 = document.ChildNodes[0];
            Assert.AreEqual(7, documentPurchaseOrders0.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0.Attributes.Length);
            Assert.AreEqual("PurchaseOrders", documentPurchaseOrders0.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0.NodeType);

            var documentPurchaseOrders0Text0 = documentPurchaseOrders0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0Text0.NodeType);
            Assert.AreEqual("\n  ", documentPurchaseOrders0Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1 = documentPurchaseOrders0.ChildNodes[1];
            Assert.AreEqual(9, documentPurchaseOrders0PurchaseOrder1.ChildNodes.Length);
            Assert.AreEqual(2, documentPurchaseOrders0PurchaseOrder1.Attributes.Length);
            Assert.AreEqual("PurchaseOrder", documentPurchaseOrders0PurchaseOrder1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder1.Attributes["PurchaseOrderNumber"]);
            Assert.AreEqual("PurchaseOrderNumber", documentPurchaseOrders0PurchaseOrder1.Attributes["PurchaseOrderNumber"].Name);
            Assert.AreEqual("99503", documentPurchaseOrders0PurchaseOrder1.Attributes["PurchaseOrderNumber"].Value);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder1.Attributes["OrderDate"]);
            Assert.AreEqual("OrderDate", documentPurchaseOrders0PurchaseOrder1.Attributes["OrderDate"].Name);
            Assert.AreEqual("1999-10-20", documentPurchaseOrders0PurchaseOrder1.Attributes["OrderDate"].Value);

            var documentPurchaseOrders0PurchaseOrder1Text0 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[1];
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1.Attributes.Length);
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder1Address1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder1Address1.Attributes["Type"]);
            Assert.AreEqual("Type", documentPurchaseOrders0PurchaseOrder1Address1.Attributes["Type"].Name);
            Assert.AreEqual("Shipping", documentPurchaseOrders0PurchaseOrder1Address1.Attributes["Type"].Value);

            var documentPurchaseOrders0PurchaseOrder1Address1Text0 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Name1 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1Name1.Attributes.Length);
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder1Address1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1Name1Text0 = documentPurchaseOrders0PurchaseOrder1Address1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Name1Text0.NodeType);
            Assert.AreEqual("Ellen Adams", documentPurchaseOrders0PurchaseOrder1Address1Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text2 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Street3 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1Street3.Attributes.Length);
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder1Address1Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1Street3Text0 = documentPurchaseOrders0PurchaseOrder1Address1Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Street3Text0.NodeType);
            Assert.AreEqual("123 Maple Street", documentPurchaseOrders0PurchaseOrder1Address1Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text4 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1City5 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1City5.Attributes.Length);
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder1Address1City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1City5Text0 = documentPurchaseOrders0PurchaseOrder1Address1City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1City5Text0.NodeType);
            Assert.AreEqual("Mill Valley", documentPurchaseOrders0PurchaseOrder1Address1City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text6 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1State7 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1State7.Attributes.Length);
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder1Address1State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1State7Text0 = documentPurchaseOrders0PurchaseOrder1Address1State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1State7Text0.NodeType);
            Assert.AreEqual("CA", documentPurchaseOrders0PurchaseOrder1Address1State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text8 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Zip9 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[9];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1Zip9.Attributes.Length);
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder1Address1Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address1Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address1Zip9Text0 = documentPurchaseOrders0PurchaseOrder1Address1Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Zip9Text0.NodeType);
            Assert.AreEqual("10999", documentPurchaseOrders0PurchaseOrder1Address1Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Text10 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address1Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address1Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address1Country11 = documentPurchaseOrders0PurchaseOrder1Address1.ChildNodes[11];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address1Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address1Country11.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder1Address3 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[3];
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3.Attributes.Length);
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder1Address3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder1Address3.Attributes["Type"]);
            Assert.AreEqual("Type", documentPurchaseOrders0PurchaseOrder1Address3.Attributes["Type"].Name);
            Assert.AreEqual("Billing", documentPurchaseOrders0PurchaseOrder1Address3.Attributes["Type"].Value);

            var documentPurchaseOrders0PurchaseOrder1Address3Text0 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Name1 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3Name1.Attributes.Length);
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder1Address3Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3Name1Text0 = documentPurchaseOrders0PurchaseOrder1Address3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Name1Text0.NodeType);
            Assert.AreEqual("Tai Yee", documentPurchaseOrders0PurchaseOrder1Address3Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text2 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Street3 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3Street3.Attributes.Length);
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder1Address3Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3Street3Text0 = documentPurchaseOrders0PurchaseOrder1Address3Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Street3Text0.NodeType);
            Assert.AreEqual("8 Oak Avenue", documentPurchaseOrders0PurchaseOrder1Address3Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text4 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3City5 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3City5.Attributes.Length);
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder1Address3City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3City5Text0 = documentPurchaseOrders0PurchaseOrder1Address3City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3City5Text0.NodeType);
            Assert.AreEqual("Old Town", documentPurchaseOrders0PurchaseOrder1Address3City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text6 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3State7 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3State7.Attributes.Length);
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder1Address3State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3State7Text0 = documentPurchaseOrders0PurchaseOrder1Address3State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3State7Text0.NodeType);
            Assert.AreEqual("PA", documentPurchaseOrders0PurchaseOrder1Address3State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text8 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Zip9 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[9];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3Zip9.Attributes.Length);
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder1Address3Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Address3Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Address3Zip9Text0 = documentPurchaseOrders0PurchaseOrder1Address3Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Zip9Text0.NodeType);
            Assert.AreEqual("95819", documentPurchaseOrders0PurchaseOrder1Address3Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Text10 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Address3Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Address3Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Address3Country11 = documentPurchaseOrders0PurchaseOrder1Address3.ChildNodes[11];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Address3Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Address3Country11.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder1DeliveryNotes5 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.Attributes.Length);
            Assert.AreEqual("DeliveryNotes", documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1DeliveryNotes5Text0 = documentPurchaseOrders0PurchaseOrder1DeliveryNotes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1DeliveryNotes5Text0.NodeType);
            Assert.AreEqual("Please leave packages in shed by driveway.", documentPurchaseOrders0PurchaseOrder1DeliveryNotes5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Text6 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Text6.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7 = documentPurchaseOrders0PurchaseOrder1.ChildNodes[7];
            Assert.AreEqual(5, documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7.Attributes.Length);
            Assert.AreEqual("Items", documentPurchaseOrders0PurchaseOrder1Items7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Text0 = documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder1Items7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1 = documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes[1];
            Assert.AreEqual(9, documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1.Attributes.Length);
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder1Items7Item1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item1.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder1Items7Item1.Attributes["PartNumber"]);
            Assert.AreEqual("PartNumber", documentPurchaseOrders0PurchaseOrder1Items7Item1.Attributes["PartNumber"].Name);
            Assert.AreEqual("872-AA", documentPurchaseOrders0PurchaseOrder1Items7Item1.Attributes["PartNumber"].Value);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.Attributes.Length);
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1Text0.NodeType);
            Assert.AreEqual("Lawnmower", documentPurchaseOrders0PurchaseOrder1Items7Item1ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Text2 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrders0PurchaseOrder1Items7Item1Quantity3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Text4 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.Attributes.Length);
            Assert.AreEqual("USPrice", documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5Text0.NodeType);
            Assert.AreEqual("148.95", documentPurchaseOrders0PurchaseOrder1Items7Item1USPrice5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Text6 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item1Text6.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7 = documentPurchaseOrders0PurchaseOrder1Items7Item1.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item1Comment7.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder1Items7Item3 = documentPurchaseOrders0PurchaseOrder1Items7.ChildNodes[3];
            Assert.AreEqual(9, documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3.Attributes.Length);
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder1Items7Item3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item3.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder1Items7Item3.Attributes["PartNumber"]);
            Assert.AreEqual("PartNumber", documentPurchaseOrders0PurchaseOrder1Items7Item3.Attributes["PartNumber"].Name);
            Assert.AreEqual("926-AA", documentPurchaseOrders0PurchaseOrder1Items7Item3.Attributes["PartNumber"].Value);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.Attributes.Length);
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1Text0.NodeType);
            Assert.AreEqual("Baby Monitor", documentPurchaseOrders0PurchaseOrder1Items7Item3ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Text2 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3Text0.NodeType);
            Assert.AreEqual("2", documentPurchaseOrders0PurchaseOrder1Items7Item3Quantity3Text0.TextContent);


            var documentPurchaseOrders0PurchaseOrder1Items7Item3Text4 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.Attributes.Length);
            Assert.AreEqual("USPrice", documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.NodeType);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5Text0 = documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5Text0.NodeType);
            Assert.AreEqual("39.98", documentPurchaseOrders0PurchaseOrder1Items7Item3USPrice5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3Text6 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder1Items7Item3Text6.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder1Items7Item3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7 = documentPurchaseOrders0PurchaseOrder1Items7Item3.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder1Items7Item3ShipDate7.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder3 = documentPurchaseOrders0.ChildNodes[3];
            Assert.AreEqual(9, documentPurchaseOrders0PurchaseOrder3.ChildNodes.Length);
            Assert.AreEqual(2, documentPurchaseOrders0PurchaseOrder3.Attributes.Length);
            Assert.AreEqual("PurchaseOrder", documentPurchaseOrders0PurchaseOrder3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder3.Attributes["PurchaseOrderNumber"]);
            Assert.AreEqual("PurchaseOrderNumber", documentPurchaseOrders0PurchaseOrder3.Attributes["PurchaseOrderNumber"].Name);
            Assert.AreEqual("99505", documentPurchaseOrders0PurchaseOrder3.Attributes["PurchaseOrderNumber"].Value);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder3.Attributes["OrderDate"]);
            Assert.AreEqual("OrderDate", documentPurchaseOrders0PurchaseOrder3.Attributes["OrderDate"].Name);
            Assert.AreEqual("1999-10-22", documentPurchaseOrders0PurchaseOrder3.Attributes["OrderDate"].Value);

            var documentPurchaseOrders0PurchaseOrder3Text0 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[1];
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1.Attributes.Length);
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder3Address1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder3Address1.Attributes["Type"]);
            Assert.AreEqual("Type", documentPurchaseOrders0PurchaseOrder3Address1.Attributes["Type"].Name);
            Assert.AreEqual("Shipping", documentPurchaseOrders0PurchaseOrder3Address1.Attributes["Type"].Value);

            var documentPurchaseOrders0PurchaseOrder3Address1Text0 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Name1 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1Name1.Attributes.Length);
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder3Address1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1Name1Text0 = documentPurchaseOrders0PurchaseOrder3Address1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Name1Text0.NodeType);
            Assert.AreEqual("Cristian Osorio", documentPurchaseOrders0PurchaseOrder3Address1Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text2 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Street3 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1Street3.Attributes.Length);
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder3Address1Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1Street3Text0 = documentPurchaseOrders0PurchaseOrder3Address1Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Street3Text0.NodeType);
            Assert.AreEqual("456 Main Street", documentPurchaseOrders0PurchaseOrder3Address1Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text4 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1City5 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1City5.Attributes.Length);
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder3Address1City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1City5Text0 = documentPurchaseOrders0PurchaseOrder3Address1City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1City5Text0.NodeType);
            Assert.AreEqual("Buffalo", documentPurchaseOrders0PurchaseOrder3Address1City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text6 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1State7 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1State7.Attributes.Length);
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder3Address1State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1State7Text0 = documentPurchaseOrders0PurchaseOrder3Address1State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1State7Text0.NodeType);
            Assert.AreEqual("NY", documentPurchaseOrders0PurchaseOrder3Address1State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text8 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Zip9 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[9];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1Zip9.Attributes.Length);
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder3Address1Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address1Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address1Zip9Text0 = documentPurchaseOrders0PurchaseOrder3Address1Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Zip9Text0.NodeType);
            Assert.AreEqual("98112", documentPurchaseOrders0PurchaseOrder3Address1Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Text10 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address1Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address1Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address1Country11 = documentPurchaseOrders0PurchaseOrder3Address1.ChildNodes[11];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address1Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address1Country11.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder3Address3 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[3];
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3.Attributes.Length);
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder3Address3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder3Address3.Attributes["Type"]);
            Assert.AreEqual("Type", documentPurchaseOrders0PurchaseOrder3Address3.Attributes["Type"].Name);
            Assert.AreEqual("Billing", documentPurchaseOrders0PurchaseOrder3Address3.Attributes["Type"].Value);

            var documentPurchaseOrders0PurchaseOrder3Address3Text0 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Name1 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3Name1.Attributes.Length);
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder3Address3Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3Name1Text0 = documentPurchaseOrders0PurchaseOrder3Address3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Name1Text0.NodeType);
            Assert.AreEqual("Cristian Osorio", documentPurchaseOrders0PurchaseOrder3Address3Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text2 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Street3 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3Street3.Attributes.Length);
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder3Address3Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3Street3Text0 = documentPurchaseOrders0PurchaseOrder3Address3Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Street3Text0.NodeType);
            Assert.AreEqual("456 Main Street", documentPurchaseOrders0PurchaseOrder3Address3Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text4 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3City5 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3City5.Attributes.Length);
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder3Address3City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3City5Text0 = documentPurchaseOrders0PurchaseOrder3Address3City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3City5Text0.NodeType);
            Assert.AreEqual("Buffalo", documentPurchaseOrders0PurchaseOrder3Address3City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text6 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3State7 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3State7.Attributes.Length);
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder3Address3State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3State7Text0 = documentPurchaseOrders0PurchaseOrder3Address3State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3State7Text0.NodeType);
            Assert.AreEqual("NY", documentPurchaseOrders0PurchaseOrder3Address3State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text8 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Zip9 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[9];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3Zip9.Attributes.Length);
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder3Address3Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Address3Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Address3Zip9Text0 = documentPurchaseOrders0PurchaseOrder3Address3Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Zip9Text0.NodeType);
            Assert.AreEqual("98112", documentPurchaseOrders0PurchaseOrder3Address3Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Text10 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Address3Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Address3Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Address3Country11 = documentPurchaseOrders0PurchaseOrder3Address3.ChildNodes[11];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Address3Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Address3Country11.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder3DeliveryNotes5 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.Attributes.Length);
            Assert.AreEqual("DeliveryNotes", documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.NodeType);

            var documentPurchaseOrders0PurchaseOrder3DeliveryNotes5Text0 = documentPurchaseOrders0PurchaseOrder3DeliveryNotes5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3DeliveryNotes5Text0.NodeType);
            Assert.AreEqual("Please notify me before shipping.", documentPurchaseOrders0PurchaseOrder3DeliveryNotes5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Text6 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Text6.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7 = documentPurchaseOrders0PurchaseOrder3.ChildNodes[7];
            Assert.AreEqual(3, documentPurchaseOrders0PurchaseOrder3Items7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Items7.Attributes.Length);
            Assert.AreEqual("Items", documentPurchaseOrders0PurchaseOrder3Items7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Items7.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Items7Text0 = documentPurchaseOrders0PurchaseOrder3Items7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder3Items7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1 = documentPurchaseOrders0PurchaseOrder3Items7.ChildNodes[1];
            Assert.AreEqual(7, documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Items7Item1.Attributes.Length);
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder3Items7Item1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Items7Item1.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder3Items7Item1.Attributes["PartNumber"]);
            Assert.AreEqual("PartNumber", documentPurchaseOrders0PurchaseOrder3Items7Item1.Attributes["PartNumber"].Name);
            Assert.AreEqual("456-NM", documentPurchaseOrders0PurchaseOrder3Items7Item1.Attributes["PartNumber"].Value);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Text0 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder3Items7Item1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.Attributes.Length);
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1Text0 = documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1Text0.NodeType);
            Assert.AreEqual("Power Supply", documentPurchaseOrders0PurchaseOrder3Items7Item1ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Text2 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder3Items7Item1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3Text0 = documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrders0PurchaseOrder3Items7Item1Quantity3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1Text4 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder3Items7Item1Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder3Items7Item1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5 = documentPurchaseOrders0PurchaseOrder3Items7Item1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder3Items7Item1USPrice5.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder5 = documentPurchaseOrders0.ChildNodes[5];
            Assert.AreEqual(7, documentPurchaseOrders0PurchaseOrder5.ChildNodes.Length);
            Assert.AreEqual(2, documentPurchaseOrders0PurchaseOrder5.Attributes.Length);
            Assert.AreEqual("PurchaseOrder", documentPurchaseOrders0PurchaseOrder5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder5.Attributes["PurchaseOrderNumber"]);
            Assert.AreEqual("PurchaseOrderNumber", documentPurchaseOrders0PurchaseOrder5.Attributes["PurchaseOrderNumber"].Name);
            Assert.AreEqual("99504", documentPurchaseOrders0PurchaseOrder5.Attributes["PurchaseOrderNumber"].Value);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder5.Attributes["OrderDate"]);
            Assert.AreEqual("OrderDate", documentPurchaseOrders0PurchaseOrder5.Attributes["OrderDate"].Name);
            Assert.AreEqual("1999-10-22", documentPurchaseOrders0PurchaseOrder5.Attributes["OrderDate"].Value);

            var documentPurchaseOrders0PurchaseOrder5Text0 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Text0.NodeType);
            Assert.AreEqual("\n    ", documentPurchaseOrders0PurchaseOrder5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[1];
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1.Attributes.Length);
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder5Address1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder5Address1.Attributes["Type"]);
            Assert.AreEqual("Type", documentPurchaseOrders0PurchaseOrder5Address1.Attributes["Type"].Name);
            Assert.AreEqual("Shipping", documentPurchaseOrders0PurchaseOrder5Address1.Attributes["Type"].Value);

            var documentPurchaseOrders0PurchaseOrder5Address1Text0 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Name1 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1Name1.Attributes.Length);
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder5Address1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1Name1Text0 = documentPurchaseOrders0PurchaseOrder5Address1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Name1Text0.NodeType);
            Assert.AreEqual("Jessica Arnold", documentPurchaseOrders0PurchaseOrder5Address1Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text2 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Street3 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1Street3.Attributes.Length);
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder5Address1Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1Street3Text0 = documentPurchaseOrders0PurchaseOrder5Address1Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Street3Text0.NodeType);
            Assert.AreEqual("4055 Madison Ave", documentPurchaseOrders0PurchaseOrder5Address1Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text4 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1City5 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1City5.Attributes.Length);
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder5Address1City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1City5Text0 = documentPurchaseOrders0PurchaseOrder5Address1City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1City5Text0.NodeType);
            Assert.AreEqual("Seattle", documentPurchaseOrders0PurchaseOrder5Address1City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text6 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1State7 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1State7.Attributes.Length);
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder5Address1State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1State7Text0 = documentPurchaseOrders0PurchaseOrder5Address1State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1State7Text0.NodeType);
            Assert.AreEqual("WA", documentPurchaseOrders0PurchaseOrder5Address1State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text8 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Zip9 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[9];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1Zip9.Attributes.Length);
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder5Address1Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address1Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address1Zip9Text0 = documentPurchaseOrders0PurchaseOrder5Address1Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Zip9Text0.NodeType);
            Assert.AreEqual("98112", documentPurchaseOrders0PurchaseOrder5Address1Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Text10 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address1Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address1Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address1Country11 = documentPurchaseOrders0PurchaseOrder5Address1.ChildNodes[11];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address1Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address1Country11.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder5Address3 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[3];
            Assert.AreEqual(13, documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3.Attributes.Length);
            Assert.AreEqual("Address", documentPurchaseOrders0PurchaseOrder5Address3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder5Address3.Attributes["Type"]);
            Assert.AreEqual("Type", documentPurchaseOrders0PurchaseOrder5Address3.Attributes["Type"].Name);
            Assert.AreEqual("Billing", documentPurchaseOrders0PurchaseOrder5Address3.Attributes["Type"].Value);

            var documentPurchaseOrders0PurchaseOrder5Address3Text0 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Name1 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3Name1.Attributes.Length);
            Assert.AreEqual("Name", documentPurchaseOrders0PurchaseOrder5Address3Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3Name1.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3Name1Text0 = documentPurchaseOrders0PurchaseOrder5Address3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Name1Text0.NodeType);
            Assert.AreEqual("Jessica Arnold", documentPurchaseOrders0PurchaseOrder5Address3Name1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text2 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text2.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Street3 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3Street3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3Street3.Attributes.Length);
            Assert.AreEqual("Street", documentPurchaseOrders0PurchaseOrder5Address3Street3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3Street3.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3Street3Text0 = documentPurchaseOrders0PurchaseOrder5Address3Street3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Street3Text0.NodeType);
            Assert.AreEqual("4055 Madison Ave", documentPurchaseOrders0PurchaseOrder5Address3Street3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text4 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text4.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3City5 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3City5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3City5.Attributes.Length);
            Assert.AreEqual("City", documentPurchaseOrders0PurchaseOrder5Address3City5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3City5.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3City5Text0 = documentPurchaseOrders0PurchaseOrder5Address3City5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3City5Text0.NodeType);
            Assert.AreEqual("Buffalo", documentPurchaseOrders0PurchaseOrder5Address3City5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text6 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text6.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text6.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3State7 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[7];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3State7.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3State7.Attributes.Length);
            Assert.AreEqual("State", documentPurchaseOrders0PurchaseOrder5Address3State7.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3State7.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3State7Text0 = documentPurchaseOrders0PurchaseOrder5Address3State7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3State7Text0.NodeType);
            Assert.AreEqual("NY", documentPurchaseOrders0PurchaseOrder5Address3State7Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text8 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[8];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text8.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text8.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Zip9 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[9];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3Zip9.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3Zip9.Attributes.Length);
            Assert.AreEqual("Zip", documentPurchaseOrders0PurchaseOrder5Address3Zip9.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Address3Zip9.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Address3Zip9Text0 = documentPurchaseOrders0PurchaseOrder5Address3Zip9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Zip9Text0.NodeType);
            Assert.AreEqual("98112", documentPurchaseOrders0PurchaseOrder5Address3Zip9Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Text10 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[10];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Address3Text10.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Address3Text10.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Address3Country11 = documentPurchaseOrders0PurchaseOrder5Address3.ChildNodes[11];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Address3Country11.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Address3Country11.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder5Items5 = documentPurchaseOrders0PurchaseOrder5.ChildNodes[5];
            Assert.AreEqual(5, documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5.Attributes.Length);
            Assert.AreEqual("Items", documentPurchaseOrders0PurchaseOrder5Items5.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Text0 = documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Text0.NodeType);
            Assert.AreEqual("\n      ", documentPurchaseOrders0PurchaseOrder5Items5Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1 = documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes[1];
            Assert.AreEqual(7, documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item1.Attributes.Length);
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder5Items5Item1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item1.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder5Items5Item1.Attributes["PartNumber"]);
            Assert.AreEqual("PartNumber", documentPurchaseOrders0PurchaseOrder5Items5Item1.Attributes["PartNumber"].Name);
            Assert.AreEqual("898-AZ", documentPurchaseOrders0PurchaseOrder5Items5Item1.Attributes["PartNumber"].Value);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.Attributes.Length);
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1Text0.NodeType);
            Assert.AreEqual("Computer Keyboard", documentPurchaseOrders0PurchaseOrder5Items5Item1ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Text2 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item1Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrders0PurchaseOrder5Items5Item1Quantity3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1Text4 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item1Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item1Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5 = documentPurchaseOrders0PurchaseOrder5Items5Item1.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item1USPrice5.Attributes.Length);
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

            var documentPurchaseOrders0PurchaseOrder5Items5Item3 = documentPurchaseOrders0PurchaseOrder5Items5.ChildNodes[3];
            Assert.AreEqual(7, documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes.Length);
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item3.Attributes.Length);
            Assert.AreEqual("Item", documentPurchaseOrders0PurchaseOrder5Items5Item3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item3.NodeType);
            Assert.IsNotNull(documentPurchaseOrders0PurchaseOrder5Items5Item3.Attributes["PartNumber"]);
            Assert.AreEqual("PartNumber", documentPurchaseOrders0PurchaseOrder5Items5Item3.Attributes["PartNumber"].Name);
            Assert.AreEqual("898-AM", documentPurchaseOrders0PurchaseOrder5Items5Item3.Attributes["PartNumber"].Value);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3Text0.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[1];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.Attributes.Length);
            Assert.AreEqual("ProductName", documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1Text0.NodeType);
            Assert.AreEqual("Wireless Mouse", documentPurchaseOrders0PurchaseOrder5Items5Item3ProductName1Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Text2 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3Text2.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item3Text2.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[3];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.NodeType);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3Text0 = documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentPurchaseOrders0PurchaseOrder5Items5Item3Quantity3Text0.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3Text4 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentPurchaseOrders0PurchaseOrder5Items5Item3Text4.NodeType);
            Assert.AreEqual("\n        ", documentPurchaseOrders0PurchaseOrder5Items5Item3Text4.TextContent);

            var documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5 = documentPurchaseOrders0PurchaseOrder5Items5Item3.ChildNodes[5];
            Assert.AreEqual(1, documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5.ChildNodes.Length);
            Assert.AreEqual(0, documentPurchaseOrders0PurchaseOrder5Items5Item3USPrice5.Attributes.Length);
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

        [TestMethod]
        public void XmlMultiplePurchaseOrderInNamespace()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8""?>
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
</aw:PurchaseOrders>");

            //TODO
        }

        [TestMethod]
        public void XmlTestConfiguration()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0""?>
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
</Tests>");

            var documentTests0 = document.ChildNodes[0];
            Assert.AreEqual(13, documentTests0.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0.Attributes.Length);
            Assert.AreEqual("Tests", documentTests0.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0.NodeType);

            var documentTests0Text0 = documentTests0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Text0.NodeType);
            Assert.AreEqual("\n  ", documentTests0Text0.TextContent);

            var documentTests0Test1 = documentTests0.ChildNodes[1];
            Assert.AreEqual(9, documentTests0Test1.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test1.Attributes.Length);
            Assert.AreEqual("Test", documentTests0Test1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test1.NodeType);
            Assert.IsNotNull(documentTests0Test1.Attributes["TestId"]);
            Assert.AreEqual("TestId", documentTests0Test1.Attributes["TestId"].Name);
            Assert.AreEqual("0001", documentTests0Test1.Attributes["TestId"].Value);
            Assert.IsNotNull(documentTests0Test1.Attributes["TestType"]);
            Assert.AreEqual("TestType", documentTests0Test1.Attributes["TestType"].Name);
            Assert.AreEqual("CMD", documentTests0Test1.Attributes["TestType"].Value);

            var documentTests0Test1Text0 = documentTests0Test1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test1Text0.TextContent);

            var documentTests0Test1Name1 = documentTests0Test1.ChildNodes[1];
            Assert.AreEqual(1, documentTests0Test1Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test1Name1.Attributes.Length);
            Assert.AreEqual("Name", documentTests0Test1Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test1Name1.NodeType);

            var documentTests0Test1Name1Text0 = documentTests0Test1Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Name1Text0.NodeType);
            Assert.AreEqual("Convert number to string", documentTests0Test1Name1Text0.TextContent);

            var documentTests0Test1Text2 = documentTests0Test1.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test1Text2.TextContent);

            var documentTests0Test1CommandLine3 = documentTests0Test1.ChildNodes[3];
            Assert.AreEqual(1, documentTests0Test1CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test1CommandLine3.Attributes.Length);
            Assert.AreEqual("CommandLine", documentTests0Test1CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test1CommandLine3.NodeType);

            var documentTests0Test1CommandLine3Text0 = documentTests0Test1CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test1CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp1.EXE", documentTests0Test1CommandLine3Text0.TextContent);

            var documentTests0Test1Text4 = documentTests0Test1.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test1Text4.TextContent);

            var documentTests0Test1Input5 = documentTests0Test1.ChildNodes[5];
            Assert.AreEqual(1, documentTests0Test1Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test1Input5.Attributes.Length);
            Assert.AreEqual("Input", documentTests0Test1Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test1Input5.NodeType);

            var documentTests0Test1Input5Text0 = documentTests0Test1Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Input5Text0.NodeType);
            Assert.AreEqual("1", documentTests0Test1Input5Text0.TextContent);

            var documentTests0Test1Text6 = documentTests0Test1.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test1Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test1Text6.TextContent);

            var documentTests0Test1Output7 = documentTests0Test1.ChildNodes[7];
            Assert.AreEqual(1, documentTests0Test1Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test1Output7.Attributes.Length);
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

            var documentTests0Test3 = documentTests0.ChildNodes[3];
            Assert.AreEqual(9, documentTests0Test3.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test3.Attributes.Length);
            Assert.AreEqual("Test", documentTests0Test3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test3.NodeType);
            Assert.IsNotNull(documentTests0Test3.Attributes["TestId"]);
            Assert.AreEqual("TestId", documentTests0Test3.Attributes["TestId"].Name);
            Assert.AreEqual("0002", documentTests0Test3.Attributes["TestId"].Value);
            Assert.IsNotNull(documentTests0Test3.Attributes["TestType"]);
            Assert.AreEqual("TestType", documentTests0Test3.Attributes["TestType"].Name);
            Assert.AreEqual("CMD", documentTests0Test3.Attributes["TestType"].Value);

            var documentTests0Test3Text0 = documentTests0Test3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test3Text0.TextContent);

            var documentTests0Test3Name1 = documentTests0Test3.ChildNodes[1];
            Assert.AreEqual(1, documentTests0Test3Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test3Name1.Attributes.Length);
            Assert.AreEqual("Name", documentTests0Test3Name1.NodeName);

            Assert.AreEqual(NodeType.Element, documentTests0Test3Name1.NodeType);
            var documentTests0Test3Name1Text0 = documentTests0Test3Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Name1Text0.NodeType);
            Assert.AreEqual("Find succeeding characters", documentTests0Test3Name1Text0.TextContent);

            var documentTests0Test3Text2 = documentTests0Test3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test3Text2.TextContent);

            var documentTests0Test3CommandLine3 = documentTests0Test3.ChildNodes[3];
            Assert.AreEqual(1, documentTests0Test3CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test3CommandLine3.Attributes.Length);
            Assert.AreEqual("CommandLine", documentTests0Test3CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test3CommandLine3.NodeType);

            var documentTests0Test3CommandLine3Text0 = documentTests0Test3CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test3CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp2.EXE", documentTests0Test3CommandLine3Text0.TextContent);

            var documentTests0Test3Text4 = documentTests0Test3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test3Text4.TextContent);

            var documentTests0Test3Input5 = documentTests0Test3.ChildNodes[5];
            Assert.AreEqual(1, documentTests0Test3Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test3Input5.Attributes.Length);
            Assert.AreEqual("Input", documentTests0Test3Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test3Input5.NodeType);

            var documentTests0Test3Input5Text0 = documentTests0Test3Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Input5Text0.NodeType);
            Assert.AreEqual("abc", documentTests0Test3Input5Text0.TextContent);

            var documentTests0Test3Text6 = documentTests0Test3.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test3Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test3Text6.TextContent);

            var documentTests0Test3Output7 = documentTests0Test3.ChildNodes[7];
            Assert.AreEqual(1, documentTests0Test3Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test3Output7.Attributes.Length);
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

            var documentTests0Test5 = documentTests0.ChildNodes[5];
            Assert.AreEqual(9, documentTests0Test5.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test5.Attributes.Length);
            Assert.AreEqual("Test", documentTests0Test5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test5.NodeType);
            Assert.IsNotNull(documentTests0Test5.Attributes["TestId"]);
            Assert.AreEqual("TestId", documentTests0Test5.Attributes["TestId"].Name);
            Assert.AreEqual("0003", documentTests0Test5.Attributes["TestId"].Value);
            Assert.IsNotNull(documentTests0Test5.Attributes["TestType"]);
            Assert.AreEqual("TestType", documentTests0Test5.Attributes["TestType"].Name);
            Assert.AreEqual("GUI", documentTests0Test5.Attributes["TestType"].Value);

            var documentTests0Test5Text0 = documentTests0Test5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test5Text0.TextContent);

            var documentTests0Test5Name1 = documentTests0Test5.ChildNodes[1];
            Assert.AreEqual(1, documentTests0Test5Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test5Name1.Attributes.Length);
            Assert.AreEqual("Name", documentTests0Test5Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test5Name1.NodeType);

            var documentTests0Test5Name1Text0 = documentTests0Test5Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Name1Text0.NodeType);
            Assert.AreEqual("Convert multiple numbers to strings", documentTests0Test5Name1Text0.TextContent);

            var documentTests0Test5Text2 = documentTests0Test5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test5Text2.TextContent);

            var documentTests0Test5CommandLine3 = documentTests0Test5.ChildNodes[3];
            Assert.AreEqual(1, documentTests0Test5CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test5CommandLine3.Attributes.Length);
            Assert.AreEqual("CommandLine", documentTests0Test5CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test5CommandLine3.NodeType);

            var documentTests0Test5CommandLine3Text0 = documentTests0Test5CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test5CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp2.EXE /Verbose", documentTests0Test5CommandLine3Text0.TextContent);

            var documentTests0Test5Text4 = documentTests0Test5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test5Text4.TextContent);

            var documentTests0Test5Input5 = documentTests0Test5.ChildNodes[5];
            Assert.AreEqual(1, documentTests0Test5Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test5Input5.Attributes.Length);
            Assert.AreEqual("Input", documentTests0Test5Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test5Input5.NodeType);

            var documentTests0Test5Input5Text0 = documentTests0Test5Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Input5Text0.NodeType);
            Assert.AreEqual("123", documentTests0Test5Input5Text0.TextContent);

            var documentTests0Test5Text6 = documentTests0Test5.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test5Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test5Text6.TextContent);

            var documentTests0Test5Output7 = documentTests0Test5.ChildNodes[7];
            Assert.AreEqual(1, documentTests0Test5Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test5Output7.Attributes.Length);
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

            var documentTests0Test7 = documentTests0.ChildNodes[7];
            Assert.AreEqual(9, documentTests0Test7.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test7.Attributes.Length);
            Assert.AreEqual("Test", documentTests0Test7.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test7.NodeType);
            Assert.IsNotNull(documentTests0Test7.Attributes["TestId"]);
            Assert.AreEqual("TestId", documentTests0Test7.Attributes["TestId"].Name);
            Assert.AreEqual("0004", documentTests0Test7.Attributes["TestId"].Value);
            Assert.IsNotNull(documentTests0Test7.Attributes["TestType"]);
            Assert.AreEqual("TestType", documentTests0Test7.Attributes["TestType"].Name);
            Assert.AreEqual("GUI", documentTests0Test7.Attributes["TestType"].Value);

            var documentTests0Test7Text0 = documentTests0Test7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test7Text0.TextContent);

            var documentTests0Test7Name1 = documentTests0Test7.ChildNodes[1];
            Assert.AreEqual(1, documentTests0Test7Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test7Name1.Attributes.Length);
            Assert.AreEqual("Name", documentTests0Test7Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test7Name1.NodeType);

            var documentTests0Test7Name1Text0 = documentTests0Test7Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Name1Text0.NodeType);
            Assert.AreEqual("Find correlated key", documentTests0Test7Name1Text0.TextContent);

            var documentTests0Test7Text2 = documentTests0Test7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test7Text2.TextContent);

            var documentTests0Test7CommandLine3 = documentTests0Test7.ChildNodes[3];
            Assert.AreEqual(1, documentTests0Test7CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test7CommandLine3.Attributes.Length);
            Assert.AreEqual("CommandLine", documentTests0Test7CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test7CommandLine3.NodeType);

            var documentTests0Test7CommandLine3Text0 = documentTests0Test7CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test7CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp3.EXE", documentTests0Test7CommandLine3Text0.TextContent);

            var documentTests0Test7Text4 = documentTests0Test7.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test7Text4.TextContent);

            var documentTests0Test7Input5 = documentTests0Test7.ChildNodes[5];
            Assert.AreEqual(1, documentTests0Test7Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test7Input5.Attributes.Length);
            Assert.AreEqual("Input", documentTests0Test7Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test7Input5.NodeType);

            var documentTests0Test7Input5Text0 = documentTests0Test7Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Input5Text0.NodeType);
            Assert.AreEqual("a1", documentTests0Test7Input5Text0.TextContent);

            var documentTests0Test7Text6 = documentTests0Test7.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test7Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test7Text6.TextContent);

            var documentTests0Test7Output7 = documentTests0Test7.ChildNodes[7];
            Assert.AreEqual(1, documentTests0Test7Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test7Output7.Attributes.Length);
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

            var documentTests0Test9 = documentTests0.ChildNodes[9];
            Assert.AreEqual(9, documentTests0Test9.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test9.Attributes.Length);
            Assert.AreEqual("Test", documentTests0Test9.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test9.NodeType);
            Assert.IsNotNull(documentTests0Test9.Attributes["TestId"]);
            Assert.AreEqual("TestId", documentTests0Test9.Attributes["TestId"].Name);
            Assert.AreEqual("0005", documentTests0Test9.Attributes["TestId"].Value);
            Assert.IsNotNull(documentTests0Test9.Attributes["TestType"]);
            Assert.AreEqual("TestType", documentTests0Test9.Attributes["TestType"].Name);
            Assert.AreEqual("GUI", documentTests0Test9.Attributes["TestType"].Value);

            var documentTests0Test9Text0 = documentTests0Test9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test9Text0.TextContent);

            var documentTests0Test9Name1 = documentTests0Test9.ChildNodes[1];
            Assert.AreEqual(1, documentTests0Test9Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test9Name1.Attributes.Length);
            Assert.AreEqual("Name", documentTests0Test9Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test9Name1.NodeType);

            var documentTests0Test9Name1Text0 = documentTests0Test9Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Name1Text0.NodeType);
            Assert.AreEqual("Count characters", documentTests0Test9Name1Text0.TextContent);

            var documentTests0Test9Text2 = documentTests0Test9.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test9Text2.TextContent);

            var documentTests0Test9CommandLine3 = documentTests0Test9.ChildNodes[3];
            Assert.AreEqual(1, documentTests0Test9CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test9CommandLine3.Attributes.Length);
            Assert.AreEqual("CommandLine", documentTests0Test9CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test9CommandLine3.NodeType);

            var documentTests0Test9CommandLine3Text0 = documentTests0Test9CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test9CommandLine3Text0.NodeType);
            Assert.AreEqual("FinalExamp.EXE", documentTests0Test9CommandLine3Text0.TextContent);

            var documentTests0Test9Text4 = documentTests0Test9.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test9Text4.TextContent);

            var documentTests0Test9Input5 = documentTests0Test9.ChildNodes[5];
            Assert.AreEqual(1, documentTests0Test9Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test9Input5.Attributes.Length);
            Assert.AreEqual("Input", documentTests0Test9Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test9Input5.NodeType);

            var documentTests0Test9Input5Text0 = documentTests0Test9Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Input5Text0.NodeType);
            Assert.AreEqual("This is a test", documentTests0Test9Input5Text0.TextContent);

            var documentTests0Test9Text6 = documentTests0Test9.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test9Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test9Text6.TextContent);

            var documentTests0Test9Output7 = documentTests0Test9.ChildNodes[7];
            Assert.AreEqual(1, documentTests0Test9Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test9Output7.Attributes.Length);
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

            var documentTests0Test11 = documentTests0.ChildNodes[11];
            Assert.AreEqual(9, documentTests0Test11.ChildNodes.Length);
            Assert.AreEqual(2, documentTests0Test11.Attributes.Length);
            Assert.AreEqual("Test", documentTests0Test11.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test11.NodeType);
            Assert.IsNotNull(documentTests0Test11.Attributes["TestId"]);
            Assert.AreEqual("TestId", documentTests0Test11.Attributes["TestId"].Name);
            Assert.AreEqual("0006", documentTests0Test11.Attributes["TestId"].Value);
            Assert.IsNotNull(documentTests0Test11.Attributes["TestType"]);
            Assert.AreEqual("TestType", documentTests0Test11.Attributes["TestType"].Name);
            Assert.AreEqual("GUI", documentTests0Test11.Attributes["TestType"].Value);

            var documentTests0Test11Text0 = documentTests0Test11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Text0.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test11Text0.TextContent);

            var documentTests0Test11Name1 = documentTests0Test11.ChildNodes[1];
            Assert.AreEqual(1, documentTests0Test11Name1.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test11Name1.Attributes.Length);
            Assert.AreEqual("Name", documentTests0Test11Name1.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test11Name1.NodeType);

            var documentTests0Test11Name1Text0 = documentTests0Test11Name1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Name1Text0.NodeType);
            Assert.AreEqual("Another Test", documentTests0Test11Name1Text0.TextContent);

            var documentTests0Test11Text2 = documentTests0Test11.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Text2.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test11Text2.TextContent);

            var documentTests0Test11CommandLine3 = documentTests0Test11.ChildNodes[3];
            Assert.AreEqual(1, documentTests0Test11CommandLine3.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test11CommandLine3.Attributes.Length);
            Assert.AreEqual("CommandLine", documentTests0Test11CommandLine3.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test11CommandLine3.NodeType);

            var documentTests0Test11CommandLine3Text0 = documentTests0Test11CommandLine3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test11CommandLine3Text0.NodeType);
            Assert.AreEqual("Examp2.EXE", documentTests0Test11CommandLine3Text0.TextContent);

            var documentTests0Test11Text4 = documentTests0Test11.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Text4.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test11Text4.TextContent);

            var documentTests0Test11Input5 = documentTests0Test11.ChildNodes[5];
            Assert.AreEqual(1, documentTests0Test11Input5.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test11Input5.Attributes.Length);
            Assert.AreEqual("Input", documentTests0Test11Input5.NodeName);
            Assert.AreEqual(NodeType.Element, documentTests0Test11Input5.NodeType);

            var documentTests0Test11Input5Text0 = documentTests0Test11Input5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Input5Text0.NodeType);
            Assert.AreEqual("Test Input", documentTests0Test11Input5Text0.TextContent);

            var documentTests0Test11Text6 = documentTests0Test11.ChildNodes[6];
            Assert.AreEqual(NodeType.Text, documentTests0Test11Text6.NodeType);
            Assert.AreEqual("\n    ", documentTests0Test11Text6.TextContent);

            var documentTests0Test11Output7 = documentTests0Test11.ChildNodes[7];
            Assert.AreEqual(1, documentTests0Test11Output7.ChildNodes.Length);
            Assert.AreEqual(0, documentTests0Test11Output7.Attributes.Length);
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

        [TestMethod]
        public void XmlCustomersAndOrders()
        {
            var document = DocumentBuilder.Xml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
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
</xs:schema>");

            //TODO
        }

        [TestMethod]
        public void XmlNumericalData()
        {
            var document = DocumentBuilder.Xml(@"<Root>
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
</Root>");

            var documentRoot0 = document.ChildNodes[0];
            Assert.AreEqual(17, documentRoot0.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0.Attributes.Length);
            Assert.AreEqual("Root", documentRoot0.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0.NodeType);

            var documentRoot0Text0 = documentRoot0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Text0.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text0.TextContent);

            var documentRoot0TaxRate1 = documentRoot0.ChildNodes[1];
            Assert.AreEqual(1, documentRoot0TaxRate1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0TaxRate1.Attributes.Length);
            Assert.AreEqual("TaxRate", documentRoot0TaxRate1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0TaxRate1.NodeType);

            var documentRoot0TaxRate1Text0 = documentRoot0TaxRate1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0TaxRate1Text0.NodeType);
            Assert.AreEqual("7.25", documentRoot0TaxRate1Text0.TextContent);

            var documentRoot0Text2 = documentRoot0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Text2.NodeType);
            Assert.AreEqual("\n  ", documentRoot0Text2.TextContent);

            var documentRoot0Data3 = documentRoot0.ChildNodes[3];
            Assert.AreEqual(7, documentRoot0Data3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data3.Attributes.Length);
            Assert.AreEqual("Data", documentRoot0Data3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data3.NodeType);

            var documentRoot0Data3Text0 = documentRoot0Data3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data3Text0.TextContent);

            var documentRoot0Data3Category1 = documentRoot0Data3.ChildNodes[1];
            Assert.AreEqual(1, documentRoot0Data3Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data3Category1.Attributes.Length);
            Assert.AreEqual("Category", documentRoot0Data3Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data3Category1.NodeType);

            var documentRoot0Data3Category1Text0 = documentRoot0Data3Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Category1Text0.NodeType);
            Assert.AreEqual("A", documentRoot0Data3Category1Text0.TextContent);

            var documentRoot0Data3Text2 = documentRoot0Data3.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data3Text2.TextContent);

            var documentRoot0Data3Quantity3 = documentRoot0Data3.ChildNodes[3];
            Assert.AreEqual(1, documentRoot0Data3Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data3Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentRoot0Data3Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data3Quantity3.NodeType);

            var documentRoot0Data3Quantity3Text0 = documentRoot0Data3Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Quantity3Text0.NodeType);
            Assert.AreEqual("3", documentRoot0Data3Quantity3Text0.TextContent);

            var documentRoot0Data3Text4 = documentRoot0Data3.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data3Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data3Text4.TextContent);

            var documentRoot0Data3Price5 = documentRoot0Data3.ChildNodes[5];
            Assert.AreEqual(1, documentRoot0Data3Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data3Price5.Attributes.Length);
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

            var documentRoot0Data5 = documentRoot0.ChildNodes[5];
            Assert.AreEqual(7, documentRoot0Data5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data5.Attributes.Length);
            Assert.AreEqual("Data", documentRoot0Data5.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data5.NodeType);

            var documentRoot0Data5Text0 = documentRoot0Data5.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data5Text0.TextContent);

            var documentRoot0Data5Category1 = documentRoot0Data5.ChildNodes[1];
            Assert.AreEqual(1, documentRoot0Data5Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data5Category1.Attributes.Length);
            Assert.AreEqual("Category", documentRoot0Data5Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data5Category1.NodeType);

            var documentRoot0Data5Category1Text0 = documentRoot0Data5Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Category1Text0.NodeType);
            Assert.AreEqual("B", documentRoot0Data5Category1Text0.TextContent);

            var documentRoot0Data5Text2 = documentRoot0Data5.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data5Text2.TextContent);

            var documentRoot0Data5Quantity3 = documentRoot0Data5.ChildNodes[3];
            Assert.AreEqual(1, documentRoot0Data5Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data5Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentRoot0Data5Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data5Quantity3.NodeType);

            var documentRoot0Data5Quantity3Text0 = documentRoot0Data5Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Quantity3Text0.NodeType);
            Assert.AreEqual("1", documentRoot0Data5Quantity3Text0.TextContent);

            var documentRoot0Data5Text4 = documentRoot0Data5.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data5Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data5Text4.TextContent);

            var documentRoot0Data5Price5 = documentRoot0Data5.ChildNodes[5];
            Assert.AreEqual(1, documentRoot0Data5Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data5Price5.Attributes.Length);
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

            var documentRoot0Data7 = documentRoot0.ChildNodes[7];
            Assert.AreEqual(7, documentRoot0Data7.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data7.Attributes.Length);
            Assert.AreEqual("Data", documentRoot0Data7.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data7.NodeType);

            var documentRoot0Data7Text0 = documentRoot0Data7.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data7Text0.TextContent);

            var documentRoot0Data7Category1 = documentRoot0Data7.ChildNodes[1];
            Assert.AreEqual(1, documentRoot0Data7Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data7Category1.Attributes.Length);
            Assert.AreEqual("Category", documentRoot0Data7Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data7Category1.NodeType);

            var documentRoot0Data7Category1Text0 = documentRoot0Data7Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Category1Text0.NodeType);
            Assert.AreEqual("A", documentRoot0Data7Category1Text0.TextContent);

            var documentRoot0Data7Text2 = documentRoot0Data7.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data7Text2.TextContent);

            var documentRoot0Data7Quantity3 = documentRoot0Data7.ChildNodes[3];
            Assert.AreEqual(1, documentRoot0Data7Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data7Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentRoot0Data7Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data7Quantity3.NodeType);

            var documentRoot0Data7Quantity3Text0 = documentRoot0Data7Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Quantity3Text0.NodeType);
            Assert.AreEqual("5", documentRoot0Data7Quantity3Text0.TextContent);

            var documentRoot0Data7Text4 = documentRoot0Data7.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data7Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data7Text4.TextContent);

            var documentRoot0Data7Price5 = documentRoot0Data7.ChildNodes[5];
            Assert.AreEqual(1, documentRoot0Data7Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data7Price5.Attributes.Length);
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

            var documentRoot0Data9 = documentRoot0.ChildNodes[9];
            Assert.AreEqual(7, documentRoot0Data9.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data9.Attributes.Length);
            Assert.AreEqual("Data", documentRoot0Data9.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data9.NodeType);

            var documentRoot0Data9Text0 = documentRoot0Data9.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data9Text0.TextContent);

            var documentRoot0Data9Category1 = documentRoot0Data9.ChildNodes[1];
            Assert.AreEqual(1, documentRoot0Data9Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data9Category1.Attributes.Length);
            Assert.AreEqual("Category", documentRoot0Data9Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data9Category1.NodeType);

            var documentRoot0Data9Category1Text0 = documentRoot0Data9Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Category1Text0.NodeType);
            Assert.AreEqual("A", documentRoot0Data9Category1Text0.TextContent);

            var documentRoot0Data9Text2 = documentRoot0Data9.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data9Text2.TextContent);

            var documentRoot0Data9Quantity3 = documentRoot0Data9.ChildNodes[3];
            Assert.AreEqual(1, documentRoot0Data9Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data9Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentRoot0Data9Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data9Quantity3.NodeType);

            var documentRoot0Data9Quantity3Text0 = documentRoot0Data9Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Quantity3Text0.NodeType);
            Assert.AreEqual("3", documentRoot0Data9Quantity3Text0.TextContent);

            var documentRoot0Data9Text4 = documentRoot0Data9.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data9Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data9Text4.TextContent);

            var documentRoot0Data9Price5 = documentRoot0Data9.ChildNodes[5];
            Assert.AreEqual(1, documentRoot0Data9Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data9Price5.Attributes.Length);
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

            var documentRoot0Data11 = documentRoot0.ChildNodes[11];
            Assert.AreEqual(7, documentRoot0Data11.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data11.Attributes.Length);
            Assert.AreEqual("Data", documentRoot0Data11.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data11.NodeType);

            var documentRoot0Data11Text0 = documentRoot0Data11.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data11Text0.TextContent);

            var documentRoot0Data11Category1 = documentRoot0Data11.ChildNodes[1];
            Assert.AreEqual(1, documentRoot0Data11Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data11Category1.Attributes.Length);
            Assert.AreEqual("Category", documentRoot0Data11Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data11Category1.NodeType);

            var documentRoot0Data11Category1Text0 = documentRoot0Data11Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Category1Text0.NodeType);
            Assert.AreEqual("B", documentRoot0Data11Category1Text0.TextContent);

            var documentRoot0Data11Text2 = documentRoot0Data11.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data11Text2.TextContent);

            var documentRoot0Data11Quantity3 = documentRoot0Data11.ChildNodes[3];
            Assert.AreEqual(1, documentRoot0Data11Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data11Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentRoot0Data11Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data11Quantity3.NodeType);

            var documentRoot0Data11Quantity3Text0 = documentRoot0Data11Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Quantity3Text0.NodeType);
            Assert.AreEqual("10", documentRoot0Data11Quantity3Text0.TextContent);

            var documentRoot0Data11Text4 = documentRoot0Data11.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data11Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data11Text4.TextContent);

            var documentRoot0Data11Price5 = documentRoot0Data11.ChildNodes[5];
            Assert.AreEqual(1, documentRoot0Data11Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data11Price5.Attributes.Length);
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

            var documentRoot0Data13 = documentRoot0.ChildNodes[13];
            Assert.AreEqual(7, documentRoot0Data13.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data13.Attributes.Length);
            Assert.AreEqual("Data", documentRoot0Data13.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data13.NodeType);

            var documentRoot0Data13Text0 = documentRoot0Data13.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data13Text0.TextContent);

            var documentRoot0Data13Category1 = documentRoot0Data13.ChildNodes[1];
            Assert.AreEqual(1, documentRoot0Data13Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data13Category1.Attributes.Length);
            Assert.AreEqual("Category", documentRoot0Data13Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data13Category1.NodeType);

            var documentRoot0Data13Category1Text0 = documentRoot0Data13Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Category1Text0.NodeType);
            Assert.AreEqual("A", documentRoot0Data13Category1Text0.TextContent);

            var documentRoot0Data13Text2 = documentRoot0Data13.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data13Text2.TextContent);

            var documentRoot0Data13Quantity3 = documentRoot0Data13.ChildNodes[3];
            Assert.AreEqual(1, documentRoot0Data13Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data13Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentRoot0Data13Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data13Quantity3.NodeType);

            var documentRoot0Data13Quantity3Text0 = documentRoot0Data13Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Quantity3Text0.NodeType);
            Assert.AreEqual("15", documentRoot0Data13Quantity3Text0.TextContent);

            var documentRoot0Data13Text4 = documentRoot0Data13.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data13Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data13Text4.TextContent);

            var documentRoot0Data13Price5 = documentRoot0Data13.ChildNodes[5];
            Assert.AreEqual(1, documentRoot0Data13Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data13Price5.Attributes.Length);
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

            var documentRoot0Data15 = documentRoot0.ChildNodes[15];
            Assert.AreEqual(7, documentRoot0Data15.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data15.Attributes.Length);
            Assert.AreEqual("Data", documentRoot0Data15.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data15.NodeType);

            var documentRoot0Data15Text0 = documentRoot0Data15.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Text0.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data15Text0.TextContent);

            var documentRoot0Data15Category1 = documentRoot0Data15.ChildNodes[1];
            Assert.AreEqual(1, documentRoot0Data15Category1.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data15Category1.Attributes.Length);
            Assert.AreEqual("Category", documentRoot0Data15Category1.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data15Category1.NodeType);

            var documentRoot0Data15Category1Text0 = documentRoot0Data15Category1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Category1Text0.NodeType);
            Assert.AreEqual("B", documentRoot0Data15Category1Text0.TextContent);

            var documentRoot0Data15Text2 = documentRoot0Data15.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Text2.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data15Text2.TextContent);

            var documentRoot0Data15Quantity3 = documentRoot0Data15.ChildNodes[3];
            Assert.AreEqual(1, documentRoot0Data15Quantity3.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data15Quantity3.Attributes.Length);
            Assert.AreEqual("Quantity", documentRoot0Data15Quantity3.NodeName);
            Assert.AreEqual(NodeType.Element, documentRoot0Data15Quantity3.NodeType);

            var documentRoot0Data15Quantity3Text0 = documentRoot0Data15Quantity3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Quantity3Text0.NodeType);
            Assert.AreEqual("8", documentRoot0Data15Quantity3Text0.TextContent);

            var documentRoot0Data15Text4 = documentRoot0Data15.ChildNodes[4];
            Assert.AreEqual(NodeType.Text, documentRoot0Data15Text4.NodeType);
            Assert.AreEqual("\n    ", documentRoot0Data15Text4.TextContent);

            var documentRoot0Data15Price5 = documentRoot0Data15.ChildNodes[5];
            Assert.AreEqual(1, documentRoot0Data15Price5.ChildNodes.Length);
            Assert.AreEqual(0, documentRoot0Data15Price5.Attributes.Length);
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
