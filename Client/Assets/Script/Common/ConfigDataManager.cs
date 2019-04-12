//Author : Copy form FF Code
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System.Xml;

namespace Common
{
    public class ConfigDataManager : SingletonModule<ConfigDataManager>
    {
        private Dictionary<string, List<CSVBaseData>> m_ConfigDataLists = new Dictionary<string, List<CSVBaseData>>();
        private Dictionary<string, CSVBaseData> m_ConfigDataItemss = new Dictionary<string, CSVBaseData>();

        private Dictionary<string, List<XmlBaseData>> m_XmlConfigDataLists = new Dictionary<string, List<XmlBaseData>>();
        private Dictionary<string, XmlBaseData> m_XmlConfigDataItemss = new Dictionary<string, XmlBaseData>();

        protected override void OnInit()
        {
        }
        protected override void OnCleanup()
        {
            m_ConfigDataLists = new Dictionary<string, List<CSVBaseData>>();
            m_ConfigDataItemss = new Dictionary<string, CSVBaseData>();
        }
        public List<T> LoadCSVNoCache<T>(ResourceID resID) where T : CSVBaseData
        {
            UnityEngine.Object resourceObj = ResourceManager.Instance.GetResource(resID);
            if (resourceObj == null)
            {
                return null;
            }
            string csvText = "";
            TextAsset csvTextAsset = UnityEngine.Object.Instantiate(resourceObj) as TextAsset;
            byte[] csv_bs;

#if ENABLE_ENCRYPTION
            ResDecryption.Decryption(csvTextAsset.bytes, out csv_bs);
#else
            csv_bs = csvTextAsset.bytes;
#endif
            csvText = Encoding.UTF8.GetString(csv_bs);

            if (string.IsNullOrEmpty(csvText))
                return null;

            return ParseCSV<T>(csvText);
        }
        public void LoadXmlData<T>(ResourceID resID) where T : XmlBaseData
        {
            string typeKey = GetDataListsKey<T>();
            if (m_XmlConfigDataLists.ContainsKey(typeKey))
            {
                Debug.LogWarning("duplicated key in config: " + typeKey);
                return;
            }
            List<T> result = ParseXml<T>(resID);
            if (result != null)
            {
                List<XmlBaseData> list = new List<XmlBaseData>();
                for (int i = 0; i < result.Count; i++)
                {
                    XmlBaseData item = result[i];
                    list.Add(item);
                    string pKey = item.GetPrimaryKey();
                    if (string.IsNullOrEmpty(pKey) == false)
                    {
                        string itemKey = GetDataItemKey<T>(pKey);
                        if (m_XmlConfigDataItemss.ContainsKey(itemKey) == false)
                        {
                            m_XmlConfigDataItemss.Add(itemKey, item);
                        }
                        else
                        {
                            Debug.Log("duplicate item key: " + itemKey);
                        }
                    }
                }
                m_XmlConfigDataLists.Add(typeKey, list);
            }
        }
        public void LoadCSV<T>(ResourceID resID) where T : CSVBaseData
        {
            string typeKey = GetDataListsKey<T>();
            if (m_ConfigDataLists.ContainsKey(typeKey))
            {
                Debug.LogWarning("duplicated key in config: " + typeKey);
                return;
            }
            List<T> result = LoadCSVNoCache<T>(resID);
            if (result != null)
            {
                List<CSVBaseData> list = new List<CSVBaseData>();
                for (int i = 0; i < result.Count; i++)
                {
                    CSVBaseData item = result[i];
                    list.Add(item);
                    string pKey = item.GetPrimaryKey();
                    if (string.IsNullOrEmpty(pKey) == false)
                    {
                        string itemKey = GetDataItemKey<T>(pKey);
                        if (m_ConfigDataItemss.ContainsKey(itemKey) == false)
                        {
                            m_ConfigDataItemss.Add(itemKey, item);
                        }
                        else
                        {
                            Debug.Log("duplicate item key: " + itemKey);
                        }
                    }
                }
                m_ConfigDataLists.Add(typeKey, list);
            }
        }
        public List<CSVBaseData> GetDataList<T>() where T : CSVBaseData
        {
            string typeKey = GetDataListsKey<T>();
            List<CSVBaseData> result;
            if (m_ConfigDataLists.TryGetValue(typeKey, out result))
            {
                return result;
            }
            return null;
        }

        public List<XmlBaseData> GetXmlDataList<T>() where T : XmlBaseData
        {
            string typeKey = GetDataListsKey<T>();
            List<XmlBaseData> result;
            if (m_XmlConfigDataLists.TryGetValue(typeKey, out result))
            {
                return result;
            }
            return null;
        }

        public T GetXmlData<T>(string key) where T : XmlBaseData
        {
            string typeKey = GetDataItemKey<T>(key);
            XmlBaseData result;
            if (m_XmlConfigDataItemss.TryGetValue(typeKey, out result))
            {
                return result.As<T>();
            }
            return null;
        }

        public T GetData<T>(string key) where T : CSVBaseData
        {
            string typeKey = GetDataItemKey<T>(key);
            CSVBaseData result;
            if (m_ConfigDataItemss.TryGetValue(typeKey, out result))
            {
                return result.As<T>();
            }
            return null;
        }
        private string GetDataListsKey<T>()
        {
            return typeof(T).ToString();
        }
        private string GetDataItemKey<T>(string pKey)
        {
            return string.Format("{0}|{1}", GetDataListsKey<T>(), pKey);
        }
        static public List<T> ParseCSV<T>(string csvText) where T : CSVBaseData
        {
            TextReader reader = new StringReader(csvText);
            CsvReader csv = new CsvReader(reader, true);
            int fieldCount = csv.FieldCount;
            string[] headers = csv.GetFieldHeaders();
            //csv.ReadNextRecord();//skip desc
            List<T> result = new List<T>();
            while (csv.ReadNextRecord())
            {
                long index = csv.CurrentRecordIndex;
                string[] values = new string[fieldCount];
                csv.CopyCurrentRecordTo(values);

                T obj = (T)Activator.CreateInstance(typeof(T));
                obj.ParseData(index, fieldCount, headers, values);
                result.Add(obj);
            }
            return result;
        }
        public static List<T> ParseXml<T>(string filepath) where T : XmlBaseData
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            T obj = (T)Activator.CreateInstance(typeof(T));
            XmlNodeList nodeList = xmlDoc.SelectSingleNode(XmlBaseData.GetRootName()).ChildNodes;
            List<T> result = new List<T>();
            foreach (XmlElement xe in nodeList)
            {
                obj.ParseXmlData(xe);
            }
            result.Add(obj);

            return result;
        }
    }


}
