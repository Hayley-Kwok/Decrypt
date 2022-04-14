using Privasight.Model.Shared.AbstractClasses;
using Privasight.Model.Shared.Attributes;

namespace Privasight.Model.Shared
{
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
