using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Extension
{
    static class DelayExtension
    {
        public delegate void Action();

        public static void Delay(this MonoBehaviour mono, float seconds, Action action)
        {
            mono.StartCoroutine(DelayCo(seconds, action));
        }

        static IEnumerator DelayCo(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action();
        }

    }
}
