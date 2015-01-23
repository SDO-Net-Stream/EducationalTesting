using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
    public enum AttributeCode: int
    {
        /// <summary>
        /// type=integer, minutes
        /// </summary>
        TestTimeLimit = 1,
        /// <summary>
        /// Test is public if attribute exists. value does not matter
        /// </summary>
        TestIsPublic = 2,
        /// <summary>
        /// type=integer
        /// </summary>
        TestRandomSubsetSize = 3,
        AnswerMediaType = 4
    }
}
