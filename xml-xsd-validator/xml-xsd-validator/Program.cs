using System;

namespace xml_xsd_validator
{
    class Program
    {
        static void PrintUsage()
        {
            string binary_name = System.IO.Path.GetFileName(Environment.GetCommandLineArgs()[0]);
            Console.WriteLine("Usage: {0} xml_filename xsd_filename", binary_name);
        }

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                PrintUsage();
                return;
            }

            string xml_filename = args[0];
            string xsd_filename = args[1];

            var validator = new Validator(xml_filename, xsd_filename);
            if (validator.Validate() == false)
            {
                Environment.ExitCode = 1;
            }
        }
    }
}
