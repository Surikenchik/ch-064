﻿using OnlineExam.Pages.POM;
using OnlineExam.Pages.POM.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Xunit;

namespace OnlineExam.Tests
{
    [Collection("MyTestCollection")]
    public class SolutionCodePageTest : BaseTest
    {
        private Header header;
        private SideBar sidebar;
        private CourseManagementPage CoursesList;

        public SolutionCodePageTest(BaseFixture fixture) : base(fixture)
        {
            BeginTest();

            string courseName = "C# Starter";
            var header = ConstructPage<Header>();
            var logInPage = header.GoToLogInPage();
            logInPage.SignIn(Constants.STUDENT_EMAIL, Constants.STUDENT_PASSWORD);
            var sidebar = ConstructPage<SideBar>();
            sidebar.GoToCourseManagementPage();
            var CoursesList = ConstructPage<CourseManagementPage>();
            var block = CoursesList.GetBlocks();
            if (block != null)
            {
                var firstBlock = block.FirstOrDefault(x => x.GetCourseName().Equals(courseName, StringComparison.OrdinalIgnoreCase));

                if (firstBlock != null)
                {
                    firstBlock.ClickCourseLink();
                }
            }
        }




        [Fact]
        public void TaskDone()
        {
            UITest(() =>
            {
                string TaskName = "Simple addition";
                var ListOfTasks = ConstructPage<TasksPage>();
                var blocks = ListOfTasks.GetBlocks();
                if (blocks != null)
                {
                    var firstblock = blocks.FirstOrDefault(x => x.GetName().Equals(TaskName, StringComparison.OrdinalIgnoreCase));
                    firstblock.ClickOnTasksButton();
                    var TaskView = ConstructPage<TaskViewPage>();
                    TaskView.ClickOnStartButton();
                    var Code = ConstructPage<SolutionCodePage>();
                    Code.ClickOnExecuteButton("Total");
                    Code.ClickOnDoneButton();

                }
                var CoursesPage = ConstructPage<SideBar>().GoToCourseManagementPage();
                var CoursesList = ConstructPage<CourseManagementPage>();
                var block = CoursesList.GetBlocks();
                if (block != null)
                {
                    var firstBlock = block.FirstOrDefault(x => x.GetCourseName().Equals("C# Starter", StringComparison.OrdinalIgnoreCase));

                    if (firstBlock != null)
                    {
                        firstBlock.ClickCourseLink();
                    }
                    ListOfTasks = ConstructPage<TasksPage>();
                    blocks = ListOfTasks.GetBlocks();
                    if (blocks != null)
                    {
                        var firstblock = blocks.FirstOrDefault(x => x.GetName().Equals(TaskName, StringComparison.OrdinalIgnoreCase));
                        firstblock.ClickOnTasksButton();
                        var TaskView = ConstructPage<TaskViewPage>();
                        TaskView.ClickOnStartButton();
                        var Code = ConstructPage<SolutionCodePage>();
                        var review = Code.MessageAboutreviewingSolution.Text;
                        Assert.NotEmpty(review);
                    }
                }


            }
            );
        }

        [Fact]
        public void ExitButton()
        {
            UITest(() =>
            {
                string TaskName = "Simple addition";
                var ListOfTasks = ConstructPage<TasksPage>();
                var blocks = ListOfTasks.GetBlocks();
                if (blocks != null)
                {
                    var firstblock = blocks.FirstOrDefault(x => x.GetName().Equals(TaskName, StringComparison.OrdinalIgnoreCase));
                    firstblock.ClickOnTasksButton();
                    var TaskView = ConstructPage<TaskViewPage>();
                    TaskView.ClickOnStartButton();
                    var Code = ConstructPage<SolutionCodePage>();
                    Code.ClickOnExitButton();
                    var url = driver.GetCurrentUrl();
                    Assert.Equal("http://localhost:55842/", url);
                }
            }
        );

        }


        [Fact]
        public void Compilation()
        {
            UITest(() =>
            {
                string TaskName = "Simple addition";
                var ListOfTasks = ConstructPage<TasksPage>();
                var blocks = ListOfTasks.GetBlocks();
                if (blocks != null)
                {
                    var firstblock = blocks.FirstOrDefault(x => x.GetName().Equals(TaskName, StringComparison.OrdinalIgnoreCase));
                    firstblock.ClickOnTasksButton();
                    var TaskView = ConstructPage<TaskViewPage>();
                    TaskView.ClickOnStartButton();
                    var Code = ConstructPage<SolutionCodePage>();
                    Code.ClickOnExecuteButton("Total");
                    Code = ConstructPage<SolutionCodePage>();
                    var result = Code.FieldWithResultOfCompilationCode.Text;
                    Assert.NotEmpty(result);
                }
            }
);

        }

    }
}
