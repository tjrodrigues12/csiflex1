using CSIFLEX.FocasLibrary.CncMachines;
using CSIFLEX.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIFLEX.FocasConnector
{
    public static class FocasConnector
    {
        public static bool TestConnection(string ipAddress, int port = 8193)
        {
            if (UtpFocasLib.cnc_allclibhndl3(ipAddress, (ushort)port, 15) == 0)
                return true;

            return false;
        }


        public static void SendG008_6(string ipAddress, int port = 8193)
        {
            if (UtpFocasLib.cnc_allclibhndl3(ipAddress, (ushort)port, 15) == 0)
            {
                Console.WriteLine($"machine {ipAddress}:8193 connected");
                try
                {
                    FocasLibBase.IODBPMC0 G008_6 = new FocasLibBase.IODBPMC0()
                    {
                        type_a = 0,// G address
                        type_d = 0,//byte
                        datano_s = 8,//008
                        datano_e = 9,//only 8
                        cdata = new byte[5] { 0b01000000, 0, 0, 0, 0 }//6th bit
                    };
                    if (UtpFocasLib.pmc_wrpmcrng(13, G008_6) == 0)
                        Log.Info("G008.6 successfull");
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }

    }
}
