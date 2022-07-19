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

namespace Server
{
    public partial class Form1 : Form
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        bool terminating = false;
        bool listening = false;

        List<Post> Postlist = new List<Post>();
        Dictionary<Socket, string> Users = new Dictionary<Socket, string>();
        List<string> connectedusers = new List<string>();
        Dictionary<string, List<string>> friendslist = new Dictionary<string, List<string>>();

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
            if (!File.Exists(@"../../chat.txt"))
            {
                var hi = File.Create(@"../../chat.txt");
                hi.Close();
            }
            if (!File.Exists(@"../../friendlist.txt"))
            {
                var hello = File.Create(@"../../friendlist.txt");
                hello.Close();
            }

            var Flist = File.ReadLines(@"../../friendlist.txt");
            var chatlog = File.ReadLines(@"../../chat.txt");

            foreach (var line in chatlog)
            { 
                string[] data = line.Split('⍟');
                DateTime time = DateTime.Parse(data[0]);
                string username = data[1];
                int id = Int32.Parse(data[2]);
                string posttext = data[3];

                Post newpost = new Post(username, posttext, id, time);
                Postlist.Add(newpost);
            }

            if (Flist?.Any() == true)
            {
                foreach (var line in Flist)
                {
                    string user = line.Split(':')[0];
                    friendslist[user] = new List<string>();
                }

                foreach (string line in Flist)
                {
                    if (line != "")
                    {
                        string user = line.Split(':')[0];
                        string[] friends = line.Split(':')[1].Split(',');

                        foreach (string friend in friends)
                        {
                            friendslist[user].Add(friend);
                        }
                    }
                }
            }
        }
        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void listen_Click_1(object sender, EventArgs e)
        {
            int serverPort;
            if (Int32.TryParse(port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                try
                {
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                listen.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started Listening on port: " + serverPort+ "\n");
                }
                catch
                {
                    logs.AppendText("The Port " + serverPort + " is already in use!\n");
                }

            }
            else
            {
                logs.AppendText("Please check port number\n");
            }
        }

        

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                    //logs.AppendText("A client is connected.\n");

                    Thread receiveThread = new Thread(() => Receive(newClient)); // updated
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient) // updated
        {
            bool connected = true;

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[128];
                    thisClient.Receive(buffer);

                    string inmessage = Encoding.Default.GetString(buffer);
                    inmessage = inmessage.Substring(0, inmessage.IndexOf("\0"));
                    bool isusername = false, ispost = false, ispostlist = false, addFriend = false, removeFriend = false, isfriendsposts = false, ismyposts = false, isdeletepost = false;
                    

                    if (inmessage.StartsWith("username:"))
                    {
                        isusername = true;
                        inmessage = inmessage.Substring(inmessage.IndexOf(":") + 1, inmessage.Length - inmessage.IndexOf(":") - 1);
                    }
                    else if(inmessage.StartsWith("post:"))
                    {
                        ispost = true;
                        inmessage = inmessage.Substring(inmessage.IndexOf(":") + 1, inmessage.Length - inmessage.IndexOf(":") - 1);
                    }
                    else if (inmessage.StartsWith("addfriend:"))
                    {
                        addFriend = true;
                        inmessage = inmessage.Substring(inmessage.IndexOf(":") + 1, inmessage.Length - inmessage.IndexOf(":") - 1);
                    }
                    else if (inmessage.StartsWith("removefriend:"))
                    {
                        removeFriend = true;
                        inmessage = inmessage.Substring(inmessage.IndexOf(":") + 1, inmessage.Length - inmessage.IndexOf(":") - 1);
                    }
                    else if (inmessage.StartsWith("deletepost:"))
                    {
                        isdeletepost = true;
                        inmessage = inmessage.Substring(inmessage.IndexOf(":") + 1, inmessage.Length - inmessage.IndexOf(":") - 1);
                    }
                    else if (inmessage.StartsWith("allposts:")) { ispostlist = true; }
                    else if (inmessage.StartsWith("friendspost:")) { isfriendsposts = true; }
                    else if (inmessage.StartsWith("myposts:")) { ismyposts = true; }


                    var allusers = File.ReadLines(@"../../user-db.txt");
                    if (isusername)
                    {
                        bool exists = false;
                        if (allusers.Contains(inmessage))
                        {
                            exists = true;
                        }
                        if (!exists)
                        {
                            logs.AppendText(inmessage + " tried to connect to the server but cannot!\n");
                            Byte[] sendbuffer = Encoding.Default.GetBytes("Please enter a valid username.");
                            thisClient.Send(sendbuffer);
                        }
                        else
                        {
                            if (connectedusers.Contains(inmessage))
                            {
                                Byte[] sendbuffer = Encoding.Default.GetBytes(inmessage + " is already connected.");
                                thisClient.Send(sendbuffer);
                            }
                            else
                            {
                                Users[thisClient] = inmessage;
                                connectedusers.Add(Users[thisClient]);
                                logs.AppendText(Users[thisClient] + " has connected.\n");
                                Byte[] sendbuffer = Encoding.Default.GetBytes("Hello " + Users[thisClient] + "! You are connected to the server.");
                                thisClient.Send(sendbuffer);
                                if (friendslist.ContainsKey(Users[thisClient]))
                                {
                                    foreach (string friend in friendslist[Users[thisClient]])
                                    {
                                        sendbuffer = Encoding.Default.GetBytes("You have added " + friend + " as your friend.nolog");
                                        thisClient.Send(sendbuffer);
                                        Thread.Sleep(20);
                                    }
                                }
                                else
                                {
                                    friendslist[Users[thisClient]] = new List<string>();
                                }
                            }
                        }
                    }
                    else if (ispost)
                    {
                        logs.AppendText( Users[thisClient] + "has sent a post:\n" + inmessage + "\n" );
                        Byte[] sendbuffer = Encoding.Default.GetBytes(Users[thisClient] + ": " + inmessage);
                        thisClient.Send(sendbuffer);
                        Post newpost;

                        if(Int32.TryParse(inmessage,out int hi))
                        {
                            inmessage += " ";
                        }

                        if (Postlist.Count == 0)
                        {
                            newpost = new Post(Users[thisClient], inmessage, 0);
                        }
                        else
                        {
                            newpost = new Post(Users[thisClient], inmessage, Postlist.Last().id + 1);
                        }
                        Postlist.Add(newpost);
                        using (StreamWriter file = new StreamWriter("../../chat.txt", append: true))
                        {
                            file.WriteLine(newpost.ToString());
                        }
                    }
                    else if (ispostlist)
                    {
                        logs.AppendText("Showing all posts for " + Users[thisClient] + "\n");
                        foreach (Post post in Postlist)
                        {
                            if (post.username != Users[thisClient])
                            {
                                Byte[] sendbuffer = Encoding.Default.GetBytes(post.ToClient());
                                thisClient.Send(sendbuffer);
                                Thread.Sleep(10);
                            }
                        }
                    }
                    else if (addFriend)
                    {
                        addnewFriend(allusers, inmessage, thisClient);
                    }
                    else if (removeFriend)
                    {
                        removeFriends(inmessage, thisClient);
                    }
                    else if (isfriendsposts)
                    {
                        logs.AppendText("Showing all friends post for " + Users[thisClient] + ". \n");
                        foreach (Post post in Postlist)
                        {
                            if (friendslist[Users[thisClient]].Contains(post.username))
                            {
                                Byte[] sendbuffer = Encoding.Default.GetBytes(post.ToClient());
                                thisClient.Send(sendbuffer);
                                Thread.Sleep(10);
                            }
                        }

                    }
                    else if (ismyposts)
                    {
                        logs.AppendText("Showing " + Users[thisClient] + "'s posts. \n");
                        foreach (Post post in Postlist)
                        {
                            if (post.username == Users[thisClient])
                            {
                                Byte[] sendbuffer = Encoding.Default.GetBytes(post.ToClient());
                                thisClient.Send(sendbuffer);
                                Thread.Sleep(10);
                            }
                        }

                    }
                    else if (isdeletepost)
                    {
                        bool yourpost = false, postexists = false;

                        int post_id = Int32.Parse(inmessage);

                        if (Postlist.Count > 0)
                        {
                            if (post_id > Postlist.Last().id)
                            {
                                logs.AppendText("Post with id:" + post_id + " does not exist!");
                                Byte[] sendbuffer = Encoding.Default.GetBytes("post with ID " + post_id + " does not exist!");
                                thisClient.Send(sendbuffer);
                                return;
                            }
                        }
                        foreach (Post post in Postlist)
                        {
                            if(post.id == post_id)
                            {
                                if (Users[thisClient] == post.username)
                                {
                                    yourpost = true;
                                    postexists = true;
                                    Postlist.Remove(post);
                                    logs.AppendText("Post with id:" + post_id + " is deleted.\n");
                                    Byte[] sendbuffer = Encoding.Default.GetBytes("post with ID " + post_id + " deleted successfuly!");
                                    thisClient.Send(sendbuffer);
                                    break;
                                }
                                else
                                {
                                    logs.AppendText("Post with id:" + post_id + " is not " + Users[thisClient] + "'s.\n");
                                    Byte[] sendbuffer = Encoding.Default.GetBytes("post with ID " + post_id + " is not yours!");
                                    thisClient.Send(sendbuffer);
                                    postexists = true;
                                    break;
                                }
                            }
                        }
                        if (yourpost)
                        {
                            File.Copy("../../chat.txt", "../../chattemp.txt", true);
                            var Flist = File.ReadLines(@"../../chattemp.txt");
                            File.Create("../../chat.txt").Close();

                            using (StreamWriter file = new StreamWriter("../../chat.txt", append: true))
                            {
                                foreach (string line in Flist)
                                {
                                    if (!line.Contains("⍟" + post_id + "⍟"))
                                    {
                                        file.WriteLine(line);
                                    }
                                }
                            }
                            File.Delete("../../chattemp.txt");

                        }
                        else if (!postexists)
                        {
                            logs.AppendText("post with id:" + post_id + " does not exist!\n");
                            Byte[] sendbuffer = Encoding.Default.GetBytes("post with id:" + post_id + " does not exist!");
                            thisClient.Send(sendbuffer);
                        }
                    }
                    /*
                    sending to buffer
                    Byte[] sendbuffer = Encoding.Default.GetBytes(str);
                    thisClient.Send(sendbuffer);
                    */
                }
                catch
                {

                    if (Users.ContainsKey(thisClient))
                    {
                        logs.AppendText(Users[thisClient] + " has disconnected!\n");
                        connectedusers.Remove(Users[thisClient]);
                        Users.Remove(thisClient);
                    }

                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                    /*
                    try
                    {
                      logs.AppendText(Users[thisClient] + " has disconnected!\n");
                    }
                    catch
                    {}
                    thisClient.Close();
                    try
                    {
                        connectedusers.Remove(Users[thisClient]);
                    }
                    catch
                    {}//hopefully this works
                    Users.Remove(thisClient);
                    clientSockets.Remove(thisClient);
                    connected = false;
                    */
                }
            }
        }
        public void addnewFriend(IEnumerable<string> allusers, string friend, Socket thisClient)
        {
            if (allusers.Contains(friend))
            {
                if (!friendslist.ContainsKey(Users[thisClient]))
                {
                    friendslist[Users[thisClient]] = new List<string>() {};
                }
                if(!friendslist.ContainsKey(friend))
                {
                    friendslist[friend] = new List<string>() {};
                }
                if (!friendslist[Users[thisClient]].Contains(friend))
                {
                    addeachother(friend, thisClient, true);
                    addeachother(friend, thisClient, false);
                }
                else
                {
                    Byte[] sendbuffer = Encoding.Default.GetBytes(friend + " is already your friend!");
                    thisClient.Send(sendbuffer);
                }
        }
            else
            {
                Byte[] sendbuffer = Encoding.Default.GetBytes("This user does not exist!");
                thisClient.Send(sendbuffer);
            }

        }
        public void addeachother(string friend, Socket thisClient, bool sender)
        {
            Byte[] sendbuffer;
            string user = Users[thisClient];
            if (sender)
            {
                sendbuffer = Encoding.Default.GetBytes("You have added " + friend + " as your friend.");
                thisClient.Send(sendbuffer);               
            }
            else
            {
                Socket otherClient = Users.FirstOrDefault(x => x.Value == friend).Key;
                if (otherClient != null)
                {
                    if (connectedusers.Contains(Users[otherClient]))
                    {
                        sendbuffer = Encoding.Default.GetBytes(user + " has added you as a friend!");
                        otherClient.Send(sendbuffer);
                    }
                }
                user = friend;
                friend = Users[thisClient];
            }

            friendslist[user].Add(friend);
            string newline = user + ":";
            string begin = newline;

            File.Copy("../../friendlist.txt", "../../fltemp.txt", true);
            var Flist = File.ReadLines(@"../../fltemp.txt");
            File.Create("../../friendlist.txt").Close();

            foreach (string fren in friendslist[user])
            {
                newline += fren + ",";
            }
            newline = newline.Substring(0, newline.Length - 1);

            using (StreamWriter file = new StreamWriter("../../friendlist.txt", append: true))
            {
                foreach (string line in Flist)
                {
                    if (!line.Contains(begin) && line != string.Empty)
                    {
                        file.WriteLine(line);
                    }
                }
                file.WriteLine(newline);
            }
            File.Delete("../../fltemp.txt");
        }
        public void removeFriends(string friend, Socket thisClient)
        {
            if (!friendslist.ContainsKey(Users[thisClient]))
            {
                Byte[] sendbuffer = Encoding.Default.GetBytes(friend + "is not your friend!");
                thisClient.Send(sendbuffer);
                return;
            }
            removeeachother(friend, thisClient, true);
            removeeachother(friend, thisClient, false);
        }
        public void removeeachother(string friend, Socket thisClient, bool sender)
        {
            Byte[] sendbuffer;
            string user = Users[thisClient];
            if (sender)
            {
                sendbuffer = Encoding.Default.GetBytes("You have removed " + friend + " from your list.");
                thisClient.Send(sendbuffer);
            }
            else
            {
                Socket otherClient = Users.FirstOrDefault(x => x.Value == friend).Key;
                if (otherClient != null)
                {
                    if (connectedusers.Contains(Users[otherClient]))
                    {
                        sendbuffer = Encoding.Default.GetBytes(user + " has removed you as a friend.");
                        otherClient.Send(sendbuffer);
                    }
                }
                user = friend;
                friend = Users[thisClient];
            }

            friendslist[user].Remove(friend);
            string newline = user + ":";
            string begin = newline;


            File.Copy("../../friendlist.txt", "../../fltemp.txt", true);
            var Flist = File.ReadLines(@"../../fltemp.txt");
            File.Create("../../friendlist.txt").Close();


            //var Flist = File2Str("friendlist");
            bool flag = true;
            foreach (string fren in friendslist[user])
            {
                newline += fren + ",";
                flag = false;
            }
            if (flag)
            {
                newline = String.Empty;
            }
            else if (!newline.Contains(","))
            {
                newline = newline.Substring(0, newline.Length - 1);
            }

            using (StreamWriter file = new StreamWriter("../../friendlist.txt", append: true))
            {
                foreach (string line in Flist)
                {
                    if (!line.Contains(begin) && line != string.Empty)
                    {
                        file.WriteLine(line);
                    }
                }
                file.WriteLine(newline);
            }
            File.Delete("../../fltemp.txt");
        }

        public class Post
        {
            public string username;
            public int id = 0;
            public string post;
            public DateTime posttime;


            public Post(string uname, string msg, int index)
            {
                this.id = index;
                this.username = uname;
                this.post = msg;
                this.posttime = DateTime.Now;
            }
            public Post(string uname, string msg, int index, DateTime time)
            {
                this.id = index;
                this.username = uname;
                this.post = msg;
                this.posttime = time;
            }

            public override string ToString()
            {
                return this.posttime.ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "⍟" + this.username + "⍟" + this.id + "⍟" + this.post + "⍟";
            }

            public string ToClient()
            {
                return "Username: " + this.username + "\n" + "PostID: " + this.id + "\n" + "Post: " + this.post + "\n" + "Time: " + this.posttime.ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + "\n";
            }

        }
    }
}
