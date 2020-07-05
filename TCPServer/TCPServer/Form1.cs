using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(reveiver));
            thread.Start();

        }

        public void reveiver()
        {
            int recv;
            byte[] data = new byte[1024];

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8088);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(endPoint);
            socket.Listen(10);
            Socket client = socket.Accept();

            IPEndPoint clientEndPoit = (IPEndPoint)client.RemoteEndPoint; ;

            while(true)
            {
                data = new byte[1024];
                recv = client.Receive(data);
                if (recv == 0)
                {
                    break;
                }

                this.receivedMessage.Text = Encoding.ASCII.GetString(data, 0, recv);
                client.Send(data, recv, SocketFlags.None);
                Thread.Sleep(1000);
            }
        }
    }
}
