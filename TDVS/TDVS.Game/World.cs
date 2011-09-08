using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TDVS.Common.Input;
using TDVS.Common.Resources;
using TDVS.Common.Utils;
using TDVS.EntitySystem;
using TDVS.Game.Components;
using TDVS.Game.Components.UI;
using TDVS.Game.Systems.UI;

namespace TDVS.Game
{
	public class World : WorldBase
	{
		private readonly Microsoft.Xna.Framework.Game _game;
		private GraphicsDevice _device;
		public SpriteBatch SpriteBatch { get; private set; }

		private EntitySystem.EntitySystem UISceneGraphUpdateSystem { get; set; }
		private EntitySystem.EntitySystem UISceneGraphRenderSystem { get; set; }

		public World( Microsoft.Xna.Framework.Game game )
		{
			_game = game;
		}

		public override void Initialize()
		{
			_device = _game.GraphicsDevice;
		}

		public override void LoadResource()
		{
			SpriteBatch = new SpriteBatch( _game.GraphicsDevice );

			UISceneGraphUpdateSystem = SystemManager.SetSystem( new UISceneGraphUpdateSystem() );
			UISceneGraphRenderSystem = SystemManager.SetSystem( new UISceneGraphRenderSystem( SpriteBatch, _device ) );

			var root = EntityManager.Create();
			root.SetTag( "UIROOT" );
			var rootNode = ( SceneNodeComponent )root.AddComponent( new SceneNodeComponent() );
			root.AddComponent( new Transform2DComponent() );
			root.AddComponent( new LocalTransform2DComponent() );
			root.Refresh();

			var e = EntityManager.Create();
			e.AddComponent( new SceneNodeComponent() { Parent = root } );
			e.AddComponent( new Transform2DComponent() );
			e.AddComponent( new LocalTransform2DComponent() { Transform = new Vector2( 30, 100 ) } );
			e.AddComponent( new ColorComponent() );
			var border = ( UIBorderComponent )e.AddComponent( new UIBorderComponent() );
			border.TextureID = ResourceManager.GetID<Texture2D>( @"Textures\Borders\Serenity" );
			border.Height = 450;
			border.Width = 200;
			rootNode.Children.Add( e );

			e = EntityManager.Create();
			e.AddComponent( new SceneNodeComponent() { Parent = root } );
			e.AddComponent( new Transform2DComponent() );
			e.AddComponent( new LocalTransform2DComponent() { Transform = new Vector2( 300, 100 ) } );
			e.AddComponent( new ColorComponent() );
			border = ( UIBorderComponent )e.AddComponent( new UIBorderComponent() );
			border.TextureID = ResourceManager.GetID<Texture2D>( @"Textures\Borders\Sion" );
			border.Height = 250;
			border.Width = 100;
			rootNode.Children.Add( e );
			e.Refresh();

			e = EntityManager.Create();
			e.AddComponent( new SceneNodeComponent() { Parent = root } );
			e.AddComponent( new Transform2DComponent() );
			e.AddComponent( new LocalTransform2DComponent() { Transform = new Vector2( 300, 370 ) } );
			e.AddComponent( new ColorComponent() );
			border = ( UIBorderComponent )e.AddComponent( new UIBorderComponent() );
			border.TextureID = ResourceManager.GetID<Texture2D>( @"Textures\Borders\Renaitre_Beveled" );
			border.Height = 150;
			border.Width = 300;
			rootNode.Children.Add( e );
			e.Refresh();

			e = EntityManager.Create();
			e.AddComponent( new SceneNodeComponent() { Parent = root } );
			e.AddComponent( new Transform2DComponent() );
			e.AddComponent( new LocalTransform2DComponent() { Transform = new Vector2( 420, 100 ) } );
			e.AddComponent( new ColorComponent() );
			border = ( UIBorderComponent )e.AddComponent( new UIBorderComponent() );
			border.TextureID = ResourceManager.GetID<Texture2D>( @"Textures\Borders\slim_highlight" );
			border.Height = 250;
			border.Width = 300;
			rootNode.Children.Add( e );
			e.Refresh();
		}

