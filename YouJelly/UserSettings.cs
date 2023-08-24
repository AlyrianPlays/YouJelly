using System;
using System.Collections.Generic;
using System.Text;

namespace YouJelly
{
    public class UserSettings
    {
        public List<bool> bSettings { get; set; }
        public List<int> iSettings { get; set; }
        public UserSettings() 
        {
            bSettings = new List<bool>();
            iSettings = new List<int>();
            foreach (BSettings bset in Enum.GetValues(typeof(BSettings)))
            {
                bSettings.Insert((int)bset, false);
            }
            foreach (ISettings iset in Enum.GetValues(typeof(ISettings)))
            {
                iSettings.Insert((int)iset, 0);
            }
        }
        public void Initialize()
        {
            bSettings[(int)BSettings.fullscreen] = true;
            bSettings[(int)BSettings.v_sync] = false;

            iSettings[(int)ISettings.resolution_x] = 1920;
            iSettings[(int)ISettings.resolution_y] = 1080;
        }

        //TODO: Create serialization
    }
}
