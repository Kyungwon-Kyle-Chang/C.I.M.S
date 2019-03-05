using C.I.M.S_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.ViewModel
{
    public class StatusbarViewModel : ViewModelBase
    {
        private string _log;
        public string Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value); }
        }

        private bool _isInProgress = false;
        public bool IsInProgress
        {
            get { return _isInProgress; }
            set { SetProperty(ref _isInProgress, value); }
        }

        private string _workDescription;
        public string WorkDescription
        {
            get { return _workDescription; }
            set { SetProperty(ref _workDescription, value); }
        }

        public StatusbarViewModel()
        {
            Messenger.Instance.Register<string>(this, WriteLog, Context.WRITE_LOG);
            Messenger.Instance.Register<bool>(this, SwitchProgressBar, Context.PROGRESSBAR);
            Messenger.Instance.Register<string>(this, WriteProgressDescription, Context.PROGRESS_DESC);
        }

        private void WriteLog(string s)
        {
            Log = s;
        }

        private void SwitchProgressBar(bool state)
        {
            IsInProgress = state;
        }

        private void WriteProgressDescription(string s)
        {
            WorkDescription = s;
        }
    }
}
