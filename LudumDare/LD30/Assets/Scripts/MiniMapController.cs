using UnityEngine;
using System.Collections;

public class MiniMapController : MonoBehaviour
{
    public RenderTexture MapTexture;
    public int MapWidth = 256;
    public int MapHeight = 256;

    private GUIStyle _style = new GUIStyle();
    public void Start()
    {
    }

    public void OnGUI()
    {
        GUI.Box(new Rect(0, 0, MapWidth, MapHeight), MapTexture);
    }


}
