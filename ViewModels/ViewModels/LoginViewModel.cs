using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.BaseClasses;
using IOops;
using System.ComponentModel;

namespace ViewModels.ViewModels
{
    public class LoginViewModel:ObservableObjects,IDataErrorInfo
    {
        #region privateVariable
        private string _name;
        private string _email;
        private string _errormessage;
        #endregion

        #region publicVariables
        public static Student CurrentStudent;
        #endregion

        #region publicProperties
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errormessage;
            }
            set
            {
                _errormessage = value;
                OnPropertyChanged();
            }
        }

        

        public RelayCommand LoginCommand { get; set; }

        #endregion
        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    return string.IsNullOrEmpty(this.Name) ? "Required value" : null;
                }

                if (columnName == "Email")
                {
                    return string.IsNullOrEmpty(this.Email) ? "Required value" : null;
                }
            
                return null;
            }
        }
       


        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(LoginExecute,LoginCanExecute);
        }

        #region Commands
        public void LoginExecute(object parameter)
        {

        }

        public bool LoginCanExecute(object parameter)
        {
            bool canExecute = false;

              CurrentStudent = Student.Login(Name, Email);

            if (CurrentStudent != null)
            {                
                 canExecute = true;
             }
            else
            {  canExecute = false; }
                

            if(canExecute == false)
            {
                ErrorMessage = "Incorrect Credentials";
            }
            else
                ErrorMessage = "";

            return canExecute;
        }

        #endregion
    }
}
