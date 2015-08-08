using System;
using System.Xml;
using System.Xml.Schema;

namespace xml_xsd_validator
{
    class Validator
    {
        private string xml_filename_;
        private string xsd_filename_;

        private bool is_success_;

        public Validator(string xml_filename, string xsd_filename)
        {
            xml_filename_ = xml_filename;
            xsd_filename_ = xsd_filename;

            is_success_ = false;
        }

        public bool Validate()
        {
            is_success_ = false;

            XmlDocument document = new XmlDocument();
            document.Load(xml_filename_);

            XmlReaderSettings setting = new XmlReaderSettings();
            setting.CloseInput = true;
            setting.ValidationEventHandler += Handler;
            setting.ValidationType = ValidationType.Schema;
            setting.Schemas.Add(null, xsd_filename_);
            setting.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings |
                                      XmlSchemaValidationFlags.ProcessIdentityConstraints |
                                      XmlSchemaValidationFlags.ProcessInlineSchema |
                                      XmlSchemaValidationFlags.ProcessSchemaLocation;

            is_success_ = true;

            using (XmlReader validationReader = XmlReader.Create(xml_filename_, setting))
            {
                while (validationReader.Read())
                {
                    /* do nothing for while */
                }
            }

            return is_success_;
        }

        private void Handler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error ||
                e.Severity == XmlSeverityType.Warning)
            {
                System.Console.WriteLine(String.Format("Line: {0}, Position: {1} '{2}'",
                    e.Exception.LineNumber, e.Exception.LinePosition, e.Exception.Message));

                is_success_ = false;
            }
        }
    }
}
