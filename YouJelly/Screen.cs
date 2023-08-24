using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YouJelly
{
    public class Screen
    {
        public int screenHeight { get; set; }
        public int screenWidth { get; set; }
        public int cowSize { get; set; }

        public Screen()
        {
            screenHeight = 0;
            screenWidth = 0;
            cowSize = 0;
        }

        public Screen(Rectangle screenBounds)
        {
            screenHeight = (screenBounds.Height / 36) * 36;
            screenWidth = (screenBounds.Width / 64) * 64;
            findCowandSizes();
        }

        public void Update(Rectangle screenBounds)
        {
            screenHeight = (screenBounds.Height / 36) * 36;
            screenWidth = (screenBounds.Width / 64) * 64;
            findCowandSizes();
        }

        public void findCowandSizes()
        {
            int heightMax = screenHeight / 36;
            int widthMax = screenWidth / 64;
            if(heightMax > widthMax)
            {
                cowSize = widthMax;
            }
            else
            {
                cowSize = heightMax;
            }
            screenHeight = cowSize * 36;
            screenWidth = cowSize * 64;
        }
    }
}
