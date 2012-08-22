using System;
using MrPhilEngine;
using SFML.Graphics;
using SFML.Window;
using System.IO;
using SFML.Audio;

namespace LD24.MrPhil
{
    // Display a button
    // Play sound when clicked
    class Program
    {
        private static bool quit = false;
        private static Screen currentScreen;
        private static Sound sound;

        static int Main(string[] args)
        {
            // The Screen or Window
            VideoMode videoMode = new VideoMode(1024, 768);
            RenderWindow window = new RenderWindow(videoMode,
                "Learn SFML");
            window.Closed += new EventHandler(window_Closed);
            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(window_MouseButtonPressed);
            window.MouseMoved += new EventHandler<MouseMoveEventArgs>(window_MouseMoved);

            Start();

            Console.Out.WriteLine("Engine Started Successfully!");

            while (window.IsOpen()
                && !quit)
            {
                window.DispatchEvents();

                // Draw
                currentScreen.Draw(window);
                window.Display();
            }
            window.Close();

            return 0;
        }

        static void window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            currentScreen.MouseMoved(e.X, e.Y);
        }

        static void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            currentScreen.MouseButtonPressed(e.Button, e.X, e.Y);
        }

        static void window_Closed(object sender, EventArgs e)
        {
            quit = true;
        }

        static void Start()
        {
            // Create screen
            Screen screen = new Screen();

            // Register the screen
            currentScreen = screen;

            // Register the background image
            MrPhilEngine.Image image = screen.CreateImage(Path.GetFullPath("Map.png"));

            // Register the button with the screen
            Button button = screen.CreateButton();
            button.Click += new Button.ClickEventHandler(button_Click);
            button.SetTextureNameButton(Path.GetFullPath("airplane.png"));
            button.SetTextureNameButtonMouseOver(Path.GetFullPath("airplane-Inverse.png"));
            button.SetSoundClick(Path.GetFullPath("Click.wav"));

            // Create the sound
            sound = screen.CreateSound("Click2.wav");
        }

        static void button_Click(object sender, EventArgs e)
        {
            // Play the sound
            sound.Play();
        }
    }
}
