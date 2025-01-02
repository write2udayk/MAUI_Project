using ECommerceApp.ViewModels;

namespace ECommerceApp.UI.Views;

public partial class ProductsPage : ContentPage
{
	public ProductsPage(ProductViewModel productViewModel)
	{
		InitializeComponent();
		BindingContext=productViewModel;
		Task.Run(async () => await productViewModel.LoadProductsCommond.ExecuteAsync(null));
	}
}