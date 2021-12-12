using System.ComponentModel;

namespace GroupProject.Model
{
    /// <summary>
    /// Class to act as a view model
    /// </summary>
    public class ItemViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// PropertyChangedEventHandler if properties are changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Triggered when properties change, causing a re-render of component
        /// </summary>
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// The id of the item used for updates
        /// </summary>
        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }

        /// <summary>
        /// The Description of the item
        /// </summary>
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        /// <summary>
        /// The Price of the item
        /// </summary>
        private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
    }
}