using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountrys.ViewModels
{
    public class NewCountryModelView : BaseViewModel
    {
        private string _Alpha = "";

        public string Alpha
        {
            get { return _Alpha; }

            set { _Alpha = value; OnPropertyChanged("Alpha"); }
        }

        private string _Name = "";

        public string Name
        {
            get { return _Name; }

            set { _Name = value; OnPropertyChanged("Name"); }
        }

        private int _Code;

        public int Code
        {
            get { return _Code; }

            set { _Code = value; OnPropertyChanged("Code"); }
        }
    }
}
