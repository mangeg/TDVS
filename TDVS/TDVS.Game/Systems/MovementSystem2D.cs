using TDVS.EntitySystem;
using TDVS.Game.EntitySystems.Components;

namespace TDVS.Game.Systems
{
	class MovementSystem2D : EntityProcessingSystem
	{
		public MovementSystem2D() :
			base( typeof( Transform2D ), typeof( Velocity2D ) )
		{
		}

		public override void Process( Entity e )
		{
			var t = _world.EntityManager.GetComponent<Transform2D>( e );
			var v = _world.EntityManager.GetComponent<Velocity2D>( e );

			t.Transform += v.Direction * v.Speed * 0.01f;
		}
	}
}
