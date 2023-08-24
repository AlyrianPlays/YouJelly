using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

/* Simple.cs
 * 
 * Member Fields
 * 
 * visuals: to hold all Texture2D objects built from the paths stored in visualPaths
 * visualPaths: all strings to generate Texture2D objects required to draw simple
 * position: current position, relative to current displayed screen
 * animations: all animation objects for simple, first is always static image
 * phys: physics engine for simple (simples only update position)
 * currVis: current visual to be drawn to screen in the next update
 */

namespace YouJelly
{
    public class Simple
    {
        public List<Texture2D> visuals { get; set; }
        public List<string> visualPaths { get; set; }
        public Vector2 position { get; set; }
        public List<Animation> animations { get; set; }
        public Physics phys { get; set; }
        public Texture2D currVis { get; set; }
        public string currVisPath { get; set; }
        public Microsoft.Xna.Framework.Content.ContentManager content { get; set; }
        // May need a reference to the current level so we can check bounds for the physics class. Either that or have a static level object that can be checked from anywhere.
        public Simple()
        {
            visuals = new List<Texture2D>();
            visualPaths = new List<string>();
            position = new Vector2();
            animations = new List<Animation>();
            phys = new Physics();
            currVis = visuals[0];
            currVisPath = visualPaths[0];
        }

        public Simple(List<string> visualPaths, Vector2 position, List<Animation> animations, Physics phys, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.content = content;
            this.visualPaths = visualPaths;
            this.position = position; // Need to protect against invalid positioning higher in call sequence (level coordinate bounds, physics, etc.)
            this.animations = animations;
            this.phys = phys;

            visuals = new List<Texture2D>();
            foreach (string s in visualPaths)
            {
                visuals.Add(this.content.Load<Texture2D>(s));
            }
            currVis = visuals[0];
            currVisPath = this.visualPaths[0];
        }

        public Simple(SerialSimple ssimple, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            this.content = content;
            visualPaths = new List<string>();
            foreach (string s in ssimple.visualPaths)
            {
                visualPaths.Add(s);
            }

            visuals = new List<Texture2D>();
            foreach (string s in visualPaths)
            {
                visuals.Add(this.content.Load<Texture2D>(s));
            }

            position = new Vector2(ssimple.position[0], ssimple.position[1]);

            animations = new List<Animation>();

            phys = new Physics();

            currVis = this.content.Load<Texture2D>(ssimple.currVis);
            currVisPath = ssimple.currVis;
        }

        // maybe want to handle saving in a parent class or in a class all its own, so function doesn't need to be redundantly defined
        public string Serialize()
        {
            SerialSimple ssimple = new SerialSimple(this);
            string jsonString = JsonSerializer.Serialize(ssimple);
            //System.Diagnostics.Debug.WriteLine(fileName);
            //System.Diagnostics.Debug.WriteLine(jsonString);
            //File.WriteAllText(fileName, jsonString);
            return jsonString;
        }

        public class SerialSimple
        {
            public List<string> visualPaths { get; set; }
            public List<float> position { get; set; }
            public List<string> animations { get; set; }
            public List<string> phys { get; set; }
            public string currVis { get; set; }

            public SerialSimple()
            {
                visualPaths = new List<string>();
                position = new List<float>();
                animations = new List<string>();
                phys = new List<string>();
                currVis = "";
            }
            public SerialSimple(Simple simple)
            {
                visualPaths = new List<string>();
                foreach(string s in simple.visualPaths)
                {
                    visualPaths.Add(s);
                }

                position = new List<float>();
                position.Add(simple.position.X);
                position.Add(simple.position.Y);

                animations = new List<string>();
                animations.Add("Placeholder");

                phys = new List<string>();
                phys.Add("Placeholder");

                currVis = simple.currVisPath;
            }
        }
    }
}
