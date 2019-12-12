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
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;


namespace FileExplorer
{
    
    public class user_and_pass
    {
        public string username { get; set; }
        public string password { get; set; }
        public user_and_pass(string username,string password)
        {
            this.username = username;
            this.password = password;
        }
    }
    public class user_and_pass_list
    {
        public List<user_and_pass> user { get; set; }
    }
    public partial class Form2 : Form
    {
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
            string jsonfile = "../passwd.json";
            string loginName = this.textBox1.Text;
            string loginPass = Hash_MD5_32(this.textBox2.Text);
            user_and_pass_list user_and_pass_lists;
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JsonSerializer js = new JsonSerializer();
                    user_and_pass_lists = js.Deserialize<user_and_pass_list>(reader);
                    //Random ran = new Random();
                    //int n;
                    //byte[] key=new byte[8];
                    //for(int i=0;i<8;i++)//随机生成des key
                    //{
                    //    n = ran.Next(0, 255);
                    //    key[i] = (byte)n;
                    //}
                    user_and_pass new_user = new user_and_pass(loginName, loginPass);
                    user_and_pass_lists.user.Add(new_user);

                }
                
            }
            using (StreamWriter sw = new StreamWriter(jsonfile))
            {
                JsonSerializer js = new JsonSerializer();
                JsonWriter writer = new JsonTextWriter(sw);
                js.Serialize(writer, user_and_pass_lists);
            }
            string user_dir = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), loginName);
            System.IO.Directory.CreateDirectory(user_dir);
            //设置权限
            string cmd = "chmod 700 " + user_dir;
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \" {cmd}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                }
            };
            process.Start();
            process.WaitForExit();
            string key_path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "../" + loginName + ".key");
            des_key.generate_key(key_path);
            cmd = "chmod 700 " + key_path;
            process.Start();
            process.WaitForExit();
            MessageBox.Show("Sign Up success");

        }
        private void button2_Click(object sender, EventArgs e)
        {
            string jsonfile = "../passwd.json";
            string loginName = this.textBox1.Text;
            string loginPass = Hash_MD5_32(this.textBox2.Text);
            
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
        public static string Hash_MD5_32(string word, bool toUpper = true)
        {
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider MD5CSP
                    = new System.Security.Cryptography.MD5CryptoServiceProvider();

                byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(word);
                byte[] bytHash = MD5CSP.ComputeHash(bytValue);
                MD5CSP.Clear();

                //根据计算得到的Hash码翻译为MD5码
                string sHash = "", sTemp = "";
                for (int counter = 0; counter < bytHash.Count(); counter++)
                {
                    long i = bytHash[counter] / 16;
                    if (i > 9)
                    {
                        sTemp = ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp = ((char)(i + 0x30)).ToString();
                    }
                    i = bytHash[counter] % 16;
                    if (i > 9)
                    {
                        sTemp += ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp += ((char)(i + 0x30)).ToString();
                    }
                    sHash += sTemp;
                }

                //根据大小写规则决定返回的字符串
                return toUpper ? sHash : sHash.ToLower();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    public class des_key
    {
        [DllImport("../libfilebox.so", CallingConvention = CallingConvention.Cdecl)]
        public static extern void generate_key(string key_path);
    }

}

       