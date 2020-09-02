using UnityEngine;

namespace OregoFramework.App
{
    public abstract class UIBaseBackPressController : UIBackPressController
    {
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) ||
                Input.GetKeyDown(KeyCode.Backspace) ||
                Input.GetKeyDown(KeyCode.Home))
            {
                this.OnBackPressed();
            }
        }
    }
}