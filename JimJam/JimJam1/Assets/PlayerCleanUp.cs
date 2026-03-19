using UnityEngine;

public class PlayerCleanUp : MonoBehaviour {

	void Start ()
    {

        if(PlayerController.Instance != null)
		Destroy(PlayerController.Instance);

        if(PlayerHUDController.Instance != null)
            Destroy(PlayerHUDController.Instance);
    }
	

}
