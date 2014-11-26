using System;
using System.Diagnostics;

using Astecien.Bezier.Portable;

using C3.XNA;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Astecien
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class AstecienGame : Game
    {
        private readonly BezierControlPointQuartetCollection bezierControlPointQuartetCollection;

        private readonly ControlHandlerMover controlHandlerMover;

        private readonly Drawer drawer;

        private readonly BezierPathPointCalculator calculator;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D handle;

        private Texture2D car;

        public AstecienGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            bezierControlPointQuartetCollection = BezierControlPointCollectionFactory.CreateDemoCollection();
            var bezierPathPointSelector = new BezierPathPointSelector(bezierControlPointQuartetCollection);
            controlHandlerMover = new ControlHandlerMover(bezierControlPointQuartetCollection, bezierPathPointSelector);
            calculator = new BezierPathPointCalculator();
            drawer = new Drawer(bezierControlPointQuartetCollection, bezierPathPointSelector, calculator);
            controlHandlerMover.AlignAll();

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
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
            TouchPanel.EnabledGestures =
                GestureType.Hold |
                GestureType.Tap |
                GestureType.FreeDrag;
                
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
            handle = Content.Load<Texture2D>("Handle");
            car = Content.Load<Texture2D>("car");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
            // TODO: Unload any non ContentManager content here
        }


        BezierPathPoint spritePosition;

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);

            // calculator time runs from 0 to 1 * number of quartets.
            seconds += (float)0.02;

            selectedRotation += 0.2f;

            if (seconds > bezierControlPointQuartetCollection.NumberOfQuartets)
            {
                seconds = 0;
            }

            spritePosition = calculator.CalculatePathPoint(bezierControlPointQuartetCollection.GetQuartet(seconds), seconds);

            GetTouchInput();
            GetKeyboardInput();

        }

        KeyboardState oldState;

        private void GetKeyboardInput()
        {
            var newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.OemPlus) && !oldState.IsKeyDown(Keys.OemPlus))
            {
                bezierControlPointQuartetCollection.Add(new BezierControlPointQuartet(
                                                            40, 164,
                                                            259, 1204,
                                                            399, 154,
                                                            439, 164));
                controlHandlerMover.AlignAll();
            }

            if (newState.IsKeyDown(Keys.OemMinus) && !oldState.IsKeyDown(Keys.OemMinus))
            {
                bezierControlPointQuartetCollection.RemoveLast();
                controlHandlerMover.AlignAll();
            }


            oldState = newState;
        }

        float seconds;

        private int selectedBezierQuartetIndex;
        
        private int selectedControlPointIndex;

        private bool controlPointIsSelected;

        private ControlPointHandlerId selectedControlPoint = new ControlPointHandlerId();

        public void GetTouchInput()
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            foreach (TouchLocation tl in touchCollection)
            {
                if ((tl.State == TouchLocationState.Pressed))
                {
                    if (bezierControlPointQuartetCollection.GivenPositionIsInsideControlPoint((int)tl.Position.X, (int)tl.Position.Y, handle.Width, out selectedBezierQuartetIndex, out selectedControlPointIndex))
                    {
                        controlPointIsSelected = true;
                    
                        //Update the position of the selected control point.
                        selectedControlPoint.ControlPointIndex = selectedControlPointIndex;
                        selectedControlPoint.QuartetIndex = selectedBezierQuartetIndex;
                    }
                }

                if (tl.State == TouchLocationState.Moved && controlPointIsSelected)
                {
                    controlHandlerMover.MoveControlHandlerTo(selectedControlPoint, (int)tl.Position.X, (int)tl.Position.Y);
                }

                if ((tl.State == TouchLocationState.Released))
                {
                    controlPointIsSelected = false;
                }
            }
        }

        float rotation;

        float selectedRotation;

        Vector2 newPosition;

        Vector2 previousPosition;

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            drawer.DrawControlLines((point1, point2) => spriteBatch.DrawLine(point1.X, point1.Y, point2.X, point2.Y, Color.White * 0.25f));

            drawer.DrawControlPoints(point => spriteBatch.Draw(handle, new Vector2(point.X - handle.Width / 2, point.Y - handle.Height / 2), Color.White * 0.3f), controlPointIsSelected, selectedControlPoint);

            drawer.DrawPath((startPathPoint, endPathPoint) => spriteBatch.DrawLine(startPathPoint.XPosition, startPathPoint.YPosition, endPathPoint.XPosition, endPathPoint.YPosition, Color.Teal, 3));

            if (controlPointIsSelected)
            {
                BezierControlPoint selectedControlPointLocation = bezierControlPointQuartetCollection.GetBezierControlPoint(selectedControlPoint);

                spriteBatch.Draw(handle, new Vector2(selectedControlPointLocation.X, selectedControlPointLocation.Y), null, null, new Vector2(handle.Width / 2, handle.Height / 2), selectedRotation, new Vector2(1.2f, 1.2f));
            }
            
            newPosition = new Vector2(spritePosition.XPosition, spritePosition.YPosition);

            CalculateRotation();

            spriteBatch.Draw(car, newPosition, null, null, new Vector2(22, 48), rotation);

            previousPosition = newPosition;

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CalculateRotation()
        {
            float deltaX = newPosition.X - previousPosition.X;
            float deltaY = newPosition.Y - previousPosition.Y;

            rotation = (float)Math.Atan2(deltaY, deltaX) + 180 + 35;
        }
    }
}
