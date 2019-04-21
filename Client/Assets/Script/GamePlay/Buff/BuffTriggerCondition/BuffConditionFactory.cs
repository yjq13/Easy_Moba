using System;
namespace GamePlay
{
    public static class BuffConditionFactory
    {
        public static ITriggerCondition CreateBuffCondition(Buff_NOTIFY_TYPE notifyType)
        {
            switch (notifyType) 
            {
                case Buff_NOTIFY_TYPE.CAUSE_DAMAGE:
                    {
                        return new CauseDamageCondition();
                    }
            }
            return null;
        }
    }
}
