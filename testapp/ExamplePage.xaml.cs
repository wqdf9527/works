using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;
using Xamarin.Forms;

namespace testapp
{
    public partial class ExamplePage : ContentPage
    {
        public ExamplePage()
        {
            InitializeComponent();
            this.BindingContext = new Computer();
        }
    }


    public enum OperatorType
    {
        None,
        Plus,
        Minus,
        Divided,
        Multiplied,
        Equal
    }

    public class Computer : INotifyPropertyChanged
    {
        private string _value = "0";

        //private string record = "";
        //private string secondrecord = "";

        //private bool Divided = false;
        //private bool Multiplied = false;
        //private bool Minus = false;
        //private bool Plus = false;
        //private bool Equal = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Value { get { return _value; } set { _value = value; PropertyChanged(this, new PropertyChangedEventArgs("Value")); } }

        public ICommand Zero { get; set; }
        public ICommand Reciprocal { get; set; }
        public ICommand Percent { get; set; }
        public ICommand Caculation { get; set; }
        public ICommand Operator { get; set; }

        public Computer()
        {

            //string math = "100 / 2";
            //string value = new DataTable().Compute(math, null).ToString();

           

            this.Reciprocal = new Command(num =>
            {
                this.Value = (double.Parse(this.Value) * (-1)).ToString();
            });

            this.Percent = new Command(() =>
            {
                this.Value = (double.Parse(this.Value) / 100).ToString();
            });
            

            string calculateString = string.Empty;
            OperatorType currentOperatorType = OperatorType.None;
            this.Operator = new Command<OperatorType>(arg =>
            {
                currentOperatorType = arg;
                switch (arg)
                {
                    case OperatorType.Plus:
                        calculateString += "+";
                        break;
                    case OperatorType.Minus:
                        calculateString += "-";
                        break;
                    case OperatorType.Multiplied:
                        calculateString += "*";
                        break;
                    case OperatorType.Divided:
                        calculateString += "/";
                        break;
                    case OperatorType.Equal:
                        this.Value = new DataTable().Compute(calculateString, null).ToString();
                        break;
                }
            });


            this.Caculation = new Command<string>(num =>
            {
                if (currentOperatorType == OperatorType.None)
                    this.Value = this.Value == "0" ? num : this.Value += num;
                else
                {
                    currentOperatorType = OperatorType.None;
                    this.Value = num;
                }
                calculateString += num;
            });

            this.Zero = new Command(() =>
            {
                this.Value = "0";
                calculateString = string.Empty;


            });


        }
    }
}