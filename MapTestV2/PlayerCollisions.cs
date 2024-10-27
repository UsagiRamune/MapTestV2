using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace MapTestV2
{
    internal class PlayerCollision : Collisions
    {
        private readonly Game1 _game;
        public int Velocity = 5;
        Vector2 move;
        public IShapeF Bounds { get; }
        private KeyboardState _currentKey;
        private KeyboardState _oldKey;
        public PlayerCollision(Game1 game, IShapeF box)
        {
            _game = game;
            Bounds = box;
        }
        public virtual void Update(GameTime gameTime)
        {
            _currentKey = Keyboard.GetState();
            if (_currentKey.IsKeyDown(Keys.W) || _currentKey.IsKeyDown(Keys.A) || _currentKey.IsKeyDown(Keys.S) || _currentKey.IsKeyDown(Keys.D))
            {
                if (_currentKey.IsKeyDown(Keys.D) && Bounds.Position.X < _game.GetMapWidth() - ((RectangleF)Bounds).Width)
                {
                    move = new Vector2(Velocity, 0) * gameTime.GetElapsedSeconds() * 50;
                    if (_game.GetCameraShiftX() <= -50 && Game1.cameraPos.X < Game1.MapWidth - 640)
                    {
                        _game.UpdateCamera(move);
                    }
                    Bounds.Position += move;
                    Game1.playerPos += move;
                    Game1.pic = 1;


                }
                if (_currentKey.IsKeyDown(Keys.A) && Bounds.Position.X > 0)
                {
                    move = new Vector2(-Velocity, 0) * gameTime.GetElapsedSeconds() * 50;
                    if (_game.GetCameraShiftX() >= 50 && Game1.cameraPos.X > 640)
                    {
                        _game.UpdateCamera(move);
                    }
                    Bounds.Position += move;
                    Game1.playerPos += move;
                    Game1.pic = 3;



                }
                if (_currentKey.IsKeyDown(Keys.W) && Bounds.Position.Y > 0)
                {
                    move = new Vector2(0, -Velocity) * gameTime.GetElapsedSeconds() * 50;
                    if (_game.GetCameraShiftY() >= 50 && Game1.cameraPos.Y > 360)
                    {
                        _game.UpdateCamera(move);
                    }


                    Bounds.Position += move;
                    Game1.playerPos += move;
                    Game1.pic = 2;
                }
                if (_currentKey.IsKeyDown(Keys.S) && Bounds.Position.Y < _game.GetMapHeight() - ((RectangleF)Bounds).Height)
                {
                    move = new Vector2(0, Velocity) * gameTime.GetElapsedSeconds() * 50;
                    if (_game.GetCameraShiftY() <= -50 && Game1.cameraPos.Y < Game1.MapHeight - 360)
                    {
                        _game.UpdateCamera(move);
                    }


                    Bounds.Position += move;
                    Game1.playerPos += move;
                    Game1.pic = 0;
                }
            }

            _oldKey = _currentKey;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3f);
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            if (collisionInfo.Other.ToString().Contains("RPGCollision"))
            {
                Bounds.Position -= collisionInfo.PenetrationVector;
                Game1.playerPos -= collisionInfo.PenetrationVector;
            }
        }
    }
}
