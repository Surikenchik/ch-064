﻿using AventStack.ExtentReports;
using OnlineExam.Pages.POM;
using OnlineExam.Pages.POM.CodeHistory.Favourites;
using OnlineExam.Pages.POM.UserDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineExam.Tests
{
    [Collection("MyTestCollection")]
    public class TestSaveExecutedTestToCodeHistory : BaseTest
    {
        private Header header;
        private LogInPage logInPage;
        private SideBar sideBar;



        public TestSaveExecutedTestToCodeHistory(BaseFixture fixture) : base(fixture)
        {
            BeginTest();
            header = ConstructPage<Header>();
        }

        [Fact]
        public void SaveExecutedTestToCodeHistory()
        {

            UITest(() =>
            {
                var logInPage = header.GoToLogInPage();
                logInPage.SignIn("student3@gmail.com", Constants.STUDENT_PASSWORD);
                sideBar = ConstructPage<SideBar>();
                var courseManagment = sideBar.GoToCourseManagementPage();
                var coursesBlocks = courseManagment.GetBlocks();
                var courseItem = coursesBlocks.FirstOrDefault(x => x.GetCourseName().Equals("C# Essential", StringComparison.OrdinalIgnoreCase));
                courseItem.ClickCourseLink();

                var ListOfTasks = ConstructPage<TasksPage>();
                string TaskName = "Indexers";
                string nameOfExecutedTask = "";


                var blocks = ListOfTasks.GetBlocks();
                if (blocks != null)
                {
                    var firstblock = blocks.FirstOrDefault(x => x.GetName().Equals(TaskName, StringComparison.OrdinalIgnoreCase));
                    nameOfExecutedTask = firstblock.GetName();
                    firstblock.ClickOnTasksButton();

                    var TaskView = ConstructPage<TaskViewPage>();
                    TaskView.ClickOnStartButton();

                    var Code = ConstructPage<SolutionCodePage>();

                    Code.ClickOnExecuteButton("Total");

                }
                var historyPage = sideBar.GoToCodeHistoryPage();
                var blocksHistory = historyPage.GetHistoryBlocks();

                bool blockOfExecutedCode = false;

                foreach (var block in blocksHistory)
                {
                    if (block.GetTitle() == nameOfExecutedTask)
                    {
                        blockOfExecutedCode = true;
                        break;
                    }
                }
                Assert.True(blockOfExecutedCode);
            });
        }
    }
}
