using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace FileExplorer
{
    
    public class user_and_pass
    {
        public string username { get; set; }
        public string password { get; set; }
        public string key { get; set; }
        public user_and_pass(string username,string password,string key)
        {
            this.username = username;
            this.password = password;
            this.key = key;
        }
    }
    public class user_and_pass_list
    {
        public List<user_and_pass> user { get; set; }
    }
    public partial class Form2 : Form
    {
        public static string key;
        public static string loginUser;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string jsonfile = "passwd.json";
            string loginName = this.textBox1.Text;
            string loginPass = this.textBox2.Text;
            user_and_pass_list user_and_pass_lists;
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JsonSerializer js = new JsonSerializer();
                    user_and_pass_lists = js.Deserialize<user_and_pass_list>(reader);
                    Random ran = new Random();
                    int n;
                    string key="";
                    for(int i=0;i<8;i++)//随机生成des key
                    {
                        n = ran.Next(0, 255);
                        key += ((char)n).ToString();
                    }
                    user_and_pass new_user = new user_and_pass(loginName, loginPass,key);
                    user_and_pass_lists.user.Add(new_user);

                }
                
            }
            using (StreamWriter sw = new StreamWriter(jsonfile))
            {
                JsonSerializer js = new JsonSerializer();
                JsonWriter writer = new JsonTextWriter(sw);
                js.Serialize(writer, user_and_pass_lists);
            }
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), loginName));
            MessageBox.Show("Sign Up success");

        }
        private void button2_Click(object sender, EventArgs e)
        {
            string jsonfile = "passwd.json";
            string loginName = this.textBox1.Text;
            string loginPass = this.textBox2.Text;
            
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject ob = (JObject)JToken.ReadFrom(reader);
                    var user_list = ob["user"];
                    foreach (JObject user in user_list)
                    {
                        var username = user["username"];
                        var password = user["password"];
                        if (username.ToString() == loginName)
                        {
                            if (password.ToString() == loginPass)
                            {
                                key = user["key"].ToString();
                                loginUser = username.ToString();
                                this.DialogResult = DialogResult.OK;
                                this.Dispose();
                                this.Close();
                            }
                        }
                    }
                }
            }

        }
    }
}

       