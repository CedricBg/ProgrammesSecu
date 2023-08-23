using ProgrammesSecu.ViewModels;

namespace ProgrammesSecu.Views;

public partial class ServerPage : ContentPage
{
	public ServerPage(ServerPageViewModel serverPageViewModel )
	{
		InitializeComponent();
		BindingContext = serverPageViewModel;

    }
}