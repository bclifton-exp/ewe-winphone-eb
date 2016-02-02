using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Expedia.Client.Interfaces;
using Expedia.Client.ViewModels;
using Expedia.Entities.Suggestions;
using Expedia.Injection;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Expedia.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchHotelsView : Page
    {
        public SearchHotelsView()
        {
            this.DataContext = ExpediaKernel.Instance().Get<ISearchHotelsViewModel>();
            this.InitializeComponent();
        }

        private void AutoSuggestBox_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var context = DataContext as SearchHotelsViewModel;
            context.SetSearchSuggestion(args.SelectedItem as SuggestionResult);
        }

        private void AutoSuggestBox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var context = DataContext as SearchHotelsViewModel;
            context.SuggestionTextChanged(sender.Text);
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as AutoSuggestBox;
            box.IsSuggestionListOpen = true;
        }
    }
}
