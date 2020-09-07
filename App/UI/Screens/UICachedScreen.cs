using OregoFramework.Util;

namespace OregoFramework.App
{
    public abstract class UICachedScreen : UIScreen
    {
        protected void StartPreviousScreen(IUIScreenTransition transition = null)
        {
            var parent = this.parent.As<UICachedScreenController>();
            parent.StartPreviousScreen(this, transition);
        }
    }
}