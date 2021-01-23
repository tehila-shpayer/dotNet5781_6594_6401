using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DL
{
    /// <summary>
    /// מחלקה עזר לעבודה עם קבצי XML
    /// </summary>
    public class XmlTools
    {
        static string dir = @"xml\";
        static XmlTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        #region SaveLoadWithXElement
        /// <summary>
        /// שמירה לקובץ XML על ידי שימוש במחלקה XElement
        /// </summary>
        /// <param name="rootElem">שורש הקובץ</param>
        /// <param name="filePath">מיקום הקובץ</param>
        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        /// <summary>
        /// העלאה מקובץ XML ע"י שימוש במחלקה XElement
        /// </summary>
        /// <param name="filePath">מיקום קובץ</param>
        /// <returns>רשימת האלמנטים בקובץ</returns>
        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    return XElement.Load(dir + filePath);
                }
                else
                {
                    XElement rootElem = new XElement(dir + filePath);
                    rootElem.Save(dir + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

        #region SaveLoadWithXMLSerializer
        /// <summary>
        /// שמירה לקובץ XML ע"י שימוש בXMLSerializer
        /// </summary>
        /// <typeparam name="T">טיפוס האלמנטים בקובץ</typeparam>
        /// <param name="list">רשימת אלמנטים מטיפוס גנרי לשמירה בקובץ</param>
        /// <param name="filePath">מיקום הקובץ</param>
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dir + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        /// <summary>
        /// העלאה מקובץ XML ע"י שימוש בXMLSerializer
        /// </summary>
        /// <typeparam name="T">טיפוס האלמנטים בקובץ</typeparam>
        /// <param name="filePath">מיקום הקובץ</param>
        /// <returns>רשימת האלמנטים בקובץ מטיפוס גנרי</returns>
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
    }
}
