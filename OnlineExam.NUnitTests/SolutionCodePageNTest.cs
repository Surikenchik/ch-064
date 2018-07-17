﻿using OnlineExam.Pages.POM;
using OnlineExam.Pages.POM.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace OnlineExam.NUnitTests
{
    [TestFixture]
    public class SolutionCodePageNTest : BaseNTest
    {
        private Header header;
        private SideBar sidebar;
        private CourseManagementPage CoursesList;

        [SetUp]
        public void SetUp()
        {
            BeginTest();

            string courseName = "C# Starter";
            var header = ConstructPage<Header>();
            var logInPage = header.GoToLogInPage();
            logInPage.SignIn(ConstantsN.STUDENT_EMAIL, ConstantsN.STUDENT_PASSWORD);
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




        [Test]
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
                        Assert.IsNotEmpty(review);
                    }
                }


            }
            );
        }

        [Test]
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
                    Assert.AreEqual("http://localhost:55842/", url);
                }
            }
        );

        }


        [Test]
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
                    Assert.IsNotEmpty(result);
                }
            }
);

        }

    }
}