using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TDVS.Common.Extensions;
using TDVS.EntitySystem;
using TDVS.Game.EntitySystems.Components;

namespace TDVS.Game.Systems
{
	class UIRenderSystem : EntityProcessingSystem
	{
		private SpriteBatch _batch;
		private ContentManager _content;
		private SpriteFont _font;

		public UIRenderSystem( SpriteBatch batch, ContentManager content, Type reqType, params Type[] types )
			: base( reqType, types )
		{
			_batch = batch;
			_content = content;

			_font = _content.Load<SpriteFont>( @"Fonts\DefaultMenuFont" );
		}

		public override void Process( Entity e )
		{
			var s = typeof( String ).GetDerivedTypes();
			foreach ( var ss in s )
			{

			}
			var t = _world.EntityManager.GetComponent<Transform2D>( e );
			_batch.DrawString( _font, "Entity pos: {0}:{1}".FormatWidth( t.Transform.X, t.Transform.Y ), t.Transform, Color.Red );
		}
	}
}
