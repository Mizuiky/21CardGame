using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRound
{
    public IPlayer [] Players { get; }

    public int RoundNumber { get; set; }

    public void StartRound();

    public void FinishRound();
}
