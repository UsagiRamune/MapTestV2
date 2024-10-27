using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.VisualBasic;

namespace MapTestV2
{
    public class Game1 : Game
    {
        public static Vector2 playerPos;
        private GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;
        TiledMapObjectLayer rpgTiledObj;
        public const int MapWidth = 21600;
        public const int MapHeight = 21600;
        TiledMap _tiledMap;
        //private TiledMapTileLayer layer4;
        public static int pic;
        public static OrthographicCamera _camera;
        public static Vector2 camerashift;
        public static Vector2 cameraPos;
        TiledMapRenderer _tiledMapRenderer;
        private readonly List<Collisions> _entities = new List<Collisions>();
        public readonly CollisionComponent _collisionComponent;
        Texture2D player, black;
        //private MouseState currentMouseState;
        //private MouseState previousMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, MapWidth, MapHeight));
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1920; // Set the width of window
            _graphics.PreferredBackBufferHeight = 1080; // Set the height of window
            _graphics.ApplyChanges();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1280, 720);
            var transformMatrix = viewportadapter.GetScaleMatrix();
            _camera = new OrthographicCamera(viewportadapter);

            playerPos = new Vector2(1728, 7920);
            //playerPos = new Vector2(2808, 2808);
            cameraPos = new Vector2(playerPos.X, playerPos.Y);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("ShamanMap");
            player = Content.Load<Texture2D>("MC2");
            black = Content.Load<Texture2D>("black1x1");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //_spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            SamplerState samplerState = SamplerState.PointClamp;
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "ObjectHitbox")
                {
                    rpgTiledObj = layer;
                }
            }
            //Create entities from map
            foreach (TiledMapObject obj in rpgTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new RPGCollision(this, new RectangleF(position, obj.Size)));
            }
            _entities.Add(new PlayerCollision(this, new RectangleF(new Vector2(playerPos.X - 18 + 4, playerPos.Y + 18), new Size2(28, 18))));
            foreach (Collisions entity in _entities)
            {
                _collisionComponent.Insert(entity);
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (Collisions entity in _entities)
            {
                entity.Update(gameTime);
            }
            _collisionComponent.Update(gameTime);
            // TODO: Add your update logic here
            _tiledMapRenderer.Update(gameTime);
            _camera.LookAt(cameraPos);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            RasterizerState rasterizerState = RasterizerState.CullNone;
            var transformMatrix = _camera.GetViewMatrix();
            GraphicsDevice.RasterizerState = new RasterizerState { ScissorTestEnable = true };

            // TODO: Add your drawing code here
            _tiledMapRenderer.Draw(0, transformMatrix);
            _tiledMapRenderer.Draw(1, transformMatrix);
            _tiledMapRenderer.Draw(2, transformMatrix);
            _tiledMapRenderer.Draw(3, transformMatrix);
            _tiledMapRenderer.Draw(4, transformMatrix);
            _tiledMapRenderer.Draw(5, transformMatrix);

            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone);
            _spriteBatch.Draw(player, new Vector2(playerPos.X - 36, playerPos.Y - 72), new Rectangle(pic * 36, 0, 36, 72), Color.White, 0, Vector2.Zero, new Vector2(2, 2), 0, 0.2f);
            foreach (Collisions entity in _entities)
            {
                entity.Draw(_spriteBatch);
            }

            _spriteBatch.Draw(black, playerPos, Color.White);
            Console.WriteLine(camerashift);
            _spriteBatch.End();

            _tiledMapRenderer.Draw(6, transformMatrix);
            _tiledMapRenderer.Draw(7, transformMatrix);
            _tiledMapRenderer.Draw(8, transformMatrix);
            _tiledMapRenderer.Draw(9, transformMatrix);
            _tiledMapRenderer.Draw(10, transformMatrix);
            _tiledMapRenderer.Draw(11, transformMatrix);
            _tiledMapRenderer.Draw(12, transformMatrix);
            _tiledMapRenderer.Draw(13, transformMatrix);
            _tiledMapRenderer.Draw(14, transformMatrix);

            base.Draw(gameTime);
        }

        public int GetMapWidth()
        {
            return MapWidth;
        }
        public int GetMapHeight()
        {
            return MapHeight;
        }
        public void UpdateCamera(Vector2 move)
        {
            cameraPos += move;
        }
        public float GetCameraShiftX()
        {
            return camerashift.X = cameraPos.X - playerPos.X;
        }
        public float GetCameraShiftY()
        {
            return camerashift.Y = cameraPos.Y - playerPos.Y;
        }
    }
}
