using System.Linq;
using Assets.Data;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class EndScene : MonoBehaviour
    {
        public TMP_Text TotalDeaths;
        public TMP_Text TotalTime;

        private void Update()
        {
            if (Input.GetKey("escape"))
                Helper.Quit(name, GetType());
        }

        private void Start()
        {
            var data = DataWriter.Read();
            var lastLevel = data.LastOrDefault();
            if (lastLevel == null)
                return;

            TotalTime.text = Helper.TimeFromFloat(lastLevel.TimeInSeconds);
            TotalDeaths.text = lastLevel.Deaths.ToString();
        }
    }
}