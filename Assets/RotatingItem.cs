using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingItem : MonoBehaviour
{
    public enum ITEM_TYPE
    {
        HEALTH,
        AMMO
    }

    public class HealthItem
    {
        public float xRotation = 0.5f;
        public float yRotation = 0.5f;
        public float zRotation = 0.5f;
    }

    public class AmmoItem
    {
        public float xRotation = 0;
        public float yRotation = 2;
        public float zRotation = 0;
    }

    public HealthItem healthItem = new HealthItem();
    public AmmoItem ammoItem = new AmmoItem();
    public ITEM_TYPE itemType;
    // Update is called once per frame

    void Update()
    {
        switch (itemType)
        {
            case ITEM_TYPE.HEALTH:
                transform.Rotate(healthItem.xRotation, healthItem.yRotation, healthItem.zRotation);
                break;
            case ITEM_TYPE.AMMO:
                transform.Rotate(ammoItem.xRotation, ammoItem.yRotation, ammoItem.zRotation);
                break;
            default:
                break;
        }
    }

}
