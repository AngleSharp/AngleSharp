using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleInteraction
{
    /// <summary>
    /// Creates tests based on the official tests
    /// specified at the XmlConf project page(s):
    /// http://xmlconf.sourceforge.net
    /// http://xml.coverpages.org/xmlConformance.html
    /// </summary>
    class XMLConfTest
    {
        #region Members

        List<String> passed;
        List<String> failed;
        List<String> invalid;
        Dictionary<String, Action<String>> tests;

        #endregion

        #region ctor

        XMLConfTest()
        {
            passed = new List<String>();
            failed = new List<String>();
            invalid = new List<String>();
            tests = new Dictionary<String, Action<String>>();

            var methods = GetType().GetMethods().Where(m => m.IsPrivate && m.GetParameters().Length == 1 && m.Name.EndsWith("Generator"));

            foreach (var method in methods)
                tests.Add(method.Name.Replace("Generator", "").ToLower(), (Action<String>)Delegate.CreateDelegate(typeof(Action<String>), this, method));
        }

        #endregion

        #region Properties

        public List<String> Passed
        {
            get { return passed; }
        }

        public List<String> Failed
        {
            get { return failed; }
        }

        public List<String> Invalid
        {
            get { return invalid; }
        }

        #endregion

        #region Generator

        /// <summary>
        /// Generates the unit tests starting at the given root path.
        /// e.g. Generate(@"C:\Downloads\xmlconf")
        /// </summary>
        /// <param name="rootPath">The root path of the XML conf test suite.</param>
        public static XMLConfTest Generate(String rootPath)
        {
            var baseDir = new DirectoryInfo(rootPath);
            var dirs = baseDir.GetDirectories();
            var test = new XMLConfTest();

            foreach (var dir in dirs)
            {
                if (test.tests.ContainsKey(dir.Name))
                    test.tests[dir.Name](dir.FullName);
            }

            return test;
        }

        #endregion

        #region Sub-Generators

        void EdUniGenerator(String path)
        {
            //TODO

            /*
             * errata-2e/E*.xml
             * errata-3e/E*.xml
             * errata-4e/E*.xml
             * namespaces/1.0/###.xml
             */
        }

        void IBMGenerator(String path)
        {
            //TODO

            /*
             * invalid/P##/*.xml
             * not-wf/P##/*.xml
             * valid/P##/*.xml
             */
        }

        void OasisGenerator(String path)
        {
            //TODO

            /*
             * p##(fail|pass)#.xml
             */
        }

        void SunGenerator(String path)
        {
            //TODO

            /*
             * invalid/*.xml
             * not-wf/*.xml
             * valid/*.xml
             */
        }

        void XmlTestGenerator(String path)
        {
            //TODO

            /*
             * invalid/*.xml
             * not-wf/sa/*.xml
             * valid/sa/*.xml
             */
        }

        #endregion
    }
}
