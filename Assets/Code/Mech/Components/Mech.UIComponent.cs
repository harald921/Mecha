public partial class Mech // Holds references to UI, and displays it depending on what is happening
{
    public class UIComponent : Component
    {
        readonly MechUIStats _mechUIStats;
        readonly MechUIActions _mechUIActions;


        public UIComponent(Mech inMech) : base(inMech)
        {
            _mechUIStats   = UnityEngine.Object.FindObjectOfType<MechUIStats>();
            _mechUIActions = UnityEngine.Object.FindObjectOfType<MechUIActions>();

            mech.inputComponent.OnSelected      += ShowMechGUI;
            mech.inputComponent.OnSelectionLost += HideMechGUI;
        }


        void ShowMechGUI()
        {
            _mechUIStats.Display(mech);
            _mechUIActions.Display(mech);
        }

        void HideMechGUI()
        {
            _mechUIStats.Hide(mech);
            _mechUIActions.Hide(mech);
        }
    }
}
