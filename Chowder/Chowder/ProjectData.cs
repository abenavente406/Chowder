using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace Chowder
{
    public class ProjectData
    {
        public static  int GAMEWIDTH = 720;
        public static  int GAMEHEIGHT = 480;
        public static  bool ISFULLSCREEN = false;

        public static  GraphicsDeviceManager GDM;
        public static  GraphicsDevice GraphicsDevice;
        public static  ContentManager Content;
        public static  string DirectoryPath;

        public  ProjectData(Game game)
        {
            var project = (Game1)game;
            GDM = project.graphics;
            GraphicsDevice = GDM.GraphicsDevice;
            Content = project.Content;
            DirectoryPath = Directory.GetCurrentDirectory();
        }
    }
}
