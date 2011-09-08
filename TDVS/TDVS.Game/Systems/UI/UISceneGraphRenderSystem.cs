using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDVS.Common.Resources;
using TDVS.EntitySystem;
using TDVS.Game.Components;
using TDVS.Game.Components.UI;

namespace TDVS.Game.Systems.UI
{
	public class UISceneGraphRenderSystem : TagSystem
	{
		private const float EPSILON = float.Epsilon;
		private readonly SpriteBatch _batch;
		private GraphicsDevice _device;

		public UISceneGraphRenderSystem( SpriteBatch batch, GraphicsDevice device )
			: base( "UIROOT" )
		{
			_device = device;
			_batch = batch;
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
			var transform = e.GetComponent<Transform2DComponent>();
			var sprite = e.GetComponent<SpriteComponent>();
			var border = e.GetComponent<UIBorderComponent>();
			var colorComponent = e.GetComponent<ColorComponent>();

			if ( sprite != null )
			{
				var texture = ResourceManager.Get<Texture2D>( sprite.TextureID );
				if ( texture != null )
				{
					if ( sprite.UseRect )
					{
						_batch.Draw( texture, transform.Transform, sprite.Rect, sprite.Color );
					}
					else
					{
						_batch.Draw( texture, transform.Transform, sprite.Color );
					}
				}
			}

			if ( border != null )
			{
				DrawBorder( colorComponent, border, transform );
			}
		}

		private void DrawBorder( ColorComponent colorComponent, UIBorderComponent border, Transform2DComponent transform )
		{
			var texture = ResourceManager.Get<Texture2D>( border.TextureID );
			Debug.Assert( texture.Width == texture.Height, "texture.Width == texture.Height" );

			var sections = border.Sections;
			var color = colorComponent != null ? colorComponent.Color : Color.White;
			var size = texture.Width;
			var scale = transform.Scale;
			var x = transform.Transform.X;
			var y = transform.Transform.Y;
			var iX = ( int )x;
			var iY = ( int )y;
			var h = border.Height;
			var w = border.Width;
			var trd = size / 3;
			//var rest = size % 3;
			var section = ( int )( trd * scale );
			//var origin = new Vector2( w / 2, h / 2 );
			//var rect = new Rectangle( ( int )x, ( int )y, w, h );

			var cl = ( int )( ( w - trd * 2 ) * scale );
			var ch = ( int )( ( h - trd * 2 ) * scale );

			//	----------------------------------------------
			//	| iX	  | iX + section |  iX + section * 2 |  Position
			//	----------------------------------------------
			//	| section |	iX + section |	iX + section * 2 |
			//	| + ch	  |	iY + section |	iY + section	 |
			//	----------------------------------------------
			//	| section | section + cl |  section			 |  Width
			//	----------------------------------------------
			//		Height

			// Do stuff for rotating. (render to render target first and then roate the resulting texture).
			if ( Math.Abs( transform.Roation - 0 ) > EPSILON )
			{
				/*
					RenderTarget2D renderTarget = new RenderTarget2D( _device, ( int )( w * scale ), ( int )( h * scale ), true, _device.DisplayMode.Format, DepthFormat.Depth24 );
					Texture2D tmpTex;

					_device.SetRenderTarget( renderTarget );
					_device.Clear( Color.DarkBlue );

					using ( var tempBatch = new SpriteBatch( _device ) )
					{
						tempBatch.Begin();
						tempBatch.Draw( border, new Rectangle( x, y, ( int )( trd * scale ), ( int )( trd * scale ) ),
										  new Rectangle( 0, 0, trd, trd ), c );
						tempBatch.Draw( border, new Rectangle( ( int )( x + trd * scale ), y, ( int )cl, ( int )( trd * scale ) ),
										  new Rectangle( trd, 0, trd, trd ), c );
						tempBatch.Draw( border,
										  new Rectangle( ( int )( x + trd * scale + cl ), y, ( int )( trd * scale ), ( int )( trd * scale ) ),
										  new Rectangle( trd * 2 + rest, 0, trd, trd ), c );

						tempBatch.Draw( border, new Rectangle( x, ( int )( y + trd * scale ), ( int )( trd * scale ), ( int )ch ),
										  new Rectangle( 0, trd, trd, trd ), c );
						tempBatch.Draw( border,
										  new Rectangle( ( int )( x + trd * scale + cl ), ( int )( y + trd * scale ), ( int )( trd * scale ),
														 ( int )ch ), new Rectangle( trd * 2 + rest, trd, trd, trd ), c );

						tempBatch.Draw( border,
										  new Rectangle( x, ( int )( y + trd * scale + ch ), ( int )( trd * scale ), ( int )( trd * scale ) ),
										  new Rectangle( 0, trd * 2 + rest, trd, trd ), c );
						tempBatch.Draw( border,
										  new Rectangle( ( int )( x + trd * scale ), ( int )( y + trd * scale + ch ), ( int )cl,
														 ( int )( trd * scale ) ), new Rectangle( trd, trd * 2 + rest, trd, trd ), c );
						tempBatch.Draw( border,
										  new Rectangle( ( int )( x + trd * scale + cl ), ( int )( y + trd * scale + ch ),
														 ( int )( trd * scale ), ( int )( trd * scale ) ),
										  new Rectangle( trd * 2 + rest, trd * 2 + rest, trd, trd ), c );
						tempBatch.End();
					}
					_device.SetRenderTarget( null );
					tmpTex = ( Texture2D )renderTarget;
					*/
			}
			else
			{
				// Corners
				if ( ( sections & BorderSections.TopLeft ) == BorderSections.TopLeft )
					_batch.Draw( texture, new Rectangle( iX, iY, section, section ), new Rectangle( 0, 0, trd, trd ), color );
				if ( ( sections & BorderSections.TopRight ) == BorderSections.TopRight )
					_batch.Draw( texture, new Rectangle( iX + section + cl, iY, section, section ), new Rectangle( trd * 2, 0, trd, trd ), color );
				if ( ( sections & BorderSections.BottomLeft ) == BorderSections.BottomLeft )
					_batch.Draw( texture, new Rectangle( iX, iY + section + ch, section, section ), new Rectangle( 0, trd * 2, trd, trd ), color );
				if ( ( sections & BorderSections.BottomRight ) == BorderSections.BottomRight )
					_batch.Draw( texture, new Rectangle( iX + section + cl, iY + section + ch, section, section ), new Rectangle( trd * 2, trd * 2, trd, trd ), color );

				// Sides
				if ( ( sections & BorderSections.Top ) == BorderSections.Top )
					_batch.Draw( texture, new Rectangle( iX + section, iY, cl, section ), new Rectangle( trd, 0, trd, trd ), color );
				if ( ( sections & BorderSections.Bottom ) == BorderSections.Bottom )
					_batch.Draw( texture, new Rectangle( iX + section, iY + section + ch, cl, section ), new Rectangle( trd, trd * 2, trd, trd ), color );
				if ( ( sections & BorderSections.Left ) == BorderSections.Left )
					_batch.Draw( texture, new Rectangle( iX, iY + section, section, ch ), new Rectangle( 0, trd, trd, trd ), color );
				if ( ( sections & BorderSections.Right ) == BorderSections.Right )
					_batch.Draw( texture, new Rectangle( iX + section + cl, iY + section, section, ch ), new Rectangle( trd * 2, trd, trd, trd ), color );
			}
		}
	}
}
