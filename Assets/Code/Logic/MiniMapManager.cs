using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Logic
{
    class MiniMapManager : MonoBehaviour
    {
        public GameObject miniMapCanvas;
        public GameObject miniMapCamera;


        void Start()
        {
            miniMapCanvas.SetActive(false);
            CommunicationService.LevelChange += (Tuple<string, char[][]> level) =>
            {
                var z = level.Item2.Length;
                var x = level.Item2.Max(v => v.Length);

                miniMapCamera.transform.position = new Vector3(x / 2, (z+x+5), z / 2);
            };
        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                miniMapCanvas.SetActive(false);

            }

            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                miniMapCanvas.SetActive(!miniMapCanvas.activeSelf);
            }

        }

    }
}
