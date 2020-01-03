using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class PlayerEvents
{
    public static MovementEvent PlayerWalking = new MovementEvent();
    public static UnityEvent destroyObject = new UnityEvent();

    public static UnityEvent askingForDamage = new UnityEvent();
    public static DamageEvent returningDamage = new DamageEvent();

    public static UnityEvent askingForDamageObject = new UnityEvent();
    public static DamageEvent returningDamageObject = new DamageEvent();
    public static DamageEvent returningDamageObjectToPlayer = new DamageEvent();

    public static DamageEvent playerRecivingDamage = new DamageEvent();
    public static UnityEvent gameOver = new UnityEvent();
    public static UnityEvent Respawn = new UnityEvent();
    public static UnityEvent showBossHealthBar = new UnityEvent();
    public static UnityEvent unshowBossHealthBar = new UnityEvent();

    public static InventoryPickUpEvent pickUpThisItem = new InventoryPickUpEvent();
    public static UnityEvent playerInventoryChanged = new UnityEvent();
}

public class MovementEvent : UnityEvent<int,int> { }
public class DamageEvent : UnityEvent<float> { }
public class InventoryPickUpEvent : UnityEvent<PickableObject>{}

