using System;
using System.Diagnostics;
using System.IO;

namespace Callisto.Tests.Fixtures
{
    /// <summary>
    /// Defines the <see cref="AzureStorageFixture" />
    /// </summary>
    public class AzureStorageFixture 
    {
        public AzureStorageFixture()
        {
            var process = Process.GetProcessesByName("AzureStorageEmulator");
            if (process.Length > 0)
            {
                return;
            }
            else
            {
                var path = @"C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\AzureStorageEmulator.exe";
                if (File.Exists(path))
                {
                    Process.Start(path, "start");
                }
                else
                {
                    throw new InvalidProgramException("Storage tests require azure emulator to be installed");
                }
            }
        }
    }
}
