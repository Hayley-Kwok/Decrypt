using Privasight.Model.Shared.DataStructures.AbstractClasses;
using Privasight.Model.Shared.Display.Attributes;

namespace Privasight.Model.Shared.DataStructures
{
    /// <summary>
    /// Used for transforming a Json String list to a list of object with generation date
    /// </summary>
    public class StringWrapperDbObj : DbTableObj
    {
        [DataListValue]
        [DetailedTableDisplayData("Title")]
        public string StrVal { get; set; }

        public StringWrapperDbObj(string strVal)
        {
            StrVal = strVal;
        }
    }
}
