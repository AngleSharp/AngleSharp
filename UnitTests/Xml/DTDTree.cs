using AngleSharp.DTD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    /// <summary>
    /// Some examples taken from
    /// http://xmlwriter.net/xml_guide/doctype_declaration.shtml.
    /// </summary>
    [TestClass]
    public class DTDTree
    {
        [TestMethod]
        public void SubjectsDtd()
        {
            var dtd = @"<!--see Element Type Declarations
  for an explanation of the following syntax-->
<!ELEMENT document
  (title*,subjectID,subjectname,prerequisite?,
  classes,assessment,syllabus,textbooks*)>
<!ELEMENT prerequisite (subjectID,subjectname)>
<!ELEMENT textbooks (author,booktitle)>
<!ELEMENT title (#PCDATA)>
<!ELEMENT subjectID (#PCDATA)>
<!ELEMENT subjectname (#PCDATA)>
<!ELEMENT classes (#PCDATA)>
<!ELEMENT assessment (#PCDATA)>
<!ATTLIST assessment assessment_type (exam | assignment) #IMPLIED>
<!ELEMENT syllabus (#PCDATA)>
<!ELEMENT author (#PCDATA)>
<!ELEMENT booktitle (#PCDATA)>";

            var parser = new DtdParser(dtd);
            parser.Parse();

            var result = parser.Result;
            Assert.AreEqual(12, result.Count);
            Assert.AreEqual(1, result.Attributes.Count());
            Assert.AreEqual(11, result.Elements.Count());
        }
    }
}
