using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExamAdminSys
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            bool ok = await AccountHelper.VerifyUserNamePassword(uid.Text, psw.Text);
            if (ok)
            {
                var form = new Main();
                form.Show();
                this.Close();
            }
            else
            {
                lbInfo.Content = "用户名或密码错误";
            }

        }
    }
}
