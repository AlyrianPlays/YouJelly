using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YouJelly
{
    public class YouJelly : Game
    {
        public GraphicsDeviceManager _graphics { get; set; }
        public SpriteBatch _spriteBatch { get; set; }
        public Simple testSimple { get; set; }
        public string contentDir { get; set; }
        public Level testLevel { get; set; }
        public Camera testCamera { get; set; }
        public UserSettings testSettings { get; set; }
        public GameState testGS { get; set; }
        public Screen testScreen { get; set; }

        public YouJelly()
        {
            _graphics = new GraphicsDeviceManager(this);
            //FHD
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            //HD, wonky with fullscreen on FHD monitor
            //_graphics.PreferredBackBufferWidth = 1280;
            //_graphics.PreferredBackBufferHeight = 720;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            testScreen = new Screen(GraphicsDevice.Viewport.Bounds);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //Testing Json save
            contentDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + @"\Content\";
            testSimple = new Simple(new List<string>() { "WholeJellyMale_Small" }, 
                                    new Vector2(50,50), 
                                    new List < Animation >(), 
                                    new Physics(),
                                    Content);
            List<Simple> testSimples = new List<Simple>();
            testSimples.Add(testSimple);
            //testLevel = Level.DeSerialize(contentDir + "SaveFile.json", Content);
            testLevel = new Level(testSimples, Content, 1080, 1920);
            testCamera = new Camera(GraphicsDevice.Viewport, testLevel.levelWidth, testLevel.levelHeight, 1.0f);
            testGS = new GameState();
        }

        protected override void Update(GameTime gameTime)
        {
            testGS.Update(this);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatch sb_test = new SpriteBatch(GraphicsDevice);
            sb_test.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, testCamera.GetTransformation());
            foreach(Simple s in testLevel.simples)
            {
                sb_test.Draw(s.currVis, s.position, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
            // TODO: Add your drawing code here

            sb_test.End();
            base.Draw(gameTime);
        }
    }
}
