using System;
using UnityEngine;

namespace Debugging
{
    public class DebugMenu : MonoBehaviour
    {
        public Canvas debugMenu;
        public Action<int> diceNumber;

        public void ToggleDebugMenu(bool isVisible)
        {
            debugMenu.enabled = isVisible;
        }

        public void GoToMenu()
        {
        }

        public void SetDiceNumber(string text)
        {
            int.TryParse(text, out var value);
            diceNumber?.Invoke(value);
        }
    }
}