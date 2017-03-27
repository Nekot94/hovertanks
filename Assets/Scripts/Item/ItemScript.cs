using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate void ItemAction(GameObject player);

public class ItemScript : MonoBehaviour
{

    public static ItemScript instance;

    public enum ItemStates { Heal, Bomb }

    public ItemStates itemState = ItemStates.Heal;

    public Color[] stateColors;

    private ItemAction[] allActions = { Heal, Bomb };

    void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            allActions[(int)itemState](other.gameObject);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        if (other.gameObject.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }
    }

    private static void Heal(GameObject player)
    {
        ChangeHealth(player, true);
    }


    private static void Bomb(GameObject player)
    {
        ChangeHealth(player, false);
    }

    private static void ChangeHealth(GameObject player, bool positive)
    {
        TankHealth tankHealth = player.GetComponent<TankHealth>();
        if (tankHealth != null)
        {
            float halfHealth = tankHealth.startingHealth / 2;
            if (positive)
                halfHealth = -halfHealth;
            tankHealth.TakeDamage(halfHealth);

        } 
    }




}
