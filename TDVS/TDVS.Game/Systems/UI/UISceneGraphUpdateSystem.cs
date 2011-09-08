using System.Diagnostics;
using TDVS.EntitySystem;
using TDVS.Game.Components;

namespace TDVS.Game.Systems.UI
{
	public class UISceneGraphUpdateSystem : TagSystem
	{
		public UISceneGraphUpdateSystem()
			: base( "UIROOT" )
		{
		}

		public override void Process( Entity e )
		{
			var node = _worldBase.EntityManager.GetComponent<SceneNodeComponent>( e );
			if ( node == null )
				throw new EntitySystemException( "SceneNodeComponent component missing.", GetType() );

			foreach ( var child in node.Children )
			{
				Process( child );
			}

			Update( e, node );
		}

		private void Update( Entity e, SceneNodeComponent node )
		{
			var localTransform = e.GetComponent<LocalTransform2DComponent>();
			var transform = e.GetComponent<Transform2DComponent>();

			Debug.Assert( transform != null, "transform != null" );
			Debug.Assert( localTransform != null, "localTransform != null" );

			if ( node.Parent != null )
			{
				var parentTransform = node.Parent.GetComponent<Transform2DComponent>();
				transform.Transform = parentTransform.Transform + localTransform.Transform;
				transform.Roation = parentTransform.Roation + localTransform.Roation % 360;
			}
			else
			{
				transform.Transform = localTransform.Transform;
			}
		}
	}
}
