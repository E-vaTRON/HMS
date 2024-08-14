using System.Collections.ObjectModel;

namespace HMS;

public class BillingSearchHandler : SearchHandler
{
    #region [CTor]
    public BillingSearchHandler()
    {
        this.Bills = new();
    }
    #endregion

    #region [Bindable Properties]
    public static readonly BindableProperty BillsProperty = BindableProperty.Create(nameof(Bills),
                                                                               typeof(ObservableCollection<BillingPageItemModel>),
                                                                               typeof(BillingSearchHandler),
                                                                               new ObservableCollection<BillingPageItemModel>(),
                                                                               BindingMode.OneWay);
    #endregion

    #region [Properties]
    public ObservableCollection<BillingPageItemModel> Bills
    {
        get => (ObservableCollection<BillingPageItemModel>)GetValue(BillsProperty);
        set => SetValue(BillsProperty, value);
    }
    #endregion

    #region [Delegates]
    public delegate void SelectCardEventHandler(BillingPageItemModel control);
    #endregion

    #region [Event Handlers]
    public event SelectCardEventHandler SelectCard;

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            ItemsSource = Bills.Where(x => x.Title.ToLower().Contains(newValue.ToLower())).ToList();
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);
        var selectedCard = item as BillingPageItemModel;
        SelectCard?.Invoke(selectedCard);
    }

    #endregion
}
