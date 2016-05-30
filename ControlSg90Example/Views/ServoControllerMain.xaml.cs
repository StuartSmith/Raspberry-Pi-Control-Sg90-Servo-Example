using ControlSg90Example.ViewModels;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ControlSg90Example.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ServoControllerMain : Page
    {
        private VM_ServoControllerMain viewModel;

        public ServoControllerMain()
        {
            this.InitializeComponent();

            viewModel = new VM_ServoControllerMain();
        }

       

       
    }
}
