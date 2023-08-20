using Plugin.NFC;
using ProgrammesSecu.ViewModels;

namespace ProgrammesSecu.Views;

public partial class NfcPage : ContentPage
{
	public NfcPage(NfcViewModel nfcViewModel)
	{
		InitializeComponent();
		BindingContext = nfcViewModel;
    }

}