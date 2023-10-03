using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum PickupType
{
    Health,
    Ammo
}

public class Pickup : MonoBehaviourPun
{
   public PickupType type;
   public int value;


    void OnTriggerEnter(Collider other)
    {
        if(!PhotonNetwork.IsMasterClient)
            return;

        if(other.CompareTag("Player"))
        {
            //get the player
            PlayerController player = GameManager.instance.GetPlayer(other.gameObject);

            //give the player the values
            if(type == PickupType.Health)
                player.photonView.RPC("Heal", player.photonPlayer, value);
            else if(type == PickupType.Ammo)
                player.photonView.RPC("GiveAmmo", player.photonPlayer, value);

            //destroy the ammo
            photonView.RPC("DestroyPickup", RpcTarget.AllBuffered);    
        }
    }
    [PunRPC]
    public void DestroyPickup()
    {
        Destroy(gameObject);
    }
}
