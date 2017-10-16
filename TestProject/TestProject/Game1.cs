using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Player _player;
        KeyboardState _currentKeyboardState;
        KeyboardState _previousKeyboardState;
        float _playerMoveSpeed;
        // Image used to display the static background
        Texture2D _mainBackground;
        Rectangle _rectBackground;
        ParallaxingBackground _bgLayer1;
        ParallaxingBackground _bgLayer2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _player = new Player();
            //Background
            _bgLayer1 = new ParallaxingBackground();
            _bgLayer2 = new ParallaxingBackground();
            _playerMoveSpeed = 8.0f;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Load the player resources

            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Bear");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 32, 32, 0, 3, 4, 300, Color.White, 2f, true);
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            _player.Initialize(playerAnimation, playerPosition);
            // Load the parallaxing background
            _bgLayer1.Initialize(Content, "Area4Capsules4", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            _bgLayer2.Initialize(Content, "Area4PurpleBushColumn", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);

            _mainBackground = Content.Load<Texture2D>("Area4PurpleBushColumn");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void PlayerUpdate(GameTime gameTime)
        {
            _player.Update(gameTime);
            if (_currentKeyboardState.IsKeyDown(Keys.Left))
            {
                _player._position.X -= _playerMoveSpeed;
            }
            if (_currentKeyboardState.IsKeyDown(Keys.Right))
            {
                _player._position.X += _playerMoveSpeed;
            }
            if (_currentKeyboardState.IsKeyDown(Keys.Up))
            {
                _player._position.Y -= _playerMoveSpeed;
            }
            if (_currentKeyboardState.IsKeyDown(Keys.Down))
            {
                _player._position.Y += _playerMoveSpeed;
            }
            _player._position.X = MathHelper.Clamp(_player._position.X, 1, GraphicsDevice.Viewport.Width - _player.Width);

            _player._position.Y = MathHelper.Clamp(_player._position.Y, 1, GraphicsDevice.Viewport.Height - _player.Height);
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Save the previous state of the keyboard  so we can determine single key presses
            _previousKeyboardState = _currentKeyboardState;
            
            // Read the current state of the keyboard and gamepad and store it
            _currentKeyboardState = Keyboard.GetState();

            //Update the player
            PlayerUpdate(gameTime);

            // Update the parallaxing background
            _bgLayer1.Update(gameTime);
            _bgLayer2.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _bgLayer2.Draw(_spriteBatch);
            _bgLayer1.Draw(_spriteBatch);

            _player.Draw(_spriteBatch);

            //Draw the Main Background Texture
            _spriteBatch.Draw(_mainBackground, _rectBackground, Color.White);

            // Draw the moving background
            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
