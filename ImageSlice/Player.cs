using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ImageSlice
{
    public class Player
    {
        public Texture2D PlayerTexture;
        public Vector2 Position { get; set; }
        public float Depth { get; set; }
        

        public Player(ContentManager content, Vector2 position)
        {
            Position = position;
            PlayerTexture = content.Load<Texture2D>("PlayerSprites\\PlayerDot");
        }


        public Vector2 Origin
        {
            get
            {
                return new Vector2(PlayerTexture.Width / 2, PlayerTexture.Height / 2);
            }
        }


        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            PlayerMovement(keyboardState);
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, Position, new Rectangle(0, 0, PlayerTexture.Width, PlayerTexture.Height), Color.White, 0f, Origin, 1f, SpriteEffects.None, Depth);
        }


        private void PlayerMovement(KeyboardState keyboardState)
        {
            Vector2 direction = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                direction.Y--;
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                direction.Y++;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                direction.X--;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                direction.X++;
            }

            if (direction.Length() > 0.0f)
                direction.Normalize();

            ImposeMovingLimits();
            Position += direction * Config.PLAYER_SPEED;
        }


        private void ImposeMovingLimits()
        {
            var location = Position;
            var playerAreaLimit = new Rectangle(0, 0, Config.SCREEN_WIDTH, Config.SCREEN_HEIGHT);

            if (location.X < (playerAreaLimit.Left + PlayerTexture.Width))
                location.X = playerAreaLimit.Left + PlayerTexture.Width;

            if (location.X > (playerAreaLimit.Right - PlayerTexture.Width))
                location.X = playerAreaLimit.Right - PlayerTexture.Width;

            if (location.Y < (playerAreaLimit.Top + PlayerTexture.Height))
                location.Y = playerAreaLimit.Top + PlayerTexture.Height;

            if (location.Y > (playerAreaLimit.Bottom - PlayerTexture.Height))
                location.Y = (playerAreaLimit.Bottom - PlayerTexture.Height);

            Position = location;
        }
    }
}
