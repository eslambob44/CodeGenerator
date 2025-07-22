using Microsoft.Win32;
using System;

namespace Presentation_Layer
{
    static public class clsRegistry
    {
        static string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\CodeGenerator";

        static public void WriteToRegistry(string ValueName, string Value)
        {
            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, ValueName, Value, RegistryValueKind.String);



            }
            catch (Exception ex)
            { 

            }
        }

        static public string ReadFromRegistry(string ValueName)
        {
            try
            {
                // Read the value from the Registry
                string value = Registry.GetValue(keyPath, ValueName, null) as string;
                return value;

                
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}