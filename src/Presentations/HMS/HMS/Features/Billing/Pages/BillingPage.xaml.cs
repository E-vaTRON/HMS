using CommunityToolkit.Maui.Core.Platform;

namespace HMS;

public partial class BillingPage
{
    #region [ Fields ]

    private readonly BillingPageViewModel vm;
    #endregion

    #region [ CTors ]

    public BillingPage(BillingPageViewModel vm)
    {
        InitializeComponent();

        this.vm = vm;
        BindingContext = vm;
    }
    #endregion

    #region [ Event Handlers ]

    private void BillSearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        vm.Items.Clear();

        var result = vm.CachedItems
                        .Where(x => x.Title.ToLower()
                                           .Contains(BillSearchBar.Text.ToLower(),
                                            StringComparison.OrdinalIgnoreCase));

        foreach (var item in result)
        {
            vm.Items.Add(item);
        }

        DoneButton.IsVisible = true;
    }

    private void Search_Clicked(object sender, EventArgs e)
        => BillSearchBar.Focus();

    private void Done_Clicked(object sender, EventArgs e)
    {
        BillSearchBar.HideKeyboardAsync(CancellationToken.None);
        DoneButton.IsVisible = false;
    }
    #endregion
}