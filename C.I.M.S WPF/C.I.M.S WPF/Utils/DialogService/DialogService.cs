using C.I.M.S_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.I.M.S_WPF.Utils.DialogService
{
    public static class DialogService
    {
        public static DialogResult OpenDialog(ViewModelBase vm)
        {
            DialogWindow win = new DialogWindow();
            win.DataContext = vm;
            win.ShowDialog();

            DialogResult result = (win.DataContext as DialogViewModelBase).UserDialogResult;
            return result;
        }
    }
}
