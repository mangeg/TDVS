using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TDVS.Common.Settings.Targets
{
	/// <summary>
	/// XmlTarget for settings.
	/// </summary>
	/// <typeparam name="T">Type of the settings that this XmlTarget handles.</typeparam>
	public class XmlTarget<T> : ITarget where T : SettingsBase
	{
		private readonly XmlSerializer _xmls;
		private object _value;
		private String _fileName;
		private String _folderPath;

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		object ITarget.Value
		{
			get { return _value; }
			set { _value = value; }
		}
		public String FileName
		{
			get { return _fileName; }
			set
			{
				if ( String.IsNullOrEmpty( value ) )
					throw new ArgumentNullException( "FileName", "FileName cannot be empty or null" );
				_fileName = Path.GetFullPath( value );
				_folderPath = Path.GetDirectoryName( _fileName );
			}
		}
		public bool CreatePathIfMissing { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlTarget&lt;T&gt;"/> class.
		/// </summary>
		public XmlTarget()
		{
			_xmls = new XmlSerializer( typeof( T ) );
		}

		/// <summary>
		/// Loads the settings.
		/// </summary>
		void ITarget.Load()
		{
			if ( !File.Exists( FileName ) )
			{
				_value = ( T )Activator.CreateInstance( typeof( T ), true );
				return;
			}
			using ( var fs = File.OpenRead( FileName ) )
			{
				using ( var xr = XmlReader.Create( fs ) )
				{
					if ( _xmls.CanDeserialize( xr ) )
					{
						_value = _xmls.Deserialize( xr );
					}
				}
			}
		}
		/// <summary>
		/// Saves the settings.
		/// </summary>
		void ITarget.Save()
		{
			if ( !Directory.Exists( _folderPath ) && CreatePathIfMissing )
				Directory.CreateDirectory( _folderPath );

			var s = new XmlWriterSettings { Indent = true, IndentChars = "\t" };
			using ( var fs = new FileStream( FileName, FileMode.Create, FileAccess.Write ) )
			{
				using ( var xw = XmlWriter.Create( fs, s ) )
				{
					_xmls.Serialize( xw, _value );
				}
			}
		}
	}
}