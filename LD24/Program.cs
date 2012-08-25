using System;
using MrPhilEngine;
using SFML.Graphics;
using SFML.Window;
using System.IO;
using SFML.Audio;

namespace LD24.MrPhil
{

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
            window.KeyPressed += new EventHandler<KeyEventArgs>(window_KeyPressed);

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

        static void window_KeyPressed(object sender, KeyEventArgs e)
        {
            currentScreen.MessageKeyPressed(e.Code);
        }

        static void window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            currentScreen.MessageMouseMove(e.X, e.Y);
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

            // a Key drive button
            Button button2 = screen.CreateButton();
            button2.Shortcut += new Button.ShortcutEventHandler(button2_Shortcut);
            button2.SetTextureNameButton(Path.GetFullPath("airplane.png"));
            button2.SetKeyShortcut(Keyboard.Key.S);
            button2.X = 100;

            // Play a sound when the letter K is pressed
            Button button3 = screen.CreateButton();
            button3.Shortcut += new Button.ShortcutEventHandler(button3_Shortcut);
            button3.SetKeyShortcut(Keyboard.Key.K);

            // Create the sound
            sound = screen.CreateSound("Click2.wav");

            // Show some Text
            Text text = new Text("MrPhil was here.");
        }

        static void button3_Shortcut(object sender, EventArgs e)
        {
            // Play the sound
            sound.Play();
        }

        static void button2_Shortcut(object sender, EventArgs e)
        {
            ((Button)sender).SetTextureNameButton(Path.GetFullPath("airplane-Inverse.png"));
        }

        static void button_Click(object sender, EventArgs e)
        {
            // Play the sound
            sound.Play();
        }
    }
}
