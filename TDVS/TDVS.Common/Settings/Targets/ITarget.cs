namespace TDVS.Common.Settings.Targets
{
	/// <summary>
	/// Interface for settings targets.
	/// </summary>
	public interface ITarget
	{
		/// <summary>
		/// Loads the settings.
		/// </summary>
		void Load();
		/// <summary>
		/// Saves the settings.
		/// </summary>
		void Save();
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		object Value { get; set; }
	}
}