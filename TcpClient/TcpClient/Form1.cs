using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpClient
{
    public partial class Form1 : Form
    {
        IPEndPoint endPoint;
        Socket socket;
        public Form1()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            String addr = this.ipAddress.Text;
            Int32 portNum = 13000;
            if (int.TryParse(this.port.Text, out portNum)) {
                endPoint = new IPEndPoint(IPAddress.Parse(addr), portNum);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try { 
                    socket.Connect(endPoint);
                }
                catch (SocketException er)
                {
                    return;
                }
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            String message = this.message.Text;
            socket.Send(Encoding.ASCII.GetBytes(message));
        }
    }
}
