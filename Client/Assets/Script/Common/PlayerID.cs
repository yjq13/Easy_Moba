using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public struct PlayerID : IEquatable<PlayerID>
    {
        private readonly uint m_Value;
        private PlayerID(uint value)
        {
            this.m_Value = value;
        }

        public static implicit operator uint(PlayerID id)
        {
            return id.m_Value;
        }

        public static implicit operator PlayerID(uint value)
        {
            PlayerID id = new PlayerID(value);
            return id;
        }

        public bool Equals(PlayerID other)
        {
            return m_Value == other.m_Value;
        }
    }
}
