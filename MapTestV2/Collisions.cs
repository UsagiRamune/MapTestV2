using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace MapTestV2
{
    internal interface Collisions : ICollisionActor
    {
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
