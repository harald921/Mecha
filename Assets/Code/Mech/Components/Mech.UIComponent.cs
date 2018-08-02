public partial class Mech // Holds references to UI, and displays it depending on what is happening
{
    public class UIComponent : Component
    {
        public readonly MechGUI mechGUI;

        public UIComponent(Mech inMech) : base(inMech)
        {
            mechGUI = UnityEngine.Object.FindObjectOfType<MechGUI>();

            mech.inputComponent.OnSelected += () => mechGUI.Display(mech);
        }
    }
}
