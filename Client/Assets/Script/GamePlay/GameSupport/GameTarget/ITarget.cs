using System;
using System.Collections.Generic;

namespace GamePlay
{
    public interface Interface_Target
    {
        void Clear();
        bool CheckFinishChoose();
        bool NeedChooseTarget();
        List<GamePlayer> GetCanChooseTarget();
        void AddChooseTarget();
        List<GamePlayer> GetChoosedTarget();
    }
}
