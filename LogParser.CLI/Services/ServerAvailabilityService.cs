using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LogParser.CLI.Services
{
    public class ServerAvailabilityService
    {
        // experimental part
        public static bool IsHostAvailable(string host)
        {
            bool pingable = false;
            // if it has port
            if (host.Count(x => x == ':') > 1)
            {
                TcpClient tcp = null;
                try
                {
                    var portSeparation = host.LastIndexOf(":");
                    var splitted = new[] { host.Substring(0, portSeparation), host.Substring(portSeparation + 1) };
                    tcp = new TcpClient(splitted[0], int.Parse(splitted[1]));
                    pingable = true;
                }
                catch (Exception)
                {
                    // Do nothing
                }
                finally
                {
                    tcp?.Close();
                }
            }
            else
            {
                Ping pinger = new Ping();
                try
                {
                    PingReply reply = pinger.Send(host);
                    pingable = reply.Status == IPStatus.Success;
                }
                catch (PingException)
                {
                    // Do nothing
                }
                finally
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }
    }
}
