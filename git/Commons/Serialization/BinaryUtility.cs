using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Commons
{
    public class BinaryUtility
    {
        /// <summary>
        /// 序列化
        /// </summary>
        public static byte[] SerializeToByte<T>(T input)
        { 
            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, input);

                return ms.ToArray();
            }    
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static T DeserializeFromByte<T>(byte[] input)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream(input))
            {                
                return (T)bf.Deserialize(ms);                
            }    
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public static string SerializeToString<T>(T input)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, input);

                return Encoding.Default.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public static T DeserializeFromString<T>(string input)
        {
            BinaryFormatter bf = new BinaryFormatter();

            byte[] byteData = Encoding.Default.GetBytes(input);

            using (MemoryStream ms = new MemoryStream(byteData))
            {
                return (T)bf.Deserialize(ms);
            }
        }
    }
}
