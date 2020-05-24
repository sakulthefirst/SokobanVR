using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Logic
{
    public class LevelFileLoader
    {
        static TextAsset[]   levels = Resources.LoadAll<TextAsset>("Levels");

        public static string[] ListCollections()
        {
          return levels.Select(v => v.name).ToArray();
        }

        public static IEnumerable<Tuple<string, char[][]>> GetLevels(string collectionPath)
        {
            var text = levels.Where(v => v.name == collectionPath).Select(v=>v.text).FirstOrDefault();
            var name = "";
            var level = new List<String>();
            var parts = text.Split(new string[] { "\n" }, StringSplitOptions.None);

            foreach (var line in parts)
            {
                if (line.StartsWith(";"))
                {
                    name = line.Replace(";", "");
                    yield return new Tuple<string, char[][]>(name, level.Select(v => v.ToCharArray()).ToArray());
                    level.Clear();
                }
                else
                {
                    level.Add(line);
                }
            }
        }
    }
}
