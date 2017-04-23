using System.Collections;
using System.Collections.Generic;
using Assets.Systems.PlayerManager;
using Assets.Systems.TileMap;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelView : MonoBehaviour
{
    public Text IronText;
    public Text FaithText;
    public Text HumanText;
    public Text WorldDecayText;
	
	void Update ()
	{
	    var map = TileMapService.Instance.Map;
	    var nonvoid = map.TotalTiles - map.VoidTiles;
	    var percent = Mathf.RoundToInt((float) nonvoid / (float) map.TotalTiles * 100f);


        IronText.text = PlayerService.Instance.Iron.ToString();
	    FaithText.text = PlayerService.Instance.Faith.ToString();
	    HumanText.text = (PlayerService.Instance.TotalHuamns - PlayerService.Instance.UsedHumans) + "/" +
	                     PlayerService.Instance.TotalHuamns;
	    WorldDecayText.text = percent + " %";

	}
}
