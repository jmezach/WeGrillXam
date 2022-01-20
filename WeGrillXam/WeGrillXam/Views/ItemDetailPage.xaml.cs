using System.ComponentModel;
using Xamarin.Forms;
using WeGrillXam.ViewModels;

namespace WeGrillXam.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
