using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }

    [SerializeField] private List<Sword> allSwords = new List<Sword>();

    private void Awake()
    {
        Instance = this;
    }

    public Sword GetActiveWeapon()
    {
        foreach (Sword sword in allSwords)
        {
            if (sword != null && sword.gameObject.activeSelf)
            {
                return sword;
            }
        }
        return null;
    }

    public void EquipWeaponByIndex(int index)
    {
        foreach (Sword sword in allSwords)
        {
            if (sword != null)
                sword.gameObject.SetActive(false);
        }
        if (index >= 0 && index < allSwords.Count)
        {
            allSwords[index].gameObject.SetActive(true);
        }
    }
}