using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TestProject
{
    class Entity
    {
        public Vector2 _position;
        bool _active;
        int _health;
        Animation _animation;

        public void Initialize(Animation animation, Vector2 position)
        {
            _animation = animation;
            // Set the starting position of the player around the middle of the screen and to the back
            _position = position;
            // Set the player to be active
            _active = true;
            // Set the player health
            _health = 100;
        }
        public void Update(GameTime gameTime)
        {
            _animation.Position = _position;
            _animation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _animation.Draw(spriteBatch);
        }

        public int Width
        {
            get { return _animation.FrameWidth; }
        }
        public int Height
        {
            get { return _animation.FrameHeight; }
        }
        internal Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
