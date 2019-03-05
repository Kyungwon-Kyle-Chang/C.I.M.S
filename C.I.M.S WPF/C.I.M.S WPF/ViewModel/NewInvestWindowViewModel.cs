using C.I.M.S_WPF.Model;
using C.I.M.S_WPF.Utils;
using C.I.M.S_WPF.Utils.DialogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace C.I.M.S_WPF.ViewModel
{
    public class NewInvestWindowViewModel : DialogViewModelBase
    {
        private string _investmentTitle;
        public string InvestmentTitle
        {
            get { return _investmentTitle; }
            set
            {
                SetProperty(ref _investmentTitle, value);
            }
        }

        private string _principal = "0";
        public string Principal
        {
            get { return _principal; }
            set
            {
                SetProperty(ref _principal, value);
            }
        }

        public CommandBase EnterCommand { get; private set; }
        private Action<string, double> _createNewFunc;


        public NewInvestWindowViewModel(Action<string, double> createNewFunc)
        {
            DialogTitle = "신규 투자 기록";

            EnterCommand = new CommandBase(EnterCommandExecute, EnterCommandCanExecute);
            _createNewFunc = createNewFunc;
        }

        private void EnterCommandExecute(object window)
        {
            CloseDialogWithResult(window as Window, DialogResult.Yes);

            _createNewFunc(InvestmentTitle, double.Parse(Principal));           
        }

        private bool EnterCommandCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(InvestmentTitle) && RegexManager.IsTextUnsignedDouble(Principal);
        }
    }
}
