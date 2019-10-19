namespace Dennkind.Framework.Data
{
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Serializes and deserializes objects into and from XML documents.
    /// </summary>
    public class XmlSerialization
    {
        /// <summary>
        /// Deserializes the XML document.
        /// </summary>
        /// <typeparam name="T">The deserialized object type.</typeparam>
        /// <param name="filename">The XML documents filename.</param>
        /// <returns>
        /// The System.Object being deserialized. Returns the default object type
        /// if file does not exist.
        /// </returns>
        public T ReadXml<T>(string filename)
        {
            if (!File.Exists(filename))
                return default(T);

            using (var reader = new StreamReader(filename))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var t = (T)serializer.Deserialize(reader);
                reader.Close();

                return t;
            }
        }

        /// <summary>
        /// Serializes the specified System.Object and writes the XML document to the file.
        /// </summary>
        /// <typeparam name="T">The serialized object type.</typeparam>
        /// <param name="t">The serialized object instance.</param>
        /// <param name="filename">The XML documents filename.</param>
        public void WriteXml<T>(T t, string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, t);
                writer.Close();
            }
        }
    }
}