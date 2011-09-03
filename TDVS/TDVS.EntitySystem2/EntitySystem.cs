using System;
using System.Collections.Generic;

namespace TDVS.EntitySystem2
{
	public class EntitySystem
	{
		public long SystemBit { get; set; }
		public long TypeFlags { get; set; }

		protected bool Enabled = true;
		protected World World;

		private readonly Dictionary<int, Entity> _actives = new Dictionary<int, Entity>();


		public EntitySystem() { }

		public EntitySystem( params Type[] types )
		{
			for ( int i = 0, j = types.Length; i < j; i++ )
			{
				var type = types[ i ];
				var componentType = ComponentTypeManager.GetTypeFor( type );
				TypeFlags |= componentType.Bit;
			}
		}

		public virtual void Begin() { }

		public virtual void Process()
		{
			if ( !CheckProcessing() ) 
				return;

			Begin();
			ProcessEntities( _actives );
			End();
		}

		public virtual void End() { }

		public virtual void ProcessEntities( Dictionary<int, Entity> actives ) { }


		public virtual bool CheckProcessing()
		{
			return Enabled;
		}

		public virtual void Initialize() { }

		public virtual void Added( Entity e ) { }

		public virtual void Removed( Entity e ) { }

		public void Change( Entity e )
		{
			var contains = ( SystemBit & e.SystemBits ) == SystemBit;
			var interest = ( TypeFlags & e.TypeBits ) == TypeFlags;

			if ( interest && !contains && TypeFlags > 0 )
			{
				_actives.Add( e.Id, e );
				e.AddSystemBit( SystemBit );
				Added( e );
			}
			else if ( !interest && contains && TypeFlags > 0 )
			{
				Remove( e );
			}
		}

		private void Remove( Entity e )
		{
			_actives.Remove( e.Id );
			e.RemoveSystemBit( SystemBit );
			Removed( e );
		}

		public void SetWorld( World world ) { World = world; }
		public void Toggle() { Enabled = !Enabled; }
		public void Enable() { Enabled = true; }
		public void Disable() { Enabled = false; }

		public static Type[] GetMergedTypes( Type requiredType, params Type[] otherTypes )
		{
			var types = new Type[ 1 + otherTypes.Length ];
			types[ 0 ] = requiredType;
			for ( int i = 0, j = otherTypes.Length; j > i; i++ )
			{
				types[ i + 1 ] = otherTypes[ i ];
			}

			return types;
		}
	}
}
