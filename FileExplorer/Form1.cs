using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        [DllImport("../libfilebox.so")]
        public static extern void encrypted_copy(string src_file_path, string dst_enc_file_path, string des_key_path);
        [DllImport("../libfilebox.so")]
        public static extern void decrypted_copy(string src_enc_file_path, string dst_dec_file_path, string des_key_path);
        /// <summary>
        /// 定义初始的全局变量
        /// </summary>
        public string key_path;
        public string loginUser;
        public string root_path;
        public string explorerPath;
        public string treeViewPath;
        public string tmp_file_path;
        public Form1()
        {
            InitializeComponent();
            loginUser = Form2.loginUser;
            root_path = System.IO.Directory.GetCurrentDirectory();
            tmp_file_path = System.IO.Path.Combine(root_path, "tmp.file");
            //Console.Write("root:{0}", root_path);
            key_path = System.IO.Path.Combine(root_path.Substring(0, root_path.LastIndexOf("/")), loginUser + ".key");
            //Console.Write("key:{0}",key_path);
            treeViewPath = root_path;
            explorerPath = root_path;
            this.treeView_list.Nodes.Clear();
            getExplorerView(null,root_path);
        }


        /// <summary>
        /// 导入文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_open_Click(object sender, EventArgs e)
        {
           
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string orig_path = openFileDialog1.FileName;
                    //string enc_file_path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), loginUser);
                    
                    string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                    string enc_file_path = System.IO.Path.Combine(explorerPath, filename);
                   // Console.Write("src:{0},dst:{1},key:{2}", orig_path, enc_file_path,key_path);
                    string exec_file_path = System.IO.Path.Combine(root_path, "encrypted_copy.out");
                    var cmd=exec_file_path+" "+orig_path+" "+enc_file_path+" "+key_path;
                    var process = new Process()
                    {
                        StartInfo = new ProcessStartInfo
                            {
                                FileName="/bin/bash",
                                Arguments=$"-c \" {cmd}\"",
                                CreateNoWindow=true,
                                UseShellExecute=false,
                            }
                    };
                    process.Start();
                    process.WaitForExit();
                    getFolderView(explorerPath);
                //Console.Write(cmd);
                //encrypted_copy(orig_path, enc_file_path, key_path);
                //this.treeView_list.Nodes.Clear();
                //getExplorerView(null, this.root_path);

            }
         
          
        }
        /// <summary>
        /// 单层遍历，仅显示当前目录下的文件夹和文件
        /// </summary>
        /// <param name="path"></param>
        private void getFolderView(string path)
        {
            explorerPath = path;

            try
            {
                this.listView_show.Items.Clear();
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                //遍历文件夹
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    this.listView_show.Items.Add("[" + NextFolder.Name + "]");

                }
                //遍历文件
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                    this.listView_show.Items.Add(NextFile.Name); //再添加文件大小显示
            }
            catch(Exception)
            {

            }
           
        }       
        /**
         * 遍历方案： -- 深度优先遍历
         * 1.获取当前目录下的所有文件夹
         * 2.将第一个目录添加到treeView节点，并返回该节点的 TreeNode对象 
         * 3.递归将返回到treeNode对象传递进去，还有子级目录的文件夹名称
         * 4.循环
         * 5.深度结束条件：子级目录为空 
         * 问题，第一层根节点怎么传值？ 解决 传一个 null
         */ 

        /// <summary>
        /// 遍历函数
        /// </summary>
        /// <param name="path"></param>        
        private void getExplorerView(TreeNode node,string path)
        {
            try
            {
                TreeNode newnode = null;
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                //遍历文件夹
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    newnode = addTreeViewLevelNode(node, NextFolder.Name); //添加一个节点
                    getExplorerView(newnode, NextFolder.FullName);
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("路径不存在");
            }

           
        }

        /// <summary>
        /// 目录遍历函数
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private TreeNode addTreeViewLevelNode(TreeNode node, string name)
        {
            TreeNode levelNode = new TreeNode();
            levelNode.Text = name;
            if (node == null)
            {
                treeView_list.Nodes.Add(levelNode);
            }
            else
            {              
                levelNode.Text = name;
                node.Nodes.Add(levelNode);
            }
            return levelNode;
        }
        /// <summary>
        /// treeView节点点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>          
        private void treeView_list_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string newPath = "";
            TreeNode node = null;                   
            try
            {
                node = e.Node.Parent;
                newPath = e.Node.Text;   
                while (true)
                {
                    newPath = newPath.Insert(0, node.Text + "/"); //node.Text+"/"                 
                    node = node.Parent;        
                }                
            }
            catch (NullReferenceException)
            {
           
            }
            if (!newPath.Equals(""))
            {
                explorerPath = newPath.Insert(0, treeViewPath + "/");                            
                getFolderView(explorerPath);//遍历该层文件 
            }           
        }
        /// <summary>
        /// 处理listView中目录点击展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        private void listView_show_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                string filename = this.listView_show.SelectedItems[0].SubItems[0].Text;
                if (filename.StartsWith("[")) //目录处理方式
                {
                    filename = filename.Replace("[", "");
                    filename = filename.Replace("]", "");
                    string newPath = explorerPath + "/" + filename;
                    getFolderView(newPath);//遍历该层文件                    
                }
                else //文件处理方式
                {                 
                    string newPath = explorerPath + "/" + filename;    
                    //显示文件大小
                    
                    Read_line(newPath); 
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                //MessageBox.Show(ex.ToString());
            }          
           
        }
        /// <summary>
        /// 后退按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_back_Click(object sender, EventArgs e)
        {
            try
            {
                string newPath = explorerPath.Substring(0, explorerPath.LastIndexOf("/"));
                if(newPath==root_path)
                {
                    MessageBox.Show("cannot access parent directory");
                }
                else
                {
                    getFolderView(newPath);
                }
                
            }catch(ArgumentOutOfRangeException)
            {
            }           
        }
        
        /// <summary>
        /// 读取文本文件 - 自动换行
        /// </summary>
        /// <param name="path"></param>
        public void Read_line(string path)
        {
            this.richTextBox_txtShow.Visible = true;
            this.richTextBox_txtShow.Clear();
            FileInfo fi = new FileInfo(path);
            if (fi.Length == 0)
            {
                MessageBox.Show("file size=0 byte,Writing process go wrong!Maybe you don't have specific privilege to read/write file");
                return;
            }

            //IntPtr i;

            //decrypted_copy(path,tmp_file_path, key_path);

            //this.richTextBox_txtShow.Text += Marshal.PtrToStringAuto(i);
            string exec_file_path = System.IO.Path.Combine(root_path, "decrypted_copy.out");
            var cmd = exec_file_path + " " + path + " " + tmp_file_path + " " + key_path;
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
            StreamReader sr = new StreamReader(tmp_file_path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                this.richTextBox_txtShow.Text += line.ToString() + "\n";
            }
            sr.Close();
            File.Delete(tmp_file_path);
        }
       

        
        /// <summary>
        /// 定时器-状态栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatuUpdate(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString();//时间
            toolStripStatusLabel2.Text = "total"+this.listView_show.Items.Count+"project"; //文件数          
        }
        /// <summary>
        /// 转到
        /// </summary>
        /// <param name = "sender" ></ param >
        /// < param name="e"></param>
        //private void button_go_Click(object sender, EventArgs e)
        //{
        //    string path = this.textBox_path.Text;
        //    explorerPath = path;
        //    treeViewPath = path;
        //    this.treeView_list.Nodes.Clear();
        //    getExplorerView(null, path);
        //}
        /// <summary>
        /// 文件删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = this.listView_show.SelectedItems[0].SubItems[0].Text;
                string newPath = explorerPath + "/" + filename;
                if (!filename.Equals(""))
                {
                    DialogResult r1 = MessageBox.Show("are you sure？", "yes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (r1.ToString() == "Yes")
                    {
                        //删除文件                   
                        File.Delete(newPath);
                        getFolderView(explorerPath);
                    }

                    else if (r1.ToString().Equals("No"))
                    {
                        return;
                    }
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("not allow to delete directory");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_export_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = this.listView_show.SelectedItems[0].SubItems[0].Text;
                if (filename.StartsWith("[")) //目录处理方式
                {
                    filename = filename.Replace("[", "");
                    filename = filename.Replace("]", "");
                    string newPath = explorerPath + "/" + filename;
                    getFolderView(newPath);//遍历该层文件                    
                }
                else //文件处理方式
                {
                    string newPath = explorerPath + "/" + filename;
                    //显示文件大小

                    export_file(newPath);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 读取文本文件 - 自动换行
        /// </summary>
        /// <param name="path"></param>
        public void export_file(string path)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dst_path = saveFileDialog1.FileName;
                string exec_file_path = System.IO.Path.Combine(root_path, "decrypted_copy.out");
                var cmd = exec_file_path + " " + path + " " + dst_path + " " + key_path;
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
                Console.Write(cmd);
            }
        }

        private void button_create_folder_Click(object sender, EventArgs e)
        {
            string foldername = textBox_folder_name.Text;
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(explorerPath,foldername));
            getFolderView(explorerPath);
        }
    }
    

    //public class des
    //{
    //    [DllImport("../libfilebox.so", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void encrypted_copy(string src_file_path, string dst_enc_file_path, string des_key_path);
    //    [DllImport("../libfilebox.so", CallingConvention = CallingConvention.Cdecl)]
    //    public static extern void decrypted_copy(string src_enc_file_path, string dst_dec_file_path, string des_key_path);
    //    //[DllImport("../libfilebox.so", CallingConvention = CallingConvention.Cdecl)]
    //    //public static extern void encrypted_write(string enc_file_path, string orig_file_path, string des_key_path);
    //    //[DllImport("../libfilebox.so", CallingConvention = CallingConvention.Cdecl)]
    //    //public static extern void decrypted_read(string filepath, string des_key_path,string tmp_file_path);

    //}
}
