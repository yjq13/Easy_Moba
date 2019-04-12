using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace GamePlay
{
    public enum GameTargetType
    {
        NONE = 0,
        SELF = 1,
        SELF_ONE = 2,
        SELF_ALL = 3,
        SELF_ONE_EXCEPT_SELF = 4,
        SELF_ALL_EXCEPT_SELF = 5,
        OPPO_ONE = 6,
        OPPO_ALL = 7,
        ALL = 8,
        ALL_EXCEPT_SELF = 9

    }

    public interface Interface_Target
    {
        void Clear();
        bool CheckFinishChoose();
        bool NeedChooseTarget();
        List<GamePlayer> GetCanChooseTarget();
        void AddChooseTarget();
        List<GamePlayer> GetChoosedTarget();
    }

    public class GameTargetManager : SingletonModule<GameTargetManager>
    {
        private Dictionary<GameTargetType, Interface_Target> m_tartgetDic;
        private Interface_Target m_CurrentTarget;

        public List<GamePlayer> TryToGetTarget(GameTargetType tartget)
        {
            Interface_Target I_target = null;
            if (m_tartgetDic.TryGetValue(tartget,out I_target))
            {
                m_CurrentTarget = I_target;
                if (!I_target.NeedChooseTarget())
                {
                    return I_target.GetChoosedTarget();
                }
                else
                {

                }
            }

            return null;
        }

        public bool CheckFinishChoose()
        {
            if (m_CurrentTarget != null)
            {
                return m_CurrentTarget.CheckFinishChoose();
            }
            return false;
        }

        public void RequestAddTarget(GamePlayer tartget)
        {
            if (m_CurrentTarget != null)
            {
                m_CurrentTarget.AddChooseTarget();
            }
        }

        protected override void OnInit()
        {
            m_tartgetDic = new Dictionary<GameTargetType, Interface_Target>();
            m_tartgetDic.Add(GameTargetType.SELF,null);
        }

        protected override void OnCleanup()
        {
            m_tartgetDic.Clear();
            m_tartgetDic = null;
        }
    }
}
