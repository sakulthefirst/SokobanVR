using Assets.Code.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Logic
{
    public class LevelBuilder : MonoBehaviour
    {
        public List<GameObject> instances = new List<GameObject>();

        private Vector3 playerPosition = Vector3.zero;

        public LevelBuilder()
        {
            CommunicationService.LevelChange += (Tuple<string, char[][]> level) =>
            {
                Create(level.Item2);
            };
        }


        private void Clear()
        {
            foreach (var instance in instances)
            {
                DestroyImmediate(instance);
            }
        }


        private void Create(char[][] level)
        {
            Clear();
            for (int row = 0; row < level.Length; row++)
            {

                bool startWithEven = row % 2 == 0;

                for (int column = 0; column < level[row].Length; column++)
                {

                    GameObject floor = null;
                    if (startWithEven)
                    {
                        floor = column % 2 == 0 ? GameObjectRegistry.FloorEvenPrefab : GameObjectRegistry.FloorOddPrefab;
                    }
                    else
                    {
                        floor = column % 2 == 0 ? GameObjectRegistry.FloorOddPrefab : GameObjectRegistry.FloorEvenPrefab;
                    }

                    instances.Add(Instantiate(floor, new Vector3(column, 0, row), Quaternion.identity));

                    switch (level[row][column])
                    {
                        case '$':
                            instances.Add(Instantiate(GameObjectRegistry.BoxPrefab, new Vector3(column, 1, row), Quaternion.identity));
                            break;
                        case '#':
                            instances.Add(Instantiate(GameObjectRegistry.WallPrefab, new Vector3(column, 1, row), Quaternion.identity));
                            break;
                        case '@':
                            playerPosition = new Vector3(column, 3, row);
                            break;
                        case '.':
                            instances.Add(Instantiate(GameObjectRegistry.GoalPrefab, new Vector3(column, 1, row), Quaternion.identity));
                            break;
                        case '*':
                            instances.Add(Instantiate(GameObjectRegistry.GoalPrefab, new Vector3(column, 1, row), Quaternion.identity));
                            instances.Add(Instantiate(GameObjectRegistry.BoxPrefab, new Vector3(column, 1, row), Quaternion.identity));
                            break;
                        case '+':
                            instances.Add(Instantiate(GameObjectRegistry.GoalPrefab, new Vector3(column, 1, row), Quaternion.identity));
                            playerPosition = new Vector3(column, 3, row);
                            break;
                    }

                }
            }
            GameObjectRegistry.Player.gameObject.SetActive(false);
            GameObjectRegistry.Player.gameObject.transform.position = playerPosition;
            GameObjectRegistry.Player.gameObject.SetActive(true);
        }

    }
}

