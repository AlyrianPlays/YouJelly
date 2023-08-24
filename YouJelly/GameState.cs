using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YouJelly
{
    public class GameState
    {
        public List<bool> currState {get; set;}

        public GameState()
        {
            currState = new List<bool>();
            foreach(GameStates gs in Enum.GetValues(typeof(GameStates)))
            {
                currState.Insert((int)gs, false);
            }
        }

        public GameState(YouJelly currGame)
        {
            currState = new List<bool>();
            foreach (GameStates gs in Enum.GetValues(typeof(GameStates)))
            {
                currState.Insert((int) gs, false);
            }
            this.Update(currGame);
        }

        public void Update(YouJelly currGame)
        {
            if(currGame != null)
            {
                //Need to re-map this in 2 ways, checking for the settings_menu gamestate AND checking for the actual UI button that selects for fullscreen when built
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    if (!currState[(int) GameStates.p_pressed])
                    {
                        currState[(int)GameStates.p_pressed] = !currState[(int)GameStates.p_pressed];
                        currState[(int)GameStates.fullscreen] = !currState[(int)GameStates.fullscreen];
                        currGame._graphics.ToggleFullScreen();
                    }
                }
                //Need to re-map to follow player's keybinds, also shouldn't just be a default exit button. Should navigate from the settings_menu
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    File.WriteAllText(currGame.contentDir + "SaveFile.json", currGame.testLevel.Serialize());
                    currState[(int)GameStates.exiting] = true;
                    currGame.Exit();
                }
                
                //Reset latches if any keys no longer pressed
                if (Keyboard.GetState().IsKeyUp(Keys.P))
                {
                    currState[(int)GameStates.p_pressed] = false;
                }
            }
        }

        //TODO: Create serialization
    }
}
