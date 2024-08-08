using PaginationMaui.Models;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public class BreedsViewModel : INotifyPropertyChanged
{
    private const int PageSize = 10; 
    private const int InitialLoadCount = 6; 

    private int _currentPage = 1;
    private bool _isLoading;
    private ObservableCollection<Breed> _breeds;
    private bool _hasMoreItems = true;
    private readonly ApiService _apiService;
    private string _apiUrl = "https://dogapi.dog/api/v2/breeds";

    private string _errorMessage;
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Breed> Breeds
    {
        get => _breeds;
        set
        {
            _breeds = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool HasMoreItems
    {
        get => _hasMoreItems;
        set
        {
            _hasMoreItems = value;
            OnPropertyChanged();
        }
    }

    public BreedsViewModel()
    {
        _apiService = new ApiService();
        Breeds = new ObservableCollection<Breed>();
        LoadInitialBreedsAsync().ConfigureAwait(false);
    }

    private async Task LoadInitialBreedsAsync()
    {
        IsLoading = true;

        try
        {
            // Load the initial set of breeds
            var initialResponse = await _apiService.GetBreedsAsync($"{_apiUrl}?page[number]={_currentPage}&page[size]={InitialLoadCount}");
            if (initialResponse?.Data != null)
            {
                foreach (var breed in initialResponse.Data)
                {
                    Breeds.Add(breed);
                }
                _currentPage++;
                // Set HasMoreItems to false if there are no more items to load initially
                if (initialResponse.Data.Count < InitialLoadCount)
                {
                    HasMoreItems = false;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load breeds: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Error loading breeds: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task LoadMoreBreedsAsync()
    {
        if (IsLoading || !HasMoreItems) return;

        IsLoading = true;

        try
        {
            var url = $"{_apiUrl}?page[number]={_currentPage}&page[size]={PageSize}";
            var response = await _apiService.GetBreedsAsync(url);

            if (response?.Data == null || response.Data.Count < PageSize)
                HasMoreItems = false;

            foreach (var breed in response.Data)
            {
                Breeds.Add(breed);
            }

            _currentPage++;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load breeds: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Error loading breeds: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

