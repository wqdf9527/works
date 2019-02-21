using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace testapp
{
    public partial class MyPage2 : ContentPage
    {
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new MyPageList());
            //this.Navigation.PopAsync();
            //this.Navigation.PopModalAsync();
        }

        public MyPage2()
        {
            InitializeComponent();
        }
    }
}
