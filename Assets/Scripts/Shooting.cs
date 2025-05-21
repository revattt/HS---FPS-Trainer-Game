using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Shooting : MonoBehaviour
{
    public Camera playerCamera;
    public Transform weaponHolder;
    public List<WeaponData> weapons = new List<WeaponData>();
    private int currentWeaponIndex = -1;
    private GameObject currentWeapon;
    private float nextFireTime = 0f;

    public TextMeshProUGUI totalKillsText;
    public TextMeshProUGUI headshotPercentageText;
    public Transform muzzlePoint;

    private int totalShots = 0;
    private int headshotCount = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime && currentWeaponIndex >= 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupWeapon();
        }
    }

    void Shoot()
    {
        WeaponData weapon = weapons[currentWeaponIndex];
        nextFireTime = Time.time + weapon.fireRate;

        RaycastHit hit;
        Vector3 shootDirection = playerCamera.transform.forward;
        if (Physics.Raycast(playerCamera.transform.position, shootDirection, out hit, weapon.shootingRange))
        {
            if (hit.collider.CompareTag("Headshot"))
            {
                headshotCount++;
            }
            if (hit.collider.CompareTag("Target"))
            {
                Destroy(hit.collider.gameObject);
                totalShots++;
            }
            UpdateUI();
        }
    }

    public void EquipWeapon(WeaponData weaponData)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentWeapon = Instantiate(weaponData.weaponPrefab, weaponHolder.position, weaponHolder.rotation);
        currentWeapon.transform.SetParent(weaponHolder);
        currentWeaponIndex = weapons.IndexOf(weaponData);
    }

    void TryPickupWeapon()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider col in colliders)
        {
            WeaponPickup pickup = col.GetComponent<WeaponPickup>();
            if (pickup != null)
            {
                EquipWeapon(pickup.weaponData);
                Destroy(pickup.gameObject);
                break;
            }
        }
    }

    void UpdateUI()
    {
        int totalKills = headshotCount + (totalShots - headshotCount);
        float headshotPercentage = (totalShots > 0) ? (headshotCount / (float)totalShots) * 100f : 0f;
        totalKillsText.text = "Total Kills: " + totalKills;
        headshotPercentageText.text = "HS%: " + headshotPercentage.ToString("F1") + "%";
    }
    public void ResetScores()
    {
        totalShots = 0;
        headshotCount = 0;
    }

}
