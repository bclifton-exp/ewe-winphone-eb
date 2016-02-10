using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Suggestions;
using Expedia.Injection;

namespace Expedia.Client.Views
{
    public sealed partial class SearchFlightsView : Page
    {
        public SearchFlightsView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<ISearchFlightsViewModel>();
            this.InitializeComponent();
        }

        private void AutoSuggestBox_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var context = DataContext as SearchFlightsViewModel;
            context.SetSearchSuggestion(args.SelectedItem as SuggestionResult);
        }

        private void AutoSuggestBox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var context = DataContext as SearchFlightsViewModel;
            context.SuggestionTextChanged(sender.Text);
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as AutoSuggestBox;
            box.IsSuggestionListOpen = true;
        }
    }
}
