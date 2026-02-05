using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CSIFLEX.PartAnalyzer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var sfKey = "MTUxNDYwQDMxMzcyZTMzMmUzME0xWi9yOWZwMkFaRUkyRkM4a0RoZ0F5cndTbmFEV0pNU0tkem9jMmZYNnM9;MTUxNDYxQDMxMzcyZTMzMmUzMEk4ZkMrSzV5NDRxRk15VGFPa295Z2xmWGJzVkdYODBoNVhGZWNhZ1ZMck09;MTUxNDYyQDMxMzcyZTMzMmUzMEcybk9BSmFtcnV3c0E1bVI5d2V6QVlRTVV3T1dmbTNtSnV0cGxJdi9tUE09;MTUxNDYzQDMxMzcyZTMzMmUzME81T1V1TXVmWmMwOWtvVUF2b08xT1ErUVpLdzMyMXhHbTlVbnRWdVVXcFk9;MTUxNDY0QDMxMzcyZTMzMmUzMEZGQlEwc1NsM0p0ek5iRlM0VXRUQm9ielJPaFNSNFNpSXA2dlVPQjRBczg9";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(sfKey);

        }
    }
}
