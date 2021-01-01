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
        //Creating global variable
        class Global
        {
            public static string filename;

        }

        //Intialise Components
        public Form1()
        {
            InitializeComponent();
        }
        

        //Getting IP From the text box (User entered)
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
        //Dialog box to select a file
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            textBox1.Text = op.FileName;
            Global.filename = textBox1.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            try {


                //Create a TCPClient object at the IP and port no
                TcpClient tcpClient1 = new TcpClient();
                tcpClient1.Connect(new IPEndPoint(IPAddress.Parse(GetIP()), 5050));

                byte[] buffern = new byte[1500];
                
                //creating network stream object
                NetworkStream nwStream = tcpClient1.GetStream();

                //Encoding the filename string
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(Path.GetFileName(Global.filename));


                //send the text through the stream
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                tcpClient1.Close();


                Console.ReadLine();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {   //Creates a TextReader that reads characters from a byte stream in a particular encoding.
                StreamReader sr = new StreamReader(textBox1.Text);

                //Create a TCPClient object at the IP and port no
                TcpClient tcpClient = new TcpClient();
                //Establishing connection 
                tcpClient.Connect(new IPEndPoint(IPAddress.Parse(GetIP()), 8085));

                byte[] buffer = new byte[1500];
                long bytesSent = 0;

                //loop for sending chunks of data
                while (bytesSent < sr.BaseStream.Length)
                {  
                    //Writes data to the NetworkStream from a specified range of a byte array.
                    int bytesRead = sr.BaseStream.Read(buffer, 0, 1500);
                    tcpClient.GetStream().Write(buffer, 0, bytesRead);
                   

                    bytesSent += bytesRead;
                }
                //Closing tcp connection 
                tcpClient.Close();

                Message("File Sent...");
                Console.ReadLine();
            } 
            //Catching any errors with message box
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //Function to display message to listbox
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
