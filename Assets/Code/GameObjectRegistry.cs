using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
    static class GameObjectRegistry
    {

        public static GameObject Player { get; set; }
        public static GameObject FloorEvenPrefab { get; set; }
        public static GameObject FloorOddPrefab { get; set; }
        public static GameObject BoxPrefab { get; set; }
        public static GameObject GoalPrefab { get; set; }
        public static GameObject WallPrefab { get; internal set; }
        public static GameObject LevelButtonTemplate { get; internal set; }
        public static GameObject MenuCanvasInstance { get; internal set; }
        public static GameObject ResetButtonInstance { get; internal set; }
        public static GameObject MenuLayoutInstance { get; internal set; }
        public static GameObject NextButtonInstance { get; internal set; }
        public static GameObject PreviousButtonInstance { get; internal set; }
        public static GameObject CollecionTextInstance { get; internal set; }
        public static GameObject MiniMapCanvasInstance { get; internal set; }
        public static GameObject SelectedBoxInstance { get; set; }
        public static GameObject BoxSelectorInstance { get; set; }
        public static GameObject InfoCanvasTextInstance { get; internal set; }
        public static GameObject InfoCanvasInstance { get; internal set; }

    }
}
