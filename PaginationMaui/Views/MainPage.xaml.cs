using Microsoft.Maui.Controls;
using System;

namespace PaginationMaui
{
    public partial class MainPage : ContentPage
    {
        private BreedsViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new BreedsViewModel();
            BindingContext = _viewModel;
        }

        private async void OnRemainingItemsThresholdReached(object sender, EventArgs e)
        {
            // Ensure that we're not already loading data
            if (_viewModel.IsLoading || !_viewModel.HasMoreItems)
                return;

            await _viewModel.LoadMoreBreedsAsync();
        }
    }
}
