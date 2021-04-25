﻿using HashtilCagesMap.Entities;
using HashtilCagesMap.HttpReq;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace HashtilCagesMap
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>

    public partial class Login : Window
    {
        ObservableCollection<UserLogin> userLogins = new ObservableCollection<UserLogin>();
        UserLogin ul = new UserLogin();

      
        public delegate void busyind();
            
        public Login()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzMxMTI4QDMxMzgyZTMzMmUzMFJpMXFFNjNYUTRnYzArdWhuaVpZR0htaWtsancwSVRQd1NmK3l3dXlNNmc9");

            InitializeComponent();
            ApiHelper.InitializeClient();
            loginsfbi.IsBusy = false;
            if(Properties.Settings.Default.UseName != string.Empty)
            {
                username.Text = Properties.Settings.Default.UseName;
                password.Password = Properties.Settings.Default.Password;
            }

            


        }

       


        private void CloseWindow()
        {
            this.Close();
        }

        private async  void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            loginsfbi.IsBusy = true;
          
            if (rmechbox.IsChecked == true)
            {
                Properties.Settings.Default.UseName = username.Text;
                Properties.Settings.Default.Password = password.Password;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.UseName = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
            try
            {
                var LoginUsersList = await LoginHttp.UserLoginCheck(username.Text, password.Password);
                if (LoginUsersList.Count == 1)
                {
                    try
                    {
                        ul.UserId = LoginUsersList[0].UserId;
                        ul.UserName = LoginUsersList[0].UserName;
                        ul.UserNickName = LoginUsersList[0].UserNickName;
                        ul.UserLevel = LoginUsersList[0].UserLevel;
                        MainWindow mw = new MainWindow();
                        mw.logonuser.Text = ul.UserNickName;
                        mw.txtblkmap.Text = ul.UserNickName;
                        mw.logonusercxsreen.Text = ul.UserNickName;
                        mw.userlevel.Text = Convert.ToString(ul.UserLevel);
                        CloseWindow();

                        loginsfbi.IsBusy = false;
                        mw.ShowDialog();
                    }
                    catch (Exception l)
                    {
                        MessageBox.Show(l.ToString());
                    }

                }
                else
                {
                    invaliduser.Text = "שם משתמש ו/או סיסמא לא נכונים!";
                    loginsfbi.IsBusy = false;
                }
            }
            catch(Exception)
            {
                invaliduser.Text = "שם משתמש ו/או סיסמא לא נכונים!";
                loginsfbi.IsBusy = false;
            }
           


        }
    }
  
}
