using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonogameDemo.Input
{
    public class Button
    {
        /*
          A button has:
            -Texture (Optionally?)
            -Array of texture sample areas (Optionally)
            -Collision box
            -Position
            -Pressed-ness state
            -Disabled state
        */
        Texture2D texture;
        Rectangle[] sampleAreas;
        Rectangle collision;
        Vector2 position;
        PressedState state;
        public bool JustClicked { get; private set; }
        public bool JustReleased { get; private set; }
        public bool JustHovered { get; private set; }
        public bool JustUnhovered { get; private set; }

        /*
          A button can:
            -Be pressed
            -Be disabled
            -Handle input
            -Be drawn
              -Only if there is a texture
              -If sample areas were provided, choose one based on the state
        */
        public Button(Rectangle collisionBox, Texture2D texture = null, Rectangle[] samples = null, Vector2 position = new Vector2(), bool disabled = false)
        {
            this.texture = texture;
            sampleAreas = samples;
            collision = collisionBox;
            this.position = position;
            if (disabled)
                state = PressedState.Disabled;
            else
                state = PressedState.Idle;
        }

        /*
          Basically all of the logic for the button is done in here so that the things that rely on it will be able to gather the results that 
          they need from it when they have their Update() called.
        */
        public void HandleInput()
        {
            /*
              All of these only happen on one frame, so we'll set them to false right at the start.
            */
            JustClicked = false;
            JustHovered = false;
            JustReleased = false;
            JustUnhovered = false;
            if (state != PressedState.Disabled)
            {
                MouseState mouse = Mouse.GetState();
                /*
                 The collision box may not perfectly line up with the texture's top left corner, so we've got to add a bunch of things together in 
                 order to properly check whether or not the mouse is within the box.
                */
                if (mouse.X > collision.X + position.X && mouse.X < collision.X + collision.Width + position.X &&
                    mouse.Y > collision.Y + position.Y && mouse.Y < collision.Y + collision.Height + position.Y)
                {
                    if (state == PressedState.Idle)
                        JustHovered = true;
                    if(mouse.LeftButton == ButtonState.Pressed)
                    {
                        if (state != PressedState.Pressed)
                            JustClicked = true;
                        state = PressedState.Pressed;
                    }
                    else
                    {
                        if (state == PressedState.Pressed)
                            JustReleased = true;
                        state = PressedState.Hovered;
                    }
                }
                else
                {
                    if(state != PressedState.Idle)
                    {
                        if (state == PressedState.Pressed)
                            JustReleased = true;
                        JustUnhovered = true;
                        state = PressedState.Idle;
                    }
                }
            }
        }

        public void Update()
        {

        }
        public void Draw(SpriteBatch sb)
        {
            if (texture != null)
            {
                if (sampleAreas != null)
                    sb.Draw(texture, position, sampleAreas[(int)state], Color.White);
                else
                    sb.Draw(texture, position, Color.White);
            }
        }

        enum PressedState
        {
            Idle,
            Hovered,
            Pressed,
            Disabled
        }
    }
}
