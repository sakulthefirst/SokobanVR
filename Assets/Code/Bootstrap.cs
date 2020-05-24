using Assets.Code.Extension;
using Assets.Code.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code
{
    public class Bootstrap : MonoBehaviour
    {

        // world
        public GameObject wallPrefab;
        public GameObject floorEvenPrefab;
        public GameObject floorOddPrefab;
        public GameObject boxPrefab;
        public GameObject goalPrefab;
        public GameObject player;

    
        private bool finishedLock = false;


        void Start()
        {
            // register game objects
            GameObjectRegistry.WallPrefab = wallPrefab;
            GameObjectRegistry.FloorEvenPrefab = floorEvenPrefab;
            GameObjectRegistry.FloorOddPrefab = floorOddPrefab;
            GameObjectRegistry.BoxPrefab = boxPrefab;
            GameObjectRegistry.GoalPrefab = goalPrefab;
            GameObjectRegistry.Player = player;


            // add levelbuilder
            gameObject.AddComponent<LevelBuilder>();

            // load levels
            if (GameState.Collections == null)
            {
                GameState.Collections = LevelFileLoader.ListCollections();
                GameState.CurrentCollectionIndex = 0;
                GameState.CurrentCollectionLevels = LevelFileLoader.GetLevels(GameState.Collections[0]).ToArray();
            }
         
            // show info on level change
            CommunicationService.LevelChange += (Tuple<string, char[][]> level) =>
            {
                CommunicationService.DisplayInfo(level.Item1);
            };

        }

        private void CheckIfFinshed()
        {
            if (!finishedLock)
            {
                var boxes = FindObjectsOfType<BoxManager>();
                if (boxes.Length > 0)
                {
                    var finished = true;
                    foreach (var box in boxes)
                    {
                        if (box.IsInGoal == false)
                        {
                            finished = false;
                        }
                    }

                    if (finished)
                    {
                        finishedLock = true;
                        CommunicationService.DisplayInfo("Finish");
                        this.Delay(2, () =>
                        {
                            GameState.CurrentLevelIndex = GameState.CurrentLevelIndex < GameState.CurrentCollectionLevels.Length ? GameState.CurrentLevelIndex + 1 : 0;
                            CommunicationService.ChangeLevel(GameState.CurrentCollectionLevels[GameState.CurrentLevelIndex]);
                            finishedLock = false;
                        });
                    }
                }
            }

        }

        void Update()
        {
            CheckIfFinshed();
        }
    }
}
