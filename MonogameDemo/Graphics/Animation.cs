using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameDemo.Graphics
{
    /// <summary>
    /// A single frame of an animation.
    /// </summary>
    public struct Frame
    {
        /// <summary>
        /// The area on the image that this frame will sample from.
        /// </summary>
        public Rectangle Area;
        /// <summary>
        /// The length of time, in game frames, that this frame of the animation will persist for.
        /// </summary>
        public int Time;

        public Frame(Rectangle sampleArea, int frameTime)
        {
            Area = sampleArea;
            Time = frameTime;
        }
    }

    public class Animation
    {
        /*
         What is an animation
            -Series of Rectangles in an image
            -Seperated by times
            -The image itself
            -Position on the screen
            -Index of current frame
            -Loop + Paused booleans
         */
        public Vector2 Position;
        public bool Paused;
        public bool Loop;

        Frame[] frames;
        int frameIndex;
        int timeLeft;
        Texture2D texture;
         /*
         Needed functionality:
            Display a frame for a period of time, then move on to the next one
            Ability to pause the animation
            Automatic looping (optionally)
            Different timings per frame
         */
        public Animation(Texture2D texture, Frame[] frames, Vector2 position = new Vector2(), bool loop = true, bool paused = false, int startIndex = 0)
        {
            this.texture = texture;
            this.frames = frames;
            Position = position;
            Loop = loop;
            Paused = paused;
            frameIndex = startIndex;
            timeLeft = frames[startIndex].Time;
        }

        public void Update()
        {
            if (!Paused)
            {
                if (timeLeft == 0)
                {
                    if (Loop || frameIndex != frames.Length - 1)
                    {
                        frameIndex = (frameIndex + 1) % frames.Length;
                        timeLeft = frames[frameIndex].Time;
                    }
                }
                timeLeft--;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, Position, frames[frameIndex].Area, Color.White);
        }
    }
}
