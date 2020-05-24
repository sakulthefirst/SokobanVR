using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
    static class GameState
    {

        public static string[] Collections { get; set; } = null;
        public static int CurrentCollectionIndex { get; set; } = 0;

        public static Tuple<string, char[][]>[] CurrentCollectionLevels { get; set; } = null;
        public static int CurrentLevelIndex { get; set; } = 0;

        public static GameObject SelectedBoxInstance { get; set; } = null;


    }
}
