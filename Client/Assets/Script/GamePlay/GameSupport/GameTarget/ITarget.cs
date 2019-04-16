using System;
using System.Collections.Generic;

namespace GamePlay
{
    public interface Interface_Target
    {
        void Reset();
        void Init();
        bool CheckFinishChoose();
        bool NeedChooseTarget();
        List<GamePlayer> GetCanChooseTarget();
        void AddChooseTarget(GamePlayer target);
        void RemoveChooseTarget(GamePlayer target);
        List<GamePlayer> GetChoosedTarget();
    }


    public abstract class TargetBase : Interface_Target
    {
        protected List<GamePlayer> m_gameplayer;

        public void AddChooseTarget(GamePlayer target)
        {
            if (CheckFinishChoose())
            {
                m_gameplayer.RemoveAt(0);
            }
            m_gameplayer.Add(target);
        }

        public void RemoveChooseTarget(GamePlayer target)
        {
            m_gameplayer.Remove(target);
        }

        public abstract bool CheckFinishChoose();

        public virtual void Reset()
        {
            m_gameplayer.Clear();
        }

        public virtual void Init()
        {
            m_gameplayer = new List<GamePlayer>();
        }

        public abstract List<GamePlayer> GetCanChooseTarget();

        public List<GamePlayer> GetChoosedTarget()
        {
            return m_gameplayer;
        }

        public abstract bool NeedChooseTarget();
    }
}
