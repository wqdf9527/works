using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;

namespace testapp
{
    public partial class MyPage8 : ContentPage
    {
        public MyPage8()
        {
            InitializeComponent();

            var path=Environment.GetFolderPath(Environment.SpecialFolder.Personal); //android
            path=System.IO.Path.Combine(path, "..", "Library","sql.db");


            SQLiteConnection conn = new SQLiteConnection(path);

            conn.CreateTable<MyTable>();
            conn.Insert(new MyTable()
            {
                id=Guid.NewGuid().ToString(),
                Value="123"
            });

            var result=conn.Table<MyTable>().Where(x => x.Value == "123").ToList();

            //conn.Delete<MyTable>(1);
            //conn.Update();
        }

        [Table("My")] //換名字
        public class MyTable
        {
            [Column("IDkey")]
            [SQLite.PrimaryKey]
            public string id { get; set; }

            public string Value { get; set; }
        }
    }
}
