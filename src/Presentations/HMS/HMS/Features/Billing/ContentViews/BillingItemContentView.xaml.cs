namespace HMS;

public partial class BillingItemContentView : ContentView
{

    #region [ CTor ]


    public BillingItemContentView()
    {
        InitializeComponent();
    }
    #endregion

    #region [ Properties ]

    public static readonly BindableProperty ComponentDataProperty = BindableProperty.Create(
        nameof(ComponentData),
        typeof(BillingPageItemModel),
        typeof(BillingItemContentView),
        default(BillingPageItemModel)
    );
    public BillingPageItemModel ComponentData
    {
        get => (BillingPageItemModel)GetValue(ComponentDataProperty);
        set => SetValue(ComponentDataProperty, value);
    }
    #endregion
}