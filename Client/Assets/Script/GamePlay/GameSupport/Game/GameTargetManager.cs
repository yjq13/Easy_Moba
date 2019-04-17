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

    public class GameTargetManager
    {
        private Dictionary<GameTargetType, Interface_Target> m_tartgetDic;
        private Interface_Target m_CurrentTarget;

        public List<GamePlayer> TryToGetTarget(GameTargetType tartget)
        {
            Interface_Target I_target = null;
            if (m_tartgetDic.TryGetValue(tartget,out I_target))
            {
                m_CurrentTarget = I_target;
                m_CurrentTarget.Reset();
                if (!I_target.NeedChooseTarget())
                {
                    return I_target.GetChoosedTarget();
                }
                else
                {
                    List<GamePlayer> alternate_target = I_target.GetCanChooseTarget();
                    GameFacade.GetCurrentCardGame().GameEventDispatcher.DispatchEvent((uint)EventID.UI_CHECK_ADD_TARGET, alternate_target);
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
                m_CurrentTarget.AddChooseTarget(tartget);
            }
        }

        public void Init()
        {
            m_tartgetDic = new Dictionary<GameTargetType, Interface_Target>
            {
                { GameTargetType.SELF,new SelfTarget() },
                { GameTargetType.SELF_ALL,new SelfAllTarget() },
                { GameTargetType.SELF_ONE,new SelfOneTarget() },
                { GameTargetType.SELF_ALL_EXCEPT_SELF,new SelfAllExceptSelfTarget() },
                { GameTargetType.SELF_ONE_EXCEPT_SELF,new SelfOneExceptSelfTarget() },
                { GameTargetType.OPPO_ONE,new OppoOneTarget() },
                { GameTargetType.OPPO_ALL,new OppoAllTarget() },
                { GameTargetType.ALL,new AllTarget() },
                { GameTargetType.ALL_EXCEPT_SELF,new AllExceptSelfTarget() },
            };
        }

        public void CleanUp()
        {
            m_tartgetDic.Clear();
            m_tartgetDic = null;
        }
    }
}
