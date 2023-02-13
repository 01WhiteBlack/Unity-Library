using UnityEngine;
using System.IO;
namespace UnityLibrary
{
    public class LoadOutSideTexture
    {
        public Texture2D LoadTexture(string path)
        {
            Texture2D texture = null;
            if (File.Exists(path))
            {
                byte[] bytes = File.ReadAllBytes(path);
                texture = new Texture2D(1, 1);
                texture.LoadImage(bytes);
            }
            return texture;
        }
    }
}