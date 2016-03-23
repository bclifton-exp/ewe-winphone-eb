using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Expedia.Entities.Suggestions;

namespace Expedia.Styles.Templates
{
    public class SuggestionIconTemplateSelector : DataTemplateSelectorExtension
    {
        public DataTemplate MultiCity { get; set; }
        public DataTemplate Neighborhood { get; set; }
        public DataTemplate Default { get; set; }
        public DataTemplate Hotel { get; set; }
        public DataTemplate Airport { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var suggestion = item as SuggestionResult;

            if (suggestion == null) return Default;

            if (suggestion.Type == SuggestionType.Neighborhood && suggestion.IsLinkedToCity)
            {
                return Neighborhood;
            }

            if (suggestion.Type == SuggestionType.Multicity)
            {
                return MultiCity;
            }

            if (suggestion.Type == SuggestionType.Hotel)
            {
                return Hotel;
            }

            if (suggestion.Type == SuggestionType.Airport)
            {
                return Airport;
            }


            return Default;
        }
    }

    public abstract class DataTemplateSelectorExtension : ContentControl
    {
        public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return null;
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            ContentTemplate = SelectTemplate(newContent, this);
        }
    }
}
