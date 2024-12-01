using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Pickup")]
public class ItemPickup : ScriptableObject
{
    new public string name = "New Item";
    public string type;

}
