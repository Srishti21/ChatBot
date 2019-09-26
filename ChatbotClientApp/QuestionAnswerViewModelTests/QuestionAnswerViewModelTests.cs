using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuestionAnswerViewModelLib;
using Newtonsoft.Json;
using System.Net.Http;
namespace QuestionAnswerViewModelTests
{
    [TestClass]
    public class QuestionAnswerViewModelTests
    {
        private static QuestionAnswerViewModel _viewModelRef;
        [AssemblyInitialize]
        public static void TestInitialize(TestContext testContext)
        {
            _viewModelRef = new QuestionAnswerViewModel();
        }

        [TestMethod]
        public void When_FirstQuestion_Is_Called_It_Should_Set_Question()
        {
            _viewModelRef.FirstQuestion();
            Assert.AreEqual("What is the basic requirement of your monitor",_viewModelRef.Question);
        }

        [TestMethod]
        public void
            Given_SerializedQuestionAnswerString_When_SplitQuestionAnswer_isInvoked_It_SplitsIntoQuestionAndAnswer()
        {
            string serializedDict = JsonConvert.SerializeObject(new Dictionary<int, string>() {{0,"question"},{1,"answer"}});
            _viewModelRef.SplitQuestionAndAnswer(serializedDict);
            Assert.AreEqual("question",_viewModelRef.Question);
        }

        [TestMethod]
        public void When_GetClient_Is_Called_client_need_To_Be_Assigned()
        {
            _viewModelRef.GetClient();
            Assert.AreNotEqual(null,_viewModelRef.Client);
        }
        [TestMethod]
        public void Given_SelectedOption_When_NextQuestion_Is_Called_Question_IsAssigned()
        {
            _viewModelRef.FirstQuestion();
            _viewModelRef.SelectedOption = "1";
            _viewModelRef.GetNextQuestion();
            Assert.AreEqual(2,_viewModelRef.QuestionId);
        }

        [TestMethod]
        public void Given_QuestionAndMonitorID_When_Show_Monitor_Suggested()
        {
            _viewModelRef.FirstQuestion();
            _viewModelRef.SelectedOption = "4";
            _viewModelRef.GetNextQuestion();
            _viewModelRef.ShowMonitor();
            Assert.AreEqual("Efficia CMS 200", _viewModelRef.Monitor);
        }

        [TestMethod]
        public void When_GetPreviousQuestion_Is_Called_QuestionId_Is_Changed_To_PreviousQuestion()
        {
            _viewModelRef.FirstQuestion();
            _viewModelRef.SelectedOption = "1";
            _viewModelRef.GetNextQuestion();
            _viewModelRef.GetPrevQuestion();
            Assert.AreEqual(1, _viewModelRef.QuestionId);

        }
    }
}
