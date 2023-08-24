using System;
using System.Collections.Generic;
using System.Text;

namespace YouJelly
{
    public enum GameStates
    {
        player_moveleft,
        player_moveright,
        player_jump,
        fullscreen,
        p_pressed,
        pause,
        exiting
    }

    public enum BSettings
    {
        fullscreen,
        v_sync
    }

    public enum ISettings
    {
        resolution_x,
        resolution_y
    }

    public enum KeyBinds
    {
        moveleft,
        moveright,
        jump,
        basic_attack,
        super_attack,
        ability1,
        ability2,
        inventory,
        map,
        settings,
        console
    }
}
