using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Linq;

namespace testapp
{
    public partial class HWPage : ContentPage
    {
        public HWPage()
        {
            this.BindingContext = new CalculationViewmodle();
            InitializeComponent();
        }                  
    }

    public class CalculationViewmodle : INotifyPropertyChanged
    {
        private string _value = "0";
        public string Value
        {
            get { return _value; }
            set { _value = value; PropertyChanged(this, new PropertyChangedEventArgs("Value")); }
        }

        private string record = string.Empty;
        private string secondrecord = string.Empty;
        private string symbol = string.Empty;
        private string status = string.Empty;
        private bool isdot = false;      
        private bool isequal = false;
        private bool islonger = false;

        public enum Status { Isplus, Issubtract, Ismultiplication, Isdivision ,first};   
        public ICommand Caculation { get; set; }
        public ICommand Zero { get; set; }
        public ICommand Percent { get; set; }
        public ICommand Reciprocal { get; set; }

        public CalculationViewmodle()
        {
            this.Zero = new Command(() =>
            {
                this.Value = "0";
                record = string.Empty;
                secondrecord = string.Empty;
                status = string.Empty;
                symbol = string.Empty;
                isdot = false;
                isequal = false;
                islonger = false;
                SpeakNow("0");
            });

            Reciprocal = new Command(() =>
            {
                this.Value = (double.Parse(this.Value) * (-1)).ToString();
                SpeakNow(this.Value);
            });


            Percent = new Command(() =>
            {
                this.Value = (double.Parse(this.Value) / 100).ToString();
                SpeakNow(this.Value);
                this.Isdecimal();
            });

            Caculation = new Command(input =>
                {
                    switch (input)
                    {
                        case "+":
                            this.Operate("+", Status.Isplus.ToString());
                            SpeakNow("加");
                            break;

                        case "-":
                            this.Operate("-", Status.Issubtract.ToString());
                            SpeakNow("減");
                            break;

                        case "*":
                            this.Operate("*", Status.Ismultiplication.ToString());
                            SpeakNow("乘");
                            break;

                        case "/":
                            this.Operate("/", Status.Isdivision.ToString());
                            SpeakNow("除");
                            break;

                        case ".":
                            this.Isdecimal();
                            if (!isdot)
                            {
                                if (status == string.Empty || secondrecord != string.Empty)
                                {
                                    this.Value += ".";
                                    SpeakNow(".");
                                }                         
                                isdot = true;
                            }            
                            break;

                        case "=":
                            SpeakNow("等於");
                            symbol = string.Empty;
                            islonger = false;
                            if (!string.IsNullOrEmpty(record) && !string.IsNullOrEmpty(this.Value))
                            {
                                if (!isequal)
                                {
                                    if (status == "Isplus")
                                    {
                                        secondrecord = this.Value;
                                        this.Value = (double.Parse(record) + double.Parse(this.Value)).ToString();
                                    }
                                    else if (status == "Issubtract")
                                    {
                                        secondrecord = this.Value;
                                        this.Value = (double.Parse(record) - double.Parse(this.Value)).ToString();
                                    }
                                    else if (status == "Ismultiplication")
                                    {
                                        secondrecord = this.Value;
                                        this.Value = (double.Parse(record) * double.Parse(this.Value)).ToString();
                                    }
                                    else if (status == "Isdivision")
                                    {
                                        secondrecord = this.Value;
                                        this.Value = (double.Parse(record) / double.Parse(this.Value)).ToString();
                                    }
                                    isequal = true;
                                }
                                else
                                {
                                    if (status == "Isplus")
                                    {
                                        this.Value = (double.Parse(this.Value) + double.Parse(secondrecord)).ToString();
                                    }
                                    else if (status == "Issubtract")
                                    {
                                        this.Value = (double.Parse(this.Value) - double.Parse(secondrecord)).ToString();
                                    }
                                    else if (status == "Ismultiplication")
                                    {
                                        this.Value = (double.Parse(this.Value) * double.Parse(secondrecord)).ToString();
                                    }
                                    else if (status == "Isdivision")
                                    {
                                        this.Value = (double.Parse(this.Value) / double.Parse(secondrecord)).ToString();
                                    }
                                }
                            }
                            SpeakNow(this.Value);
                            this.Isdecimal();
                            break;

                        default:
                            if (Value == "0")
                            {
                                this.Value = input.ToString();
                                SpeakNow(input.ToString());
                            }
                            else if(isequal&& !islonger)
                            {
                                this.Value = input.ToString();
                                SpeakNow(input.ToString());
                                islonger = true;
                            }
                            else if(symbol!= string.Empty && !islonger)
                            {                              
                                this.Value = input.ToString();
                                SpeakNow(input.ToString());
                                secondrecord = this.Value;
                                islonger = true;
                            }
                            else if(record!=string.Empty)
                            {
                                if (symbol == string.Empty && islonger==false)
                                {
                                    this.Value = input.ToString();
                                    SpeakNow(input.ToString());
                                    record = string.Empty;
                                }
                                else
                                {
                                    this.Value += input;
                                    SpeakNow(input.ToString());
                                }
                            }
                            else
                            {
                                this.Value += input;
                                SpeakNow(input.ToString());
                            }
                            break;
                    }
                });
        }

