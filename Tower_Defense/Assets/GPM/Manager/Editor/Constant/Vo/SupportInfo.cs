using System.Xml.Serialization;

namespace Gpm.Manager.Constant
{
    [XmlRoot("supportInfo")]
    public class SupportInfo
    {
        public string mail = string.Empty;
    }
}