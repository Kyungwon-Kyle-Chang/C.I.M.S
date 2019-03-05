using C.I.M.S_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace C.I.M.S_WPF.Utils.DialogService
{
    public class DialogViewModelBase : ViewModelBase
    {
        public DialogResult UserDialogResult { get; private set; }

        private string _dialogTitle = "DialogWindow";
        public string DialogTitle
        {
            get { return _dialogTitle; }
            set { SetProperty(ref _dialogTitle, value); }
        }

        public void CloseDialogWithResult(Window dialog, DialogResult result)
        {
            UserDialogResult = result;
            if (dialog != null)
                dialog.DialogResult = true;
        }
    }
}
