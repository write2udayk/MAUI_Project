using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ECommerceApp.Services.Services;
using ECommerceApp.Models;


namespace ECommerceApp.ViewModels
{
    public partial class ProductViewModel : ObservableObject 
    {
        private readonly DatabaseService _databaseService;

        public ObservableCollection<Product> Products { get; }=new ();

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private int price;


        public ProductViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoadProductsCommond = new AsyncRelayCommand(LoadProductsAsync);
            AddProductCommand = new AsyncRelayCommand(AddProductAsync);
            DeleteProductCommand = new AsyncRelayCommand<Product>(DeleteProductAsync);
        }


        public IAsyncRelayCommand LoadProductsCommond { get; }

        public IAsyncRelayCommand AddProductCommand { get; }

        public IAsyncRelayCommand<Product> DeleteProductCommand { get; }

        private async Task DeleteProductAsync(Product product) 
        {
            if (product != null) 
            {   
                await _databaseService.DeleteProductAsync(product);
                Products.Remove(product);

            }
        }
        private async Task LoadProductsAsync() 
        {
            Products.Clear();
            var products = await _databaseService.GetProtuctAsync();
            foreach (var item in products)
            {
               Products.Add(item);
            }

        }

        private async Task AddProductAsync() 
        {
            // Validation checks
            if (string.IsNullOrWhiteSpace(Title))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a title", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a description", "OK");
                return;
            }

            if (Price <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a valid price", "OK");
                return;
            }

            var newProduct = new Product
            {
                Title = title,
                Description = description,
                Price = price,
            };

            await _databaseService.SaveproductAsync(newProduct);
            await LoadProductsAsync();

            Title = string.Empty;
            Description = string.Empty;
            Price = 0;


        }


    }

}
