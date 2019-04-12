using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Common
{
    public  abstract class XmlBaseData
    {
        public static string GetRootName()
        {
            return "XmlItems";
        }
        public abstract string GetPrimaryKey();
        public abstract void ParseXmlData(XmlElement xe);

        public T As<T>() where T : XmlBaseData
        {
            return (T)this;
        }

        protected static float ReadFloat(string fieldName, XmlElement xe, float defaultValue = 0f)
        {
            string value = GetAttributeWithName(fieldName, xe);
            return StringConverter.ToFloat(value, defaultValue);
        }
        protected static int ReadInt(string fieldName, XmlElement xe, int defaultValue = 0)
        {
            string value = GetAttributeWithName(fieldName, xe);
            return StringConverter.ToInt(value, defaultValue);
        }

        protected static uint ReadUInt(string fieldName, XmlElement xe, uint defaultValue = 0)
        {
            string value = GetAttributeWithName(fieldName, xe);
            return StringConverter.ToUInt(value, defaultValue);
        }

        protected static string ReadString(string fieldName, XmlElement xe, int defaultValue = 0)
        {
            return GetAttributeWithName(fieldName, xe);
        }

        private static string GetAttributeWithName(string attributeName , XmlElement xe)
        {
            return xe.GetAttribute(attributeName);
        }
    }
}
