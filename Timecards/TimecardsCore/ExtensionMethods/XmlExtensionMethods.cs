using System.Xml;

namespace TimecardsCore.ExtensionMethods
{
    public static class XmlExtensions
    {
        public static XmlNode AddAttribute(this XmlNode node, string attributeName, string attributeValue)
        {
            var attr = node.OwnerDocument.CreateAttribute(attributeName);
            attr.Value = attributeValue;
            node.Attributes.Append(attr);
            return node;
        }
    }
}
