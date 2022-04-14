using Privasight.Model.Shared.AbstractClasses;
using Privasight.Model.Shared.Attributes;

namespace Privasight.Model.Shared
{
    /// <summary>
    /// Used for transforming a Json String list to a list of object with generation date
    /// </summary>
    public class StringWrapperDbObj : DbTableObj
    {
        [DisplayData("Title")]
        public string StrVal { get; set; }

        public StringWrapperDbObj(string strVal)
        {
            StrVal = strVal;
        }
    }
}
