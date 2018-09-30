using System.ComponentModel;
using XForm.Forms;

namespace XForm.Fields.Interfaces
{
    public interface IField : INotifyPropertyChanged
    {
        Form Form { get; set; }
        
        bool Enabled { get; set; }
        
        bool Hidden { get; set; }
        
        string Title { get; set; }
    }
}