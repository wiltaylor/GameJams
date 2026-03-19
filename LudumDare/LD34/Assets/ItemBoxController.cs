using UnityEngine;
using System.Collections;

public class ItemBoxController : MonoBehaviour {

    public float SpinSpeed = 1f;
    public RepairItems Item;
    public float HealAmmount = 0f;
    public AudioClip SFX;
    
    public enum RepairItems
    {
        Steering,
        Breaking,
        Guns,
        None,
        Health
    }
	
	void Update ()
    {
        transform.Rotate(new Vector3(0, SpinSpeed * Time.deltaTime, 0));
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            switch (Item)
            {
                case RepairItems.Breaking:
                    GlobalController.Instance.HasBreak = true;
                    GlobalController.Instance.AddMessage("Breaks repaired!\nPress back to break.");
                    break;
                case RepairItems.Steering:
                    GlobalController.Instance.HasSteering = true;
                    GlobalController.Instance.AddMessage("Steering repaired!\nYou can now strafe left and ritght.");
                    break;
                case RepairItems.Guns:
                    GlobalController.Instance.HasGuns = true;
                    GlobalController.Instance.AddMessage("Weapons repaired!\nYou can now fire with the left mouse button.");
                    break;
                case RepairItems.Health:
                    GlobalController.Instance.CurrentPlayer.HP += HealAmmount;
                    break;
            }

            if(SFX != null)
            {
                GlobalController.Instance.AudioController.PlaySFXAt(SFX, transform.position);
            }

            Destroy(gameObject);
        }
    }
}
