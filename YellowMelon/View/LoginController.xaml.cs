using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YellowMelon.Model;
using YellowMelon.ViewModel;

// 사용자 정의 컨트롤 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234236에 나와 있습니다.

namespace YellowMelon.View
{
    public sealed partial class LoginController : UserControl
    {
        public event EventHandler LoginEnd;
        UserViewModel userViewModel = new UserViewModel();
        public string txId { get; set; }
        public string txPw { get; set; }
        public string txName { get; set; }
        bool issignup = false;
        public LoginController()
        {
            this.InitializeComponent();
            changeType();
            DataContext = this;
        }
        private void changeType()
        {
            if (issignup)
            {
                NameGrid.Visibility = Visibility.Visible;
                btnSignup.Content = "로그인으로";
                btnLogin.Content = "회원가입";
            }
            else
            {
                NameGrid.Visibility = Visibility.Collapsed;
                btnSignup.Content = "회원가입으로";
                btnLogin.Content = "로그인";
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (issignup)
            {
                if (userViewModel.Signup(txId, txPw, txName))
                {
                    await new MessageDialog("회원가입에 성공했습니다.").ShowAsync();
                    issignup = false;
                    changeType();
                }
                else
                {
                    await new MessageDialog("해당 아이디는 사용 중입니다.").ShowAsync();
                }
            }
            else
            {
                User user = userViewModel.Login(txId, txPw);
                if (user != null)
                {
                    //   await new MessageDialog("성공").ShowAsync();
                    LoginEnd(user, null);
                }
                else
                {
                    await new MessageDialog("아이디 혹은 비밀번호가 틀렸습니다.").ShowAsync();
                }
            }

        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            issignup = !issignup;
            changeType();
        }
    }
}
