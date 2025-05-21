using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponHolder;  // Where the weapon will be held
    private WeaponData currentWeapon;
    private GameObject currentWeaponInstance;

    public void PickupWeapon(WeaponData newWeapon)
    {
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance);  // Remove previous weapon
        }

        currentWeapon = newWeapon;
        currentWeaponInstance = Instantiate(newWeapon.weaponPrefab, weaponHolder);
        currentWeaponInstance.transform.localPosition = Vector3.zero;
        currentWeaponInstance.transform.localRotation = Quaternion.identity;

        // Assign animations
        GetComponent<Animator>().runtimeAnimatorController = newWeapon.weaponAnimator;
    }
}
