using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace testapp
{
    public partial class MyPageList : ContentPage
    {
        public MyPageList()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            this.Navigation.PopToRootAsync();
        }
    }

    public class ListViewMode
    {
        public List<Data> Items { get; set; }

        public ListViewMode()
        {
            Items = new List<Data>();

            for(int i=1;i<=100;i++)
            {
                Items.Add(new Data{ Value = $"我是第{i}個" ,Index=i});
            }
        }
    }

    public class Data
    {
        public string Value { get; set; }
        public int Index { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
