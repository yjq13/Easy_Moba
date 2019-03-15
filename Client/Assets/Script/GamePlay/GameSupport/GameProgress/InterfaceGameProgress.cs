using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamePlay
{
    public interface InterfaceGameProgress
    {
        void InitProgress(List<Player> playerList);

        void StartProgress();

        void DuringProgress();

        void EndProgress();
    } 
}

