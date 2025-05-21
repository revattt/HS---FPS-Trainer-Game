using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;  // The 3D model of the weapon
    public float fireRate = 0.2f;  // Fire rate in seconds
    public float shootingRange = 100f;  // Max range of bullets
    public AudioClip shootSound;  // Sound effect for shooting
    public GameObject muzzleFlashPrefab;  // Flash effect at muzzle
    public RuntimeAnimatorController weaponAnimator; // Animator for weapon
}
