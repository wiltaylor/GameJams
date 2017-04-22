using UnityEngine;

namespace Assets.Systems.CommandManager
{
    public class CommandService
    {
        private static CommandService _instance;

        public static CommandService Instance
        {
            get { return _instance ?? (_instance = new CommandService()); }
        }

        public void ReportTileClick(int x, int y, int btn)
        {
            Debug.Log("Tile X: " + x + " Y: " + y + " was clicked with Button: " + btn);
        }
    }
}
