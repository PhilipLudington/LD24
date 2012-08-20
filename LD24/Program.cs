using System;
using MrPhilEngine;
using SFML.Graphics;
using SFML.Window;

namespace LD24.MrPhil
{
    // Display a button
    // Play sound when clicked
    class Program
    {
        private static Sound sound;
        private static bool quit = false;

        static int Main(string[] args)
        {
            // The Screen or Window
            VideoMode videoMode = new VideoMode(1024, 768);
            RenderWindow window = new RenderWindow(videoMode,
                "Learn SFML");
            window.Closed += new EventHandler(window_Closed);

            Start();

            while (window.IsOpen()
                && !quit)
            {
                window.DispatchEvents();

                // Draw
                window.Display();
            }
            window.Close();

            return 0;
        }

        static void window_Closed(object sender, EventArgs e)
        {
            quit = true;
        }

        static void Start()
        {
            // Create screen

            // Register the screen

            Button button = new Button();
            button.Click += new Button.ClickEventHandler(button_Click);

            // Register the button with the screen

            // Create the sound
            sound = new Sound();

            Console.Out.WriteLine("Start!");
        }

        static void button_Click(object sender, EventArgs e)
        {
            // Play the sound
            sound.Play();
        }
    }
}
