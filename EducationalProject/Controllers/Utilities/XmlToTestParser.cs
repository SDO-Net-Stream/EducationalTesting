using System;
using System.Collections.Generic;
using System.Xml;
using EducationalProject.Models;

namespace EducationalProject.Controllers.Utilities
{
    public static class XmlToTestParser
    {
        public static Test ParseFromXml(string pathFile)
        {
            var test = new Test {PathFile = pathFile};
            var xtr = new XmlTextReader(pathFile) {WhitespaceHandling = WhitespaceHandling.None};
            xtr.Read(); // read the XML declaration node, advance to <test> tag
            xtr.Read();
            test.TestName =  xtr.GetAttribute("testname");
            test.Order = xtr.GetAttribute("order");
            
            test.Questions=new List<Question>();
            while (!xtr.EOF)
            {
                if (xtr.Name == "test" && !xtr.IsStartElement()) break;

                while (xtr.Name != "question" || !xtr.IsStartElement())
                    xtr.Read(); // advance to <question> tag
                Question question;
                switch (xtr.GetAttribute("type"))
                {
                    case "Radio":
                        question = QuestionWithVariants(xtr);
                        break;
                    case "Checked":
                        question = QuestionWithVariants(xtr);
                        break;
                    default:
                        throw new Exception();
                }
                test.Questions.Add(question);
                xtr.Read();// and now either at <question> tag or </test> tag
            }
            xtr.Close();
            test.DateDownload = DateTime.Now;
            return test;
        }

        public static QuestionWithVariants QuestionWithVariants(XmlTextReader xtr)
        {
            var question=new QuestionWithVariants
            {
                TestType = xtr.GetAttribute("type"),
                Number = Convert.ToInt32(xtr.GetAttribute("number"))
            };

            xtr.Read(); // advance to <text> tag
            question.Text = xtr.ReadElementString("text"); // consumes the </text> tag
            question.Answer = xtr.ReadElementString("answer"); // consumes the </answer> tag
            xtr.Read(); // advance to <variantsanswer> tag

            question.VariantAnswers=new List<VariantAnswer>();
            while (xtr.NodeType != XmlNodeType.EndElement)
            {
                question.VariantAnswers.Add(new VariantAnswer{Text = xtr.ReadElementString("variant")});
            }
            xtr.Read();
            return question;
        }
    }
}