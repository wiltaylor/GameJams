using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndTurnHandler : MonoBehaviour
{

    public void EndTurn()
    {
        TurnService.Instance.NextTurn();
    }
}
