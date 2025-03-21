using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace ScreenCaptrue
{

    public partial class Form2 : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        public Form2()
        {
            InitializeComponent();

            // 初始化托盘图标
            trayIcon = new NotifyIcon();
            trayIcon.Text = "ScreenCaptrue"; // 托盘图标提示文本
            trayIcon.Icon = new Icon("screenCaptrue.ico", 40, 40); // 设置托盘图标

            // 初始化托盘菜单
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Exit", null, OnExit); // 添加“退出”菜单项
            this.ShowInTaskbar = false; // 隐藏任务栏图标
            // 将菜单绑定到托盘图标
            trayIcon.ContextMenuStrip = trayMenu;

            // 双击托盘图标时还原窗口
            trayIcon.DoubleClick += OnTrayIconDoubleClick;

            // 显示托盘图标
            trayIcon.Visible = true;
            this.Resize += OnResize; // 处理窗体大小调整事件
        }

        // 处理“退出”菜单项点击事件
        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.Visible = false; // 隐藏托盘图标
            Application.Exit(); // 退出应用程序
        }


        // 双击托盘图标时还原窗口
        private void OnTrayIconDoubleClick(object sender, EventArgs e)
        {
            this.Show(); // 显示窗体
            this.WindowState = FormWindowState.Normal; // 还原窗口状态
        }


        void ScreenCapture()
        {
            try
            {
                DllImportHelper.RunSnap();
            }
            catch (Exception ex)
            {
            }
        }

        void FromFroceClosee()
        {
            this.Close();
            this.Dispose();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            HotkeyHelper.ProcessHotKey(m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScreenCapture();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //注册热键(窗体句柄,热键ID,辅助键,实键)   

            try
            {
                HotkeyHelper.Regist(this.Handle, HotkeyModifiers.MOD_ALT, Keys.Escape, FromFroceClosee);
            }
            catch (Exception)
            {

            }
            try
            {

                HotkeyHelper.Regist(this.Handle, HotkeyModifiers.MOD_ALT, Keys.A, ScreenCapture);
                button1.Text = "截图 (快捷键 Alt + A ) ";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Alt + A 热键被占用");
                try
                {
                    HotkeyHelper.Regist(this.Handle, HotkeyModifiers.MOD_CONTROL | HotkeyModifiers.MOD_ALT, Keys.A, ScreenCapture);
                    button1.Text = "截图 (快捷键 CTRL + Alt + A ) ";
                }
                catch (Exception ex1)
                {
                    MessageBox.Show("CTRL + Alt + A 热键被占用");
                }

            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            //注消热键(句柄,热键ID)   
            HotkeyHelper.UnRegist(this.Handle, ScreenCapture);
        }

        // 处理窗体大小调整事件
        private void OnResize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); // 隐藏窗体
            }
        }
    }
}
