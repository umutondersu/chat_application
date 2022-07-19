namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ip = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.post = new System.Windows.Forms.TextBox();
            this.connect = new System.Windows.Forms.Button();
            this.disconnect = new System.Windows.Forms.Button();
            this.send = new System.Windows.Forms.Button();
            this.all_posts = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.friends_posts = new System.Windows.Forms.Button();
            this.friends_list = new System.Windows.Forms.ListBox();
            this.delete_post = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.add_friend_form = new System.Windows.Forms.TextBox();
            this.delete_post_form = new System.Windows.Forms.TextBox();
            this.add_friend = new System.Windows.Forms.Button();
            this.remove_friend = new System.Windows.Forms.Button();
            this.user_posts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username:";
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(80, 18);
            this.ip.Margin = new System.Windows.Forms.Padding(2);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(132, 20);
            this.ip.TabIndex = 3;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(80, 52);
            this.port.Margin = new System.Windows.Forms.Padding(2);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(132, 20);
            this.port.TabIndex = 4;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(80, 84);
            this.username.Margin = new System.Windows.Forms.Padding(2);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(132, 20);
            this.username.TabIndex = 5;
            // 
            // post
            // 
            this.post.Enabled = false;
            this.post.Location = new System.Drawing.Point(80, 299);
            this.post.Margin = new System.Windows.Forms.Padding(2);
            this.post.Name = "post";
            this.post.Size = new System.Drawing.Size(132, 20);
            this.post.TabIndex = 6;
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(225, 21);
            this.connect.Margin = new System.Windows.Forms.Padding(2);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(69, 28);
            this.connect.TabIndex = 7;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // disconnect
            // 
            this.disconnect.Enabled = false;
            this.disconnect.Location = new System.Drawing.Point(221, 65);
            this.disconnect.Margin = new System.Windows.Forms.Padding(2);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(82, 25);
            this.disconnect.TabIndex = 8;
            this.disconnect.Text = "Disconnect";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // send
            // 
            this.send.Enabled = false;
            this.send.Location = new System.Drawing.Point(225, 297);
            this.send.Margin = new System.Windows.Forms.Padding(2);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(63, 23);
            this.send.TabIndex = 9;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // all_posts
            // 
            this.all_posts.Enabled = false;
            this.all_posts.Location = new System.Drawing.Point(329, 322);
            this.all_posts.Margin = new System.Windows.Forms.Padding(2);
            this.all_posts.Name = "all_posts";
            this.all_posts.Size = new System.Drawing.Size(63, 28);
            this.all_posts.TabIndex = 10;
            this.all_posts.Text = "All Posts";
            this.all_posts.UseVisualStyleBackColor = true;
            this.all_posts.Click += new System.EventHandler(this.all_posts_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 301);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Post:";
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(307, 10);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(242, 307);
            this.logs.TabIndex = 12;
            this.logs.Text = "";
            // 
            // friends_posts
            // 
            this.friends_posts.Enabled = false;
            this.friends_posts.Location = new System.Drawing.Point(434, 322);
            this.friends_posts.Name = "friends_posts";
            this.friends_posts.Size = new System.Drawing.Size(98, 29);
            this.friends_posts.TabIndex = 13;
            this.friends_posts.Text = "Friend\'s Posts";
            this.friends_posts.UseVisualStyleBackColor = true;
            this.friends_posts.Click += new System.EventHandler(this.friends_posts_Click);
            // 
            // friends_list
            // 
            this.friends_list.Enabled = false;
            this.friends_list.FormattingEnabled = true;
            this.friends_list.Location = new System.Drawing.Point(80, 118);
            this.friends_list.Name = "friends_list";
            this.friends_list.Size = new System.Drawing.Size(132, 95);
            this.friends_list.TabIndex = 14;
            // 
            // delete_post
            // 
            this.delete_post.Enabled = false;
            this.delete_post.Location = new System.Drawing.Point(225, 331);
            this.delete_post.Name = "delete_post";
            this.delete_post.Size = new System.Drawing.Size(63, 23);
            this.delete_post.TabIndex = 15;
            this.delete_post.Text = "Delete";
            this.delete_post.UseVisualStyleBackColor = true;
            this.delete_post.Click += new System.EventHandler(this.delete_post_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Post ID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Username:";
            // 
            // add_friend_form
            // 
            this.add_friend_form.Enabled = false;
            this.add_friend_form.Location = new System.Drawing.Point(80, 259);
            this.add_friend_form.Name = "add_friend_form";
            this.add_friend_form.Size = new System.Drawing.Size(132, 20);
            this.add_friend_form.TabIndex = 18;
            // 
            // delete_post_form
            // 
            this.delete_post_form.Enabled = false;
            this.delete_post_form.Location = new System.Drawing.Point(80, 331);
            this.delete_post_form.Name = "delete_post_form";
            this.delete_post_form.Size = new System.Drawing.Size(132, 20);
            this.delete_post_form.TabIndex = 19;
            // 
            // add_friend
            // 
            this.add_friend.Enabled = false;
            this.add_friend.Location = new System.Drawing.Point(221, 257);
            this.add_friend.Name = "add_friend";
            this.add_friend.Size = new System.Drawing.Size(73, 23);
            this.add_friend.TabIndex = 20;
            this.add_friend.Text = "Add Friend";
            this.add_friend.UseVisualStyleBackColor = true;
            this.add_friend.Click += new System.EventHandler(this.add_friend_Click);
            // 
            // remove_friend
            // 
            this.remove_friend.Enabled = false;
            this.remove_friend.Location = new System.Drawing.Point(101, 219);
            this.remove_friend.Name = "remove_friend";
            this.remove_friend.Size = new System.Drawing.Size(87, 23);
            this.remove_friend.TabIndex = 21;
            this.remove_friend.Text = "Remove Friend";
            this.remove_friend.UseVisualStyleBackColor = true;
            this.remove_friend.Click += new System.EventHandler(this.remove_friend_Click);
            // 
            // user_posts
            // 
            this.user_posts.Enabled = false;
            this.user_posts.Location = new System.Drawing.Point(382, 357);
            this.user_posts.Name = "user_posts";
            this.user_posts.Size = new System.Drawing.Size(75, 23);
            this.user_posts.TabIndex = 22;
            this.user_posts.Text = "My Posts";
            this.user_posts.UseVisualStyleBackColor = true;
            this.user_posts.Click += new System.EventHandler(this.user_posts_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 386);
            this.Controls.Add(this.user_posts);
            this.Controls.Add(this.remove_friend);
            this.Controls.Add(this.add_friend);
            this.Controls.Add(this.delete_post_form);
            this.Controls.Add(this.add_friend_form);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.delete_post);
            this.Controls.Add(this.friends_list);
            this.Controls.Add(this.friends_posts);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.all_posts);
            this.Controls.Add(this.send);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.post);
            this.Controls.Add(this.username);
            this.Controls.Add(this.port);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ip;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox post;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Button all_posts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Button friends_posts;
        private System.Windows.Forms.ListBox friends_list;
        private System.Windows.Forms.Button delete_post;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox add_friend_form;
        private System.Windows.Forms.TextBox delete_post_form;
        private System.Windows.Forms.Button add_friend;
        private System.Windows.Forms.Button remove_friend;
        private System.Windows.Forms.Button user_posts;
    }
}

