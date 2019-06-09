using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlay
{
    
    public abstract class EffectBase
    {
        private ELEMENT_PROPERTY elementProp;

        public  ELEMENT_PROPERTY EltProp
        {
            get
            {
                return elementProp;
            }
        }

        // 必须传入元素属性。元素属性=卡牌属性或buff属性
        public void InitEffect(ELEMENT_PROPERTY property, params object[] objs)
        {
            elementProp = property;
            OnInitEffect(objs);
        }

        public void TakeEffect(GamePlayer source_player, List<GamePlayer> players, params object[] objs)
        {
            if(players == null)
            {
                return;
            }
            foreach(var player in players)
            {
                OnTakeEffect(source_player, player);
            }

        }

        protected abstract void OnInitEffect(params object[] objs);

        protected abstract void OnTakeEffect(GamePlayer source_player, GamePlayer player);
    }
}
