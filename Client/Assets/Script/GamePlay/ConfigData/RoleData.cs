using Common;
namespace GamePlay
{
    class RoleData : CSVBaseData
    {
        public uint ID;
        public uint HP;
        public uint Speed;

        public override string GetPrimaryKey()
        {
            return ID.ToString();
        }

        public override void ParseData(long index, int fieldCount, string[] headers, string[] values)
        {
            ID = ReadUInt("ID", headers, values, 0);
            HP = ReadUInt("HP", headers, values, 0);
            Speed = ReadUInt("Speed", headers, values, 0);
        }
    }
}
