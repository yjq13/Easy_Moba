using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class GameTarget
    {
        public List<GamePlayer> TargetPlayer;
        public GameTargetType TargetType;

        public int NeedPlayerCountAndType;

        private bool FinishState;

        public GameTarget()
        {
            TargetPlayer = new List<GamePlayer>();
            TargetType = GameTargetType.NONE;
            FinishState = false;
        }

        public void Reset()
        {
            TargetPlayer = new List<GamePlayer>();
            TargetType = GameTargetType.NONE;
            FinishState = false;
        }

        public void SetTargetPlayerByType(GameTargetType tartget)
        {
            Reset();
            TargetType = tartget;
            switch (tartget)
            {
                case GameTargetType.NONE:
                    {
                        break;
                    }
                case GameTargetType.SELF:
                    {
                        break;
                    }
                case GameTargetType.SELF_ONE:
                    {
                        break;
                    }
                case GameTargetType.SELF_ALL:
                    {
                        break;
                    }
                case GameTargetType.SELF_ONE_EXCEPT_SELF:
                    {
                        break;
                    }
                case GameTargetType.SELF_ALL_EXCEPT_SELF:
                    {
                        break;
                    }
                case GameTargetType.OPPO_ONE:
                    {
                        break;
                    }
                case GameTargetType.OPPO_ALL:
                    {
                        break;
                    }
                case GameTargetType.ALL:
                    {
                        break;
                    }
                case GameTargetType.ALL_EXCEPT_SELF:
                    {
                        break;
                    }
            }
        }

        private void CheckTargetPlayerNeed()
        {
            if(TargetPlayer.Count == NeedPlayerCountAndType)
            {
                FinishState = true;
            }
        }

        public void AddTargetPlayer(GamePlayer player)
        {
            TargetPlayer.Add(player);
            CheckTargetPlayerNeed();
        }
    }
}
