using C.I.M.S_WPF.Model;
using C.I.M.S_WPF.Utils;
using C.I.M.S_WPF.Utils.DialogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace C.I.M.S_WPF.ViewModel
{
    public class AskSaveWindowViewModel : DialogViewModelBase
    {
        public CommandBase YesCommand { get; private set; }
        public CommandBase NoCommand { get; private set; }
        public CommandBase CancelCommand { get; private set; }

        public AskSaveWindowViewModel()
        {
            DialogTitle = "다른 이름으로 저장";

            YesCommand = new CommandBase(YesCommandExecute);
            NoCommand = new CommandBase(NoCommandExecute);
            CancelCommand = new CommandBase(CancelCommandExecute);
        }

        private void YesCommandExecute(object window)
        {
            CloseDialogWithResult(window as Window, DialogResult.Yes);
        }

        private void NoCommandExecute(object window)
        {
            CloseDialogWithResult(window as Window, DialogResult.No);
        }

        private void CancelCommandExecute(object window)
        {
            CloseDialogWithResult(window as Window, DialogResult.Cancel);
        }
    }
}
