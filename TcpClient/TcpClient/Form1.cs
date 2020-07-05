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

        private void sendButton_Click(object sender, EventArgs e)
        {

            String addr = this.ipAddress.Text;
            Int32 portNum = 13000;
            if (int.TryParse(this.port.Text, out portNum))
            {
                endPoint = new IPEndPoint(IPAddress.Parse(addr), portNum);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Connect(endPoint);
                    String message = this.message.Text;
                    String[] tokens = message.Split(' ');
                    byte[] buffers = new byte[tokens.Length];
                    for (int idx = 0; idx < tokens.Length; ++idx)
                    {
                        buffers[idx] = byte.Parse(tokens[idx]);
                    }
                    socket.Send(buffers);
                    socket.Close();
                }
                catch (SocketException er)
                {
                    return;
                }
            }
        }
    }
}
