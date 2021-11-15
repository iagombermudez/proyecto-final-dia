using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BusquedasUI.Core.Xml
{
    public class XmlReader
    {

        protected XElement GetRootNode(string fileName)
        {
            var raiz = XElement.Load(fileName);
            return raiz;
        }

        protected IEnumerable<XElement> GetElements(XElement raiz, string node)
        {
            var elements =
                from element in raiz.Elements(node)
                select element;

            return elements;
        }
        
    }
}