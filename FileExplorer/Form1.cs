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


namespace FileExplorer
{
    public partial class Form1 : Form
    {
        public byte[] key;
        public string loginUser;
        public Form1()
        {
            InitializeComponent();
            key = System.Text.Encoding.Default.GetBytes(Form2.key);
            loginUser = Form2.loginUser;
        }
        /// <summary>
        /// 定义初始的全局变量
        /// </summary>
        string explorerPath = "";
        string treeViewPath = "";
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_open_Click(object sender, EventArgs e)
        {
           
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string orig_path = openFileDialog1.FileName;
                    string enc_file_path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), loginUser);
                    des.encrypted_write(enc_file_path, orig_path, key);
                    
                }
         
          
        }
        /// <summary>
        /// 单层遍历，仅显示当前目录下的文件夹和文件
        /// </summary>
        /// <param name="path"></param>
        private void getFolderView(string path)
        {
            explorerPath = path;
            textBox_path.Text = explorerPath;
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
                MessageBox.Show("路径不存在");
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
                    int size = getFileSize(newPath);
                    if(size > 1024*1024)
                    {
                        toolStripStatusLabel3.Text = ((float)size / (1024 * 1024)).ToString("F1") +"GB";
                    }
                    else if (size>1024)
                    {
                        toolStripStatusLabel3.Text = ((float)size / 1024).ToString("F1") + "MB";
                    }
                    else
                    {
                        toolStripStatusLabel3.Text = size + "KB";
                         
                    }
                    OpenFile(newPath); 
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
                getFolderView(newPath);
            }catch(ArgumentOutOfRangeException)
            {
            }           
        }
        /// <summary>
        /// 打开文件，仅支持文本文件，而且文件大小小于1MB
        /// </summary>
        /// <param name="path"></param>
        private void OpenFile(string path)
        {            
            if(getFileSize(path)<1000 && getFileType(path)) //如果文件小于1000KB，并且后缀为文本文件类型
            {                
                Read_line(path);
            }
        }
        /// <summary>
        /// 读取文本文件 - 自动换行
        /// </summary>
        /// <param name="path"></param>
        public void Read_line(string path)
        {
            this.richTextBox_txtShow.Visible = true;
            this.pictureBox1.Visible = false;
            this.richTextBox_txtShow.Clear();
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                this.richTextBox_txtShow.Text += line.ToString() + "\n";
            }
            sr.Close();
        }
       
        /// <summary>
        /// 获取文件的大小
        /// </summary>
        /// <param name="path"></param>
        private int getFileSize(string path)
        {
            try
            {
                FileInfo fi = new FileInfo(path); //返回的是字节大小
                float x = ((float)fi.Length / 1024);
                int result = (int)(x + 1);
                return result;
            }
            catch (FileNotFoundException)
            {
                return 0;
            }
            
        }
        /// <summary>
        /// 获取文件类型是否是文本类型，主要是判断文件后缀。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool getFileType(string path)
        {
           
            string fileType = path.Substring(path.LastIndexOf(".")+1, path.Length - path.LastIndexOf(".") -1);
            string[] type = { "txt", "xml", "ini", "conf", "java", "cs", "sql", "html", "js", "css", "c", "h", "cpp","py" };

            for (int i = 0; i < type.Length;i++ )
            {
                if (fileType.Equals(type[i]))
                {
                    return true;
                }
            }
            return false;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_go_Click(object sender, EventArgs e)
        {
            string path = this.textBox_path.Text;
            explorerPath = path;
            treeViewPath = path;            
            this.treeView_list.Nodes.Clear();
            getExplorerView(null, path);
        }
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
                    DialogResult r1 = MessageBox.Show("是否永久删除该文件？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                MessageBox.Show("暂时不允许删除目录");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    public class des
    {
        [DllImport("./libfilebox.so", CallingConvention = CallingConvention.Cdecl)]
        public static extern void encrypted_write(string enc_file_path, string orig_file_path, byte[] des_key);
        public static extern int decrypted_read(string filepath, string buffer, byte[] des_key);
    }
}
