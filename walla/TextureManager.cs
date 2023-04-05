using System;
using Raylib_cs;

public class TextureManager
{
    
    Dictionary<string, Texture2D> TexturesList = new Dictionary<string,Texture2D>();
    public Texture2D LoadTexture(string fileName)
    {

        if (TexturesList.ContainsKey(fileName))
        {
            return TexturesList[fileName];
        }
        else
        {
            Texture2D texture = Raylib.LoadTexture(fileName);
            TexturesList[fileName] = texture;
            return texture;
        }
    }
}
