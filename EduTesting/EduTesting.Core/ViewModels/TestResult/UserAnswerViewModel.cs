﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.TestResult
{
    public class UserAnswerViewModel
    {
        public int TestResultId { get; set; }
        public int QuestionId { get; set; }
        public int[] AnswersId { get; set; }
        public string AnswerText { get; set; }
    }
}
