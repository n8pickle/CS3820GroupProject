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
			if(PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		/// <summary>
		/// The id of the item used for updates
		/// </summary>
		public int Id {get; set;}

		/// <summary>
		/// The Name of the item
		/// </summary>
		private string _name;
		public string Name 
		{
			get { return _name; }
			set {
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		/// <summary>
		/// The Description of the item
		/// </summary>
		private string _description;
		public string Description 
		{
			get { return _description; }
			set {
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
			set {
				_price = value;
				OnPropertyChanged(nameof(Price));
			}
		}
	}	
}