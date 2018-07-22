using XForm.Fields.Bases;

namespace XForm.Tests.Mocks
{
    public class MockField : Field
    {
        public MockField(string title) : base(title)
        {
        }
        
        public int EnabledChangedCalledCount { get; private set; }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName)
            {
                case nameof(Enabled):
                    EnabledChangedCalledCount += 1;
                    break;
                default:
                    return;
            }
        }
    }
}