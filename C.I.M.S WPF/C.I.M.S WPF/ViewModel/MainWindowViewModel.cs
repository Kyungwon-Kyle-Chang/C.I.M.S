using C.I.M.S_WPF.Model;
using C.I.M.S_WPF.Utils;
using C.I.M.S_WPF.Utils.DialogService;
using System.Windows;

namespace C.I.M.S_WPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public InvestInfo InvestInfo { get; private set; }
        public string Title { get { return Configs.MAIN_TITLE; } }
        
        private ViewModelBase _dockPanelTop;
        public ViewModelBase DockPanelTop
        {
            get { return _dockPanelTop; }
            set { SetProperty(ref _dockPanelTop, value); }
        }

        private ViewModelBase _grid00;
        public ViewModelBase Grid00
        {
            get
            {
                return _grid00;
            }
            set
            {
                SetProperty(ref _grid00, value);
            }
        }

        private ViewModelBase _grid10;
        public ViewModelBase Grid10
        {
            get { return _grid10; }
            set
            {
                SetProperty(ref _grid10, value);
            }
        }

        private ViewModelBase _grid01;
        public ViewModelBase Grid01
        {
            get { return _grid01; }
            set { SetProperty(ref _grid01, value); }
        }

        private ViewModelBase _grid11;
        public ViewModelBase Grid11
        {
            get { return _grid11; }
            set { SetProperty(ref _grid11, value); }
        }

        private ViewModelBase _dockPanelBottom;
        public ViewModelBase DockPanelBottom
        {
            get { return _dockPanelBottom; }
            set { SetProperty(ref _dockPanelBottom, value); }
        }
        
        /// <summary>
        /// ViewModel of the MainWindow. Has been set from MainWindowView.xaml.
        /// </summary>
        public MainWindowViewModel()
        {
            DockPanelTop = new MenuPanelViewModel(x => { return InvestInfo != null; }, 
                                                    NewFile, SaveFile, OpenFile, Reset, Exit, ShowAboutProgram);
            DockPanelBottom = new StatusbarViewModel();
        }

        public void StartWithNewModel(string title, double principal)
        {
            InvestInfo investInfo = new InvestInfo(title, principal);
            LoadNewInterface(investInfo);
        }

        public void LoadNewInterface(InvestInfo loadedFile)
        {
            if (loadedFile == null) return;

            Reset(null);
            InvestInfo = loadedFile.Run();

            Grid00 = new DataInputButtonsViewModel(InvestInfo);
            Grid01 = new OutputSummaryViewModel(InvestInfo);
            Grid10 = new DataInputViewModel(InvestInfo);
            Grid11 = new OutputDetailViewModel(InvestInfo);
        }

        public void NewFile(object obj)
        {
            if (InvestInfo != null)
            {
                DialogResult result = DialogService.OpenDialog(new AskSaveWindowViewModel());
                if (result == DialogResult.Yes)
                {
                    if (DbConnector.SaveFile(InvestInfo, InvestInfo.InvestmentTitle))
                    {
                        DialogService.OpenDialog(new NewInvestWindowViewModel(StartWithNewModel));
                    }   
                }
                else if (result == DialogResult.No)
                {
                    DialogService.OpenDialog(new NewInvestWindowViewModel(StartWithNewModel));
                }
            }
            else
            {
                DialogService.OpenDialog(new NewInvestWindowViewModel(StartWithNewModel));
            }
        }

        public void SaveFile(object obj)
        {
            if(DbConnector.SaveFile(InvestInfo, InvestInfo.InvestmentTitle))
            {
                Messenger.Instance.Send("저장 완료", Context.WRITE_LOG);
            }
        }

        public void OpenFile(object obj)
        {
            if (InvestInfo != null)
            {
                DialogResult result = DialogService.OpenDialog(new AskSaveWindowViewModel());
                if (result == DialogResult.Yes)
                {
                    if (DbConnector.SaveFile(InvestInfo, InvestInfo.InvestmentTitle))
                    {
                        LoadNewInterface(DbConnector.LoadFile<InvestInfo>());
                    }
                }
                else if (result == DialogResult.No)
                {
                    LoadNewInterface(DbConnector.LoadFile<InvestInfo>());
                }
            }
            else
            {
                LoadNewInterface(DbConnector.LoadFile<InvestInfo>());
            }
        }

        public void Reset(object obj)
        {
            InvestInfo = null;

            Grid00 = null;
            Grid01 = null;
            Grid10 = null;
            Grid11 = null;
        }

        public void Exit(object obj)
        {
            Application.Current.Shutdown();
        }

        public void ShowAboutProgram(object obj)
        {
            DialogService.OpenDialog(new CreditsWindowViewModel());
        }
    }
}
