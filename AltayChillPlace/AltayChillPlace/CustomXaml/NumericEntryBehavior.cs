using System.Linq;
using Xamarin.Forms;

public class NumericEntryBehavior : Behavior<Entry>
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

    private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
    {
        if (!string.IsNullOrWhiteSpace(args.NewTextValue))
        {
            bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x));

            if (!isValid || args.NewTextValue.Length > 11) // Проверка наличия нецифровых символов или превышения длины
            {
                // Проверка, нужно ли восстановить предыдущее значение, только если текущее значение отличается от предыдущего
                if (args.NewTextValue != args.OldTextValue)
                {
                    ((Entry)sender).Text = args.OldTextValue;
                }
            }
        }
    }
}
