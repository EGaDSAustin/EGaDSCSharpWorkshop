using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EGaDSTutorial
{
    [RequireComponent(typeof(Text))]
    public class RenderMoney : MonoBehaviour
    {
        private Text _text;
        public Text Text => (_text) ? _text : (_text = GetComponent<Text>());

        void Update()
        {
            Text.text = $"Money = {Player.Instance.Money}";
        }
    }
}
