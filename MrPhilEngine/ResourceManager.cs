using System;
using System.Collections;
using SFML.Graphics;
using SFML.Audio;

namespace MrPhilEngine
{
    public class ResourceManager
    {
        Hashtable hashtableTextures;
        Hashtable hashtableSoundBuffers;

        public ResourceManager()
        {
            hashtableTextures = new Hashtable(25);
            hashtableSoundBuffers = new Hashtable(25);
        }

        public Texture GetTexture(string textureName)
        {
            if (hashtableTextures.ContainsKey(textureName))
            {
                return (Texture)hashtableTextures[textureName];
            }
            else
            {
                Texture texture = new Texture(textureName);
                hashtableTextures.Add(textureName, texture);

                return texture;
            }
        }

        public SoundBuffer GetSound(string soundName)
        {
            if (hashtableSoundBuffers.ContainsKey(soundName))
            {
                return (SoundBuffer)hashtableSoundBuffers[soundName];
            }
            else
            {
                SoundBuffer soundBuffer = new SoundBuffer(soundName);
                hashtableSoundBuffers.Add(soundName, soundBuffer);

                return soundBuffer;
            }
        }
    }
}
