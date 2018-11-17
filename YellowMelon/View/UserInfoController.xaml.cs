using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YellowMelon.Model;
using YellowMelon.ViewModel;
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace YellowMelon.View
{
    public sealed partial class UserInfoController : UserControl
    {
        UserViewModel userViewModel = new UserViewModel();
        Artist current = null;
        public UserInfoController()
        {
            this.InitializeComponent();
        }
        internal void InitData(User user)
        {
            Artist art = userViewModel.CheckArtist(user.Index);
            DataContext = user;
            if (art != null)
            {
                tbUserType.Text = " 아티스트님";
                current = art;
                btnChangeArtist.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnUpload.Visibility = Visibility.Collapsed;
            }
        }
        
    }
}
