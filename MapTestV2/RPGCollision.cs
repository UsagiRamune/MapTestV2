using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace MapTestV2
{
    public class RPGCollision : Collisions
    {
        private readonly Game1 _game;
        public IShapeF Bounds { get; }
        public RPGCollision(Game1 game, RectangleF rectangleF)
        {
            _game = game;
            Bounds = rectangleF;
        }
        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);
        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
        }
    }
}
