using System.Windows.Controls;


namespace System.Windows.Extension.Controls
{
    public class ContextMenuButton : Button
    {
        public ContextMenu Menu { get; set; }

        protected override void OnClick()
        {
            base.OnClick();
            if (Menu != null)
            {
                Menu.PlacementTarget = this;
                Menu.IsOpen = true;
            }
        }
    }
}