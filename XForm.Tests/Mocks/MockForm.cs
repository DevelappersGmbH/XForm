using XForm.Forms;

namespace XForm.Tests.Mocks
{
    public class MockForm: Form
    {
        public static void Register()
        {
            FormCreateFunc = () => new MockForm();
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