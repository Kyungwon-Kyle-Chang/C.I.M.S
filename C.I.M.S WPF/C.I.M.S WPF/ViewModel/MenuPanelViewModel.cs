using C.I.M.S_WPF.Model;
using C.I.M.S_WPF.Utils;
using C.I.M.S_WPF.Utils.DialogService;
using System;
using System.Windows;

namespace C.I.M.S_WPF.ViewModel
{
    public class MenuPanelViewModel : ViewModelBase
    {
        public CommandBase NewCommand { get; private set; }
        public CommandBase OpenCommand { get; private set; }
        public CommandBase SaveCommand { get; private set; }
        public CommandBase CloseCommand { get; private set; }
        public CommandBase ExitCommand { get; private set; }
        public CommandBase AboutCommand { get; private set; }

        public MenuPanelViewModel(Predicate<object> doesInvestInfoExist,
                                    Action<object> newCommandExecute,
                                    Action<object> saveCommandExecute,
                                    Action<object> openCommandExecute,
                                    Action<object> closeCommandExecute,
                                    Action<object> exitCommandExecute,
                                    Action<object> aboutCommandExecute)
        {
            NewCommand = new CommandBase(newCommandExecute);
            OpenCommand = new CommandBase(openCommandExecute);
            SaveCommand = new CommandBase(saveCommandExecute, doesInvestInfoExist);
            CloseCommand = new CommandBase(closeCommandExecute, doesInvestInfoExist);
            ExitCommand = new CommandBase(exitCommandExecute);
            AboutCommand = new CommandBase(aboutCommandExecute);
        }
    }
}
