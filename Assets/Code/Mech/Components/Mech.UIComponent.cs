public partial class Mech // Holds references to UI, and displays it depending on what is happening
{
    public class UIComponent : Component
    {
        readonly MechUIStats _mechUIStats;
        readonly MechUIActions _mechUIActions;

        public event System.Action<MechUIActions> OnMechUIActionsDisplayed;
        public event System.Action<MechUIActions> OnMechUIActionsHidden;


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

            OnMechUIActionsDisplayed?.Invoke(_mechUIActions);
        }

        void HideMechGUI()
        {
            _mechUIStats.Hide(mech);
            _mechUIActions.Hide(mech);

            OnMechUIActionsHidden?.Invoke(_mechUIActions);
        }
    }
}
