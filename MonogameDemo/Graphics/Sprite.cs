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
    /// This class is essentially designed to take all of the inputs to SpriteBatch.Draw() and package them up into an object that will
    /// keep track of things for us.
    /// </summary>
    class Sprite
    {
        /// <summary>
        /// The texture to draw
        /// </summary>
        Texture2D texture;
        /// <summary>
        /// An area within the texture to draw to the screen (potentially the entire are of the texture)
        /// </summary>
        Rectangle sampleArea;
        /// <summary>
        /// The color to tint the texture
        /// </summary>
        public Color Color;
        /// <summary>
        /// The position that the top left corner of the sampleArea will be drawn to
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// The location on the sampleArea that the texture will rotate around if Rotation is not zero.  Defaults to the top left corner.
        /// </summary>
        public Vector2 Origin;
        /// <summary>
        /// The degree to which the sprite is rotated, probably in radians
        /// </summary>
        public float Rotation;
        /// <summary>
        /// How the sampleArea is scaled on the x and y axes.  (1, 1) is normal, (2, 2) would be double sized, etc.
        /// </summary>
        public Vector2 Scale;
        /// <summary>
        /// Whether or not the sprite is flipped vertically or horizontally.
        /// </summary>
        public SpriteEffects Effect;
        /// <summary>
        /// Normally when draw is called multiple times, the newer calls will be drawn over top of the old ones.  This allows you to override that.
        /// </summary>
        public float LayerDepth;

        public Sprite(Texture2D texture, Rectangle? sampleArea = null, Color? color = null, Vector2 position = new Vector2(), 
            Vector2 origin = new Vector2(), float rotation = 0, Vector2? scale = null, SpriteEffects effect = SpriteEffects.None, float layerDepth = 0)
        {
            this.texture = texture;
            if (sampleArea.HasValue)
                this.sampleArea = sampleArea.Value;
            else
                this.sampleArea = new Rectangle(0, 0, texture.Width, texture.Height);

            if (color.HasValue)
                this.Color = color.Value;
            else
                this.Color = Color.White;

            Position = position;
            Origin = origin;
            Rotation = rotation;

            if (scale.HasValue)
                Scale = scale.Value;
            else
                Scale = Vector2.One;

            Effect = effect;
            LayerDepth = layerDepth;
        }

        //Draw arguments: texture, position, sourceArea, color, origin, scale, effects, layerDepth
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, Position, sampleArea, Color, Rotation, Origin, Scale, Effect, LayerDepth);
        }
    }
}
