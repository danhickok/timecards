using System.Xml;

namespace TimecardsCore.ExtensionMethods
{
    public static class XmlExtensions
    {
        /// <summary>
        /// This method allows for a "fluent" (chaining calls) way of adding attributes to an XML node
        /// </summary>
        /// <param name="node">The XML node you want to affect</param>
        /// <param name="attributeName">The new attribute's name</param>
        /// <param name="attributeValue">The new attribute's value</param>
        /// <returns></returns>
        public static XmlNode AddAttribute(this XmlNode node, string attributeName, string attributeValue)
        {
            var attr = node.OwnerDocument.CreateAttribute(attributeName);
            attr.Value = attributeValue;
            node.Attributes.Append(attr);
            return node;
        }
    }
}
