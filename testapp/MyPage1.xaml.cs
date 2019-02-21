using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace testapp
{
    public partial class MyPage1 : ContentPage
    {
        public MyPage1()
        {
            InitializeComponent();

            //var man = new Man() { Name = "Amy", Age = 44 };

            //this.BindingContext = man;

            //this.lblname.BindingContext = man;
            //this.lblage.BindingContext = man;

            //Binding namebinding = new Binding();
            //namebinding.Source = man;
            //namebinding.Path = nameof(man.Name);

            //lblname.SetBinding(Label.TextProperty, namebinding);

            //Binding agebinding = new Binding();
            //agebinding.Source = man;
            //agebinding.Path = nameof(man.Age);

            //lblage.SetBinding(Label.TextProperty, agebinding);

        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new MyPage2());
            //this.Navigation.PushModalAsync(new MyPage2()); //開新頁面不在navugation裡面沒有back
        }
    }

    public class Man: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
       public ICommand OnAddAge { get; set; }

        private int _Age = 44;
        public string Name { get; set; } = "Amy";
        public int Age 
        {
            get { return this._Age; }
            set 
            { 
                this._Age = value;
               this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Age)));
            }

      }

       public Man()
        {
            this.OnAddAge = new Command
            (
            () =>
            {
                this.Age += 1;
            });
        }





    }
}
