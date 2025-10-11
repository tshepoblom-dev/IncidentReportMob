using ClientApp.Viewmodels;

namespace ClientApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            BindingContext = mainPageViewModel;
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            
                //LoginBtn.Text;
        }
    }

}
