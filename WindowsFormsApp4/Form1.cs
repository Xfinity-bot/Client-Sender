using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        class Global
        {
            public static string filename;

        }
        public Form1()
        {
            InitializeComponent();
        }
        
        public string GetIP()
        {
            
            return textBox2.Text;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            textBox1.Text = op.FileName;
            Global.filename = textBox1.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //var ipad = "192.168.0.104";
            try {
                
                

                TcpClient tcpClient1 = new TcpClient();
                tcpClient1.Connect(new IPEndPoint(IPAddress.Parse(GetIP()), 5050));

                byte[] buffern = new byte[1500];
                

                NetworkStream nwStream = tcpClient1.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(Path.GetFileName(Global.filename));
               
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                tcpClient1.Close();


                Console.ReadLine();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                StreamReader sr = new StreamReader(textBox1.Text);

                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(new IPEndPoint(IPAddress.Parse(GetIP()), 8085));

                byte[] buffer = new byte[1500];
                long bytesSent = 0;

                while (bytesSent < sr.BaseStream.Length)
                {
                    int bytesRead = sr.BaseStream.Read(buffer, 0, 1500);
                    tcpClient.GetStream().Write(buffer, 0, bytesRead);
                   

                    bytesSent += bytesRead;
                }

                tcpClient.Close();

                Message("File Sent...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void Message(string data)
        {
            listBox1.Items.Add(data);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    } 
}
