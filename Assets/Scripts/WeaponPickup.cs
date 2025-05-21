using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponData weaponData;  // Reference to the weapon data

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponManager weaponManager = other.GetComponent<WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.PickupWeapon(weaponData);
                Destroy(gameObject);  // Remove the pickup from the scene
            }
        }
        Debug.Log("entered");
    }
}
