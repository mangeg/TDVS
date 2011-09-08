namespace TDVS.Common.Resources
{
	public class ResourceID
	{
		private static long _sNextID = 1;

		public static ResourceID Zero;

		public long ID { get; private set; }


		static ResourceID()
		{
			var nextID = _sNextID;
			Zero = new ResourceID { ID = 0 };
			_sNextID = nextID;
		}
		public ResourceID()
		{
			ID = _sNextID++;
		}

		public bool Equals( ResourceID other )
		{
			if ( ReferenceEquals( null, other ) ) return false;
			if ( ReferenceEquals( this, other ) ) return true;
			return other.ID == ID;
		}

		public override bool Equals( object obj )
		{
			if ( ReferenceEquals( null, obj ) ) return false;
			if ( ReferenceEquals( this, obj ) ) return true;
			if ( obj.GetType() != typeof( ResourceID ) ) return false;
			return Equals( ( ResourceID )obj );
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}

		public static bool operator !=( ResourceID a, ResourceID b )
		{
			return !Equals( a, b );
		}
		public static bool operator ==( ResourceID a, ResourceID b )
		{
			return Equals( a, b );
		}
	}
}