using System.Collections;
using System.Collections.Generic;
using Assets.Systems.CommandManager;
using Assets.Systems.PlayerManager;
using Assets.Systems.TileMap;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainPanelView : MonoBehaviour
{
    public Text IronText;
    public Text FaithText;
    public Text HumanText;
    public Text WorldDecayText;
    public Text TurnText;
	
	void Update ()
	{
	    var map = TileMapService.Instance.Map;
	    var nonvoid = map.TotalTiles - map.VoidTiles;
	    var percent = Mathf.RoundToInt((float) nonvoid / (float) map.TotalTiles * 100f);
	    
        IronText.text = (PlayerService.Instance.Iron - PlayerService.Instance.IronUsed) + "/" +
                        PlayerService.Instance.Iron;
        FaithText.text = PlayerService.Instance.Faith.ToString();
	    HumanText.text = (PlayerService.Instance.TotalHumans - PlayerService.Instance.UsedHumans) + "/" +
	                     PlayerService.Instance.TotalHumans;
	    WorldDecayText.text = percent + " %";
	    TurnText.text = TurnService.Instance.Turn.ToString();

	    

    }
}
