using System;
using System.Collections;
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

    public class GameTargetManager : SingletonModule<GameTargetManager>
    {
        private Dictionary<GameTargetType, Interface_Target> m_tartgetDic;
        private Interface_Target m_CurrentTarget;
        public bool ChoosingTarget = false;

        public IEnumerator StartGetTarget(GamePlayer player ,GameTargetType tartget)
        {
            ChoosingTarget = true;
                Interface_Target I_target = null;
            if (m_tartgetDic.TryGetValue(tartget, out I_target))
            {
                m_CurrentTarget = I_target;
                m_CurrentTarget.Reset();
                m_CurrentTarget.SetGameTarget(player);
                if (!I_target.NeedChooseTarget())
                {
                    ChoosingTarget = false;
                }
                else
                {
                    List<GamePlayer> alternate_target = I_target.GetCanChooseTarget();
                    GameFacade.GetCurrentCardGame().GameEventDispatcher.DispatchEvent((uint)EventID.INPUT_TARGET_ADD_REQUEST, alternate_target);

                    while (ChoosingTarget)
                    {
                        yield return null;
                    }
                }
            }
        }

        public bool CheckFinishChoose()
        {
            if (m_CurrentTarget != null)
            {
                return m_CurrentTarget.CheckFinishChoose();
            }
            return false;
        }

        public void SetConFirmChoose()
        {
            ChoosingTarget = false;
            if (m_CurrentTarget != null)
            {
                m_CurrentTarget.Reset();
            }
        }
        
        public List<GamePlayer> GetChoosedTarget()
        {
            if (m_CurrentTarget != null)
            {
                return m_CurrentTarget.GetChoosedTarget();
            }
            return null;
        }

        public void RequestAddTarget(GamePlayer tartget)
        {
            if (m_CurrentTarget != null)
            {
                m_CurrentTarget.AddChooseTarget(tartget);
            }
        }

        protected override void OnInit()
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

        protected override void OnCleanup()
        {
            m_tartgetDic.Clear();
            m_tartgetDic = null;
        }
    }
}
