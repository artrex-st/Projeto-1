using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject[] Bullet;
    private void Start()
    {
    }
    public void Shoot()
    {
        Instantiate(Bullet[0],FirePoint.position,FirePoint.rotation);
    }
}
