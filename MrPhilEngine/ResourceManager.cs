using System;
using System.Collections;
using SFML.Graphics;

namespace MrPhilEngine
{
    public class ResourceManager
    {
        Hashtable textureList;

        public ResourceManager()
        {
            textureList = new Hashtable(25);
        }

        public Texture GetTexture(string fullPathToResource)
        {
            if (textureList.ContainsKey(fullPathToResource))
            {
                return (Texture)textureList[fullPathToResource];
            }
            else
            {
                Texture texture = new Texture(fullPathToResource);
                textureList.Add(fullPathToResource, texture);

                return texture;
            }
        }
    }
}
