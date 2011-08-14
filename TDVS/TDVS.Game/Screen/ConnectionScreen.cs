using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDVS.Game.Screen
{
    public class ConnectionScreen : GameScreen
    {
        private Vector2 _Center;
        private Vector2 _Origin;
        private string _Connecting = "Connecting...";
        private Vector2 _FontSize;

        private Network.Client.Core _Client = new Network.Client.Core();

        public ConnectionScreen()
        {
            _Client.Initialize();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        // Junk..... //
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.DefaultFont;

            var x = ScreenManager.Game.GraphicsDevice.Viewport.Width / 2.0f;
            var y = ScreenManager.Game.GraphicsDevice.Viewport.Height / 2.0f;
            _FontSize = ScreenManager.DefaultFont.MeasureString(_Connecting);

            _Center = new Vector2(x, y);
            _Origin = new Vector2(_FontSize.X / 2, _FontSize.Y / 2);


            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Connecting...", _Center, Color.Black, 0.0f, _Origin, 1.0f, SpriteEffects.None, 0.0f);

            spriteBatch.End();

        }
    }
}
