using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMananger : MonoBehaviour
{
    private int magazineCapacity;
    private int extraMagazines;

    // Start is called before the first frame update
    void Start()
    {
        magazineCapacity = 0;
        extraMagazines = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (extraMagazines > 4)
            extraMagazines = 4;
    }

    public void SetMagazineCapacity(int amount)
    {
        magazineCapacity = amount;
    }

    public int GetMagazineCapacity()
    {
        return magazineCapacity;
    }

    public void SetExtraMagazines(int amount)
    {
        if (amount > 0 && extraMagazines > 0 && SimpleShoot.holdingGun)
            extraMagazines = 6;
        else if (amount > 0 && extraMagazines > 0 && !SimpleShoot.holdingGun)
            extraMagazines = 4;
        else
            extraMagazines += amount;
    }

    public int GetExtraMagazines()
    {
        return extraMagazines;
    }
}
