using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace TDVS.Common
{
	public class Component
	{
		private bool _Serialize;
		private bool _Editable;

		public Component()
		{
		}

		[XmlIgnore]
		public bool Serialize
		{
			get { return _Serialize; }
			set { _Serialize = value; }
		}
		[XmlAttribute]
		[DefaultValue( true )]
		public bool Editable
		{
			get { return _Editable; }
			set { _Editable = value; }
		}
		[XmlIgnore]
		public Entity Entity { get; set; }

		public virtual void SetEntity( Entity entity )
		{
			Entity = entity;
		}
		public virtual void InitiaizeProperties() { }
		public virtual void Update( GameTime gameTime ) { }
		public virtual void Draw( GameTime gameTime ) { }
		public virtual void Delete() { }
	}
}
