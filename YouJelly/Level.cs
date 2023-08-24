using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YouJelly
{
    public class Level
    {
        public List<Simple> simples { get; set; }
        public Microsoft.Xna.Framework.Content.ContentManager content { get; set; }
        
        //Remember: levelHeight and levelWidth are in COW units. Whereas Screen.cs height/width will be in pixels.
        public int levelHeight { get; set; }
        public int levelWidth { get; set; }

        public Level()
        {
            simples = new List<Simple>();
        }

        public Level(List<Simple> simples, Microsoft.Xna.Framework.Content.ContentManager content, int levelHeight, int levelWidth)
        {
            this.content = content;
            this.simples = simples;
            this.levelWidth = levelHeight;
            this.levelWidth = levelWidth;
        }

        public Level(SerialLevel slevel, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.content = content;
            simples = new List<Simple>();
            foreach(string s in slevel.simples)
            {
                Simple.SerialSimple ssimple = JsonSerializer.Deserialize<Simple.SerialSimple>(s);
                simples.Add(new Simple(ssimple, this.content));
            }
            levelHeight = slevel.levelHeight;
            levelWidth = slevel.levelWidth;
        }

        public string Serialize()
        {
            SerialLevel slevel = new SerialLevel(this);
            string jsonString = JsonSerializer.Serialize(slevel);
            return jsonString;
        }

        public static Level DeSerialize(string filePath, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            //List<string> ssimples = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(filePath));
            SerialLevel slevel = JsonSerializer.Deserialize<SerialLevel>(File.ReadAllText(filePath));
            //List<Simple> simples = new List<Simple>();
            //foreach(string s in ssimples)
            //{
            //    simples.Add(new Simple(JsonSerializer.Deserialize<Simple.SerialSimple>(s), content));
            //}
            return new Level(slevel, content);
        }

        public class SerialLevel
        {
            public List<string> simples { get; set; }
            public int levelHeight { get; set; }
            public int levelWidth { get; set; }

            public SerialLevel()
            {
                simples = new List<string>();
                levelHeight = 0;
                levelWidth = 0;
            }

            public SerialLevel(Level level)
            {
                simples = new List<string>();
                foreach(Simple s in level.simples)
                {
                    simples.Add(s.Serialize());
                }
                levelHeight = level.levelHeight;
                levelWidth = level.levelWidth;
            }
        }

    }
}
