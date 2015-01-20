using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduTesting.Model
{
    public enum TestResultStatus
    {
        /// <summary>
        /// Started
        /// </summary>
        InProgress = 0,
        /// <summary>
        /// Closed by student/timed out
        /// </summary>
        Finished = 1,
        /// <summary>
        /// Verified by teacher/set score for text answers
        /// </summary>
        Completed = 2
    }
}
