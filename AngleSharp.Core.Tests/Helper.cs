using NUnit.Framework;
using System;
using System.IO;
using System.Net.NetworkInformation;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Small (but quite useable) code to enable / disable some
    /// test(s) depending on the current network status.
    /// Taken from
    /// http://stackoverflow.com/questions/520347/c-sharp-how-do-i-check-for-a-network-connection
    /// </summary>
    class Helper
    {
        /// <summary>
        /// Indicates whether any network connection is available
        /// Filter connections below a specified speed, as well as virtual network cards.
        /// Additionally writes an inconclusive message if no network is available.
        /// </summary>
        /// <returns>True if a network connection is available; otherwise false.</returns>
        public static Boolean IsNetworkAvailable()
        {
            if (IsNetworkAvailable(0))
                return true;

            Assert.Inconclusive("No network has been detected. Test skipped.");
            return false;
        }

        /// <summary>
        /// Indicates whether any network connection is available.
        /// Filter connections below a specified speed, as well as virtual network cards.
        /// </summary>
        /// <param name="minimumSpeed">The minimum speed required. Passing 0 will not filter connection using speed.</param>
        /// <returns>True if a network connection is available; otherwise false.</returns>
        public static Boolean IsNetworkAvailable(Int64 minimumSpeed)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // discard because of standard reasons
                if ((ni.OperationalStatus != OperationalStatus.Up) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel))
                    continue;

                // this allow to filter modems, serial, etc.
                // I use 10000000 as a minimum speed for most cases
                if (ni.Speed < minimumSpeed)
                    continue;

                // discard virtual cards (virtual box, virtual pc, etc.)
                if ((ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0))
                    continue;

                // discard "Microsoft Loopback Adapter", it will not show as NetworkInterfaceType.Loopback but as Ethernet Card.
                if (ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                    continue;

                return true;
            }

            return false;
        }

        public static Stream StreamFromBytes(Byte[] content)
        {
            var stream = new MemoryStream(content);
            stream.Position = 0;
            return stream;
        }

        public static Stream StreamFromString(String s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
