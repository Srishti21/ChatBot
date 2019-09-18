using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using chatbotrepo;
using chatbotrepo.DAL;
using chatbotrepo.Controllers;

namespace UnitTestProject1
{
    [TestClass]
    public class ChatBotControllerTest
    {
        #region Private Fields
        private readonly IDataFetch<QuestionsTbl> _question;
        private readonly IDataFetch<MonitorsTbl> _monitor;
        private readonly IDataFetch<OptionsTbl> _option;
        #endregion

        #region Default Constructor
        public ChatBotControllerTest()
        {
            _question = new QuestionRepository(new ChatbotEntities());
            _option = new OptionsRepository(new ChatbotEntities());
            _monitor = new MonitorRepository(new ChatbotEntities());
        }
        #endregion


        #region Test Case Methods
        [TestMethod]
        public void GivenMonitoId_WhenItIsValid_ThenGiveCorrectMonitorName()
        {
            string Actual = "Avalon CL";
            string expected = _monitor.GetById(1).monitor_name;
            Assert.AreEqual(Actual, expected);
        }

        [TestMethod]
        public void GivenMonitoId_WhenItIsValid_ThenGiveCorrectMonitorName2()
        {
            string Actual = "IntelliVue MP5SC";
            string expected = _monitor.GetById(22).monitor_name;
            Assert.AreEqual(Actual, expected);
        }

        [TestMethod]
        public void GivenMonitoId_WhenItIsNotValid_ThenShouldNotGiveCorrecttMonitorName()
        {
            string Actual = "IntelliVue MP5SC";
            string expected = _monitor.GetById(2).monitor_name;
            Assert.AreNotEqual(Actual, expected);
        }

        [TestMethod]
        public void GivenQuestionId_WhenItIsValid_ThenDisplayCorrectQuestion()
        {
            string Actual = "Do you want a monitor with cable or cableless?";
            string expected = _question.GetById(2).question;
            Assert.AreEqual(Actual, expected);
        }

        [TestMethod]
        public void GivenQuestionIdAndOptionId_WhenItIsValid_ThenReturnNextQuestionId()
        {
            int Actual = 13;
            int expected = _option.FindWhere(12, 1).link_id;
            Assert.AreEqual(Actual, expected);
        }

        [TestMethod]
        public void GivenQuestionIdAndOptionId_WhenItIsNotValid_ThenShouldNotReturnNextQuestionId()
        {
            int Actual = 3;
            int expected = _option.FindWhere(12, 1).link_id;
            Assert.AreNotEqual(Actual, expected);
        }


        [TestMethod]
        public void GivenQuestionIdAndOptionId_WhenItIsValid_ThenReturnCorrespondingMonitorId()
        {
            int Actual = 19;
            int expected = _option.FindWhere(12, 5).monitor_id;
            Assert.AreEqual(Actual, expected);
        }

        [TestMethod]
        public void GivenQuestionIdAndOptionId_WhenItIsNotValid_ThenShouldNotReturnCorrespondingMonitorId()
        {
            int Actual = 1;
            int expected = _option.FindWhere(12, 5).monitor_id;
            Assert.AreNotEqual(Actual, expected);
        }

        [TestMethod]
        public void GivenQuestionIdAndOptionId_WhenItIsValid_ThenReturnCorrespondingMonitorId2()
        {
            int Actual = 10;
            int expected = _option.FindWhere(11, 3).monitor_id;
            Assert.AreEqual(Actual, expected);
        }

        [TestMethod]
        public void GivenQuestionIdAndOptionId_WhenItIsValid_ThenReturnCorrespondingNextQuestionId()
        {
            int Actual = 13;
            int expected = _option.FindWhere(12, 2).link_id;
            Assert.AreNotEqual(Actual, expected);
        }

        [TestMethod]
        public void GivenQuestionId_WhenItIsValid_ThenDisplayCorrectQuestion2()
        {
            string Actual = "Select one of the required features from the following options.";
            string expected = _question.GetById(14).question;
            Assert.AreEqual(Actual, expected);
        }

        ChatbotController chatbot = new ChatbotController();

        [TestMethod]
        public void GivenQuestionId1_WhenItIsValid_ThenDisplayCorrectQuestion2()
        {
            string Actual = "What is the basic requirement of your monitor?" + Environment.NewLine + " 1. Maternal and Fetal " + Environment.NewLine + " 2. Vital Signs" + Environment.NewLine + " 3. Bedside Patient Monitor" + Environment.NewLine + " 4. Central Monitoring System" + Environment.NewLine + " 5. IntelliVue(serves a wide range of care environment)";
            string expected = chatbot.Get();
            Assert.AreNotEqual(expected, Actual);
        }


        [TestMethod]
        public void Given_QuestionId_1_and_OptionId_1_Return_LinkId_2()
        {
            int expected = chatbot.GetLink(new OptionsTbl { question_id = 1, option_id = 1 });
            int actual = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Given_LinkId_4_Return_Question_And_Options_with_Id_4()
        {
            string expected = chatbot.FetchQuestion(new OptionsTbl { link_id = 4 });
            string actual = "Do you want video and audio interface?" + Environment.NewLine + "1.Yes" + Environment.NewLine + "2.No\n";
            Assert.AreNotEqual(expected, actual);

        }

        [TestMethod]
        public void Given_QId_And_OptId_fetch_MonitorName()
        {
            string expected = chatbot.MonitorFetch(new OptionsTbl { option_id = 3, question_id = 12 });
            string actual = "Intellivue GS";
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}