		public override void UnloadResources()
		{
		}

		public override void Update( GameTime gameTime )
		{
			InputManager.Update();
			FpsMeter.SUpdate( gameTime );
			UISceneGraphUpdateSystem.Process();
		}

		public override void Draw( GameTime gameTime )
		{
			_device.Clear( Color.DarkBlue );
			SpriteBatch.Begin();

			UISceneGraphRenderSystem.Process();

			SpriteBatch.DrawString( ResourceManager.Get<SpriteFont>( @"Fonts\DefaultBold" ), 
				"FPS: " + ( FpsMeter.sFPS ), new Vector2( 10, 10 ), Color.Green, 0, Vector2.Zero, 1f, SpriteEffects.None, 1 );
			SpriteBatch.DrawString( ResourceManager.Get<SpriteFont>( @"Fonts\DefaultBold" ), 
				"MS/f: " + ( gameTime.ElapsedGameTime.TotalMilliseconds ), new Vector2( 10, 10 + ResourceManager.Get<SpriteFont>( @"Fonts\DefaultBold" ).LineSpacing * 0.8f ), Color.Green, 0, Vector2.Zero, 1f, SpriteEffects.None, 1 );


			SpriteBatch.End();
		}

		/*
public static Texture2D CreateRectangleTexture(GraphicsDevice graphicsDevice, int width, int height,  
                                                       int borderWidth, int borderInnerTransitionWidth,  
                                                       int borderOuterTransitionWidth, Color color, Color borderColor)  
        {  
            Texture2D texture2D = new Texture2D(graphicsDevice, width, height, true, SurfaceFormat.Color);  
 
            int y = -1;  
            int j;  
            int count = width * height;  
            Color[] colorArray = new Color[count];  
            Color[] shellColor = new Color[borderWidth + borderOuterTransitionWidth + borderInnerTransitionWidth];  
            float transitionAmount;  
 
            for (j = 0; j < borderOuterTransitionWidth; j++)  
            {  
                transitionAmount = (j) / (float)(borderOuterTransitionWidth);  
                shellColor[j] = new Color(borderColor.R, borderColor.G, borderColor.B, (byte)(255 * transitionAmount));  
            }  
            for (j = borderOuterTransitionWidth; j < borderWidth + borderOuterTransitionWidth; j++)  
            {  
                shellColor[j] = borderColor;  
            }  
            for (j = borderWidth + borderOuterTransitionWidth;  
                 j < borderWidth + borderOuterTransitionWidth + borderInnerTransitionWidth;  
                 j++)  
            {  
                transitionAmount = 1 -  
                                   (j - (borderWidth + borderOuterTransitionWidth) + 1) /  
                                   (float)(borderInnerTransitionWidth + 1);  
                shellColor[j] = new Color((byte)MathHelper.Lerp(color.R, borderColor.R, transitionAmount),  
                                          (byte)MathHelper.Lerp(color.G, borderColor.G, transitionAmount),  
                                          (byte)MathHelper.Lerp(color.B, borderColor.B, transitionAmount));  
            }  
 
 
            for (int i = 0; i < count; i++)  
            {  
                if (i % width == 0)  
                {  
                    y += 1;  
                }  
                int x = i % width;  
 
                //check if pixel is in one of the rectangular border shells  
                bool isInShell = false;  
                for (int k = 0; k < shellColor.Length; k++)  
                {  
                    if (InShell(x, y, width, height, k))  
                    {  
                        colorArray[i] = shellColor[k];  
                        isInShell = true;  
                        break;  
                    }  
                }  
                //pixel is not in shell so it is in the center  
                if (!isInShell)  
                {  
                    colorArray[i] = color;  
                }  
            }  
 
            texture2D.SetData(colorArray);  
            return texture2D;  
        } */
	}
}