        /// <summary>
        /// 加減乘除的等於功能
        /// </summary>
        public void Symbol()
        {

            if (symbol == "+")
            {
                this.Value = (double.Parse(record) + double.Parse(this.Value)).ToString();
                record = this.Value;
                SpeakNow("等於"+this.Value);
                islonger = false;
                status = Status.Isplus.ToString();
            }
            else if (symbol == "-")
            {
                this.Value = (double.Parse(record) - double.Parse(this.Value)).ToString();
                record = this.Value;
                SpeakNow("等於" + this.Value);
                islonger = false;
                status = Status.Issubtract.ToString();
            }
            else if (symbol == "*")
            {
                this.Value = (double.Parse(record) * double.Parse(this.Value)).ToString();
                record = this.Value;
                SpeakNow("等於" + this.Value);
                islonger = false;
                status = Status.Ismultiplication.ToString();
            }
            else if (symbol == "/")
            {
                this.Value = (double.Parse(record) / double.Parse(this.Value)).ToString();
                record = this.Value;
                SpeakNow("等於" + this.Value);
                islonger = false;
                status = Status.Isdivision.ToString();
            }
            this.Isdecimal();
        }

        /// <summary>
        /// 判斷是否有小數點
        /// </summary>
        public void Isdecimal()
        {
            for (int i = 0; i < this.Value.Length; i++)
            {
                if (this.Value[i] == '.')
                {
                    isdot = true;
                    break;
                }
                else
                {
                    isdot = false;
                }
            }
        }
        /// <summary>
        /// 加減乘除運算
        /// </summary>
        public void Operate(string strsymbol, string strstatus)
        {
            this.Isdecimal();
            if (symbol != string.Empty)
            {
                if (secondrecord == string.Empty)
                {
                    symbol = strsymbol;
                    status = strstatus;
                }
                else
                {
                    Symbol();
                    symbol = strsymbol;
                    status = strstatus;
                    secondrecord = string.Empty;
                }
            }
            else
            {
                secondrecord = string.Empty;
                status = strstatus;
                isequal = false;
                if (this.Value != string.Empty)
                {
                    record = this.Value;
                }
                symbol = strsymbol;
                this.Value = record;
            }
            islonger = false;
        }

        /// <summary>
        /// 大聲朗讀
        /// </summary>

        public async Task SpeakNow(string content)
        {
            var locales =await TextToSpeech.GetLocalesAsync();
            var locale = locales.Where(l => l.Language == "zh-TW").First();
            var settings = new SpeechOptions()
            {
                Volume = (float?)1.0,
                Pitch = (float?)1.0,
                Locale = locale
        };
           await TextToSpeech.SpeakAsync(content, settings);
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
