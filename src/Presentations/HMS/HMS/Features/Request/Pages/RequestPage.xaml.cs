using System.Collections.Generic;

namespace HMS;

public partial class RequestPage
{
    #region [ Fields ]
    private readonly RequestPageViewModel vm;
    #endregion

    #region [ CTors ]
    public RequestPage(RequestPageViewModel vm)
	{
		InitializeComponent();

        this.vm = vm;
        BindingContext = vm;

    }
    #endregion

    #region [ Event Handler ]
    private void SymptomDataCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collectionView)
        {
            if (e.PreviousSelection.Count > e.CurrentSelection.Count)
            {
                foreach (SymptomModel deselectedSymptom in e.PreviousSelection.Except(e.CurrentSelection))
                {
                    vm.SelectedSymptoms.Remove(deselectedSymptom);
                }
            }
            else 
            {
                foreach (SymptomModel selectedSymptom in e.CurrentSelection)
                {
                    if (!vm.SelectedSymptoms.Contains(selectedSymptom))
                    {
                        vm.SelectedSymptoms.Add(selectedSymptom);
                    }
                }
            }
        }
    }
    #endregion
}