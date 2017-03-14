using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameDemo.Managers
{
    /*
      The default means for handling input have a few flaws and inconveniences, so we really ought to make our own class for handling it.

      Needed functionality:
        -Can tell us whether or not something was pushed/released this frame
        -Can handle both the mouse and the keyboard (And maybe a gamepad too, later on)
        -Only asks for the states of the inputs one per frame for consistency reasons

      This class is a major oddity in that it's key that we only ever have one of them, and that we'd like it to be globally accessible.
      That leads to it being a good idea to make nearly all of its methods static, something you almost never want to do.
    */
    class InputManager
    {
        /*
          Has:
            -This frame's state
            -Last frame's state
        */
        static KeyboardState keyState;
        static KeyboardState prevKeyState;
        static MouseState mouseState;
        static MouseState prevMouseState;
        /*
          Can:
            -Tell if a key is up/down
            -Tell if a key was pressed/released this frame
            -Tell where the mouse is
        */
    }
}
