using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong_1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Custom Variables.

        //Background variables
        Texture2D Background;

        //Paddles
        Texture2D Paddle;

        //Player One Variables
        Vector2 PaddleOnePos;
        Rectangle PaddleOneRect;

        //Player Two Variables
        Vector2 PaddleTwoPos;
        Rectangle PaddleTwoRect;

        //Ball
        Texture2D Ball;
        Vector2 BallPos;
        Vector2 BallSpeed;
        Rectangle BallRect;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //Set the screen size
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 375;

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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here


            //Load the Background image
            Background = Content.Load<Texture2D>("Pong Background");

            //Load the Paddle image
            Paddle = Content.Load<Texture2D>("Paddle");

            //Paddle One initialization
            PaddleOnePos = new Vector2(16, 195);
            PaddleOneRect = new 
                Rectangle((int)PaddleOnePos.X, (int)PaddleOnePos.Y, 
                            Paddle.Width, Paddle.Height);

            //Paddle Two initialization
            PaddleTwoPos = new Vector2(600 - 16 - Paddle.Width, 195);
            PaddleTwoRect = new
                Rectangle((int)PaddleTwoPos.X, (int)PaddleTwoPos.Y, 
                            Paddle.Width, Paddle.Height);

            //Load the Ball image
            Ball = Content.Load<Texture2D>("Pong Ball");
            //Initialize all of the ball variables
            BallPos = new Vector2(300 - (Ball.Width / 2), 
                                    75 + 150 - (Ball.Height / 2));
            BallSpeed = new Vector2(1f, 0.5f);
            BallRect = new Rectangle((int)BallPos.X, (int)BallPos.Y,
                                        Ball.Width, Ball.Height);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            //Check if the W key is pressed and adjust the left hand Paddles position and Rectangle Up 
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                PaddleOnePos.Y = PaddleOnePos.Y - 1;
                PaddleOneRect.Y = (int)PaddleOnePos.Y;
            }

            //Check if the S key is pressed and adjust the left hand Paddles position and Rectangle Down
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                PaddleOnePos.Y = PaddleOnePos.Y + 1;
                PaddleOneRect.Y = (int)PaddleOnePos.Y;
            }

            //Keep the left hand Paddle from moving above the playable screen
            if (PaddleOnePos.Y <= 75)
            {
                PaddleOnePos.Y = 75;
            }

            //Keep the left hand Paddle from moving below the playable screen
            if (PaddleOnePos.Y + Paddle.Height >= 375)
            {
                PaddleOnePos.Y = 375 - Paddle.Height;
            }

            
            //Check if the Up key is pressed and adjust the right hand Paddles position and Rectangle Up
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                PaddleTwoPos.Y = PaddleTwoPos.Y - 1;
                PaddleTwoRect.Y = (int)PaddleTwoPos.Y;
            }

            //Check if the Down key is pressed and adjust the right hand Paddles position and Rectangle Down
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                PaddleTwoPos.Y = PaddleTwoPos.Y + 1;
                PaddleTwoRect.Y = (int)PaddleTwoPos.Y;
            }

            //Keep the right hand Paddle from moving above the playable screen
            if (PaddleTwoPos.Y <= 75)
            {
                PaddleTwoPos.Y = 75;
            }

            //Keep the right hand Paddle from moving below the playable screen
            if (PaddleTwoPos.Y + Paddle.Height >= 375)
            {
                PaddleTwoPos.Y = 375 - Paddle.Height;
            }

            //Update the Ball position
            BallPos += BallSpeed;
            //Update the Ball rectangle
            BallRect = new Rectangle((int)BallPos.X, (int)BallPos.Y,
                                        Ball.Width, Ball.Height);

            //Check if the Ball hits the top of the playable screen and change the Y direction of travel
            if (BallPos.Y <= 75)
            {
                BallSpeed.Y = BallSpeed.Y * -1;
            }

            //Check if the Ball hits the bottom of the screen and change the Y direction of travel
            if (BallPos.Y >= 375 - Ball.Height)
            {
                BallSpeed.Y = BallSpeed.Y * -1;
            }

            //If the ball hits the left hand side, reset the ball to the middle of the map and 
            //make the ball move towards the opposite end of the screen
            if (BallPos.X <= 0)
            {
                BallPos = new Vector2(300 - (Ball.Width / 2),
                                      75 + 150 - (Ball.Height / 2));
                BallSpeed.X = BallSpeed.X * -1;
            }

            //If the ball hits the right hand side, reset the ball to the middle of the map and 
            //make the ball move towards the opposite end of the screen
            if (BallPos.X >= 600 - Ball.Width)
            {
                BallPos = new Vector2(300 - (Ball.Width / 2),
                                      75 + 150 - (Ball.Height / 2));
                BallSpeed.X = BallSpeed.X * -1;
            }

            //If the ball hits the left hand Paddle change the X direction of travel
            if (BallRect.Intersects(PaddleOneRect))
            {
                BallSpeed.X = BallSpeed.X * -1;
            }

            //If the ball hits the right hand Paddle change the X dirction of travel
            if (BallRect.Intersects(PaddleTwoRect))
            {
                BallSpeed.X = BallSpeed.X * -1;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Open the SpriteBatch. Make sure you end it after you've finished Drawing
            spriteBatch.Begin();

            //First Draw the Background
            spriteBatch.Draw(Background, Vector2.Zero, Color.White);

            //Draw the first Paddle
            spriteBatch.Draw(Paddle, PaddleOnePos, Color.Aqua);

            //Draw the second Paddle
            spriteBatch.Draw(Paddle, PaddleTwoPos, Color.MediumVioletRed);

            //Draw the Ball
            spriteBatch.Draw(Ball, BallPos, Color.White);

            //Ending the SpriteBatch.
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
