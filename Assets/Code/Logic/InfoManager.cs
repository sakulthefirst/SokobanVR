using Assets.Code.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Logic
{
    class InfoManager : MonoBehaviour
    {
        public GameObject infoCanvas;
        public GameObject infoText;


        void Start()
        {
            infoCanvas.SetActive(false);
            CommunicationService.InfoDisplay += (string text) =>
            {
                infoCanvas.SetActive(true);
                infoText.GetComponent<Text>().text = text;
                this.Delay(2, () =>
                {
                    infoCanvas.SetActive(false);
                });
            };
        }


    }
}
