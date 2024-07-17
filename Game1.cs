using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace pong
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Vector2 ballPosition;
        float ballSpeed;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 100f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Vector2 ballMovementVector = new Vector2(0f, 0f);
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
            {
                ballMovementVector.Y -= 1f;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                ballMovementVector.Y += 1f;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                ballMovementVector.X -= 1f;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                ballMovementVector.X += 1;
            }
            if (Math.Abs(ballMovementVector.X) > 0 || Math.Abs(ballMovementVector.Y) > 0)
            {
                ballMovementVector.Normalize();
            }

            if (Joystick.LastConnectedIndex == 0)
            {
                Vector2 deadzones = new Vector2(1024, 1024);
                JoystickState jstate = Joystick.GetState(0);
                Vector2 normalisedAxes = new Vector2(jstate.Axes[0] / 32768f, jstate.Axes[1] / 32768f);
                if (Math.Abs(jstate.Axes[1]) > deadzones.Y)
                {
                    ballMovementVector.Y += normalisedAxes.Y;
                }
                if (Math.Abs(jstate.Axes[0]) > deadzones.X)
                {
                    ballMovementVector.X += normalisedAxes.X;
                }
            }
            ballPosition.X += ballMovementVector.X * ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ballPosition.Y += ballMovementVector.Y * ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(ballTexture, ballPosition, null, Color.White, 0f, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}