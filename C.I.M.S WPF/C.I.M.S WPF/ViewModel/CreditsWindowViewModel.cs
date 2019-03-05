using C.I.M.S_WPF.Utils.DialogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.ViewModel
{
    public class CreditsWindowViewModel : DialogViewModelBase
    {
        private string _programTitle = "Cryptocurrency Investment Management System V1.0";
        public string ProgramTitle
        {
            get { return _programTitle; }
            set { SetProperty(ref _programTitle, value); }
        }

        private string _madeBy = "Made by Kyungwon Chang";
        public string MadeBy
        {
            get { return _madeBy; }
            set { SetProperty(ref _madeBy, value); }
        }

        public CreditsWindowViewModel()
        {
            DialogTitle = "About This Program";
        }
    }
}
