using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace TDVS.Common
{
	public class Entity
	{
		#region Static
		protected static List<Entity> _EntityList = new List<Entity>();

		protected static String GetNewID()
		{
			return Guid.NewGuid().ToString();
		}
		#endregion

		protected Dictionary<String, Component> _Components = new Dictionary<String, Component>();
		protected Dictionary<String, IProperty> _Properties = new Dictionary<String, IProperty>();
		protected Property<String> _IDProperty;
		private bool _Serialize = true;

		protected Property<String> IDProperty
		{
			get
			{
				if ( _IDProperty == null )
				{

				}
				return _IDProperty;
			}
		}

		public IEnumerable<IProperty> PropertyList
		{
			get { return _Properties.Values; }
		}
		public IEnumerable<Component> ComponentList
		{
			get { return _Components.Values; }
		}
		[XmlIgnore]
		public bool Serialize
		{
			get { return _Serialize; }
			set { _Serialize = value; }
		}
		[XmlIgnore]
		public String ID
		{
			get { return _IDProperty.Value; }
			set { _IDProperty.Value = value; }
		}

		public Entity()
		{
			_EntityList.Add( this );
		}

		public Property<T> GetProperty<T>()
		{
			return _Properties.Values.OfType<Property<T>>().FirstOrDefault();
		}
		public Property<T> GetProperty<T>( String name )
		{
			if ( _Properties.ContainsKey( name ) )
			{
				return _Properties[ name ] as Property<T>;
			}
			return null;
		}
		public T GetComponent<T>() where T : Component
		{
			return _Components.Values.OfType<T>().FirstOrDefault();
		}
		public T GetComponent<T>( String name ) where T : Component
		{
			if ( _Components.ContainsKey( name ) )
				return _Components[ name ] as T;
			return null;
		}
		public void Add( String name, IProperty property )
		{
			_Properties.Add( name, property );
		}
		public void Add( String name, Component component )
		{
			_Components.Add( name, component );
		}
		public void RemoveProperty( String name )
		{
			_Properties.Remove( name );
		}
		public void RemoveComponent( String name )
		{
			_Components.Remove( name );
		}

		public virtual void Update( GameTime gameTime )
		{
			foreach ( var component in _Components.Values )
			{
				component.Update( gameTime );
			}
		}
		public virtual void Draw( GameTime gameTime )
		{
			foreach ( var component in _Components.Values )
			{
				component.Draw( gameTime );
			}
		}

		public void Delete()
		{
			foreach ( var component in _Components.Values )
			{
				component.Delete();
			}
			Entity._EntityList.Remove( this );
		}

		[XmlArray( "Components" )]
		[XmlArrayItem( "Component", Type = typeof( DictionaryEntry ) )]
		public DictionaryEntry[] Components
		{
			get
			{
				IEnumerable<KeyValuePair<String, Component>> serializableComponents =
					_Components.Where( x => x.Value.Serialize );
				DictionaryEntry[] ret = new DictionaryEntry[ serializableComponents.Count() ];
				int i = 0;
				DictionaryEntry de;
				foreach ( KeyValuePair<String, Component> component in serializableComponents )
				{
					de = new DictionaryEntry( component.Key, component.Value );
					ret[ i ] = de;
					i++;
				}
				return ret;
			}
			set
			{
				_Components.Clear();
				for ( int i = 0; i < value.Length; i++ )
				{
					_Components.Add( ( String )value[ i ].Key, ( Component )value[ i ].Value );
				}
			}
		}
		[XmlArray( "Properties" )]
		[XmlArrayItem( "Property", Type = typeof( DictionaryEntry ) )]
		public DictionaryEntry[] Properties
		{
			get
			{
				DictionaryEntry[] ret = new DictionaryEntry[ _Properties.Count ];
				int i = 0;
				DictionaryEntry de;
				foreach ( KeyValuePair<String, IProperty> prop in _Properties )
				{
					de = new DictionaryEntry( prop.Key, prop.Value );
					ret[ i ] = de;
					i++;
				}
				return ret;
			}
			set
			{
				for ( int i = 0; i < value.Length; i++ )
				{
					_Properties.Add( ( String )value[ i ].Key, ( IProperty )value[ i ].Value );
				}
			}
		}
	}
}
