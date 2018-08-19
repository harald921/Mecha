public partial class Mech
{
    public class HealthComponent : Component
    {
        public int currentHealth { get; private set; }


        public HealthComponent(Mech inMech) : base(inMech)
        {
            currentHealth = mech.mobilityType.data.armorModifier + mech.armorType.data.armorModifier;
        }

        public void ModifyHealth(int inHealthModification)
        {
            int previousHealth = currentHealth;

            currentHealth += inHealthModification;

            UnityEngine.Debug.Log("Mech health went from " + previousHealth + " to " + currentHealth);
        }
    }
}