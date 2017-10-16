﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TestProject
{
    class Animation
    {
        // The image representing the collection of images used for animation
        Texture2D _spriteStrip;

        // The scale used to display the sprite strip
        float _scale;

        // The time since we last updated the frame
        int _elapsedTime;

        // The time we display a frame until the next one
        int _frameTime;

        // The number of frames that the animation contains
        int _frameCount;

        // The index of the current frame colone we are displaying
        int _currentFrameLin;

        // The index of the current frame  we are displaying
        int _currentFrameCol;

        // The color of the frame we will be displaying
        Color _color;

        // The area of the image strip we want to display
        Rectangle _sourceRect = new Rectangle();

        // The area where we want to display the image strip in the game
        Rectangle _destinationRect = new Rectangle();

        // Width of a given frame
        int _frameWidth;

        // Height of a given frame
        int _frameHeight;

        // The state of the Animation
        bool _active;

        // Determines if the animation will keep playing or deactivate after one run
        bool _looping;

        // Width of a given frame
        Vector2 _position;

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int currentFrameCol, int currentFrameLin, int frameCount, int frametime, Color color, float scale, bool looping)
        {
            // Keep a local copy of the values passed in
            _color = color;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _frameCount = frameCount;
            _frameTime = frametime;
            _scale = scale;

            _looping = looping;
            _position = position;
            _spriteStrip = texture;

            _currentFrameCol = currentFrameCol;
            _currentFrameLin = currentFrameLin;
            // Set the time to zero
            _elapsedTime = 0;
           

            // Set the Animation to active by default
            _active = true;

        }

        public void Update(GameTime gameTime)
        {
            // Do not update the game if we are not active
            if (_active == false) return;

            // Update the elapsed time
            _elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            // If the elapsed time is larger than the frame time
            // we need to switch frames
            if (_elapsedTime > _frameTime)
            {
                // Move to the next frame
                _currentFrameCol++;

                // If the currentFrame is equal to frameCount reset currentFrame to zero
                if (_currentFrameCol == _frameCount)
                {
                    _currentFrameCol = 0;
                    // If we are not looping deactivate the animation
                    if (_looping == false)
                        _active = false;
                }

                // Reset the elapsed time to zero
                _elapsedTime = 0;
            }

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the Frame width
            _sourceRect = new Rectangle(_currentFrameCol * _frameWidth, _currentFrameLin * _frameHeight, _frameWidth, _frameHeight);

            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            _destinationRect = new Rectangle((int)_position.X - (int)(_frameWidth * _scale) / 2,
            (int)_position.Y - (int)(_frameHeight * _scale) / 2,
            (int)(_frameWidth * _scale),
            (int)(_frameHeight * _scale));

        }

        // Draw the Animation Strip
        public void Draw(SpriteBatch spriteBatch)
        {
            // Only draw the animation when we are active
            if (_active)
            {
                spriteBatch.Draw(_spriteStrip, _destinationRect, _sourceRect, _color);
            }
        }
        internal int FrameHeight
        {
            get { return _frameHeight; }
        }
        internal int FrameWidth
        {
            get { return _frameWidth; }
        }
        internal Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
}
