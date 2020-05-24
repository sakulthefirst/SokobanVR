using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Logic
{
    class MenuManager : MonoBehaviour
    {

        public GameObject menuCanvas;
        public GameObject nextButton;
        public GameObject previousButton;
        public GameObject resetButton;
        public GameObject collectionText;
        public GameObject levelButtonPrefab;
        public GameObject layoutContent;

        private List<GameObject> instances = new List<GameObject>();

        private void Start()
        {

            GameState.Collections = LevelFileLoader.ListCollections();
            ChangeCollection();

            var _nextButton = nextButton.GetComponent<UnityEngine.UI.Button>();

            _nextButton.onClick.AddListener(() =>
            {
                GameState.CurrentCollectionIndex++;
                ChangeCollection();
            });

            var _previousButton = previousButton.GetComponent<UnityEngine.UI.Button>();

            _previousButton.onClick.AddListener(() =>
            {
                GameState.CurrentCollectionIndex--;
                ChangeCollection();
            });

            var _resetButton = resetButton.GetComponent<UnityEngine.UI.Button>();
            _resetButton.onClick.AddListener(() =>
            {
                CommunicationService.ChangeLevel(GameState.CurrentCollectionLevels[GameState.CurrentLevelIndex]);
            });

            CommunicationService.LevelChange += (Tuple<string, char[][]> level) =>
            {
                menuCanvas.SetActive(false);
                resetButton.SetActive(true);

            };

            resetButton.SetActive(false);

        }

        private void ChangeCollection()
        {

            this.Clear();
            var collection = GameState.Collections[GameState.CurrentCollectionIndex];
            collectionText.GetComponent<Text>().text = collection;

            GameState.CurrentCollectionLevels = LevelFileLoader.GetLevels(collection).ToArray();

            var index = 0;
            foreach (var level in GameState.CurrentCollectionLevels)
            {
                var buttonGameObject = (GameObject)Instantiate(levelButtonPrefab);
                buttonGameObject.transform.SetParent(layoutContent.transform, false);
                buttonGameObject.transform.GetChild(0).GetComponent<Text>().text = level.Item1;
                instances.Add(buttonGameObject);
                var button = buttonGameObject.GetComponent<UnityEngine.UI.Button>();

                // add lsitener
                button.onClick.AddListener(() =>
                {
                    GameState.CurrentLevelIndex = index;
                    CommunicationService.ChangeLevel(level);
                });

            }

            // switch buttons
            previousButton.SetActive(GameState.CurrentCollectionIndex > 0);
            nextButton.SetActive(GameState.CurrentCollectionIndex < collection.Length - 1);
            index++;

        }


        private void Clear()
        {
            foreach (var instance in instances)
            {
                DestroyImmediate(instance);
            }
        }


        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                menuCanvas.SetActive(!menuCanvas.activeSelf);
            }

            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                menuCanvas.SetActive(false);
            }

        }
    }
}
