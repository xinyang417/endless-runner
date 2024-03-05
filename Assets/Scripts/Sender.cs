using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

// This sender example must be used in conjunction with the listener program.
// You must run this program as follows:
// Open a console window and run the listener from the command line. 
// In another console window run the sender. In both cases you must specify 
// the local IPAddress to use. To obtain this address,  run the ipconfig command 
// from the command line. 
//  

class TestMulticastOptionSender
{
  static IPAddress mcastAddress;
  static int mcastPort;
  static Socket mcastSocket;

  static void BroadcastMessage(string message)
  {
  }

  static void Main()
  {
    // Initialize the multicast address group and multicast port.
    // Both address and port are selected from the allowed sets as
    // defined in the related RFC documents. These are the same 
    // as the values used by the sender.
    mcastAddress = IPAddress.Parse("230.0.0.1");
    mcastPort = 11000;
    IPEndPoint endPoint;

    try
    {
      mcastSocket = new Socket(AddressFamily.InterNetwork,
        SocketType.Dgram,
        ProtocolType.Udp);

      //Send multicast packets to the listener.
      endPoint = new IPEndPoint(mcastAddress, mcastPort);

      float x = 1.0f, y = 1.0f, z = 1.0f;
      while (true)
      {
        Thread.Sleep(1000);
        mcastSocket.SendTo(ASCIIEncoding.ASCII.GetBytes("ID=2;" + x + "," + y + "," + z), endPoint);
        x += 1f;
        y += 1f;
        z += 1f;
      }

      Console.WriteLine("Multicast data sent.....");
    }
    catch (Exception e)
    {
      Console.WriteLine("\n" + e.ToString());
    }

    mcastSocket.Close();
  }
}