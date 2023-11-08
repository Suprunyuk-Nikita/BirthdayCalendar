using Xamarin.Forms;

namespace BirthdayCalendar.Behaviors
{
    class WhitespaceCheckBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            string text = args.NewTextValue;
            ((Entry)sender).TextColor = !text.Trim().Contains(" ") ? Color.Default : Color.Red;
        }
    }
}
