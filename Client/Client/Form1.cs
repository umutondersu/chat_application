using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public class Global
        {
            public static string username { get; set; }
        }

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }
        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = ip.Text;

            int portNum;
            if (Int32.TryParse(port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    Global.username = username.Text;
                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();
                    connected = true;
                    try
                    {
                        Byte[] buffer = Encoding.Default.GetBytes("username:" + Global.username);
                        clientSocket.Send(buffer);
                    }
                    catch
                    {
                        logs.AppendText("There is a problem with the connection.");
                        terminating = true;
                        clientSocket.Close();
                    }
                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                logs.AppendText("Check the port\n");
            }
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[128];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if (!incomingMessage.Contains("nolog") && incomingMessage != "")
                    {
                        logs.AppendText(incomingMessage + "\n");
                    }
                    if (incomingMessage.Contains("You are connected to the server."))
                    {
                        friends_list.Enabled = delete_post_form.Enabled = add_friend_form.Enabled = remove_friend.Enabled =
                       user_posts.Enabled = friends_posts.Enabled = delete_post.Enabled = add_friend.Enabled =
                       disconnect.Enabled = post.Enabled = send.Enabled = all_posts.Enabled = true;
                        connect.Enabled = false;
                        Byte[] sendbuffer = Encoding.Default.GetBytes("startup");
                        clientSocket.Send(sendbuffer);
                    }
                    else if (incomingMessage.Contains("You have added"))
                    {
                        //You have added Ali as your friend.
                        friends_list.Items.Add(incomingMessage.Split(' ')[3]);
                    }
                    else if (incomingMessage.Contains("has added you"))
                    {
                        //Ali has added you as a friend!
                        friends_list.Items.Add(incomingMessage.Split(' ')[0]);
                    }
                    else if (incomingMessage.Contains("You have removed"))
                    {
                        //You have removed Ali as your friend.
                        friends_list.Items.Remove(incomingMessage.Split(' ')[3]);
                    }
                    else if (incomingMessage.Contains("has removed you"))
                    {
                        //Ali has removed you as a friend.
                        friends_list.Items.Remove(incomingMessage.Split(' ')[0]);
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server has disconnected\n");
                        friends_list.Enabled = delete_post_form.Enabled = add_friend_form.Enabled = remove_friend.Enabled =
                       user_posts.Enabled = friends_posts.Enabled = delete_post.Enabled = add_friend.Enabled =
                       disconnect.Enabled = post.Enabled = send.Enabled = all_posts.Enabled = false;
                        connect.Enabled = true;
                        friends_list.Items.Clear();
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void send_Click(object sender, EventArgs e)
        {
            if (post.Text == "")
            {
                logs.AppendText("Please Write a Post! \n");
            }
            else
            {
                logs.AppendText("You have successfuly sent a Post!\n");
                string str = "post:" + post.Text;
                Byte[] sendbuffer = Encoding.Default.GetBytes(str);
                clientSocket.Send(sendbuffer);
                post.Text = "";
            }
        }

        private void all_posts_Click(object sender, EventArgs e)
        {
            string str = "allposts:";
            logs.AppendText("Showing all posts from clients.\n");
            Byte[] sendbuffer = Encoding.Default.GetBytes(str);
            clientSocket.Send(sendbuffer);
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            terminating = true;
            logs.AppendText("Successfuly Disconnnected!\n");
            friends_list.Enabled = delete_post_form.Enabled = add_friend_form.Enabled = remove_friend.Enabled =
            user_posts.Enabled = friends_posts.Enabled = delete_post.Enabled = add_friend.Enabled =
            disconnect.Enabled = post.Enabled = send.Enabled = all_posts.Enabled = false;
            connect.Enabled = true;
            friends_list.Items.Clear();

            clientSocket.Close();
            connected = false;
        }

        private void add_friend_Click(object sender, EventArgs e)
        {
            if (add_friend_form.Text != "")
            {
                if (add_friend_form.Text == Global.username)
                {
                    logs.AppendText("You are always your friend you don't need to be on the list :) \n");
                    add_friend_form.Text = "";
                    return;
                }
                if (!friends_list.Items.Contains(add_friend_form.Text))
                {
                    string str = "addfriend:" + add_friend_form.Text;
                    Byte[] sendbuffer = Encoding.Default.GetBytes(str);
                    clientSocket.Send(sendbuffer);
                }
                else
                {
                    logs.AppendText(add_friend_form + " is already on your list.\n");
                }

                add_friend_form.Text = "";
            }
            else
            {
                logs.AppendText("Please Choose a friend to add.\n");
            }
        }

        private void remove_friend_Click(object sender, EventArgs e)
        {
            if (friends_list.SelectedItem == null)
            {
                logs.AppendText("Choose a Friend to remove Please.\n");
                return;
            }
            string str = "removefriend:" + friends_list.SelectedItem.ToString();
            Byte[] sendbuffer = Encoding.Default.GetBytes(str);
            clientSocket.Send(sendbuffer);
        }

        private void delete_post_Click(object sender, EventArgs e)
        {
            string str = "deletepost:" + delete_post_form.Text;
            Byte[] sendbuffer = Encoding.Default.GetBytes(str);
            clientSocket.Send(sendbuffer);
            delete_post_form.Text = "";
        }

        private void friends_posts_Click(object sender, EventArgs e)
        {
            logs.AppendText("Showing all posts from friends.\n");
            string str = "friendspost:";
            Byte[] sendbuffer = Encoding.Default.GetBytes(str);
            clientSocket.Send(sendbuffer);

        }

        private void user_posts_Click(object sender, EventArgs e)
        {
            logs.AppendText("Showing all of your posts.\n");
            string str = "myposts:";
            Byte[] sendbuffer = Encoding.Default.GetBytes(str);
            clientSocket.Send(sendbuffer);
        }
    }


}
