using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameDemo.Graphics;
using MonogameDemo.Input;

namespace MonogameDemo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch sb;
        Texture2D spritesheet;
        MoveableSprite sprite;
        Animation anim;
        Button button;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            /*
              Defines our content folder (similar to static in Django) as 'Content';
            */
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
            // TODO: Add your initialization logic here
            /*
              The mouse is invisible by default
            */
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            sb = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            /*
             Use the Game class's ContentManager Content to load our image file
             Because the file has been through the content processor, the path to the file doesn't need to include the file extension (so no .png)

            Assuming your stack hasn't covered generics yet:  This function would ideally return many different types depending on what it loaded.
            However, in C# a function may only have one return type.  Generic functions get around that by asking that a type be passed in before
            the arguments in the angle brackets seen here.  When you compile the program the compiler goes through, finds all of the types you called
            that function with, and makes normal single return type versions of the function for you.
            So, essentially, this says to use a version of Content.Load that returns a Texture2D.
                                           \/                                                  */
            spritesheet = Content.Load<Texture2D>(".\\spritesheet");

            sprite = new MoveableSprite(Content.Load<Texture2D>(".\\spritesheet"), new Rectangle(0, 0, 32, 32), new Vector2(200, 200), 5);

            Frame[] frameArray = new Frame[]{
                new Frame(new Rectangle( 0, 0, 32, 32), 10), new Frame(new Rectangle( 32, 0, 32, 32), 10), new Frame(new Rectangle(64, 0, 32, 32), 10),
                new Frame(new Rectangle(96, 0, 32, 32), 10), new Frame(new Rectangle(128, 0, 32, 32), 10), new Frame(new Rectangle(0, 32, 32, 32), 10)
                };
            anim = new Animation(Content.Load<Texture2D>(".\\spritesheet"), frameArray, new Vector2(64, 64));

            Rectangle[] samples = new Rectangle[] { new Rectangle(0, 0, 32, 32), new Rectangle(32, 0, 32, 32), new Rectangle(64, 0, 32, 32), new Rectangle(96, 0, 32, 32) };

            button = new Button(new Rectangle(0, 0, 32, 32), Content.Load<Texture2D>(".\\spritesheet"), samples, new Vector2(20, 60));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            /*
              It's usually not super important to fill this out properly.  Everything is destroyed when the game exits anyways.
              Functions like this are much more important on things like levels, where you may want to unload all of the content from one
              level to free up space for the next.
            */
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            /*
             Keyboard.GetState() gets the current state of the keyboard at the instant we called it.  Works for now, but could lead to consistency
             issues in a larger program (What if someone lifted their finger off the button in the middle of an if statement's logic?)
             This causes the need for the InputManager class.
             */
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            /*
              Another place where an InputManager would come in handy: We can tell if a key is pressed *right now*, but we don't know when a key
              is *newly* pressed.  This toggle swaps between on and off every single frame that the button is down as a result.
            */
            KeyboardState keysState = Keyboard.GetState();
            if (keysState.IsKeyDown(Keys.P))
                anim.Paused = !anim.Paused;
            sprite.HandleInput();

            button.HandleInput();
            if (button.JustClicked)
                anim.Paused = !anim.Paused;
            /*
              Once everything has dealt with the new user input, we can move on to actually dealing with that new input.
            */
            sprite.Update();

            anim.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            /*
              Overwrites all of the pixels in the game window with the color CornflowerBlue.  Without this it keeps all of the pixels from the
              last frame that weren't specifically overwitten, which would cause odd behavior for us.
            */
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            /*
              In general, calling low-level draw functions is computationally expensive.  As a result you want to try to do it as little as possible.
              SpriteBatch does its best to help with that: After you call sb.Begin(), it starts taking in calls to sb.Draw(You may not call sb.Draw()
              before sb.Begin() or after sb.End()).  Once you call sb.End(), it packs them into as few low level draw calls as it can and sends them 
              off for you.
            */
            sb.Begin();

            sb.Draw(spritesheet, new Vector2(0, 0), new Rectangle(0, 0, 32, 32), Color.White);
            anim.Draw(sb);
            sprite.Draw(sb);
            button.Draw(sb);

            sb.End();

            base.Draw(gameTime);
        }
    }

    /*
      Simple temporary demo class to show how your game object classes will often be laid out, and the usual flow of data to and from them.
    */
    public class MoveableSprite
    {
        Rectangle sampleArea;
        Vector2 position;
        Texture2D texture;
        float speed;

        public MoveableSprite(Texture2D texture, Rectangle sampleArea, Vector2 position = new Vector2(), float speed = 1)
        {
            this.texture = texture;
            this.sampleArea = sampleArea;
            this.position = position;
            this.speed = speed;
        }

        /*
          It doesn't come up early on, but later it will prove very useful to break apart recieving input and updating your game objects.
          For instance, keeping an animation running in the background while the player is focused on manipulating a menu.
        */
        public void HandleInput()
        {
            Vector2 movement = Vector2.Zero;
            KeyboardState keysState = Keyboard.GetState();
            if (keysState.IsKeyDown(Keys.Left))
                movement.X--;
            if (keysState.IsKeyDown(Keys.Right))
                movement.X++;
            /*
              The coordinate (0, 0) is in the top left on computer screens, and making Y bigger causes you to go *down*. As a result,
              movement on the y axis is the opposite of what you'd expect it to be.  This started because of how old CRT monitors updated
              the pixels on the screen, and has survived in game dev until today for no real good reason.
            */
            if (keysState.IsKeyDown(Keys.Up))
                movement.Y--;
            if (keysState.IsKeyDown(Keys.Down))
                movement.Y++;
            position += movement * speed;
        }

        public void Update()
        {
            /*Does nothing for this one!  Sometimes you'll end up with empty functions just for compatibility reasons (could have been
              skipped here though)*/
        }

        public void Draw(SpriteBatch sb)
        {
            /*
              Since the sprite batch is more efficient when it can pool a lot of calls together, we just trust that sb.Begin() and End()
              are being handled elsewhere.
            */
            sb.Draw(texture, position, sampleArea, Color.White);
        }
    }
}
