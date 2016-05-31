using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ControlSg90Example.ViewModels.BaseViewModel
{
    public class ViewModelBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { }; 

 
       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        { 
            if (PropertyChanged != null) 
                 PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); 
         }

    }
}
