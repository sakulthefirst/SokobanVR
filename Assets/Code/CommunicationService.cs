using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code
{
    class CommunicationService
    {


        // Level Change
        public delegate void LevelChangeHandler(Tuple<string, char[][]> level);

        public static event LevelChangeHandler LevelChange;
        public static void ChangeLevel(Tuple<string, char[][]> level)
        {
            LevelChange?.Invoke(level);
        }


        // Status Text
        public delegate void InfoDisplayEventHandler(string text);

        public static event InfoDisplayEventHandler InfoDisplay;
        public static void DisplayInfo(string text)
        {
            InfoDisplay?.Invoke(text);
        }

    }
}
